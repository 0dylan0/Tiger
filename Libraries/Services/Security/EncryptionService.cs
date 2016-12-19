using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public class EncryptionService
    {
        private const string encryptionPrivateKey = "273ece6f97dd844d";
        private static readonly string[] CRS_MATRIX = {
            "8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"",
            "x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8",
            "3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x",
            "p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3",
            "5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p",
            "BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5",
            "eabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5B",
            "abcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Be",
            "bcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Bea",
            "cdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beab",
            "dfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabc",
            "fghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcd",
            "ghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdf",
            "hijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfg",
            "ijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfgh",
            "jklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghi",
            "klmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghij",
            "lmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijk",
            "mnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijkl",
            "noqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklm",
            "oqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmn",
            "qrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmno",
            "rstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoq",
            "stuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqr",
            "tuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqrs",
            "uvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqrst",
            "vwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqrstu",
            "wyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqrstuv",
            "yzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqrstuvw",
            "zACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqrstuvwy",
            "ACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5Beabcdfghijklmnoqrstuvwyz",
            "CDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzA",
            "DEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzAC",
            "EFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACD",
            "FGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDE",
            "GHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEF",
            "HIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFG",
            "IJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGH",
            "JKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHI",
            "KLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJ",
            "LMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJK",
            "MNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKL",
            "NOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLM",
            "OPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMN",
            "PQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNO",
            "QRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOP",
            "RSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQ",
            "STUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQR",
            "TUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRS",
            "UVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRST",
            "VWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTU",
            "WXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUV",
            "XYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVW",
            "YZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWX",
            "Z 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXY",
            " 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ",
            "1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ ",
            "246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1",
            "46790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 12",
            "6790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 124",
            "790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246",
            "90-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 12467",
            "0-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 124679",
            "-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790",
            ".#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-",
            "#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.",
            "/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#",
            "\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/",
            "!@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\",
            "@$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!",
            "$<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@",
            "<>&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$",
            ">&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<",
            "&*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>",
            "*()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&",
            "()[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*",
            ")[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*(",
            "[]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()",
            "]{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[",
            "{}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]",
            "}';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{",
            "';:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}",
            ";:,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}'",
            ":,?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';",
            ",?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:",
            "?=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,",
            "=+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?",
            "+~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=",
            "~`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+",
            "`^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~",
            "^|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`",
            "|%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^",
            "%_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|",
            "_\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%",
            "\r\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_",
            "\n\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r",
            "\"8x3p5BeabcdfghijklmnoqrstuvwyzACDEFGHIJKLMNOPQRSTUVWXYZ 1246790-.#/\\!@$<>&*()[]{}';:,?=+~`^|%_\r\n"
        };
        private static readonly string CRS_BASE_KEY = CRS_MATRIX[0];
        private const string CRS_SALT = "SHIJI";
        private const int CRS_SALT_LENGTH = 5;

        public static string EncryptCRSPassword(string password)
        {
            password = password ?? String.Empty;
            char[] encryptedPassword = new char[password.Length];

            for (int i = 0; i < password.Length; i++)
            {
                var currentLetter = password[i];
                var currentLetterIndex = CRS_BASE_KEY.IndexOf(currentLetter);
                var currentSalt = CRS_SALT[i % CRS_SALT_LENGTH];

                foreach (var m in CRS_MATRIX)
                {
                    if (m[currentLetterIndex] == currentSalt)
                    {
                        encryptedPassword[i] = m[0];
                        break;
                    }
                }
            }

            return new String(encryptedPassword);
        }
    }
}
