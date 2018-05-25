// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using _4screen.Utils.Web;

namespace _4screen.CSB.Common
{
    public static class Extensions
    {
        #region Dropdownlist Extensions

        public static void SelectItem(this DropDownList Ddl, int Value)
        {
            Ddl.SelectItem(Value.ToString());
        }

        public static void SelectItem(this DropDownList Ddl, string Value)
        {
            try
            {
                if (Ddl.Items.FindByValue(Value) != null)
                    Ddl.SelectedIndex = Ddl.Items.IndexOf(Ddl.Items.FindByValue(Value));
                else if (Ddl.Items.Count > 0)
                    Ddl.SelectedIndex = 0;
            }
            catch
            {
                Ddl.SelectedIndex = -1;
            }
        }

        #endregion

        #region String Extensions

        /// <summary>
        /// Convert string 'hhh:mm:ss' to a valid TimeSpan
        /// </summary>
        /// <param name="time">'[hhh:][mm:][ss]'</param>
        /// <returns>TimeSpan or 'Default '24:00:00'</returns>
        public static TimeSpan ToTimeSpan(this string time)
        {
            string[] strTime = time.Split(':');
            if (strTime.Length == 1)
                return new TimeSpan(Convert.ToInt32(strTime[0]), 0, 0);
            else if (strTime.Length == 2)
                return new TimeSpan(Convert.ToInt32(strTime[0]), Convert.ToInt32(strTime[1]), 0);
            if (strTime.Length == 3)
                return new TimeSpan(Convert.ToInt32(strTime[0]), Convert.ToInt32(strTime[1]), Convert.ToInt32(strTime[2]));
            else
                return new TimeSpan(0, 0, 0);
        }

        public static double ToDouble(this string value, double defaultValue)
        {
            double ret;
            if (double.TryParse(value, out ret))
                return ret;
            else
                return defaultValue;
        }

        public static bool ToDouble(this string value, bool defaultValue)
        {
            bool ret;
            if (bool.TryParse(value, out ret))
                return ret;
            else if (value == "1" || value.ToLower() == "true")
                return true;
            else if (value == "0" || value.ToLower() == "false")
                return false;
            else
                return defaultValue;
        }

        public static int ToSqlBit(this bool value)
        {
            if (value == true)
                return 1;
            else
                return 0;
        }

        public static int ToInt32(this string value, int defaultValue)
        {
            int ret;
            if (int.TryParse(value, out ret))
                return ret;
            else
                return defaultValue;
        }

        public static bool IsEven(this int value)
        {
            return ((value & 1) == 0);
        }

        public static bool IsOdd(this int value)
        {
            return ((value & 1) == 1);
        }


        public static string ToFirstLower(this string value)
        {
            if (value.Length > 1)
                return value.Substring(0, 1).ToLower() + value.Substring(1);
            else
                return value;
        }

        public static string ToFirstUpper(this string value)
        {
            if (value.Length > 1)
                return value.Substring(0, 1).ToUpper() + value.Substring(1);
            else
                return value;
        }

        public static string GetLocalizedUrl(string relativeUrl)
        {
            string returnUrl = relativeUrl;
            bool addVPath = false;
            try
            {
                bool.TryParse(ConfigurationManager.AppSettings["UseVirtualLanguagePath"], out addVPath);
            }
            catch
            {

            }
            if (addVPath)
            {
                bool continueSearch = true;
                string languageCodeToAdd = string.Format("/{0}", CultureHandler.GetCurrentSpecificLanguageCode());
                // /someUrl.aspx                      ==> /CURRENTLANGGUAGECODE/someUrl.aspx
                // /someUrl.aspx?LG=LangCode          ==> /LangCode/someUrl.aspx
                // /someUrl.aspx?LG=langCode&Var=val  ==> /LangCode/someUrl.aspx?var=val
                // /someUrl.aspx?Var=val&LG=langCode  ==> /LangCode/someUrl.aspx?var=val
                // /LangCode/someUrl.aspx?Var=val     ==> /LangCode/someUrl.aspx?var=

                int firstIndexOfSlash = returnUrl.IndexOf("/");
                int lastIndexOfSlash = returnUrl.LastIndexOf("/");

                if (firstIndexOfSlash < lastIndexOfSlash)
                {
                    string[] urlParts = returnUrl.Split('/');
                    try
                    {
                        //Is the first part a Culture
                        CultureInfo cu = new CultureInfo(urlParts[1]);
                        languageCodeToAdd = string.Empty;
                        continueSearch = false;
                    }
                    catch (Exception)
                    {
                        //Do nothing.. just continue       
                    }

                }
                if (continueSearch)
                {
                    if (returnUrl.IndexOf('?') > 0)
                    {
                        var currentUrl = returnUrl.Split('?');
                        string queryString = currentUrl[1];
                        string queryNoLanguage = string.Empty;
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString.Replace("&amp;", "§§tempAmp§§");
                            string[] queryParams = queryString.Split('&');
                            foreach (string queryParam in queryParams)
                            {
                                var keyValuePair = queryParam.Split('=');
                                if (string.Compare(keyValuePair[0], "LG", true) != 0)
                                {
                                    queryNoLanguage += string.Format("{0}&", queryParam.Replace("§§tempAmp§§", "&amp;"));
                                }
                                else
                                {
                                    try
                                    {
                                        CultureInfo cu = new CultureInfo(keyValuePair[1]);
                                        languageCodeToAdd = string.Format("/{0}", keyValuePair[1]);
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                            if (queryNoLanguage.Length > 0)
                                queryNoLanguage = string.Format("?{0}", queryNoLanguage.TrimEnd('&'));
                        }
                        returnUrl = string.Format("{0}{1}", currentUrl[0], queryNoLanguage);
                    }
                }
                returnUrl = string.Format("{0}{1}", languageCodeToAdd, returnUrl);
            }
            return returnUrl;
        }

        public static string ToLocalizedLink(this string value)
        {
            return GetLocalizedUrl(value);
        }

        public static string Unescape(this string value)
        {
            return value.Replace("&amp;#00;", "æ").Replace("&amp;#20;", "&nbsp;").Replace("&amp;#x21;", "!").Replace("%22;", "\\\"").Replace("&amp;#x23;", "#").Replace("&amp;#x24;", "$").Replace("&amp;#x25;", "%").Replace("&amp;#x26;", "&amp;").Replace("&amp;#x27;", "\\'").Replace("&amp;#x28;", "(").Replace("&amp;#x29;", ")").Replace("&amp;#x2a;", "*").Replace("&amp;#x2b;", "+").Replace("&amp;#x2c;", ",").Replace("&amp;#x2d;", "-").Replace("&amp;#x2e;", ".").Replace("&amp;#x2f;", "/").Replace("&amp;#x3a;", ":").Replace("&amp;#x3b;", ";").Replace("&amp;#x3c;", "&lt;").Replace("&amp;#x3d;", "=").Replace("&amp;#x3e;", "&gt;").Replace("&amp;#x3f;", "?").Replace("&amp;#x40;", "@").Replace("&amp;#x5b;", "[").Replace("&amp;#x5c;", "\\").Replace("&amp;#x5d;", "]").Replace("&amp;#x5e;", "^").Replace("&amp;#x5f;", "_").Replace("&amp;#x60;", "`").Replace("&amp;#x7b;", "{").Replace("&amp;#x7c;", "|").Replace("&amp;#x7d;", "}").Replace("&amp;#x7e;", "~").Replace("&amp;#x80;", "€").Replace("&amp;#x82;", "‚").Replace("&amp;#x83;", "ƒ").Replace("&amp;#x84;", "„").Replace("&amp;#x85;", "…").Replace("&amp;#x86;", "†").Replace("&amp;#x87;", "‡").Replace("&amp;#x88;", "ˆ").Replace("&amp;#x89;", "‰").Replace("&amp;#x8a;", "Š").Replace("&amp;#x8b;", "‹").Replace("&amp;#x8c;", "Œ").Replace("&amp;#x8e;", "Ž").Replace("&amp;#x91;", "‘").Replace("&amp;#x92;", "’").Replace("&amp;#x93;", "“").Replace("&amp;#x94;", "”").Replace("&amp;#x95;", "•").Replace("&amp;#x96;", "–").Replace("&amp;#x98;", " ").Replace("&amp;#x99;", "™").Replace("&amp;#x9a;", "š").Replace("&amp;#x9b;", "›").Replace("&amp;#x9c;", "œ").Replace("&amp;#x9e;", "ž").Replace("&amp;#x9f;", "Ÿ").Replace("&amp;#xa0;", "&nbsp;").Replace("&amp;#xa1;", "¡").Replace("&amp;#xa2;", "¢").Replace("&amp;#xa3;", "£").Replace("&amp;#xa5;", "¥").Replace("&amp;#xa6;", "|").Replace("&amp;#xa7;", "§").Replace("&amp;#xa8;", "¨").Replace("&amp;#xa9;", "©").Replace("&amp;#xaa;", "ª").Replace("&amp;#xab;", "«").Replace("&amp;#xac;", "¬").Replace("&amp;#xad;", "¯").Replace("&amp;#xae;", "®").Replace("&amp;#xaf;", "¯").Replace("&amp;#xb0;", "°").Replace("&amp;#xb1;", "±").Replace("&amp;#xb2;", "²").Replace("&amp;#xb3;", "³").Replace("&amp;#xb4;", "´").Replace("&amp;#xb5;", "µ").Replace("&amp;#xb6;", "¶").Replace("&amp;#xb7;", "·").Replace("&amp;#xb8;", "¸").Replace("&amp;#xb9;", "¹").Replace("&amp;#xba;", "º").Replace("&amp;#xbb;", "»").Replace("&amp;#xbc;", "¼").Replace("&amp;#xbd;", "½").Replace("&amp;#xbe;", "¾").Replace("&amp;#xbf;", "¿").Replace("&amp;#xc0;", "À").Replace("&amp;#xc1;", "Á").Replace("&amp;#xc2;", "Â").Replace("&amp;#xc3;", "Ã").Replace("&amp;#xc4;", "Ä").Replace("&amp;#xc5;", "Å").Replace("&amp;#xc6;", "Æ").Replace("&amp;#xc7;", "Ç").Replace("&amp;#xc8;", "È").Replace("&amp;#xc9;", "É").Replace("&amp;#xca;", "Ê").Replace("&amp;#xcb;", "Ë").Replace("&amp;#xcc;", "Ì").Replace("&amp;#xcd;", "Í").Replace("&amp;#xce;", "Î").Replace("&amp;#xcf;", "Ï").Replace("&amp;#xd0;", "Ð").Replace("&amp;#xd1;", "Ñ").Replace("&amp;#xd2;", "Ò").Replace("&amp;#xd3;", "Ó").Replace("&amp;#xd4;", "Ô").Replace("&amp;#xd5;", "Õ").Replace("&amp;#xd6;", "Ö").Replace("&amp;#xd8;", "Ø").Replace("&amp;#xd9;", "Ù").Replace("&amp;#xda;", "Ú").Replace("&amp;#xdb;", "Û").Replace("&amp;#xdc;", "Ü").Replace("&amp;#xdd;", "Ý").Replace("&amp;#xde;", "Þ").Replace("&amp;#xdf;", "ß").Replace("&amp;#xe0;", "à").Replace("&amp;#xe1;", "á").Replace("&amp;#xe2;", "â").Replace("&amp;#xe3;", "ã").Replace("&amp;#xe4;", "ä").Replace("&amp;#xe5;", "å").Replace("&amp;#xe6;", "æ").Replace("&amp;#xe7;", "ç").Replace("&amp;#xe8;", "è").Replace("&amp;#xe9;", "é").Replace("&amp;#xea;", "ê").Replace("&amp;#xeb;", "ë").Replace("&amp;#xec;", "ì").Replace("&amp;#xed;", "í").Replace("&amp;#xee;", "î").Replace("&amp;#xef;", "ï").Replace("&amp;#xf0;", "ð").Replace("&amp;#xf1;", "ñ").Replace("&amp;#xf2;", "ò").Replace("&amp;#xf3;", "ó").Replace("&amp;#xf4;", "ô").Replace("&amp;#xf5;", "õ").Replace("&amp;#xf6;", "ö").Replace("&amp;#xf7;", "÷").Replace("&amp;#xf8;", "ø").Replace("&amp;#xf9;", "ù").Replace("&amp;#xfa;", "ú").Replace("&amp;#xfb;", "û").Replace("&amp;#xfc;", "ü").Replace("&amp;#xfd;", "ý").Replace("&amp;#xfe;", "þ").Replace("&amp;#xff;", "ÿ");
        }

        public static string ToValidFileName(this string fileName)
        {
            string valid = fileName.Trim();
            foreach (char invalidChar in System.IO.Path.GetInvalidFileNameChars())
            {
                valid = valid.Replace(invalidChar.ToString(), "");
            }
            return valid;
        }

        public static string ToValidPath(this string path)
        {
            string valid = path.Trim();
            foreach (char invalidChar in System.IO.Path.GetInvalidPathChars())
            {
                valid = valid.Replace(invalidChar.ToString(), "");
            }
            return valid;
        }

        public static string ToValidFilenameForWeb(this string fileName)
        {
            string myTitle = fileName.ToValidFileName();
            myTitle = myTitle.Replace("Ä", "Ae");
            myTitle = myTitle.Replace("Å", "A");
            myTitle = myTitle.Replace("â", "a");
            myTitle = myTitle.Replace("ä", "ae");
            myTitle = myTitle.Replace("á", "a");
            myTitle = myTitle.Replace("à", "a");
            myTitle = myTitle.Replace("å", "a");
            myTitle = myTitle.Replace("É", "E");
            myTitle = myTitle.Replace("È", "E");
            myTitle = myTitle.Replace("Ê", "E");
            myTitle = myTitle.Replace("Ë", "E");
            myTitle = myTitle.Replace("ê", "e");
            myTitle = myTitle.Replace("ë", "e");
            myTitle = myTitle.Replace("è", "e");
            myTitle = myTitle.Replace("é", "e");
            myTitle = myTitle.Replace("Ì", "I");
            myTitle = myTitle.Replace("Í", "I");
            myTitle = myTitle.Replace("Î", "I");
            myTitle = myTitle.Replace("Ï", "I");
            myTitle = myTitle.Replace("ï", "i");
            myTitle = myTitle.Replace("î", "i");
            myTitle = myTitle.Replace("ì", "i");
            myTitle = myTitle.Replace("í", "i");
            myTitle = myTitle.Replace("ô", "o");
            myTitle = myTitle.Replace("ö", "oe");
            myTitle = myTitle.Replace("ò", "o");
            myTitle = myTitle.Replace("ó", "o");
            myTitle = myTitle.Replace("ü", "ue");
            myTitle = myTitle.Replace("û", "uu");
            myTitle = myTitle.Replace("ù", "u");
            myTitle = myTitle.Replace("ú", "u");
            myTitle = myTitle.Replace("Ö", "Oe");
            myTitle = myTitle.Replace("Ò", "O");
            myTitle = myTitle.Replace("Ó", "O");
            myTitle = myTitle.Replace("Ô", "O");
            myTitle = myTitle.Replace("Õ", "O");
            myTitle = myTitle.Replace("Ü", "Ue");
            myTitle = myTitle.Replace("Ù", "U");
            myTitle = myTitle.Replace("Ú", "U");
            myTitle = myTitle.Replace("Û", "U");
            myTitle = myTitle.Replace("ÿ", "y");
            myTitle = myTitle.Replace("ñ", "n");
            myTitle = myTitle.Replace("Ñ", "NI");
            myTitle = myTitle.Replace("æ", "ae");
            myTitle = myTitle.Replace("Æ", "AE");
            myTitle = myTitle.Replace("Œ", "Oe");
            myTitle = myTitle.Replace("œ", "oe");
            myTitle = myTitle.Replace("ç", "c");
            myTitle = myTitle.Replace("Ç", "C");

            myTitle = myTitle.Replace("ø", "");
            myTitle = myTitle.Replace("£", "");
            myTitle = myTitle.Replace("Ø", "");
            myTitle = myTitle.Replace("×", "");
            myTitle = myTitle.Replace("ƒ", "");
            myTitle = myTitle.Replace("©", "");
            myTitle = myTitle.Replace("§", "");
            myTitle = myTitle.Replace("®", "");
            myTitle = myTitle.Replace("[", "");
            myTitle = myTitle.Replace("]", "");
            myTitle = myTitle.Replace("^", "");
            myTitle = myTitle.Replace("`", "");
            myTitle = myTitle.Replace("{", "");
            myTitle = myTitle.Replace("|", "");
            myTitle = myTitle.Replace("}", "");
            myTitle = myTitle.Replace("~", "");
            myTitle = myTitle.Replace("€", "");
            myTitle = myTitle.Replace("@", "");
            myTitle = myTitle.Replace("#", "");
            myTitle = myTitle.Replace("$", "");
            myTitle = myTitle.Replace("(", "");
            myTitle = myTitle.Replace(")", "");
            myTitle = myTitle.Replace("*", "");
            myTitle = myTitle.Replace(" ", "");
            myTitle = myTitle.Replace("&", "");
            myTitle = myTitle.Replace("?", "");
            myTitle = myTitle.Replace("%", "");
            myTitle = myTitle.Replace(",", "");
            myTitle = myTitle.Replace(";", "");
            myTitle = myTitle.Replace("!", "");
            myTitle = myTitle.Replace("+", "");
            myTitle = myTitle.Replace("-", "");
            return myTitle;
        }

        public static string CropString(this string value, int length)
        {
            if (value.Length > length && length > 3)
            {
                value = value.Substring(0, length - 3);
                value += "...";
            }
            return value;
        }


        public static string CutRight(this string value, int maxLength)
        {

            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
                return value.Substring(0, maxLength);
            else
                return value;
        }


        public static string CropString(this string value, int length, string separator)
        {
            string croppedString1 = value;
            string croppedString2 = string.Empty;
            if (value.Length > length)
            {
                croppedString1 = value.Substring(0, length);
                croppedString2 = value.Substring(length, value.Length - length);
                croppedString1 += croppedString2.Substring(0, Math.Max(0, croppedString2.IndexOf(separator)));
            }
            return croppedString1;
        }

        public static string CropStringConditional(this string value, int maxLength)
        {
            int indexOfSeparator = value.IndexOf(' ');
            if (indexOfSeparator > maxLength || indexOfSeparator == -1)
                value = value.CropString(maxLength);
            return value;
        }

        public static string StripForScript(this string value)
        {
            if (value != null)
                return value.Replace("'", @"\'").Replace("\r\n", "").Replace("\n", "");
            else
                return value;
        }

        public static string StripHTMLTags(this string value)
        {
            if (value != null)
                return Regex.Replace(value, "<.*?>", "");
            else
                return value;
        }

        public static string StripHTMLTags(this string value, List<string> acceptableTags)
        {
            //Dim AcceptableTags As String = "i|b|u|sup|sub|ol|ul|li|br|h2|h3|h4|h5|span|div|p|a|img|blockquote"
            string whiteListPattern = "</?(?(?=" + string.Join("|", acceptableTags.ToArray()) + @")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:([""']?).*?\1?)?)*\s*/?>";
            return Regex.Replace(value, whiteListPattern, "", RegexOptions.Compiled);
        }

        public static string RemoveDuplicates(this string inputString, char Delimiter)
        {
            string strOutpuString = string.Empty;
            //now that they are joined... make sure the words are not repeated... case insensitive
            StringDictionary strDict = new StringDictionary();
            string[] strAllWords = inputString.Split(Delimiter);
            for (int j = 0; j < strAllWords.Length; j++)
            {
                string strToAdd = strAllWords[j];
                if (strToAdd.Trim().Length > 0 && !strDict.ContainsKey(strToAdd.ToLower()))
                {
                    strDict.Add(strToAdd.ToLower(), strToAdd);
                    strOutpuString += strToAdd + Delimiter.ToString();
                }
            }
            strOutpuString = strOutpuString.Trim(Delimiter);
            return strOutpuString;
        }

        public static string RemoveDuplicates(this string inputString, string Delimiter)
        {
            if (Delimiter.Length == 1)
            {
                return inputString.RemoveDuplicates(Convert.ToChar(Delimiter));
            }
            else
            {
                //Replace the string delimiter with char. send it to the remove duplicate overload. and then replace the "char" delimiter with the "string" delimiter
                return inputString.Replace(Delimiter, "¦").RemoveDuplicates('¦').Replace("¦", Delimiter);
            }
        }

        public static Guid ToGuid(this string Value)
        {
            return new Guid(Value);
        }

        public static Guid? ToNullableGuid(this string Value)
        {
            if (string.IsNullOrEmpty(Value) || !Value.IsGuid())
                return null;
            else
                return new Guid(Value);
        }

        public static string PrepareLike(this string Value, bool AtBegin, bool AtEnd)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                Value = Value.Replace("'", "''");
                if (!Value.EndsWith("%") && AtEnd)
                {
                    if (Value.EndsWith("*"))
                        Value = string.Concat(Value.Substring(0, Value.Length - 1), "%");
                    else
                        Value = string.Concat(Value, "%");
                }
                if (!Value.StartsWith("%") && AtBegin)
                {
                    if (Value.StartsWith("*"))
                        Value = string.Concat("%", Value.Substring(1, Value.Length));
                    else
                        Value = string.Concat("%", Value);
                }
            }
            return Value.Trim();
        }

        public static bool IsGuid(this string candidate)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(candidate))
            {
                Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
                if (isGuid.IsMatch(candidate))
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        public static Color ToColor(this string hex)
        {
            try
            {
                return Color.FromArgb(int.Parse(hex.Substring(1, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(3, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(5, 2), NumberStyles.HexNumber));
            }
            catch
            { }
            return Color.Empty;
        }

        #endregion

        #region DateTime Extensions

        public static DateTime GetStartOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 12, 31, 23, 59, 59, 500);
        }

        public static DateTime GetStartOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month), 23, 59, 59, 500);
        }

        public static DateTime GetStartOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 500);
        }

        public static DateTime GetStartOfHour(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0, 0);
        }

        public static DateTime GetEndOfHour(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, 59, 59, 500);
        }

        public static DateTime TrimSecond(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, 0);
        }

        public static DateTime TrimMinute(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0, 0);
        }

        public static DateTime TrimHour(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
        }

        public static DateTime TrimDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime TrimMonth(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1, 0, 0, 0, 0);
        }



        #endregion

        #region EnumExtensions

        public static string GetText(this ObjectShowState objectShowState)
        {
            string retVal = string.Empty;
            switch (objectShowState)
            {
                case ObjectShowState.Blocked:
                    retVal = "Blockiert";
                    break;
                case ObjectShowState.Draft:
                    retVal = "Draft";
                    break;
                case ObjectShowState.InProgress:
                    retVal = "In Bearbeitung";
                    break;
                case ObjectShowState.ConversionFailed:
                    retVal = "Abgebrochen";
                    break;
                case ObjectShowState.Published:
                    retVal = "Publiziert";
                    break;
            }
            return retVal;
        }

        #endregion

        #region Request extensions

        public static string GetRawPath(this HttpRequest request)
        {
            return request.RawUrl.Contains("?") ? request.RawUrl.Substring(0, request.RawUrl.IndexOf('?')) : request.RawUrl;
        }

        #endregion

        public static IEnumerable<TSource> DistinctBy<TSource>(this IEnumerable<TSource> items, Func<TSource, TSource, bool> equalityComparer) where TSource : class
        {
            return items.Distinct(new LambdaComparer<TSource>(equalityComparer));
        }

        public static string ToHex(this Color color)
        {
            return string.Format("#{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
        }

        public static string GetNameWithoutExtension(this FileInfo fileInfo)
        {
            return fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));
        }

        public static bool ColumnExists(this SqlDataReader sqlDR, string columnName)
        {
            bool retVal = false;
            for (int i = 0; i < sqlDR.FieldCount; i++)
            {
                if (string.Compare(sqlDR.GetName(i), columnName, true) == 0)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }
    }
}