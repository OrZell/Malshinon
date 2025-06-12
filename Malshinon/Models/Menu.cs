using Malshinon.DAL;
using Malshinon.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Malshinon.Models
{
    public class Menu
    {
        private PeopleDAL Pda;
        private ReportDAL Rda;
        private AlertDAL Ada;
        public Menu(MySQLServer sql)
        {
            this.Pda = new PeopleDAL(sql);
            this.Rda = new ReportDAL(sql);
            this.Ada = new AlertDAL(sql);
        }
        public void MainMenu()
        {
            bool sign = true;
            do
            {
                Console.WriteLine("Please Choice:\n" +
                    "1. Enter Report\n" +
                    "2. Import CSV File\n" +
                    "3. Display Al The Potential Agents\n" +
                    "4. Display All The Dangerous People\n" +
                    "5. Display All The Alerts\n" +
                    "6. Exit");
                string userInput = Console.ReadLine()!;
                switch (userInput)
                {
                    case "1":
                        EnterReport();
                        break;

                    case "2":
                        ImportExternalCSV();
                        break;

                    case "3":
                        PrintAllAgents();
                        break;

                    case "4":
                        PrintAllDangerous();
                        break;

                    case "5":
                        PrintAllAlerts();
                        break;

                    case "6":
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

        public void EnterReport()
        {
            string reporterSecretCode = UserInputs.EnterYourSecretCode();
            CheckIfSecretCodeExist(reporterSecretCode);

            string targetSecretCode = UserInputs.EnterTargetSecretCode();
            CheckIfSecretCodeExist(targetSecretCode, 2, false);

            string Text = UserInputs.EnterYourReport();

            ChecksAndIncreases(reporterSecretCode, targetSecretCode, Text);
        }

        public void ImportExternalCSV()
        {
            string path = UserInputs.EnterPath();

            List<string[]> Data = ImportCSV.ProcessTheCSVToLists(path);
            if (!ImportCSV.ValidateTheData(Data))
            {
                throw new Exception("Invalid Data");
            }

            Data = ImportCSV.CorrectTheData(Data);

            foreach (string[] line in Data)
            {
                CheckIfSecretCodeExistForCSVFile(line[0], line[1], line[2]);
                CheckIfSecretCodeExistForCSVFile(line[3], line[4], line[5]);

                AddReportAndIncrease(line[0], line[3], line[6]);

            }
        }

        public void CheckIfSecretCodeExistForCSVFile(string secretCode, string firstName, string lastName)
        {
            if (!this.Pda.SearchByCodeName(secretCode))
            {
                if (firstName == "" || lastName == "")
                {
                    throw new Exception("Data Not Complete");
                }

                Person person = new Person(firstName, lastName, secretCode);
                this.Pda.AddPerson(person);
                Logger.CreateLog($"New Person Created {secretCode}");
            }
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
                this.Pda.UpdateToDangerous(secretCode);
            }
        }

        public void ChecksAndIncreases(string reporterSecretCode, string targetSecretCode, string text)
        {
            AddReportAndIncrease(reporterSecretCode, targetSecretCode, text);

            CheckIfToChangeTheStatusToBoth(reporterSecretCode);
            CheckIfToChangeTheStatusToBoth(targetSecretCode);

            CheckIfToChangeTheStatusToPotentialAgent(reporterSecretCode);
            CheckIfToLogAboutPotentialThreadAlert(targetSecretCode);

            CheckIfToAddAlert(targetSecretCode);
        }

        public void CheckIfToAddAlert(string secretCode)
        {
            DateTime currentDT = DateTime.Now;
            DateTime pastDT = GenerateTime.SubtractDateTime(currentDT, 15);

            if (this.Pda.CheckMentionsInTime(secretCode, pastDT, currentDT) >= 3)
            {
                string timeWindow = GenerateTime.GenerateTimeWindow(pastDT, currentDT);
                string reason = "Rapid reports detected";
                this.Ada.AddAlert(secretCode, timeWindow, reason);
                Logger.CreateLog($"{currentDT} Alert Created About {secretCode}");
            }
        }

        public void PrintAllAgents()
        {
            List<Person> Agents = this.Pda.AllPotentialAgents();
            if (Agents.Count < 1)
            {
                Console.WriteLine("There Are Not Any Of");
            }
            else
            {
                foreach (Person Agent in Agents)
                {
                    Console.WriteLine(Agent.Print());
                }
            }
        }

        public void PrintAllDangerous()
        {
            List<Person> Dangerous = this.Pda.AllPotentialDangerous();
            if (Dangerous.Count < 1)
            {
                Console.WriteLine("There Are Not Any Of");
            }
            else
            {
                foreach (Person Danger in Dangerous)
                {
                    Console.WriteLine(Danger.Print());
                }
            }
        }

        public void PrintAllAlerts()
        {

            List<Alert> Alerts = this.Ada.AllAlerts();
            if (Alerts.Count < 1)
            {
                Console.WriteLine("There Are Not Any Of");
            }
            else
            {
                foreach (Alert Alert in Alerts)
                {
                    Console.WriteLine(Alert.Print());
                }
            }
        }
    }
}
