namespace Malshinon.Models
{
    public class Menu
    {
        public static string EnterName()
        {
            Console.WriteLine("Please enter your code name");
            string userPrompt = Console.ReadLine()!;
            if (userPrompt.Contains(" "))
            {
                throw new Exception("Invalid Code Name");
            }
            return userPrompt;
        }
    }
}
