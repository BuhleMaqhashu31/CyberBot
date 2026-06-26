using Microsoft.VisualBasic.ApplicationServices;
using System.Media;

namespace CyberSecurityAssistant
{
    public class AudioPlayer
    {
        private SoundPlayer player;

        public AudioPlayer(string C:\Users\Student\source\repos\CyberbotProject\CyberbotProject\Buhle.wav)
        {
            player = new SoundPlayer(C: \Users\Student\source\repos\CyberbotProject\CyberbotProject\Buhle.wav);
        }

        public void PlayNotification()
        {
            try
            {
                player.Play();
            }
            catch
            {
                // Ignore audio errors so app keeps running
            }
        }
    }
}
