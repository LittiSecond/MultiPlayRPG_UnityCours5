using System;
using System.Collections.Generic;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class AccountManager
    {
        #region Fields

        private static List<UserAccount> _accounts = new List<UserAccount>();

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
            _accounts.Remove(account);
        }

        public static UserAccount GetAccount(NetworkConnection conn)
        {
            return _accounts.Find(acc => acc.Connection == conn);
        }

        #endregion

    }
}
