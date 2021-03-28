using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class AccountManager
    {
        #region Fields

        private static List<UserAccount> _accounts;

        #endregion


        #region Methods
        public static bool AddAccount(UserAccount account)
        {
            if (_accounts.Find(acc => acc.Login == account.Login) == null)
            {
                _accounts.Add(account);
                return true;
            }
            return false;
        }

        public static void RemoveAccount(UserAccount account) 
        {
         
        }
    
        #endregion

    }
}
