using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using DatabaseControl;

namespace MultiPlayRPG
{
    public class UserAccount
    {
        #region Fields

        public string Login;
        public string Pass;
        public UserData Data;
        public NetworkConnection Connection;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        public UserAccount(NetworkConnection conn)
        {
            Connection = conn;
        }

        #endregion


        #region Methods

        private IEnumerator SaveData()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserData));
            StringWriter writer = new StringWriter();
            xmlSerializer.Serialize(writer, Data);
            IEnumerator e = DCF.SetUserData(Login, Pass, writer.ToString());

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            string response = e.Current as string;
            if (response == "Success")
            {
                Debug.Log("UserData for user " + Login + " completely save.");
            }
            else
            {
                Debug.LogError("UserData for user " + Login + " save error with code: " + response);
            }
        }

        private IEnumerator LoadData()
        {
            IEnumerator e = DCF.GetUserData(Login, Pass);

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            string response = e.Current as string;
            if (response == "Error")
            {
                Debug.LogError("UserData for user " + Login + " load error with code: " + response);
            }
            else
            {
                Debug.Log("UserData for user " + Login + " completely load.");
                Debug.Log(response);
                if (response != "")
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserData));
                    Data = (UserData)xmlSerializer.Deserialize(new StringReader(response));
                }
                else
                {
                    Data = new UserData();
                }
            }
        }

        public IEnumerator LoginTo(string login, string pass)
        {
            IEnumerator eLogin = DCF.Login(login, pass);
            while (eLogin.MoveNext())
            {
                yield return eLogin.Current;
            }
            string response = eLogin.Current as string;

            if (response == "Success")
            {
                Debug.Log("server login success");
                Login = login;
                Pass = pass;
                if (AccountManager.AddAccount(this))
                {
                    IEnumerator eLoad = LoadData();
                    while (eLoad.MoveNext())
                    {
                        yield return eLoad.Current;
                    }
                    response = eLoad.Current as string;
                    if (response == "Error") yield return eLoad.Current;
                    else yield return eLogin.Current;
                }
                else
                {
                    Debug.Log("account already use");
                    yield return "Already use";
                }
            }
            else
            {
                Debug.Log("server login fail");
                yield return eLogin.Current;
            }
        }

        public IEnumerator Quit()
        {
            IEnumerator eSave = SaveData();
            while (eSave.MoveNext())
            {
                yield return eSave.Current;
            }
            AccountManager.RemoveAccount(this);
        }


        #endregion

    }
}
