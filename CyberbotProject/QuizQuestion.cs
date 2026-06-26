namespace CyberSecurityAssistant
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }
        public string CorrectFeedback { get; set; }
        public string WrongFeedback { get; set; }
    }
}


