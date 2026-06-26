using System;
using System.Collections.Generic;

namespace CyberSecurityAssistant
{
    public class QuizEngine
    {
        private readonly List<QuizQuestion> questions;
        private int currentQuestionIndex;

        public QuizEngine()
        {
            currentQuestionIndex = 0;
            questions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    Question = "Which character composition profile yields the highest absolute entropy for password safety?",
                    Options = new[] { "Your birth year + your name", "At least 12 characters mixing upper, lower, numbers, and symbols", "A single dictionary word capitalized at the end", "8 alphabetical lowercase letters" },
                    CorrectIndex = 1,
                    CorrectFeedback = "Correct! Length combined with multi-class complexity forces exponential brute-force calculation delays.",
                    WrongFeedback = "Incorrect. Simple dictionary variations or short alphanumeric chains are quickly cracked using precomputed rainbow tables."
                },
                new QuizQuestion {
                    Question = "What identifier component indicates that an inbound electronic mail is likely a phishing attempt?",
                    Options = new[] { "An unexpected domain name variations in the sender's email string", "The message has an image block attached", "The signature mentions standard support channels", "It arrives outside business hours" },
                    CorrectIndex = 0,
                    CorrectFeedback = "Spot on! Look-alike domains (e.g., security-paypal.com instead of paypal.com) reveal deceptive intent.",
                    WrongFeedback = "Incorrect. Attackers primarily rely on mimicking trustworthy styling while deploying misleading subdomains."
                },
                new QuizQuestion {
                    Question = "How does ransomware functionally compromise consumer hardware storage files?",
                    Options = new[] { "It deletes local operating system shortcuts", "It silently tracks keys via background hardware injection", "It encrypts local user directories using asymmetrical keys and demands payment", "It accelerates standard CPU fan speeds" },
                    CorrectIndex = 2,
                    CorrectFeedback = "Excellent! Ransomware utilizes strong data encryption to lock files until an encryption key ransom is satisfied.",
                    WrongFeedback = "Incorrect. Ransomware acts by encrypting access to your user profile documentation rather than just tracking keystrokes."
                },
                new QuizQuestion {
                    Question = "What primary advantage is obtained by introducing 2FA/MFA architectures?",
                    Options = new[] { "It reduces local web browser cookie sizes", "It requires secondary distinct verification constraints if passwords fail or leak", "It increases local network upload band speed", "It replaces the requirement for choosing strong master passwords entirely" },
                    CorrectIndex = 1,
                    CorrectFeedback = "Superb! Compromised credentials alone cannot grant system access without the synchronized multi-factor physical key token.",
                    WrongFeedback = "Incorrect. MFA acts as a layered security barrier; it does not replace passwords or boost network speeds."
                },
                new QuizQuestion {
                    Question = "What risk does utilizing a public Wi-Fi hotspot present without an active VPN tunnel?",
                    Options = new[] { "Local batteries discharge twice as quickly", "The hotspot will auto-delete installed antivirus tools", "Malicious third parties can capture unencrypted data packets transiting the network", "Your screen resolution defaults down" },
                    CorrectIndex = 2,
                    CorrectFeedback = "Correct! Unsecured wireless hotspots allow easy Man-in-the-Middle (MitM) sniffing of active data payloads.",
                    WrongFeedback = "Incorrect. Threat actors monitor the traffic streaming across public airwaves; it won't impact your battery or delete apps."
                },
                new QuizQuestion {
                    Question = "What operational behavior defines a classic Network Firewall?",
                    Options = new[] { "It cleans physical dust accumulation inside case components", "It monitors and selectively blocks inbound or outbound packet traffic via rules", "It automatically backs up system documents to remote storage arrays", "It converts weak software text strings into hashes" },
                    CorrectIndex = 1,
                    CorrectFeedback = "Correct! Firewalls act as strict boundary wardens matching packet metadata against configuration rule parameters.",
                    WrongFeedback = "Incorrect. Firewalls analyze network protocol streams; they don't generate hashes or clear up storage space."
                },
                new QuizQuestion {
                    Question = "Which protocol variation specifies that communication with a website is protected by an active SSL/TLS tunnel?",
                    Options = new[] { "HTTP", "FTP", "HTTPS", "SMTP" },
                    CorrectIndex = 2,
                    CorrectFeedback = "Brilliant! The 'S' explicitly represents Security via cryptographic socket mapping protocols.",
                    WrongFeedback = "Incorrect. Standard HTTP transmits clear text data, making it highly unsafe. Look for HTTPS."
                },
                new QuizQuestion {
                    Question = "What is a TrojanHorse in the context of endpoint malware delivery?",
                    Options = new[] { "A network cable processing splitter hardware device", "Malware disguised as legitimate or safe utility software utilities", "An automated email server scheduling mechanism", "A method for securely grouping code patterns" },
                    CorrectIndex = 1,
                    CorrectFeedback = "Exactly! Trojans mask malicious payloads inside functional structures like games or system updates.",
                    WrongFeedback = "Incorrect. Trojan malware deceives victims by appearing benign, hiding its payload inside useful applications."
                },
                new QuizQuestion {
                    Question = "What lifecycle action is vital to maintain software application security postures?",
                    Options = new[] { "Changing monitor color balances monthly", "Disabling default background system reporting engines", "Installing software security patches promptly when published", "Clearing temporary offline audio files daily" },
                    CorrectIndex = 2,
                    CorrectFeedback = "Perfect! Vendor software updates fix known remote security exploits before hackers weaponize them.",
                    WrongFeedback = "Incorrect. Delaying patches leaves severe system vulnerabilities completely open to active exploit frameworks."
                },
                new QuizQuestion {
                    Question = "What type of attack describes overloading a targeted host system with millions of fake data requests?",
                    Options = new[] { "SQL Injection", "Distributed Denial of Service (DDoS)", "Spyware Keylogging", "Phishing redirection" },
                    CorrectIndex = 1,
                    CorrectFeedback = "Correct! DDoS floods target systems, exhausting network bandwidth and computing resources to take them offline.",
                    WrongFeedback = "Incorrect. Resource exhaustion attacks are classified as Denial of Service attacks."
                },
                new QuizQuestion {
                    Question = "Why is checking the full structure of an explicit web link URL before entry highly beneficial?",
                    Options = new[] { "It confirms the download speed metrics", "It flags hidden typosquatting domain tricks intended to clone true portals", "It increases local computing RAM availability profiles", "It cleans invalid web cache registries automatically" },
                    CorrectIndex = 1,
                    CorrectFeedback = "Excellent! Typosquatting exploits tiny errors (like faceb00k.com) to catch unwary users.",
                    WrongFeedback = "Incorrect. URL checking lets you detect subtle domain spoofs, preventing credential harvesting before it happens."
                }
            };
        }

        public int TotalQuestions => questions.Count;
        public int CurrentQuestionNumber => currentQuestionIndex + 1;
        public bool IsFinished => currentQuestionIndex >= questions.Count;

        public QuizQuestion GetCurrentQuestion()
        {
            if (IsFinished) return null;
            return questions[currentQuestionIndex];
        }

        public bool SubmitAnswer(int selectedIndex, out string feedback)
        {
            var q = GetCurrentQuestion();
            if (q == null)
            {
                feedback = "No active quiz context found.";
                return false;
            }

            bool isCorrect = (selectedIndex == q.CorrectIndex);
            feedback = isCorrect ? q.CorrectFeedback : q.WrongFeedback;

            currentQuestionIndex++;
            return isCorrect;
        }

        public void Reset()
        {
            currentQuestionIndex = 0;
        }
    }
}