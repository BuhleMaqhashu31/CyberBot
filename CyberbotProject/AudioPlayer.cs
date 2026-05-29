using System;
using System.Media;

namespace CyberChatbot
{
    public class AudioPlayer
    {
        public void PlayGreeting(string filePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Audio Error]: {ex.Message}");
            }
        }
    }
}