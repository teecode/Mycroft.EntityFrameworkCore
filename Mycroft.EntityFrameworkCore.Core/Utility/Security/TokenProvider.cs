using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Core.Utility.Security
{
    public static class TokenProvider
    {

        public static string GetRandomCode(int codecount = 6, bool alphanumeric = true, bool isNumbersalone = false)
        {
            string allchars = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            if(!alphanumeric)
                allchars = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            if (isNumbersalone)
                allchars = "0,1,2,3,4,5,6,7,8,9,";
           return CreateRandomCode(codecount, allchars);
       
        }

        private static string CreateRandomCode(int codeCount, string allChar)
        {
          
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(codeCount, allChar);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions();
                       string[] randomChars = new[] {
                        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                        "abcdefghijkmnopqrstuvwxyz",    // lowercase
                        "0123456789",                   // digits
                        "!@$?_-"                        // non-alphanumeric
                    };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
