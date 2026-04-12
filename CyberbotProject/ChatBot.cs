using System;

namespace CyberChatbot
{
    public class Chatbot
    {
        public void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            // Generated using "Slant" font
            string logo = @"
   ______      __             ____        __ 
  / ____/_  __/ /_  ___  ____/ __ )____  / /_
 / /   / / / / __ \/ _ \/ ___/ __  / __ \/ __/
/ /___/ /_/ / /_/ /  __/ /  / /_/ / /_/ / /_  
\____/\__, /_.___/\___/_/  /_____/\____/\__/  
     /____/                                   ";
            Console.WriteLine(logo);
            Console.ResetColor();
        }

        public void Respond(string input, string userName)
        {
            // 1. Handle Empty Input (Step 5)
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Bot: I didn't catch that. Could you please type something?");
                Console.ResetColor();
                return;
            }

            string cleanInput = input.ToLower().Trim();

            // 2. Basic Responses (Step 4)
            if (cleanInput.Contains("how are you"))
            {
                Console.WriteLine($"Bot: I am functioning at optimal levels, {userName}. All systems are secure!");
            }
            else if (cleanInput.Contains("purpose"))
            {
                Console.WriteLine("Bot: My purpose is to assist you with cybersecurity awareness and keep your data safe.");
            }

            // 3. Cybersecurity Questions (Step 4)
            else if (cleanInput.Contains("phishing"))
            {
                Console.WriteLine("Bot: Phishing is when hackers send fake emails to steal your passwords. Always check the sender!");
            }
            else if (cleanInput.Contains("password"))
            {
                Console.WriteLine("Bot: A strong password should be long, include numbers, and use special characters like @ or #.");
            }
            else if (cleanInput.Contains("malware"))
            {
                Console.WriteLine("Bot: Malware is 'malicious software' designed to harm or exploit your computer.");
            }

            // 4. Handle Unknown Input (Step 5)
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Bot: I'm sorry {userName}, my database doesn't have information on '{input}' yet. Try asking about Phishing or Passwords.");
                Console.ResetColor();
            }
        }
    }
}