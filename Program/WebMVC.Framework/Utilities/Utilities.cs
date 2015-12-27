using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebMVC.Framework.Utilities
{
    public static class TextUtility
    {
        private static readonly string[] vietnameseSigns =
        {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public static string RemoveSign4VietnameseString(this string str)
        {
            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                    str = str.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static string MakeAlias(string str)
        {
            string removeSignString = RemoveSign4VietnameseString(str).Trim().ToLower();
            removeSignString = Regex.Replace(removeSignString, @"[^\w\d]", "-");
            return removeSignString;
        }

        /// <summary>
        /// Chuyển string sang mã hóa md5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string HtmlRemove(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        public static string TrimSpace(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return Regex.Replace(text, "\\s", string.Empty);
        }

        public static string BreakString(string original, int position)
        {
            if (string.IsNullOrEmpty(original))
                return string.Empty;

            if (position >= original.Length)
                return original;

            return original.Insert(position, " ");
        }
    }

    public class ConvertNumberUtility
    {
        public static bool ToBool(object value, bool defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ToInt(object value, int defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long ToLong(object value, long defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static float ToFloat(object value, float defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToSingle(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double ToDouble(object value, double defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static decimal ToDecimal(object value, decimal defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string ToString(object value, string defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ToBool(object value)
        {
            return ToBool(value, false);
        }

        public static int ToInt(object value)
        {
            return ToInt(value, 0);
        }

        public static long ToLong(object value)
        {
            return ToLong(value, 0);
        }

        public static float ToFloat(object value)
        {
            return ToFloat(value, 0);
        }

        public static double ToDouble(object value)
        {
            return ToDouble(value, 0);
        }

        public static decimal ToDecimal(object value)
        {
            return ToDecimal(value, 0);
        }

        public static string ToString(object value)
        {
            return ToString(value, "");
        }
    }

    public class FileUtility
    {
        public static List<string> ReadFileText(string filePath)
        {
            var listStr = new List<string>();
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    listStr.Add(line.Trim());
                }
            }

            return listStr;
        }

        public static bool ReadAndEditFile(string strPath, string content)
        {
            string path = strPath;

            try
            {
                Byte[] info =
             new UTF8Encoding(true).GetBytes(content);

                // Add some information to the file.

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    System.IO.File.WriteAllBytes(path, info);
                }
                else
                {
                    System.IO.File.WriteAllBytes(path, info);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}