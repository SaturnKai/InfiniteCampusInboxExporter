using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

using InfiniteCampusInboxExporter.Utils;
using InfiniteCampusInboxExporter.InfiniteCampus;
using InfiniteCampusInboxExporter.InfiniteCampus.Data;

namespace InfiniteCampusInboxExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Infinite Campus Inbox Exporter V1.0 - Created By: SaturnKai";
            PrintLabel("OPTION", ConsoleColor.Yellow);
            Console.WriteLine("Please Enter in Your State Code (Example: 'CA' = California, 'NV' = Nevada)");
            Console.Write(" > ");
            string stateCode = Console.ReadLine();
            PrintLabel("OPTION", ConsoleColor.Yellow);
            Console.WriteLine("Please Enter in Your District Name (Example: 'ccsd' = Clark County School District)");
            Console.Write(" > ");
            string districtName = Console.ReadLine();
            DistrictQuery districtQuery = DistrictQuery.QueryDistricts(stateCode, districtName);
            if (districtQuery.data == null || districtQuery.data.Count<1)
            {
                PrintLabel("ERROR", ConsoleColor.Red);
                Console.WriteLine("No Districts Found.\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
            District currentDistrict = new District();
            PrintLabel("OPTION", ConsoleColor.Yellow);
            Console.WriteLine("Please Select Your Correct School District\n");
            int districtIndex = 0;
            foreach (District district in districtQuery.data)
            {
                districtIndex++;
                PrintLabel(districtIndex.ToString(), ConsoleColor.Magenta);
                Console.WriteLine(district.district_name);
            }
            Console.Write("\n > ");
            try
            {
                int option = int.Parse(Console.ReadLine());
                currentDistrict = districtQuery.data[option - 1];
            }
            catch
            {
                PrintLabel("ERROR", ConsoleColor.Red);
                Console.WriteLine("Invalid Option.\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
            PrintLabel("SUCCESS", ConsoleColor.Green);
            Console.WriteLine("Successfully Selected District: " + currentDistrict.district_name);
            PrintLabel("OPTION", ConsoleColor.Yellow);
            Console.WriteLine("Please Enter in Your Infinite Campus Username: ");
            Console.Write(" > ");
            string username = Console.ReadLine();
            PrintLabel("OPTION", ConsoleColor.Yellow);
            Console.WriteLine("Please Enter in Your Infinite Campus Password: ");
            Console.Write(" > ");
            string password = ReadPassword();
            User currentUser = Auth.Login(currentDistrict, username, password);
            if (currentUser.username == null)
            {
                PrintLabel("ERROR", ConsoleColor.Red);
                Console.WriteLine("Invalid Username or Password.\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
            PrintLabel("SUCCESS", ConsoleColor.Green);
            Console.WriteLine("Successfully Authenticated Account " + currentUser.username + "!");
            List<Message> inboxMessages = JsonConvert.DeserializeObject<List<Message>>(Inbox.GetInbox(currentDistrict, currentUser));
            PrintLabel("INFO", ConsoleColor.Cyan);
            Console.WriteLine("Exporting Infinite Campus Inbox...");
            if (!Directory.Exists("Inbox Archive")) Directory.CreateDirectory("Inbox Archive");
            int messageCount = 0;
            foreach (Message message in inboxMessages)
            {
                if (!(Directory.Exists("Inbox Archive/" + message.date))) Directory.CreateDirectory("Inbox Archive/" + message.date);
                //TODO: Make this look cleaner and work better. Preferably with Regex.
                string messageName = message.name.Replace('\\', '-');
                messageName = messageName.Replace('/', '-');
                messageName = messageName.Replace(':', '-');
                messageName = messageName.Replace('*', '-');
                messageName = messageName.Replace('?', '\0');
                messageName = messageName.Replace('"', '\'');
                messageName = messageName.Replace('<', '-');
                messageName = messageName.Replace('>', '-');
                messageName = messageName.Replace('|', '-');
                if (!(SaveMessage(currentDistrict, currentUser, message, "Inbox Archive/" + message.date + "/" + messageName + ".html"))) Console.WriteLine("ERROR: Failed to Export Message: " + message.name + "!");
                else
                {
                    messageCount++;
                    if (messageCount==1) Console.Title = "Infinite Campus Exporter | 1 Message Exported";
                    else Console.Title = "Infinite Campus Exporter | " + messageCount + " Messages Exported";
                }
            }
            PrintLabel("SUCCESS", ConsoleColor.Green);
            if (messageCount == 1) Console.WriteLine("Successfully Exported 1 Message From the Infinite Campus Inbox!");
            else Console.WriteLine("Successfully Exported " + messageCount + " Messages From the Infinite Campus Inbox!");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        static bool SaveMessage(District district, User user, Message message, string outputDirectory)
        {
            try
            {
                File.WriteAllText(outputDirectory, HTTP.Get(district.district_baseurl + message.url, ref user.sessionCookies));
                return true;
            }
            catch { return false; };
        }

        static void PrintLabel(string label, ConsoleColor color)
        {
            Console.Write("\n [");
            Console.ForegroundColor = color;
            Console.Write(label);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
        }

        static string ReadPassword()
        {
            //Huge thanks to https://stackoverflow.com/questions/3404421/password-masking-console-application for this function. Edited slightly.
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                } else
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }
    }
}