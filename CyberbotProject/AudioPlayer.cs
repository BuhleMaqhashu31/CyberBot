using System;
using System.Media; // This allows us to use SoundPlayer

namespace CyberChatbot
{
    public class AudioPlayer
    {
        public void PlayGreeting(string filePath)
        {
            try
            {
                // CHANGE: Use SoundPlayer, not AudioPlayer
                using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(filePath))
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