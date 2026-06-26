using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityAssistant
{
    public class ChatBot
    {
        private readonly List<NlpIntent> intents;
        private readonly HashSet<string> stopWords;

        public ChatBot()
        {
            // Words to strip out so we focus on the true intent of the phrasing
            stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "a", "an", "the", "please", "tell", "me", "about", "what", "is", "how", "does", "do", "you", "know", "define", "explain"
            };

            intents = new List<NlpIntent>
            {
                new NlpIntent(
                    new[] { "password", "passwords", "credential", "credentials", "passphrase" },
                    "Use strong passwords containing a mix of uppercase letters, lowercase letters, numbers, and symbols. Avoid reusing passwords across different platforms, and consider using a reputable password manager."
                ),
                new NlpIntent(
                    new[] { "phishing", "phish", "email scam", "spoof", "fake link" },
                    "Phishing is a deceptive technique using malicious emails or fraudulent websites designed to steal identity data. Always carefully verify the sender domain name and avoid clicking unsolicited links."
                ),
                new NlpIntent(
                    new[] { "malware", "virus", "trojan", "spyware", "infection" },
                    "Malware refers collectively to malicious software variations like viruses, ransomware, or spyware. Keep your native antivirus system activated, and avoid downloading files from non-trusted domains."
                ),
                new NlpIntent(
                    new[] { "vpn", "virtual private network", "encryption", "public wifi" },
                    "A Virtual Private Network (VPN) creates an encrypted tunnel over public networks, masking your IP address and preserving active data confidentiality from packet sniffers."
                ),
                new NlpIntent(
                    new[] { "firewall", "network security", "packet filter" },
                    "A firewall functions as a barrier monitoring inbound and outbound network traffic. It permits or blocks data packets based on predefined structural security rules."
                ),
                new NlpIntent(
                    new[] { "2fa", "mfa", "two factor", "multi factor", "verification" },
                    "Multi-Factor or Two-Factor Authentication requires secondary validation vectors (like an authenticator app or token) beyond a standard password, radically mitigating account breaches."
                ),
                new NlpIntent(
                    new[] { "ransomware", "crypto locker", "extortion", "encrypt files" },
                    "Ransomware encrypts critical local file directories and demands digital currency ransoms. Maintain continuous cold backups and minimize elevated computer administrative access rights."
                ),
                new NlpIntent(
                    new[] { "safe browsing", "web security", "https", "secure site" },
                    "Safe browsing calls for checking HTTPS certificates, closing unexpected redirect tabs instantly, and maintaining automated client browser patch deployment."
                ),
                new NlpIntent(
                    new[] { "cybersecurity", "infosec", "digital defense" },
                    "Cybersecurity is the holistic operational practice of defending hardware networks, software services, and proprietary digital data stores from malicious network attacks."
                )
            };
        }

        public string GetResponse(string input, string userName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return $"Please type a cybersecurity question, {userName}.";
            }

            string cleanInput = input.ToLower();

            // Handle direct greeting intents
            if (cleanInput.Contains("hello") || cleanInput.Contains("hi") || cleanInput.Contains("hey"))
            {
                return $"Hello {userName}, I am your advanced assistant. Ask me anything about online safety, configuration, or malicious threats.";
            }

            if (cleanInput.Contains("how are you"))
            {
                return $"I am running optimally and ready to protect your space, {userName}. What topic can I dissect for you today?";
            }

            if (cleanInput.Contains("help"))
            {
                return "Type natural questions about passwords, fishing scams, malware protection, firewalls, or multi-factor authentication setup.";
            }

            // Advanced NLP simulation token parsing
            char[] punctuation = { ' ', '?', '!', '.', ',', ';', '-' };
            string[] userWords = cleanInput.Split(punctuation, StringSplitOptions.RemoveEmptyEntries)
                                           .Where(w => !stopWords.Contains(w))
                                           .ToArray();

            NlpIntent bestMatch = null;
            double highestScore = 0.0;

            foreach (var intent in intents)
            {
                double score = intent.CalculateMatchScore(userWords);
                if (score > highestScore)
                {
                    highestScore = score;
                    bestMatch = intent;
                }
            }

            // Confidence threshold evaluation
            if (bestMatch != null && highestScore >= 0.25)
            {
                return bestMatch.Response;
            }

            // Fallback that extracts context dynamically
            return $"I noticed you are asking about alternative subjects, {userName}. While I didn't match that exact phrase, try specifying if you need support with core security systems like data encryption, passwords, or network malware.";
        }
    }

    internal class NlpIntent
    {
        public string[] KeyPhrases { get; set; }
        public string Response { get; set; }

        public NlpIntent(string[] phrases, string response)
        {
            KeyPhrases = phrases;
            Response = response;
        }

        public double CalculateMatchScore(string[] userTokens)
        {
            if (userTokens.Length == 0) return 0.0;
            int matches = 0;

            foreach (var token in userTokens)
            {
                foreach (var phrase in KeyPhrases)
                {
                    // Full phrase matching or partial root text similarity checking
                    if (phrase.Equals(token, StringComparison.OrdinalIgnoreCase) ||
                        token.Contains(phrase) ||
                        phrase.Contains(token))
                    {
                        matches++;
                        break;
                    }
                }
            }

            return (double)matches / userTokens.Length;
        }
    }
}
