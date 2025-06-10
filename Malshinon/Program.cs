using Malshinon.Models;
using Malshinon.MySQL;

namespace Malshinon
{
    public class Program
    {
        static void Main(string[] args)
        {
            MySQLServer sql = new MySQLServer();
            Menu menu = new Menu(sql);
            menu.MainMenu();
        }
    }
}