using System;
using System.Collections.Generic;
using System.Linq;

namespace SE_Services
{
	public static class Extensions
    {
        // updates the user's time to live
        public static void RefreshUser(this Dictionary<int, Tuple<string, DateTime>> dict, string userName)
        {
            var id = dict.Where(e => e.Value.Item1 == userName).FirstOrDefault().Key;
            dict.RefreshUser(id);
        }

        public static void RefreshUser(this Dictionary<int, Tuple<string, DateTime>> dict, int id)
        {
            string userName = dict[id].Item1;
            dict[id] = new Tuple<string, DateTime>(userName, DateTime.Now);
        }
    }
}