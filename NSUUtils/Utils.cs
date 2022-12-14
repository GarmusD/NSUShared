using System;
using System.Linq;
using System.Text;
using System.Globalization;

namespace NSU.Shared.NSUUtils
{
	public static class Utils
    {
        public static T GetStatusFromString<T>(string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;
            if (Enum.TryParse(value, true, out T result))
            {
                return result;
            }
            return defaultValue;
        }

        public static string GetFirstWord (string fromStr)
        {
            return fromStr.Split (' ') [0];
        }

        public static string GetSecondWord (string fromStr)
        {
            return fromStr.Split (' ') [1];
        }

        public static string FirstLetterToUpper(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Length > 1)
                return str.Substring(0, 1).ToUpper() + str.Substring(1);
            return str.ToUpper();
        }

        public static string ValidateName (string value)
        {
            if (value.Equals ("N")) {
                return string.Empty;
            }
            return value;
        }

        public static string ValidateNameForArduino (string value)
        {
            if (string.IsNullOrWhiteSpace (value)) {
                return "N";
            }
            return value;
        }

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

        public static bool NearlyEqual (float a, float b, float epsilon)
        {
            float absA = Math.Abs (a);
            float absB = Math.Abs (b);
            float diff = Math.Abs (a - b);

            if (a == b) { // shortcut, handles infinities
                return true;
            } else if (a == 0 || b == 0 || diff < float.Epsilon) {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < epsilon;
            } else { // use relative error
                return diff / Math.Min ((absA + absB), float.MaxValue) < epsilon;
            }
        }

        public static bool NearlyEqual (float a, float b)
        {
            return NearlyEqual (a, b, 0.00001f);
        }

        public static string GetHexString(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in value)
            {
                sb.Append("#");
                sb.Append(((byte)item).ToString());
            }
            return sb.ToString();
        }
	}
}

