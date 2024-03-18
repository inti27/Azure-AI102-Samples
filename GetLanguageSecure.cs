using Azure.AI.TextAnalytics;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Azure_AI102_Samples
{
    public class GetLanguageSecure
    {
        //Secured variables
        private string CognitiveServicesEndpoint = "https://ai102-service.cognitiveservices.azure.com/";
        private string keyVaultName = "ai102bryon";
        private string TenantId = "e05c6635-e6ae-4253-8938-1abc94c2c449";
        private string AppId = "923505d7-0c1e-4d47-854c-f933c7fb3bd9";
        private string AppPassword = "lMS8Q~wROO~wfVjsUyXSnypkB36qGwPMnedrHapI";
        private string cogSvcKey;
        public void RunGetLanguageSample()
        {
            try
            {
                // Get Azure AI services key from keyvault using the service principal credentials
                var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
                ClientSecretCredential credential = new ClientSecretCredential(TenantId, AppId, AppPassword);
                var keyVaultClient = new SecretClient(keyVaultUri, credential);
                KeyVaultSecret secretKey = keyVaultClient.GetSecret("Cognitive-Services-Key");
                cogSvcKey = secretKey.Value;

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
                        string language = GetLanguageAzure(userText);
                        Console.WriteLine("Language: " + language);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private string GetLanguageAzure(string text)
        {

            // Create client using endpoint and key

            AzureKeyCredential credentials = new AzureKeyCredential(cogSvcKey);
            Uri endpoint = new Uri(CognitiveServicesEndpoint);
            var client = new TextAnalyticsClient(endpoint, credentials);


            // Call the service to get the detected language
            DetectedLanguage detectedLanguage = client.DetectLanguage(text);
            return (detectedLanguage.Name);

        }
    }
}
