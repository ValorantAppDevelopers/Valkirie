using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Valkirie.Client.Utilities
{
    public class UserConfig
    {
        [XmlRoot(ElementName = "UserConfig")]
        public class UserConfigXml
        {
            [XmlElement(ElementName = "Username")]
            public string Username { get; set; }
            [XmlElement(ElementName = "Password")]
            public string Password { get; set; }
            [XmlElement(ElementName = "Region")]
            public string Region { get; set; }
            [XmlElement(ElementName = "RememberMe")]
            public bool RememberMe { get; set; }
        }

        private static string XmlPath = @".\UserConfig.xml";

        public static UserConfigXml GetUserConfig()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserConfigXml));
            using (XmlReader reader = XmlReader.Create(XmlPath))
            {
                var settings = (UserConfigXml)serializer.Deserialize(reader);
                return settings;
            }
        }

        public static void SaveUserConfig(UserConfigXml userConfig)
        {
            XmlSerializer xml = new XmlSerializer(typeof(UserConfigXml));
            TextWriter writer = new StreamWriter(XmlPath);
            xml.Serialize(writer, userConfig);
        }

        public static void CheckUserConfigFile()
        {
            if (File.Exists(XmlPath))
                return;

            File.Create(XmlPath).Close();

            SaveUserConfig(new UserConfigXml()
            {
                Username = "null",
                Password = "null",
                Region = "null",
                RememberMe = false
            });
        }
    }
}
