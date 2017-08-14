using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Xml;
using LogInterface;

namespace LogTxt
{
    class XmlFileLog : ILog
    {
        public bool Write(string message)
        {
            string xmlFilePath = ConfigurationManager.AppSettings["LogTarget"].ToString();
            if (File.Exists(xmlFilePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                XmlNode nod = doc.SelectSingleNode("Logs");
                docFrag.InnerXml = "" + DateTime.Now.ToLocalTime().ToString()
                + "" + message + "";
                nod.AppendChild(docFrag);

                doc.Save(xmlFilePath);
                return true;
            }
            else
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;     //设置缩进
                settings.ConformanceLevel = ConformanceLevel.Auto;
                settings.IndentChars = " ";
                settings.OmitXmlDeclaration = false;
                using (XmlWriter writer = XmlWriter.Create(xmlFilePath, settings))
                {
                    //Start writing the XML document
                    writer.WriteStartDocument(false);
                    //Start with the root element
                    writer.WriteStartElement("Logs");
                    writer.WriteStartElement("Log");
                    writer.WriteStartElement("Time");
                    writer.WriteString(DateTime.Now.ToLocalTime().ToString());
                    writer.WriteEndElement();
                    writer.WriteStartElement("Message");
                    writer.WriteString(message);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    //Flush the object and write the XML data to the file
                    writer.Flush();
                    return true;
                }

            }
        }
        public bool Write(Exception ex)
        {
            Write(ex.Message);
            return true;
        }

    }
}
