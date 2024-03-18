using System.Text;
using Azure;
using Azure.AI.TextAnalytics;
using Azure_AI102_Samples;

class Program
{


    static void Main(string[] args)
    {
        while (true)
        {
            //get language
            Console.WriteLine("Please select which sample to run:");
            Console.WriteLine("1 - Get Language Sample");
            Console.WriteLine("2 - Get Language Sample Using Secure Key Vault");
            Console.WriteLine("3 - Text Analysis Sample");
            Console.WriteLine("4 - Speaking Clock Sample");
            Console.WriteLine("5 - Translate Text");

            string selected_option = Console.ReadLine();
            int SelectedOption = 0;
            if (int.TryParse(selected_option, out SelectedOption))
            {
                switch (SelectedOption)
                {
                    case 1:
                        {
                            new GetLanguage().RunGetLanguageSample();
                        }
                        break;
                    case 2:
                        {
                            new GetLanguageSecure().RunGetLanguageSample();
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Enter the text to analize");
                            string text = Console.ReadLine();
                            new TextAnalysis().RunAnalysisSample(text);
                        }
                        break;
                        case 4:
                        {
                            new SpeakingClock().RunSpeakingClockSample();
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Enter the text to translate");
                            string text = Console.ReadLine();
                            new TextTranslate().TranslateText(text);
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
}