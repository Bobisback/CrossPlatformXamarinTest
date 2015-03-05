using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PhonewordSharedCode
{
    static class PhoneTranslator {
        public static string ToNumber(string raw) {
            if (string.IsNullOrWhiteSpace(raw))
                return "Error: Null or Whitespace";
            else
                raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();
            foreach (var c in raw) {
                if (" -0123456789".Contains(c))
                    newNumber.Append(c);
                else {
                    var result = TranslateToNumber(c);
                    if (result != null)
                        newNumber.Append(result);
                }
                // otherwise we've skipped a non-numeric char
            }

			if (!validatePhoneword(newNumber.ToString())) {
				return "Error: Validation Failed";
			}

            return newNumber.ToString();
        }

		static bool validatePhoneword(string raw) {
			Regex phonewordValidation = new Regex(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$");

			return phonewordValidation.IsMatch(raw);
		}

        static bool Contains(this string keyString, char c) {
            return keyString.IndexOf(c) >= 0;
        }

        static int? TranslateToNumber(char c) {
            if ("ABC".Contains(c))
                return 2;
            else if ("DEF".Contains(c))
                return 3;
            else if ("GHI".Contains(c))
                return 4;
            else if ("JKL".Contains(c))
                return 5;
            else if ("MNO".Contains(c))
                return 6;
            else if ("PQRS".Contains(c))
                return 7;
            else if ("TUV".Contains(c))
                return 8;
            else if ("WXYZ".Contains(c))
                return 9;
            return null;
        }
    }
}
