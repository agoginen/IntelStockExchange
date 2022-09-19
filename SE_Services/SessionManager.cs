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

        public int AddUser(string userName)
        {
            lock (syncRoot)
            {
                if (ActiveUsers.Count(entry => entry.Value.Item1 == userName) == 0)
                {
                    int id = 0;
                    ActiveUsers.Add(id, new Tuple<string, DateTime>(userName, DateTime.Now));
                    return id;
                }
                else
                {
                    // if user is already logged in return guid
                    ActiveUsers.RefreshUser(userName);
                    return ActiveUsers.Where(entry => entry.Value.Item1 == userName).First().Key;
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