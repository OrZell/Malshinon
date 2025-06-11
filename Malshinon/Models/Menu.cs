using Malshinon.DAL;
using Malshinon.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class Menu
    {
        private PeopleDAL Pda;
        private ReportDAL Rda;
        public Menu(MySQLServer sql)
        {
            this.Pda = new PeopleDAL(sql);
            this.Rda = new ReportDAL(sql);
        }
        public void MainMenu()
        {
            bool sign = true;
            do
            {
                Console.WriteLine("Please Choice:\n" +
                    "1. Enter Report\n" +
                    "2. Exit");
                string userInput = Console.ReadLine()!;
                switch (userInput)
                {
                    case "1":
                        flow();
                        break;

                    case "2":
                        sign = false;
                        Console.WriteLine("Bye Bye");
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            while (sign);
        }

        public void flow()
        {
            string reporterSecretCode = UserInputs.EnterYourSecretCode();
            CheckIfSecretCodeExist(reporterSecretCode);

            string targetSecretCode = UserInputs.EnterTargetSecretCode();

            string Text = UserInputs.EnterYourReport();

            CheckIfSecretCodeExist(targetSecretCode, 2, false);

            AddReportAndIncrease(reporterSecretCode, targetSecretCode, Text);

            CheckIfToChangeTheStatusToBoth(reporterSecretCode);
            CheckIfToChangeTheStatusToBoth(targetSecretCode);

            CheckIfToChangeTheStatusToPotentialAgent(reporterSecretCode);
            CheckIfToLogAboutPotentialThreadAlert(targetSecretCode);



            Console.WriteLine("Proccessed");
        }

        public void CheckIfSecretCodeExist(string secretCode, int type = 1, bool sign = true)
        {
            string[] fullName = new string[2];
            if (!this.Pda.SearchByCodeName(secretCode))
            {
                if (sign)
                {
                    fullName = UserInputs.EnterYourFullname().Split();
                }
                else
                {
                    fullName = UserInputs.EnterTargetFullname().Split();
                }
                Person person = new Person(fullName[0], fullName[1], secretCode, type);
                this.Pda.AddPerson(person);
                Logger.CreateLog($"New Person Created {secretCode}");
            }
        }

        public void AddReportAndIncrease(string reporterSecretCode, string targetSecretCode, string Text)
        {
            int reporterID = this.Pda.FindIdBySecretCode(reporterSecretCode);
            int targetID = this.Pda.FindIdBySecretCode(targetSecretCode);
            Report report = new Report(reporterID, targetID, Text);
            this.Rda.AddReport(report);
            Logger.CreateLog("New Report Created");
            this.Pda.IncreaseReportsBySecretCode(reporterSecretCode);
            this.Pda.IncreaseMentionsBySecretCode(targetSecretCode);
        }

        public void CheckIfToChangeTheStatusToBoth(string secretCode)
        {
            if (this.Pda.ChaeckAndReturnTheType(secretCode) < 3 &&
                this.Pda.GetNumOfReportsBySecretCode(secretCode) > 0 &&
                this.Pda.GetNumOfMentionsBySecretCode(secretCode) > 0)
            {
                this.Pda.ConvertToBoth(secretCode);
            }
        }
        public void CheckIfToChangeTheStatusToPotentialAgent(string secretCode)
        {
            int RepsNum = this.Pda.GetNumOfReportsBySecretCode(secretCode);
            int AllCharters = this.Pda.GetAllChartersBySecretCode(secretCode);
            if (RepsNum >= 10 && AllCharters/RepsNum >= 100)
            {
                this.Pda.ConvertToPotentialAgent(secretCode);
            }
        }

        public void CheckIfToLogAboutPotentialThreadAlert(string secretCode)
        {
            if (this.Pda.GetNumOfMentionsBySecretCode(secretCode) >= 20 &&
                this.Pda.ChaeckAndReturnTheType(secretCode) < 4)
            {
                Logger.CreateLog($"{secretCode} Is Potential Thread Alert.");
            }
        }
    }
}
