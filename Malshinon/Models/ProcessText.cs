namespace Malshinon.Models
{
    public class ProcessText
    {
        public static string ProcessTextFromReporter(string Text)
        {
            string finalResult = null!;
            List<string> TextInWords = Text.Split().ToList();

            for (int i = 0; i < TextInWords.Count; i++)
            {
                if (TextInWords[i].Length > 14 && TextInWords[i] == TextInWords[i].ToUpper())
                {
                    finalResult = TextInWords[i];
                }
            }
            if (finalResult == null)
            {
                throw new Exception("No SecretCode Detected");
            }
            return finalResult;
        }
    }
}
