using System;
using System.Text;

namespace scorepager_desktop.Classes {
	static class CommonTools {
		public static string GenerateString(int length) {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--) res.Append(valid[rnd.Next(valid.Length)]);
            return res.ToString();
        }

        public static long DateTimeToUnixTimstamp(DateTime dt, int extraSeconds = 0) {
            return DateTimeOffset.Now.ToUnixTimeSeconds() + extraSeconds;
        }

        public static DateTime UnixTimestampToDateTime(long timestamp) {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(timestamp);
            return dt;
        }
	}
}
