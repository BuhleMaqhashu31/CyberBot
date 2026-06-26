namespace CyberSecurityAssistant
{
    public class User
    {
        public string Name { get; set; }
        public int QuizScore { get; set; }
        public int QuizAnswered { get; set; }

        public User(string name)
        {
            Name = name;
            QuizScore = 0;
            QuizAnswered = 0;
        }
    }
}
