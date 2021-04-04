using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStump
{
    public class StumpDatabaseNet
    {
        #region Fields

        //private static Lazy<StumpDatabase> _database = new Lazy<StumpDatabase>();

        #endregion


        #region Methods

        public static IEnumerator GetUserData(string username, string password)
        {
            yield return string.Empty;
        }

        public static IEnumerator Login(string username, string password)
        {
            yield return string.Empty;

        }

        public static IEnumerator RegisterUser(string username, string password, string data)
        {
            yield return string.Empty;

        }

        public static IEnumerator SetUserData(string username, string password, string data)
        {
            yield return string.Empty;

        }

        #endregion
    }
}
