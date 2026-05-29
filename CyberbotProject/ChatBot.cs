using System;

namespace CyberChatbot
{
    public class Chatbot
    {
        public void Dictionary(string input, string userName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Bot: My analytics systems register no data input.");
                return;
            }

            string cleanInput = input.ToLower().Trim();

            // Strict Keyword matching switch pattern
            switch (cleanInput)
            {
                case "how are you":
                case "how are you?":
                    Console.WriteLine($"Bot: My local encryption modules are stable, {userName}.");
                    break;

                case "purpose":
                case "what is your purpose?":
                    Console.WriteLine("Bot: I am designed to educate corporate users on digital safety and best practices.");
                    break;

                default:
                    // 1. EXTENDED PHISHING KNOWLEDGE ARCHITECTURE
                    if (cleanInput.Contains("phishing"))
                    {
                        if (cleanInput.Contains("prevent") || cleanInput.Contains("stop") || cleanInput.Contains("protect"))
                        {
                            Console.WriteLine("Bot: To prevent phishing, always verify the sender's full email address header, avoid clicking unsolicited links, enable Multi-Factor Authentication (MFA), and use email filters to flag external domains.");
                        }
                        else if (cleanInput.Contains("types") || cleanInput.Contains("examples"))
                        {
                            Console.WriteLine("Bot: Core phishing variations include:\n- Spear Phishing: Highly targeted attacks aimed at specific individuals.\n- Whaling: Phishing aimed strictly at high-profile executives.\n- Smishing & Vishing: Attacks conducted via SMS text messages or voice calls.");
                        }
                        else if (cleanInput.Contains("sign") || cleanInput.Contains("spot") || cleanInput.Contains("look for"))
                        {
                            Console.WriteLine("Bot: Warning signs of a phishing attempt include urgent or threatening language, generic greetings like 'Dear Customer', mismatched URL links when hovering, and unexpected requests for personal information.");
                        }
                        else
                        {
                            Console.WriteLine("Bot: Phishing is a social engineering attack where malicious actors impersonate legitimate organizations to trick users into revealing credentials, banking details, or sensitive company data.");
                        }
                    }

                    // 2. EXTENDED FIREWALL KNOWLEDGE ARCHITECTURE
                    else if (cleanInput.Contains("firewall"))
                    {
                        if (cleanInput.Contains("types") || cleanInput.Contains("different"))
                        {
                            Console.WriteLine("Bot: Firewalls are categorized into different types:\n- Packet Filtering: Inspects packets in isolation.\n- Stateful Inspection: Monitors active connection states.\n- Next-Generation Firewalls (NGFW): Adds deep packet inspection and application-level control.");
                        }
                        else if (cleanInput.Contains("why") || cleanInput.Contains("need") || cleanInput.Contains("importance"))
                        {
                            Console.WriteLine("Bot: You need a firewall because it acts as a protective barrier between your secure internal network and untrusted external networks, blocking unauthorized traffic from hackers and scanning tools.");
                        }
                        else if (cleanInput.Contains("limitation") || cleanInput.Contains("fail") || cleanInput.Contains("stop"))
                        {
                            Console.WriteLine("Bot: While powerful, firewalls cannot stop threats introduced internally via USB drives, nor can they block social engineering attacks where an employee willingly leaks data or credentials.");
                        }
                        else
                        {
                            Console.WriteLine("Bot: A firewall is a network security device that monitors and filters incoming and outgoing network traffic based on an organization's previously established security policies.");
                        }
                    }

                    // 3. EXTENDED MALWARE KNOWLEDGE ARCHITECTURE
                    else if (cleanInput.Contains("malware") || cleanInput.Contains("virus") || cleanInput.Contains("ransomware"))
                    {
                        if (cleanInput.Contains("remove") || cleanInput.Contains("fix") || cleanInput.Contains("clean"))
                        {
                            Console.WriteLine("Bot: To remove malware, disconnect the machine from the network immediately, boot in Safe Mode, run a full system scan using verified anti-malware software, and restore uncorrupted data from an isolated backup.");
                        }
                        else if (cleanInput.Contains("types") || cleanInput.Contains("different"))
                        {
                            Console.WriteLine("Bot: Common malware categories include:\n- Ransomware: Encrypts files and demands payment.\n- Spyware: Secretly records keystrokes and sensitive passphrases.\n- Trojans: Disguises itself as legitimate utility software to open backdoors.");
                        }
                        else if (cleanInput.Contains("spread") || cleanInput.Contains("get infected"))
                        {
                            Console.WriteLine("Bot: Malware typically spreads through email attachments, compromised software downloads, unpatched operating system vulnerabilities, and malicious peer-to-peer file sharing links.");
                        }
                        else
                        {
                            Console.WriteLine("Bot: Malware is any software intentionally designed to cause damage to a computer, server, client, or computer network.");
                        }
                    }

                    // Fallback response for completely unrelated inputs
                    else
                    {
                        Console.WriteLine($"Bot: I cannot answer that specific query, {userName}. Please ask an explicit question regarding Phishing, Firewalls, or Malware.");
                    }
                    break;
            }
        }
    }
}