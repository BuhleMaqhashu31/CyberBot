using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CyberChatbot
{
    public partial class MainForm : Form
    {
        // UI Component Declarations
        private RichTextBox rtbChatHistory;
        private TextBox txtUserInput;
        private Button btnSend;
        private Panel headerPanel;
        private Label lblTitle;

        // Core Backend Objects
        private Chatbot botEngine;
        private AudioPlayer audioPlayer;
        private string userName = "User";

        public MainForm()
        {
            InitializeComponentLayout();
            botEngine = new Chatbot();
            audioPlayer = new AudioPlayer();
        }

        private void InitializeComponentLayout()
        {
            // Main Window Configurations - Whimsical Pink Theme
            this.Size = new Size(520, 650);
            this.Text = "Cyber Security Assistant";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LavenderBlush;

            // 1. Playful Top Header Panel
            headerPanel = new Panel { Size = new Size(520, 65), Dock = DockStyle.Top, BackColor = Color.HotPink };
            lblTitle = new Label
            {
                Text = "CYBER SECURITY ASSISTANT",
                ForeColor = Color.White,
                Font = new Font("Comic Sans MS", 12, FontStyle.Bold),
                Location = new Point(15, 20),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblTitle);

            // 2. Chat History Display Log
            rtbChatHistory = new RichTextBox
            {
                Location = new Point(20, 85),
                Size = new Size(465, 430),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Courier New", 9.5f),
                BorderStyle = BorderStyle.FixedSingle
            };

            // 3. User Input Channel Textbox
            txtUserInput = new TextBox
            {
                Location = new Point(20, 540),
                Size = new Size(355, 30),
                Font = new Font("Comic Sans MS", 11),
                BackColor = Color.White,
                ForeColor = Color.Black
            };

            // 4. Send Button Hook
            btnSend = new Button
            {
                Location = new Point(385, 538),
                Size = new Size(100, 32),
                Text = "Send",
                BackColor = Color.HotPink,
                ForeColor = Color.White,
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnSend.FlatAppearance.BorderColor = Color.Black;
            btnSend.FlatAppearance.BorderSize = 1;
            btnSend.Click += new EventHandler(this.btnSend_Click);

            // Bind Elements to the Form Window Frame
            this.Controls.Add(headerPanel);
            this.Controls.Add(rtbChatHistory);
            this.Controls.Add(txtUserInput);
            this.Controls.Add(btnSend);

            this.Load += new EventHandler(this.MainForm_Load);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 1. ASCII Art Banner printed right at launch
            AppendChatText("=============================================\n", Color.DeepPink, true);
            AppendChatText("  ____       _               ____        _   \n", Color.DeepPink, false);
            AppendChatText(" / ___|_   _| |__   ___ _ __| __ )  ___ | |_ \n", Color.DeepPink, false);
            AppendChatText("| |   | | | | '_ \\ / _ \\ '__|  _ \\ / _ \\| __|\n", Color.DeepPink, false);
            AppendChatText("| |___| |_| | |_) |  __/ |  | |_) | (_) | |_ \n", Color.DeepPink, false);
            AppendChatText(" \\____|\\__, |_.__/ \\___|_|  |____/ \\___/ \\__|\n", Color.DeepPink, false);
            AppendChatText("       |___/                                 \n", Color.DeepPink, false);
            AppendChatText("=============================================\n", Color.DeepPink, true);

            // 2. Clear System Initialization Prompt
            AppendChatText("SYSTEM INITIALIZATION SUCCESSFUL\n", Color.Black, true);
            AppendChatText("Bot: Welcome! Please type your name in the chat below to begin personalization.\n\n", Color.Black, false);

            // 3. Audio Player Safely Isolated in Try-Catch
            try
            {
                audioPlayer.PlayGreeting("Buhle.wav");
            }
            catch (Exception ex)
            {
                // Writes the file search location directly to your screen if it cannot be found
                AppendChatText($"System Audio Warning: Could not execute greeting asset file. ({ex.Message})\n\n", Color.DeepPink, false);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string rawInput = txtUserInput.Text;

            if (string.IsNullOrWhiteSpace(rawInput))
            {
                AppendChatText("System: Please type something first!\n\n", Color.DeepPink, true);
                return;
            }

            // Print User Input in Black text
            AppendChatText($"{userName}: ", Color.DeepPink, true);
            AppendChatText($"{rawInput}\n", Color.Black, false);

            // User Personalization Registration Check
            if (userName == "User")
            {
                userName = rawInput.Trim();
                AppendChatText($"Bot: Hello {userName}! Feel free to ask me anything about Phishing, Firewalls, or Malware.\n\n", Color.Black, false);
                txtUserInput.Clear();
                return;
            }

            // Console Redirection Interception Engine
            using (StringWriter writer = new StringWriter())
            {
                TextWriter oldOut = Console.Out;
                Console.SetOut(writer);

                // Executes core logic method from ChatBot.cs
                botEngine.Dictionary(rawInput, userName);

                Console.SetOut(oldOut);

                // Preserves precise spacing coming from ChatBot.cs console output streams
                string botReply = writer.ToString();

                if (!string.IsNullOrEmpty(botReply))
                {
                    AppendChatText($"{botReply}\n\n", Color.Black, false);
                }
            }

            txtUserInput.Clear();
            rtbChatHistory.ScrollToCaret(); // Auto-scrolls into the ongoing chat history
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