using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Globalization; 

namespace CSBooster.Language.ExportImport
{
    class Export
    {
        private Dictionary<string, string> externalText = null;

        public void DoExport(string path, List<string> languages, bool multiFiles, bool multiLanguage, bool defaultText)
        { 
            DirectoryInfo dir = new DirectoryInfo(path);

            LoadExternalText(Path.Combine(path, "_Sieme.ExternalText.txt"));   

            List<FileInfo> filesToExport = FilterBaseFile(dir.GetFiles("*.lang"));

            CreateCsvFile(languages, filesToExport, Path.Combine(path, "_Sieme.Language.Excel.txt"), defaultText);         
        }


        private List<FileInfo> FilterBaseFile(FileInfo[] files)
        {
            List<FileInfo> list = new List<FileInfo>();

            foreach (FileInfo file in files)
            { 
                int[] foundPos = FindAll(file.Name, ".");  
                    
                if (foundPos.Length > 1)
                { 
                    int len = foundPos[foundPos.Length - 1] - foundPos[foundPos.Length - 2] - 1;
                    string langCode = file.Name.Substring(foundPos[foundPos.Length - 2] + 1, len);
                    if (IsLangCode(langCode))
                        continue;
                }
                list.Add(file); 
            }
            return list;
        }

        private void CreateCsvFile(List<string> languages, List<FileInfo> files, string fileName, bool defaultText)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();

                //Titel erstellen
                sb.AppendFormat("File{0}Key{0}Text", "\t");
                foreach (string lang in languages)
                {
                    sb.AppendFormat("{0}{1}", "\t", lang);
                }
                sw.WriteLine(sb.ToString());

                foreach (FileInfo file in files)
                {
                    XmlDocument xmlLang = new XmlDocument();
                    xmlLang.Load(file.FullName);

                    Dictionary<string, XmlDocument> langDocs = new Dictionary<string, XmlDocument>();
                    foreach (string lang in languages)
                    {
                        string langFile = Path.Combine(file.DirectoryName, string.Format("{0}.{1}.lang", file.Name.Substring(0, file.Name.Length - 5), lang));
                        XmlDocument langDoc = new XmlDocument();
                        if (File.Exists(langFile))
                            langDoc.Load(langFile);

                        langDocs.Add(lang, langDoc);
                    }

                    foreach (XmlElement xmlData in xmlLang.SelectNodes("root/data"))
                    {
                        string name = xmlData.GetAttribute("name");
                        string text = GetNodeText(xmlData);
                        if (text.IndexOf("Mein Prof") > -1)
                            text = text;

                        sb = new StringBuilder(150);
                        sb.AppendFormat("{0}\t{1}\t{2}", file.Name.Substring(0, file.Name.Length - 5), name, text);
                        foreach (string lang in languages)
                        {
                            XmlNode xmlTemp = FindInDocument(langDocs[lang], name);
                            string tempText = GetNodeText(xmlTemp, name, null, null);
                            if (tempText.Length == 0 || tempText.IndexOf("|") == 3)
                                tempText = GetExternalText(lang, text);

                            if (!string.IsNullOrEmpty(tempText))
                            {
                                sb.AppendFormat("\t{0}", tempText);
                            }
                            else
                            {
                                if (defaultText)
                                {
                                    sb.AppendFormat("\t{0}", GetNodeText(xmlTemp, name, lang, text));
                                }
                                else
                                {
                                    sb.AppendFormat("\t{0}", GetNodeText(xmlTemp, name, null, null));
                                }
                            }
                        }
                        sw.WriteLine(sb.ToString());
                    }
                }
            }

        }

        private XmlNode FindInDocument(XmlDocument xmlDoc, string name)
        {
            return xmlDoc.SelectSingleNode(string.Format("root/data[@name='{0}']", name));   
        }

        private string GetNodeText(XmlNode xmlData)
        {
            return GetNodeText(xmlData, null, null, null);  
        }

        private string GetNodeText(XmlNode xmlData, string key, string prefix, string defaultText)
        {
            string text = string.Empty;
            if (xmlData != null)
            {
                XmlNode xmlText = xmlData.SelectSingleNode("value");
                if (xmlText != null && xmlText.FirstChild != null)
                {
                    if (xmlText.FirstChild.NodeType == XmlNodeType.CDATA)
                    {
                        if (string.IsNullOrEmpty(xmlText.FirstChild.InnerXml))
                            text = xmlText.FirstChild.Value;
                        else
                            text = xmlText.FirstChild.InnerXml;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(xmlText.FirstChild.InnerXml))
                            text = xmlText.FirstChild.Value;
                        else
                            text = xmlText.FirstChild.InnerXml;
                    }

                    if (text.Length > 3 && string.IsNullOrEmpty(prefix) && text.IndexOf("|") == 3)
                    {
                        text = string.Empty;
                    }
                }
            }
            if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(defaultText))
            {
                string extText = GetExternalText(prefix, key);
                if (string.IsNullOrEmpty(extText))
                    return string.Format("{0} | {1}", prefix, defaultText.Replace(Environment.NewLine, "<br/>").Replace("\t", string.Empty));
                else
                    return extText.Replace(Environment.NewLine, "<br/>").Replace("\t", string.Empty);
            }
            else
                return text.Replace(Environment.NewLine, "<br/>").Replace("\t", string.Empty);
        }

        private bool IsLangCode(string langCode)
        {
            try
            {
                CultureInfo cu = new CultureInfo(langCode);
                return true; 
            }
            catch
            {
                return false;
            }
        }


        private int[] FindAll(string searchedString, string matchString)
        {
            int foundPos = -1;
            int count = 0;
            int startPos = 0;
            List<int> foundItems = new List<int>();
            do
            {
                foundPos = searchedString.IndexOf(matchString, startPos);
                if (foundPos > -1)
                {
                    startPos = foundPos + 1;
                    count++;
                    foundItems.Add(foundPos);
                }
            } while (foundPos > -1 && startPos < searchedString.Length);

            return ((int[])foundItems.ToArray());
        }

        private string GetExternalText(string language, string key)
        {
            if (externalText.ContainsKey(string.Format("{0}|{1}", language, key.ToLower())))
                return externalText[string.Format("{0}|{1}", language, key.ToLower())];
            else
                return null;
        }

        private void LoadExternalText(string file)
        {
            externalText = new Dictionary<string, string>();
            if (File.Exists(file))
            {
                bool header = true;
                Dictionary<int, string> columns = new Dictionary<int, string>();
    
                using (StreamReader sr = new StreamReader(file))
                {
                    string rec;
                    while ((rec = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(rec))
                            continue;

                        string[] col = rec.Split('\t');
                        if (col.Length < 2)
                            continue;

                        if (header)
                        {
                            if (col.Length > 1)
                            {
                                for (int h = 1; h < col.Length; h++)
                                {
                                    columns.Add(h, col[h]); 
                                }
                            }
                            header = false;
                            continue;
                        }

                        for (int h = 1; h < col.Length; h++)
                        {
                            if (!externalText.ContainsKey(string.Format("{0}|{1}", columns[h], col[0].ToLower())))  
                                externalText.Add(string.Format("{0}|{1}", columns[h], col[0].ToLower()), col[h]);

                            if (!externalText.ContainsKey(string.Format("{0}|*{1}", columns[h], col[0].ToLower())))
                                externalText.Add(string.Format("{0}|*{1}", columns[h], col[0].ToLower()), col[h]);    

                        }


                    }
                }
            }

        }

    }
}
