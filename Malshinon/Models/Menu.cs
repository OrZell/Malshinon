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
            string reporterSecretCode = UserInputs.EnterSecretCode();
            if (!this.Pda.SearchByCodeName(reporterSecretCode))
            {
                string[] fullName = UserInputs.EnterYourFullname().Split();
                Person person = new Person(fullName[0], fullName[1], reporterSecretCode);
                this.Pda.AddPerson(person);
            }

            string Text = UserInputs.EnterYourReport();
            string targetSecretCode = ProcessText.ProcessTextFromReporter(Text);
            if (!this.Pda.SearchByCodeName(targetSecretCode))
            {
                string[] targetFullName = UserInputs.EnterTargetFullname().Split();
                Person person = new Person(targetFullName[0], targetFullName[1], targetSecretCode, 2);
                this.Pda.AddPerson(person);
            }

            int reporterID = this.Pda.FindIdBySecretCode(reporterSecretCode);
            int targetID = this.Pda.FindIdBySecretCode(targetSecretCode);

            Report report = new Report(reporterID, targetID, Text);
            this.Rda.AddReport(report);
            this.Pda.IncreaseReportsBySecretCode(reporterSecretCode);
            this.Pda.IncreaseMentionsBySecretCode(targetSecretCode);

            if (this.Pda.ChaeckAndReturnTheType(reporterSecretCode) < 3 && 
                this.Pda.GetNumOfReportsBySecretCode(reporterSecretCode) > 0 &&
                this.Pda.GetNumOfMentionsBySecretCode(reporterSecretCode) > 0)
            {
                this.Pda.ConvertToBoth(reporterSecretCode);
            }

            if (this.Pda.ChaeckAndReturnTheType(targetSecretCode) < 3 &&
                this.Pda.GetNumOfReportsBySecretCode(targetSecretCode) > 0 &&
                this.Pda.GetNumOfMentionsBySecretCode(targetSecretCode) > 0)
            {
                this.Pda.ConvertToBoth(targetSecretCode);
            }

            if (this.Pda.GetNumOfReportsBySecretCode(reporterSecretCode) >= 10 &&
                this.Pda.GetAllChartersBySecretCode(reporterSecretCode)/this.Pda.GetNumOfReportsBySecretCode(reporterSecretCode) >= 100)
            {
                this.Pda.ConvertToPotentialAgent(reporterSecretCode);
            }

            if (this.Pda.GetNumOfMentionsBySecretCode(targetSecretCode) >= 20 &&
                this.Pda.ChaeckAndReturnTheType(targetSecretCode) > 4)
            {
                this.Pda.ConvertToPotentialAgent(targetSecretCode);
            }

            Console.WriteLine("Proccessed");
        }
    }
}
