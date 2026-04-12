using System;

namespace CyberChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize Classes
            Chatbot bot = new Chatbot();
            AudioPlayer audio = new AudioPlayer();

            // UI Setup
            Console.Title = "Cyber Awareness Assistant v1.0";
            bot.DisplayLogo();

            // Step 1: Voice Greeting
            // audio.PlayGreeting("greeting.wav"); 

            // Step 3: User Setup
            Console.Write("\nSYSTEM: Please enter your name to initialize: ");
            string nameInput = Console.ReadLine();
            User currentUser = new User(nameInput);

            Console.WriteLine($"\n[ACCESS GRANTED] Welcome, {currentUser.Name}.\n");

            // Step 4 & 5: Interaction Loop
            bool running = true;
            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{currentUser.Name} > ");
                Console.ResetColor();

                string input = Console.ReadLine();

                if (input?.ToLower() == "exit" || input?.ToLower() == "quit")
                {
                    running = false;
                    Console.WriteLine("Bot: Systems shutting down. Stay safe online!");
                }
                else
                {
                    bot.Respond(input, currentUser.Name);
                }
            }
        }
    }
}