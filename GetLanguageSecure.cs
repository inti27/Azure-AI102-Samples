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
        private Helper helper = new Helper();
        string cogSvcKey;

        public void RunGetLanguageSample()
        {
            try
            {
                // Get Azure AI services key from keyvault using the service principal credentials
                var keyVaultUri = new Uri($"https://{helper.keyVaultName}.vault.azure.net/");
                ClientSecretCredential credential = new ClientSecretCredential(helper.TenantId, helper.AppId, helper.AppPassword);
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
            Uri endpoint = new Uri(helper.CognitiveServicesEndpoint);
            var client = new TextAnalyticsClient(endpoint, credentials);


            // Call the service to get the detected language
            DetectedLanguage detectedLanguage = client.DetectLanguage(text);
            return (detectedLanguage.Name);

        }
    }
}
