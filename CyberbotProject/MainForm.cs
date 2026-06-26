using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CyberSecurityAssistant
{
    public partial class MainForm : Form
    {
        // Global Theme Layout Panels
        private Panel sideNavPanel;
        private Panel headerPanel;
        private Label lblTitle;

        // Navigation Control Buttons
        private Button btnMenuChat;
        private Button btnMenuQuiz;

        // --- Feature View Container Panels ---
        private Panel pnlChatContainer;
        private Panel pnlQuizContainer;

        // 1. Chat Component Set
        private RichTextBox rtbChatHistory;
        private TextBox txtUserInput;
        private Button btnSend;

        // 2. Quiz Component Set
        private Label lblQuestionText;
        private ListBox lstOptions;
        private Button btnSubmitAnswer;

        // Core Backend Objects
        private ChatBot botEngine;
        private QuizEngine quizEngine;
        private AudioPlayer audioPlayer;
        private string userName = "User";
        private int scoreCounter = 0;

        public MainForm()
        {
            InitializeComponentLayout();
            botEngine = new ChatBot();
            quizEngine = new QuizEngine();
            audioPlayer = new AudioPlayer("Buhle.wav");
        }

        private void InitializeComponentLayout()
        {
            // Main Window Structural Configs - Whimsical Pink Theme
            this.Size = new Size(720, 650);
            this.Text = "Cyber Security Assistant";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LavenderBlush;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ====================================================================
            // SHARED SHELL STRUCTURAL CONTAINERS
            // ====================================================================

            // Playful Top Header Banner Panel
            headerPanel = new Panel { Size = new Size(720, 65), Dock = DockStyle.Top, BackColor = Color.HotPink };
            lblTitle = new Label
            {
                Text = "CYBER SECURITY ASSISTANT",
                ForeColor = Color.White,
                Font = new Font("Comic Sans MS", 14, FontStyle.Bold),
                Location = new Point(20, 18),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblTitle);

            // Left Sidebar Navigation Panel
            sideNavPanel = new Panel { Size = new Size(160, 585), Dock = DockStyle.Left, BackColor = Color.MistyRose };

            btnMenuChat = new Button
            {
                Text = "💬 Chat Assistant",
                Location = new Point(10, 30),
                Size = new Size(140, 45),
                BackColor = Color.HotPink,
                ForeColor = Color.White,
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnMenuChat.FlatAppearance.BorderSize = 0;
            btnMenuChat.Click += (s, e) => ShowFeaturePanel(pnlChatContainer);

            btnMenuQuiz = new Button
            {
                Text = "🎯 Cyber Quiz",
                Location = new Point(10, 95),
                Size = new Size(140, 45),
                BackColor = Color.HotPink,
                ForeColor = Color.White,
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnMenuQuiz.FlatAppearance.BorderSize = 0;
            btnMenuQuiz.Click += (s, e) => { ShowFeaturePanel(pnlQuizContainer); StartQuiz(); };

            sideNavPanel.Controls.Add(btnMenuChat);
            sideNavPanel.Controls.Add(btnMenuQuiz);

            // ====================================================================
            // VIEW PANEL 1: CHAT INTERFACE WORKSPACE
            // ====================================================================
            pnlChatContainer = new Panel { Location = new Point(160, 65), Size = new Size(544, 546), Visible = true };

            rtbChatHistory = new RichTextBox
            {
                Location = new Point(15, 20),
                Size = new Size(510, 420),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Courier New", 9.5f),
                BorderStyle = BorderStyle.FixedSingle
            };

            txtUserInput = new TextBox
            {
                Location = new Point(15, 465),
                Size = new Size(390, 30),
                Font = new Font("Comic Sans MS", 11),
                BackColor = Color.White,
                ForeColor = Color.Black
            };

            btnSend = new Button
            {
                Location = new Point(415, 463),
                Size = new Size(110, 32),
                Text = "Send",
                BackColor = Color.HotPink,
                ForeColor = Color.White,
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnSend.FlatAppearance.BorderColor = Color.Black;
            btnSend.FlatAppearance.BorderSize = 1;
            btnSend.Click += new EventHandler(this.btnSend_Click);

            pnlChatContainer.Controls.Add(rtbChatHistory);
            pnlChatContainer.Controls.Add(txtUserInput);
            pnlChatContainer.Controls.Add(btnSend);

            // ====================================================================
            // VIEW PANEL 2: CYBERSECURITY ASSESSMENT QUIZ (RUBRIC COMPLIANT)
            // ====================================================================
            pnlQuizContainer = new Panel { Location = new Point(160, 65), Size = new Size(544, 546), Visible = false };

            lblQuestionText = new Label
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(500, 80),
                ForeColor = Color.DarkSlateGray,
                Text = "Loading question configuration arrays..."
            };

            lstOptions = new ListBox
            {
                Font = new Font("Segoe UI", 10F),
                ItemHeight = 22,
                Location = new Point(20, 115),
                Size = new Size(500, 200),
                BackColor = Color.White
            };

            btnSubmitAnswer = new Button
            {
                Location = new Point(20, 340),
                Size = new Size(200, 42),
                Text = "Submit Selected Answer",
                BackColor = Color.PaleVioletRed,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnSubmitAnswer.FlatAppearance.BorderSize = 0;
            btnSubmitAnswer.Click += new EventHandler(this.btnSubmitAnswer_Click);

            pnlQuizContainer.Controls.Add(lblQuestionText);
            pnlQuizContainer.Controls.Add(lstOptions);
            pnlQuizContainer.Controls.Add(btnSubmitAnswer);

            // Bind Form Window Level Structures
            this.Controls.Add(pnlChatContainer);
            this.Controls.Add(pnlQuizContainer);
            this.Controls.Add(sideNavPanel);
            this.Controls.Add(headerPanel);

            this.Load += new EventHandler(this.MainForm_Load);
        }

        private void ShowFeaturePanel(Panel targetPanel)
        {
            pnlChatContainer.Visible = false;
            pnlQuizContainer.Visible = false;

            targetPanel.Visible = true;
            targetPanel.BringToFront();
        }

        // ====================================================================
        // BUSINESS LOGIC EVENT HANDLERS
        // ====================================================================

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Render welcoming ASCII art banner
            AppendChatText("=============================================\n", Color.DeepPink, true);
            AppendChatText("  ____       _                ____        _   \n", Color.DeepPink, false);
            AppendChatText(" / ___|_   _| |__  ___ _ __| __ )  ___ | |_ \n", Color.DeepPink, false);
            AppendChatText("| |   | | | | '_ \\ / _ \\ '__|  _ \\ / _ \\| __|\n", Color.DeepPink, false);
            AppendChatText("| |___| |_| | |_) |  __/ |  | |_) | (_) | |_ \n", Color.DeepPink, false);
            AppendChatText(" \\____|\\__, |_.__/ \\___|_|  |____/ \\___/ \\__|\n", Color.DeepPink, false);
            AppendChatText("       |___/                                 \n", Color.DeepPink, false);
            AppendChatText("=============================================\n", Color.DeepPink, true);

            AppendChatText("SYSTEM INITIALIZATION SUCCESSFUL\n", Color.Black, true);
            AppendChatText("Bot: Welcome! Please enter your name below to register and begin personalization.\n\n", Color.Black, false);

            try
            {
                audioPlayer.PlayNotification();
            }
            catch (Exception ex)
            {
                AppendChatText($"System Audio Warning: Could not locate greeting media context string. ({ex.Message})\n\n", Color.DeepPink, false);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string rawInput = txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(rawInput))
            {
                AppendChatText("System: Message buffer cannot be sent empty!\n\n", Color.DeepPink, true);
                return;
            }

            AppendChatText($"{userName}: ", Color.DeepPink, true);
            AppendChatText($"{rawInput}\n", Color.Black, false);

            if (userName == "User")
            {
                userName = rawInput;
                AppendChatText($"Bot: Greetings {userName}! Ask me any security question or click 'Cyber Quiz' on the left sidebar to test your defensive knowledge.\n\n", Color.Black, false);
                DatabaseHelper.AddLog("USER_REGISTER", $"Identified active user tracking initialized under profile signature: {userName}");
                txtUserInput.Clear();
                return;
            }

            // Direct execution string capture bypassing slow stream routing redirections
            string botReply = botEngine.GetResponse(rawInput, userName);
            AppendChatText($"Bot: {botReply}\n\n", Color.Black, false);

            txtUserInput.Clear();
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
            rtbChatHistory.ScrollToCaret();
        }

        // ====================================================================
        // COMPREHENSIVE QUIZ ENGINE INTERFACES
        // ====================================================================

        private void StartQuiz()
        {
            scoreCounter = 0;
            quizEngine.Reset();
            DatabaseHelper.AddLog("QUIZ_INITIATE", $"User '{userName}' initiated cybersecurity competency quiz layout.");
            DisplayCurrentQuestion();
        }

        private void DisplayCurrentQuestion()
        {
            if (quizEngine.IsFinished)
            {
                lblQuestionText.Text = "Assessment Module Cycle Finished.";
                lstOptions.Items.Clear();
                btnSubmitAnswer.Enabled = false;

                MessageBox.Show($"Quiz Completed, {userName}!\n\nYour Final Score: {scoreCounter} / {quizEngine.TotalQuestions}",
                                "Assessment Profile Closed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DatabaseHelper.AddLog("QUIZ_SCORE", $"Completed. Final achieved record: {scoreCounter}/{quizEngine.TotalQuestions}");
                return;
            }

            btnSubmitAnswer.Enabled = true;
            var currentQ = quizEngine.GetCurrentQuestion();

            lblQuestionText.Text = $"Question {quizEngine.CurrentQuestionNumber} of {quizEngine.TotalQuestions}:\n\n{currentQ.Question}";

            lstOptions.Items.Clear();
            foreach (var option in currentQ.Options)
            {
                lstOptions.Items.Add(option);
            }
        }

        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            if (lstOptions.SelectedIndex == -1)
            {
                MessageBox.Show("Please highlight a valid option index from the list box selection.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isCorrect = quizEngine.SubmitAnswer(lstOptions.SelectedIndex, out string feedbackMessage);

            if (isCorrect)
            {
                scoreCounter++;
                MessageBox.Show(feedbackMessage, "Correct Matrix Result!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(feedbackMessage, "Incorrect Logic Vector", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            DisplayCurrentQuestion();
        }

        private void AppendChatText(string text, Color color, bool isBold)
        {
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
            rtbChatHistory.SelectionLength = 0;
            rtbChatHistory.SelectionColor = color;
            rtbChatHistory.SelectionFont = new Font(rtbChatHistory.Font, isBold ? FontStyle.Bold : FontStyle.Regular);
            rtbChatHistory.AppendText(text);
            rtbChatHistory.SelectionLength = 0;
        }
    }
}