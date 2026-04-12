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
            string cleanInput = input.ToLower().Trim();

            if (string.IsNullOrEmpty(cleanInput))
            {
                Console.WriteLine("Bot: It's a bit quiet here... are you there?");
                return;
            }

            if (cleanInput.Contains("how are you"))
            {
                Console.WriteLine($"Bot: I'm running at 100% efficiency, {userName}! How can I help your security today?");
            }
            else if (cleanInput.Contains("purpose"))
            {
                Console.WriteLine("Bot: My purpose is to assist you with cybersecurity queries and keep your data safe.");
            }
            else if (cleanInput.Contains("phishing"))
            {
                Console.WriteLine("Bot: Phishing is a social engineering attack where hackers trick you into giving up passwords.");
            }
            else if (cleanInput.Contains("firewall"))
            {
                Console.WriteLine("Bot: A firewall acts as a barrier between your trusted network and untrusted external networks.");
            }
            else
            {
                Console.WriteLine($"Bot: I'm still learning, {userName}. I don't have information on that specific topic yet.");
            }
        }
    }
}