using SE_Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace SE_Services
{
	public class SessionManager
    {
        public Dictionary<int, Tuple<string, DateTime>> ActiveUsers { get; set; }
        private static volatile SessionManager instance;
        private static object syncRoot = new Object();
        private static Timer timer;
        private static int TimeoutMinutes = 10;

        private SessionManager()
        {
            int n = 1;

            ActiveUsers = new Dictionary<int, Tuple<string, DateTime>>();
            timer = new Timer(n * 60000); // n minute timeout checking
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            // if user spends more than x minutes without request, the system removes their session
            foreach (var item in ActiveUsers)
            {
                if ((DateTime.Now - item.Value.Item2).Minutes > TimeoutMinutes)
                {
                    ActiveUsers.Remove(item.Key);
                }
            }
        }

        public static SessionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SessionManager();
                        }
                    }
                }
                return instance;
            }
        }

        public void AddUser(UserViewModel user)
        {
            lock (syncRoot)
            {
                if (ActiveUsers.Count(entry => entry.Value.Item1 == user.UserName) == 0)
                {
                    int id = user.Id;
                    ActiveUsers.Add(id, new Tuple<string, DateTime>(user.UserName, DateTime.Now));
                }
                else
                {
                    // if user is already logged in return guid
                    ActiveUsers.RefreshUser(user.UserName);
                }
            }
        }

        // validates user's guid then refreshes user's ttl
        public bool ValidateUser(int id)
        {
            bool userIsValid = false;
            lock (syncRoot)
            {
                if (ActiveUsers.ContainsKey(id))
                {
                    userIsValid = true;
                    ActiveUsers.RefreshUser(id);
                }
            }
            return userIsValid;
        }

        public string GetUserNameByGuid(int id)
        {
            return ActiveUsers.Where(entry => entry.Key == id).FirstOrDefault().Value.Item1;
        }
    }
}