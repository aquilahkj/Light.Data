using System;
using System.Text;

namespace Light.Data
{
	public class DateTimeFormater
	{
		string yearFormat = "yyyy";

		string monthFormat = "MM";

		string dayFormat = "dd";

		string hourFormat = "HH";

		string minuteFormat = "mm";

		string secondFormat = "ss";

		public string YearFormat {
			get {
				return yearFormat;
			}

			set {
				if (!string.IsNullOrEmpty (value))
					yearFormat = value;
			}
		}

		public string MonthFormat {
			get {
				return monthFormat;
			}

			set {
				if (!string.IsNullOrEmpty (value))
					monthFormat = value;
			}
		}

		public string DayFormat {
			get {
				return dayFormat;
			}

			set {
				if (!string.IsNullOrEmpty (value))
					dayFormat = value;
			}
		}

		public string HourFormat {
			get {
				return hourFormat;
			}

			set {
				if (!string.IsNullOrEmpty (value))
					hourFormat = value;
			}
		}

		public string MinuteFormat {
			get {
				return minuteFormat;
			}

			set {
				if (!string.IsNullOrEmpty (value))
					minuteFormat = value;
			}
		}

		public string SecondFormat {
			get {
				return secondFormat;
			}

			set {
				if (!string.IsNullOrEmpty (value))
					secondFormat = value;
			}
		}

		public string FormatData (string format)
		{
			StringBuilder sb = new StringBuilder ();
			char [] chars = format.ToCharArray ();
			int len = chars.Length;
			int i = 0;
			while (i < len) {
				char c = chars [i];
				if (c == 'y') {
					int ret = ParseValue (chars, i, 4, 'y');
					if (ret > 0) {
						i += ret;
						sb.Append (yearFormat);
						continue;
					}
				}
				else if (c == 'M') {
					int ret = ParseValue (chars, i, 2, 'M');
					if (ret > 0) {
						i += ret;
						sb.Append (monthFormat);
						continue;
					}
				}
				else if (c == 'd') {
					int ret = ParseValue (chars, i, 2, 'd');
					if (ret > 0) {
						i += ret;
						sb.Append (dayFormat);
						continue;
					}
				}
				else if (c == 'H') {
					int ret = ParseValue (chars, i, 2, 'H');
					if (ret > 0) {
						i += ret;
						sb.Append (hourFormat);
						continue;
					}
				}
				else if (c == 'm') {
					int ret = ParseValue (chars, i, 2, 'm');
					if (ret > 0) {
						i += ret;
						sb.Append (minuteFormat);
						continue;
					}
				}
				else if (c == 's') {
					int ret = ParseValue (chars, i, 2, 's');
					if (ret > 0) {
						i += ret;
						sb.Append (secondFormat);
						continue;
					}
				}
				sb.Append (c);
				i++;
			}
			return sb.ToString ();
		}

		int ParseValue (char [] chars, int i, int max, char c)
		{
			if (chars.Length < i + max) {
				return 0;
			}
			for (int j = i; j < i + max; j++) {
				if (chars [j] != c) {
					return 0;
				}
			}
			return max;
		}

		//int ParseYear (char [] chars, int i, out string value)
		//{
		//	value = null;
		//	if (chars.Length < i + 4) {
		//		return 0;
		//	}
		//	for (int j = i; j < i + 4; j++) {
		//		if (chars [j] != 'y') {
		//			return 0;
		//		}
		//	}
		//	value = yearFormat;
		//	return 4;
		//}

		//int ParseMonth (char [] chars, int i, out string value)
		//{
		//	value = null;
		//	if (chars.Length < i + 2) {
		//		return 0;
		//	}
		//	for (int j = i; j < i + 2; j++) {
		//		if (chars [j] != 'M') {
		//			return 0;
		//		}
		//	}
		//	value = yearFormat;
		//	return 2;
		//}

		//int ParseDay (char [] chars, int i, out string value)
		//{
		//	value = null;
		//	if (chars.Length < i + 2) {
		//		return 0;
		//	}
		//	for (int j = i; j < i + 2; j++) {
		//		if (chars [j] != 'd') {
		//			return 0;
		//		}
		//	}
		//	value = yearFormat;
		//	return 2;
		//}

		//int ParseHour (char [] chars, int i, out string value)
		//{
		//	throw new NotImplementedException ();
		//}

		//int ParseMinute (char [] chars, int i, out string value)
		//{
		//	throw new NotImplementedException ();
		//}

		//int ParseSecond (char [] chars, int i, out string value)
		//{
		//	throw new NotImplementedException ();
		//}
	}
}

