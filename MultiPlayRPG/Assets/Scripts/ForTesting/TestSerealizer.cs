using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace MultiPlayRPG.Testing
{
    public class TestSerealizer : MonoBehaviour
    {

        private UserData _userData;
        private const string _tempPath = "D:\\UnityProjects\\Unity5Course\\MultiPlayRPG\\temp";
        private const string _fileName = "database.txt";
        private string _path;

        public void TestUserDataXml()
        {
            if (_userData == null)
            {
                CreateTestUserData();
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserData));
            StringWriter writer = new StringWriter();
            xmlSerializer.Serialize(writer, _userData);
            string str = writer.ToString();

            if (!Directory.Exists(_tempPath))
            {
                Directory.CreateDirectory(_tempPath);
            }
            _path = Path.Combine(_tempPath, _fileName);

            using (StreamWriter sr = new StreamWriter(_path))
            {
                sr.Write(str);
            }
        }

        private void CreateTestUserData()
        {
            _userData = new UserData();

            _userData.CharacterPos = new Vector3(1, 2, 3);
            _userData.Inventory = new List<int>();
            _userData.Inventory.Add(4);
            _userData.Inventory.Add(5);
            _userData.Equipment = new List<int>();
            _userData.Equipment.Add(6);
            _userData.Equipment.Add(7);
            _userData.Equipment.Add(8);
            _userData.Expa = 9;
            _userData.NextLevelExp = 10;
            _userData.Level = 11;
            _userData.StatPoints = 12;
            _userData.CurrentHealth = 13;
            _userData.StatDamage = 14;
            _userData.StatArmor = 15;
            _userData.StatMoveSpeed = 16;
        }

    }
}