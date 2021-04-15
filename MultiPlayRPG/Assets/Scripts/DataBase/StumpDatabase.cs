using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DatabaseStump
{
    public class StumpDatabase
    {
        #region PrivateData

        private struct User
        {
            public string Username;
            public string Password;
            public string Data;
        }

        #endregion


        #region Fields

        private List<User> _users;

        private const string _fileName = "database.xml";
        private const string _folderName = "database";

        private const string _tempPath = "D:\\UnityProjects\\Unity5Course\\MultiPlayRPG\\temp";

        private string _path;

        #endregion


        #region ClassLifeCycles

        public StumpDatabase()
        {
            _users = new List<User>();
            //LoadDB();
        }

        #endregion


        #region Methods

        public string GetUserData(string username, string password)
        {
            string result = string.Empty;

            for (int i = 0; i < _users.Count; i++)
            {
                User current = _users[i];
                if (current.Username.Equals(username)  )
                {
                    if (current.Password.Equals(password))
                    {
                        result = current.Data;
                    }
                    break;
                }
            }

            return result;
        }

        public bool Login(string username, string password)
        {
            bool result = false;

            for (int i = 0; i < _users.Count; i++)
            {
                User current = _users[i];
                if (current.Username.Equals(username))
                {
                    if (current.Password.Equals(password))
                    {
                        result = true;
                    }
                    break;
                }
            }

            return result;
        }


        public bool RegistrUser(string username, string password, string data)
        {
            bool result = false;

            bool isUsernameExist = false;
            for (int i = 0; i < _users.Count; i++)
            {
                User current = _users[i];
                if (current.Username.Equals(username))
                {
                    isUsernameExist = true;
                }
            }

            if (!isUsernameExist)
            {
                _users.Add(new User() { Username = username, Password = password, Data = data });
                result = true;
            }

            return result;
        }

        public bool SetUserData(string username, string password, string data)
        {
            bool result = false;

            for (int i = 0; i < _users.Count; i++)
            {
                User current = _users[i];
                if (current.Username.Equals(username))
                {
                    if (current.Password.Equals(password))
                    {
                        current.Data = data;
                        _users[i] = current;
                        result = true;
                    }
                    break;
                }
            }

            return result;
        }

        private void LoadDB()
        {
            string source = LoadSource();
            if (source != null)
            {

            }
            else
            {
                _users = new List<User>();
            }
        }

        private string LoadSource()
        {
            if (_path == null)
            {
                CreatePath();
            }

            string source = null;

            if (File.Exists(_path))
            {
                using (StreamReader sr = File.OpenText(_path))
                {
                    source = sr.ReadToEnd();
                }
            }

            return source;
        }

        private void CreatePath()
        {
            //string directoryPath = Path.Combine(Application.persistentDataPath, _folderName);
            string directoryPath = _tempPath;
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            _path = Path.Combine(directoryPath, _fileName);
        }

        private void SaveDB()
        {

        }


        #endregion
    }
}
