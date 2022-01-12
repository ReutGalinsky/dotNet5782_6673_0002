using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Dal
{
    public static class XmlMethods
    {
        #region SaveXelement
        /// <summary>
        /// function that save an Xelement in xml file
        /// </summary>
        /// <param name="FileName">the name of the file we want to save</param>
        /// <param name="root">the main element</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SaveToXml(string FileName, XElement root)
        {
            if (FileName != null)
                root.Save(FileName);
            else 
            {
                throw new DO.AccessToDataBaseException("Can't save the Xelement to the file");
            }
        }
        #endregion

        #region LoadXelement
        /// <summary>
        /// load the main element from xml file
        /// </summary>
        /// <param name="FileName">the file name</param>
        /// <returns></returns>

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static XElement LoadFromXml(string FileName)
        {
            try
            {
                XElement root = XElement.Load(FileName);
                return root;
            }
            catch (Exception)
            {
                throw new DO.AccessToDataBaseException("Can't load the Xelement from the file");
            }

        }
        #endregion

        #region LoadList
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">the type of the list elements</typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>),new XmlRootAttribute("Array"));
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception )
            {
                throw new DO.AccessToDataBaseException("Can't load the list from the file");
            }
        }
        #endregion

        #region SaveList
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">the type of the list elements</typeparam>
        /// <param name="list">the list to save</param>
        /// <param name="filePath">the file name</param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType(), new XmlRootAttribute("Array"));
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception)
            {
                throw new DO.AccessToDataBaseException("Can't save the list to the file");
            }
        }
        #endregion
    }
}