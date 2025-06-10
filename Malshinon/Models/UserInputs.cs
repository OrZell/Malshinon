namespace Malshinon.Models
{
    public class UserInputs
    {
        public static string EnterSecretCode()
        {
            Console.WriteLine("Please enter your Secret Code");
            string userPrompt = Console.ReadLine()!;
            if (userPrompt.Contains(" "))
            {
                throw new Exception("Invalid Code Name");
            }
            return userPrompt;
        }

        public static string EnterYourFullname()
        {
            Console.WriteLine("Please Enter your Full name with space between");
            string fullname = Console.ReadLine();
            if (fullname != null && fullname.Split().Length != 2)
            {
                throw new Exception("Invalid Input");
            }
            return fullname;
        }

        public static string EnterTargetFullname()
        {
            Console.WriteLine("Please Enter Target Full name with space between");
            string fullname = Console.ReadLine();
            if (fullname != null && fullname.Split().Length != 2)
            {
                throw new Exception("Invalid Input");
            }
            return fullname;
        }

        public static string EnterYourReport()
        {
            Console.WriteLine("Please Enter Your Report");
            string Text = Console.ReadLine();
            return Text;
        }
    }
}
