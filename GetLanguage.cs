using Azure.AI.TextAnalytics;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure_AI102_Samples
{
    public class GetLanguage
    {
        public void RunGetLanguageSample(string cogSvcEndpoint, string cogSvcKey)
        {
            try
            {
                // Set console encoding to unicode
                Console.InputEncoding = Encoding.Unicode;
                Console.OutputEncoding = Encoding.Unicode;

                // Get user input (until they enter "quit")
                string userText = "";
                while (userText.ToLower() != "quit")
                {
                    Console.WriteLine("\nEnter some text ('quit' to stop)");
                    userText = Console.ReadLine();
                    if (userText.ToLower() != "quit")
                    {
                        // Call function to detect language
                        string language = GetLanguageAzure(userText, cogSvcEndpoint, cogSvcKey);
                        Console.WriteLine("Language: " + language);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static string GetLanguageAzure(string text, string cogSvcEndpoint, string cogSvcKey)
        {

            // Create client using endpoint and key

            Uri endpoint = new Uri(cogSvcEndpoint);
            var client = new TextAnalyticsClient(endpoint, new AzureKeyCredential(cogSvcKey));

            // Call the service to get the detected language
            DetectedLanguage detectedLanguage = client.DetectLanguage(text);
            return (detectedLanguage.Name);

        }
    }
}
