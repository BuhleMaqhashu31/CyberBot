using System;

namespace CyberSecurityAssistant
{
    public class ActivityLogEntry
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{TimeStamp:g} | {ActionType} | {Description}";
        }
    }
}

