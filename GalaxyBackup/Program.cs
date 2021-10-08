using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchestrA.GRAccess;
using System.Diagnostics;

namespace GalaxyBackup
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter Galaxy Name to Backup: ");
            string galaxyName = Console.ReadLine();

            Console.WriteLine("Name of backup -- c:/AVEVA/XXXXX.CAB");
            string backupName = Console.ReadLine();
            string fullLocation = "C:/AVEVA/" + backupName + ".cab";

            Console.WriteLine("Username: ");
            string userName = Console.ReadLine();

            Console.WriteLine("Password: ");

            string password = null;

            while (true)
            {
                ConsoleKeyInfo key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
            }

            Console.WriteLine("");

            GRAccessApp grAccess = new GRAccessApp();
            GalaxyConn gConn = new GalaxyConn(galaxyName, userName, password);

            gConn.Connect(grAccess);
            int processId = Process.GetProcessesByName("aaGR")[0].Id;

            Console.WriteLine();
            Console.WriteLine("Backing up galaxy");

            gConn.Galaxy.Backup(processId, fullLocation, Environment.MachineName, galaxyName);

            Console.WriteLine("");
            Console.WriteLine("FINISHED");
            Console.ReadLine();
        }
    }
}
