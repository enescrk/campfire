using System;
using System.Collections.Generic;

namespace camp_fire.Application.Configurations.Helpers
{
    public static class GenerateCodeHelper
    {
        public static string GenerateCode(int lenght = 4, bool OnlyInt = false)
        {
            List<string> strArray = new List<string>
            {"0","1","2","3","4","5","6","7","8","9"};
            if (!OnlyInt)
            {
                List<string> stringList = new List<string>{
                    "A", "B", "C", "D", "E", "F", "G", "H",
                    "I", "J", "K", "L", "N", "O", "P", "Q",
                    "R", "S", "T", "U", "V", "X", "Y", "Z",
                    "a", "b", "c", "d", "e", "f", "g", "h",
                    "i", "j", "k", "l", "n", "o", "p", "q",
                    "r", "s", "t", "u", "v", "x", "y", "z"};

                strArray.AddRange(stringList);
            }
            Random random = new Random();
            string empty = string.Empty;
            for (int index = 0; index < lenght; ++index)
                empty += strArray[random.Next(strArray.Count - 1)];
            return empty;
        }
    }
}