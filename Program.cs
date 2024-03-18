using System.Text;
using Azure;
using Azure.AI.TextAnalytics;
using Azure_AI102_Samples;

class Program
{

    private static string cogSvcEndpoint = "https://ai102-service.cognitiveservices.azure.com/";
    private static string cogSvcKey = "97275da49f06440a94e406a3ac902bb1";
    static void Main(string[] args)
    {
        while (true)
        {
            //get language
            Console.WriteLine("Please select which sample to run:");
            Console.WriteLine("1 - Get Language Sample");
            Console.WriteLine("2 - Get Language Sample Using Secure Key Vault");
            Console.WriteLine("3 - Get Language Sample");
            string selected_option = Console.ReadLine();
            switch (int.Parse(selected_option))
            {
                case 1:
                    {
                        new GetLanguage().RunGetLanguageSample(cogSvcEndpoint, cogSvcKey);
                    }
                    break;
                case 2:
                    {
                        new GetLanguageSecure().RunGetLanguageSample();
                    }
                    break;
                default:

                    {
                        try
                        {
                            throw new Exception("No such option...");
                        }
                        catch { }

                    }
                    break;
            }
        }

    }
}