using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosixKernel.Modules
{
    class UC
    {
        private static int perml = 0;
        public static int permlevel
        {
            get { return perml; }
            set { Elevate(value); }
        }
        public static string username {
            get { return un; }
            set { Login(value); }
        }
        private static string un = "root";
        public static List<User> Users = new List<User> {new User("root",""),new User("user",""),new User("testpwd","test")};
        private static void Login(string usern)
        {
            string pwd = "";
            foreach (var user in Users)
            {
                if (user.CheckPassword(pwd))
                {
                    Terminal.WriteLine("No PW");
                    un = usern;
                    return;
                }
            }
            Terminal.Write("Password for " + usern + ": ");
            pwd = Terminal.ReadLine();
            foreach (var user in Users)
            {
                if (user.CheckPassword(pwd))
                {
                    un = usern;
                    return;
                }
            }
        }
        
        public static void UserShell()
        {
            try
            {
                Cash.Cash.Shell();
            }
            catch (Exception e)
            {
                UserExceptionHandler(e);
            }
        }
        public static void UserExceptionHandler(Exception e)
        {
            Terminal.WriteLine("User Mode " + e.ToString());
        }
        public static void Elevate()
        {
            Terminal.WriteLine("A program in user mode is asking for elevation. Elevate to perm level " + (perml + 1).ToString() + "? (Y/N) ");
            var yn = Console.ReadKey().KeyChar.ToString();
            if (yn == "y")
            {
                perml++;
            }
        }
        private static void Elevate(int elev)
        {
            Terminal.WriteLine("A program in user mode is asking for elevation. Elevate to perm level " + elev.ToString() + "? (Y/N) ");
            var yn = Console.ReadKey().KeyChar.ToString();
            if (yn == "y")
            {
                perml = elev;
            }
        }
        public class User
        {
            public string Username { get; }
            private string Password { get; }
            public User(string username, string password)
            {
                Username = username;
                Password = password;
            }
            public bool CheckPassword(string password)
            {
                if (password == Password)
                {
                    return true;
                }
                return false;
            }
        }
        public static void SaveUserSettings()
        {
            Users.ToString();
        }
    }
}
