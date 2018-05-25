using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace CSBooster.Language.ExportImport
{
    class Import
    {
        public bool DoMerge
        {
            get;
            set;
        }

        public bool DoCopy
        {
            get;
            set;
        }

        public Import()
        {
            this.DoMerge = false;
            this.DoCopy = false;
        }

        public void DoImport(string path)
        {
            if (!FixFile(path))
                throw new Exception("Bitte eine letzte Spalte mit denm Inhalt '|E|' speichern");

            if (DoCopy)
            {
                MergeFile(path);
            }
            //SortFile(path); 

            string fileLast = string.Empty;
            Dictionary<string, XmlDocument> languages = new Dictionary<string, XmlDocument>();
            Dictionary<int, string> keys = new Dictionary<int, string>();
            bool header = true;

            using (StreamReader sr = new StreamReader(Path.Combine(path, "_Sieme.Language.Excel.txt")))
            {
                string rec;
                while ((rec = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(rec))
                        continue;

                    string[] col = rec.Split('\t');
                    if (col.Length < 2)
                        continue;

                    for (int x = 0; x < col.Length; x++)
                    {
                        col[x] = col[x].Replace("|C|", Environment.NewLine);
                    }
                    if (header)
                    {
                        if (col.Length > 3)
                        {
                            for (int h = 3; h < col.Length; h++)
                            {
                                XmlDocument xmlLang = new XmlDocument();
                                xmlLang.AppendChild(xmlLang.CreateXmlDeclaration("1.0", "utf-8", null));
                                xmlLang.AppendChild(xmlLang.CreateElement("root"));

                                languages.Add(col[h], xmlLang);
                                keys.Add(h, col[h]);
                            }
                        }
                        header = false;
                        continue;
                    }

                    string file = col[0].Trim();
                    string name = col[1].Trim();
                    if (string.IsNullOrEmpty(file))
                        continue; 

                    if (fileLast != file)
                    {
                        foreach (KeyValuePair<string, XmlDocument> kvp in languages)
                        {
                            if (kvp.Value.DocumentElement.ChildNodes.Count > 0)
                            {
                                string target = Path.Combine(path, string.Format("{0}.{1}.lang", fileLast, kvp.Key));
                                if (DoMerge)
                                {
                                    CopyNodes(target, kvp.Value);  
                                }
                                else
                                {
                                    kvp.Value.Save(target);
                                }

                                kvp.Value.DocumentElement.RemoveAll();
                            }
                        }
                        fileLast = file;
                    }

                    foreach (KeyValuePair<int, string> key in keys)
                    {
                        if (key.Key < col.Length)
                        {
                            XmlDocument xmlTemp = languages[key.Value];
                            XmlElement xmlData = xmlTemp.DocumentElement.AppendChild(xmlTemp.CreateElement("data")) as XmlElement;
                            xmlData.SetAttribute("name", name);
                            XmlNode xmlValue = xmlData.AppendChild(xmlTemp.CreateElement("value"));
                            xmlValue.InnerXml = string.Format("<![CDATA[{0}]]>", col[key.Key].Trim().TrimStart('"').TrimEnd('"').Replace("\"\"", "\""));
                        }
                    }
                }

            }

            foreach (KeyValuePair<string, XmlDocument> kvp in languages)
            {
                if (kvp.Value.DocumentElement.ChildNodes.Count > 0)
                {
                    string target = Path.Combine(path, string.Format("{0}.{1}.lang", fileLast, kvp.Key));
                    if (DoMerge)
                    {
                        CopyNodes(target, kvp.Value);  
                    }
                    else
                    {
                        kvp.Value.Save(target);
                    }
                    kvp.Value.DocumentElement.RemoveAll();
                }
            }
        }

        private void CopyNodes(string source, XmlDocument xmlTarget)
        {
            XmlDocument xmlSouce = new XmlDocument();
            xmlSouce.Load(source);
            foreach (XmlElement trgNode in xmlTarget.SelectNodes("root/data"))
            {
                XmlNodeList srcNodes = xmlSouce.SelectNodes(string.Format("root/data[@name='{0}']", trgNode.GetAttribute("name")));
                foreach (XmlNode scrNode in srcNodes)
                {
                    scrNode.InnerXml = trgNode.InnerXml;   
                }
            }
            xmlSouce.Save(source);  
        }


        private bool FixFile(string path)
        {
            string content;
            using (StreamReader sr = new StreamReader(Path.Combine(path, "_Sieme.Language.Excel.txt")))
            {
                content = sr.ReadToEnd().Replace("\t|E|" + Environment.NewLine, "|E|");
            }

            if (content.IndexOf("|E|") < 0)
                return false;

            content = content.Replace(Environment.NewLine, "|C|");
            content = content.Replace("\n", "|C|");
            content = content.Replace("\r", "|C|");
            content = content.Replace("|E|", Environment.NewLine);

            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "_Sieme.Language.Excel.txt"), false, Encoding.UTF8))
            {
                sw.Write(content);
            }

            return true;
        }

        private void MergeFile(string path)
        {
            bool header = true;
            string lastFile = "xxxx";
            List<string> languages = new List<string>();

            using (StreamReader sr = new StreamReader(Path.Combine(path, "_Sieme.Language.Excel.txt")))
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
                        for (int c = 3; c < col.Length; c++)
                        {
                            languages.Add(col[c]);
                        }
                        header = false;
                        continue;
                    }

                    string file = col[0].Trim();
                    if (string.IsNullOrEmpty(file))
                        continue; 

                    if (lastFile != file)
                    {
                        foreach (string lang in languages)
                        {
                            string source = Path.Combine(path, file + ".lang");
                            string target = Path.Combine(path, file + "." + lang + ".lang");
                            if (DoMerge)
                                File.Copy(source, target, true);
                            else
                                File.Delete(target);  
                        }
                        lastFile = file;
                    }
                }
            }
        }

        private void SortFile(string path)
        {
            bool header = true;
            string headerRec = string.Empty;
            SortedList<string, string> list = new SortedList<string, string>(1000);

            using (StreamReader sr = new StreamReader(Path.Combine(path, "_Sieme.Language.Excel.txt")))
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
                        headerRec = rec;
                        header = false;
                        continue;
                    }

                    string file = col[0].Trim();
                    string name = col[1].Trim();

                    string key = file + "|" + name;
                    int c = 0;
                    while (list.ContainsKey(key))
                    {
                        c++;
                        key += c.ToString();
                    }

                    list.Add(key, rec);
                }

            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "_Sieme.Language.Excel.txt"), false, Encoding.UTF8))
            {
                sw.WriteLine(headerRec);
                foreach (KeyValuePair<string, string> kvp in list)
                {
                    sw.WriteLine(kvp.Value);
                }
            }

        }
    }
}
