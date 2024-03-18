using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;

namespace Azure_AI102_Samples
{
    internal class TextAnalysis
    {
        private Helper helper = new Helper();

        public void RunAnalysisSample(string text)
        {
            try
            {
                // Create client using endpoint and key
                AzureKeyCredential credentials = new AzureKeyCredential(helper.cogSvcKey);
                Uri endpoint = new Uri(helper.CognitiveServicesEndpoint);
                TextAnalyticsClient CogClient = new TextAnalyticsClient(endpoint, credentials);

                // Get language
                DetectedLanguage detectedLanguage = CogClient.DetectLanguage(text);
                Console.WriteLine($"\nLanguage: {detectedLanguage.Name}");

                // Get sentiment
                // Get sentiment
                DocumentSentiment sentimentAnalysis = CogClient.AnalyzeSentiment(text);
                Console.WriteLine($"\nSentiment: {sentimentAnalysis.Sentiment}");

                // Get key phrases

                // Get key phrases
                KeyPhraseCollection phrases = CogClient.ExtractKeyPhrases(text);
                if (phrases.Count > 0)
                {
                    Console.WriteLine("\nKey Phrases:");
                    foreach (string phrase in phrases)
                    {
                        Console.WriteLine($"\t{phrase}");
                    }
                }
                // Get entities
                // Get entities
                CategorizedEntityCollection entities = CogClient.RecognizeEntities(text);
                if (entities.Count > 0)
                {
                    Console.WriteLine("\nEntities:");
                    foreach (CategorizedEntity entity in entities)
                    {
                        Console.WriteLine($"\t{entity.Text} ({entity.Category})");
                    }
                }

                // Get linked entities
                // Get linked entities
                LinkedEntityCollection linkedEntities = CogClient.RecognizeLinkedEntities(text);
                if (linkedEntities.Count > 0)
                {
                    Console.WriteLine("\nLinks:");
                    foreach (LinkedEntity linkedEntity in linkedEntities)
                    {
                        Console.WriteLine($"\t{linkedEntity.Name} ({linkedEntity.Url})");
                    }
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
