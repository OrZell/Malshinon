namespace Malshinon.Models
{
    public class Menu
    {
        public static string[] EnterName()
        {
            string[] fullName = new string[2];

            Console.WriteLine("Please enter your full name (two words)");
            string[] userPrompt = Console.ReadLine()!.Split();
            if (userPrompt.Length != 2)
            {
                throw new Exception("Invalid Name");
            }
            fullName[0] = userPrompt[0];
            fullName[1] = userPrompt[1];
            return fullName;
        }
    }
}
