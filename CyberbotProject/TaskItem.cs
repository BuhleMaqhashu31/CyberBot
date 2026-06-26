using System;

namespace CyberSecurityAssistant
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool ReminderShown { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "[Done]" : "[Pending]";
            return $"{status} {Title} - Due: {DueDate:g}";
        }
    }
}

