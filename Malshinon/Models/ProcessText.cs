namespace Malshinon.Models
{
    public class ProcessText
    {
        public static List<string> ProcessTextFromReporter(string Text)
        {
            List<string> finalResult = new List<string>();
            List<string> TextInWords = Text.Split().ToList();

            for (int i = 0; i < TextInWords.Count-1; i++)
            {
                if (TextInWords[i] == TextInWords[i].ToUpper() && TextInWords[i+1] == TextInWords[i+1].ToUpper())
                {
                    finalResult.Add(TextInWords[i]);
                    finalResult.Add(TextInWords[i+1]);
                    finalResult.Add(Text);
                }
            }
            return finalResult;
        }
    }
}
