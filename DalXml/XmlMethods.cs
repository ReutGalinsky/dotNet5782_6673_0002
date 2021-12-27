using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Dal
{
    public static class XmlMethods
    {
        public static void SaveToXml(string FileName, XElement root)
        {
            if (FileName != null)
                root.Save(FileName);
            else { }
            //
        }
        public static XElement LoadFromXml(string FileName)
        {
            try
            {

                XElement root = XElement.Load(FileName);
                return root;
            }
            catch (Exception)
            { //
                throw;
            }

        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}