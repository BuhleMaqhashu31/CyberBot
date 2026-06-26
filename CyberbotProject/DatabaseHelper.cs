using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace CyberSecurityAssistant
{
    public static class DatabaseHelper
    {
        private static readonly string DbFile = "assistant.db";
        private static readonly string ConnectionString = $"Data Source={DbFile};Version=3;";

        public static void InitializeDatabase()
        {
            try
            {
                if (!File.Exists(DbFile))
                {
                    SQLiteConnection.CreateFile(DbFile);
                }

                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string createTasksTable = @"
                        CREATE TABLE IF NOT EXISTS Tasks (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Title TEXT NOT NULL,
                            Description TEXT,
                            DueDate TEXT NOT NULL,
                            IsCompleted INTEGER NOT NULL,
                            ReminderShown INTEGER NOT NULL
                        );";

                    string createLogsTable = @"
                        CREATE TABLE IF NOT EXISTS ActivityLogs (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            TimeStamp TEXT NOT NULL,
                            ActionType TEXT NOT NULL,
                            Description TEXT NOT NULL
                        );";

                    using (var cmd = new SQLiteCommand(createTasksTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SQLiteCommand(createLogsTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database engine failed initialization configurations: " + ex.Message);
            }
        }

        // ==========================================
        // ROBUST CRUD OPERATIONS (GUI-DB SYNC)
        // ==========================================

        public static List<TaskItem> GetTasks()
        {
            var tasks = new List<TaskItem>();
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Tasks ORDER BY DueDate ASC";

                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                                IsCompleted = Convert.ToInt32(reader["IsCompleted"]) == 1,
                                ReminderShown = Convert.ToInt32(reader["ReminderShown"]) == 1
                            });
                        }
                    }
                }
                return tasks;
            }
            catch (Exception ex)
            {
                throw new Exception("CRITICAL Error fetching system task indices: " + ex.Message);
            }
        }

        public static void AddTask(TaskItem task)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO Tasks (Title, Description, DueDate, IsCompleted, ReminderShown)
                        VALUES (@Title, @Description, @DueDate, @IsCompleted, @ReminderShown);
                        SELECT last_insert_rowid();";

                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", task.Title ?? string.Empty);
                        cmd.Parameters.AddWithValue("@Description", task.Description ?? string.Empty);
                        cmd.Parameters.AddWithValue("@DueDate", task.DueDate.ToString("o"));
                        cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ReminderShown", task.ReminderShown ? 1 : 0);

                        // Assign generated DB Id back to object for accurate runtime tracking
                        task.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                AddLog("CREATE_TASK", $"Successfully added task ID {task.Id}: '{task.Title}'");
            }
            catch (Exception ex)
            {
                throw new Exception("CRITICAL Error inserting task asset: " + ex.Message);
            }
        }

        public static void UpdateTask(TaskItem task)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"
                        UPDATE Tasks
                        SET Title = @Title,
                            Description = @Description,
                            DueDate = @DueDate,
                            IsCompleted = @IsCompleted,
                            ReminderShown = @ReminderShown
                        WHERE Id = @Id";

                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", task.Id);
                        cmd.Parameters.AddWithValue("@Title", task.Title);
                        cmd.Parameters.AddWithValue("@Description", task.Description);
                        cmd.Parameters.AddWithValue("@DueDate", task.DueDate.ToString("o"));
                        cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ReminderShown", task.ReminderShown ? 1 : 0);

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows == 0)
                        {
                            throw new Exception($"Target record synchronization missing. ID: {task.Id}");
                        }
                    }
                }
                AddLog("UPDATE_TASK", $"Synchronized update state for task ID {task.Id} (Complete={task.IsCompleted}).");
            }
            catch (Exception ex)
            {
                throw new Exception("CRITICAL Error updating database context mapping: " + ex.Message);
            }
        }

        public static void DeleteTask(int taskId)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Tasks WHERE Id = @Id";

                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", taskId);
                        cmd.ExecuteNonQuery();
                    }
                }
                AddLog("DELETE_TASK", $"Purged task record sequence key ID: {taskId}");
            }
            catch (Exception ex)
            {
                throw new Exception("CRITICAL Error deleting record target entity: " + ex.Message);
            }
        }

        // ==========================================
        // ACTIVITY LOGGING WITH PAGINATION SUPPORT
        // ==========================================

        public static void AddLog(string actionType, string description)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO ActivityLogs (TimeStamp, ActionType, Description)
                        VALUES (@TimeStamp, @ActionType, @Description)";

                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("o"));
                        cmd.Parameters.AddWithValue("@ActionType", actionType);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silenced to guarantee background failures don't halt app threads
            }
        }

        /// <summary>
        /// Fetches entries using explicit structural pagination bounds to easily feed "Show More" UI configurations.
        /// </summary>
        /// <param name="offset">How many items to skip (Current count already rendered on screen).</param>
        /// <param name="limit">Max items to bring back per query step (e.g., 5 or 10).</param>
        public static List<ActivityLogEntry> GetLogsPaginated(int offset, int limit)
        {
            var logs = new List<ActivityLogEntry>();
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    // Using standard SQLite LIMIT and OFFSET architecture
                    string query = "SELECT * FROM ActivityLogs ORDER BY TimeStamp DESC LIMIT @Limit OFFSET @Offset";

                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Limit", limit);
                        cmd.Parameters.AddWithValue("@Offset", offset);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                logs.Add(new ActivityLogEntry
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    TimeStamp = DateTime.Parse(reader["TimeStamp"].ToString()),
                                    ActionType = reader["ActionType"].ToString(),
                                    Description = reader["Description"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed loading paginated logging sequence: " + ex.Message);
            }
            return logs;
        }
    }
}

