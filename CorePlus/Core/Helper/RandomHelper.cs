using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class RandomHelper
    {
        private static char[] all =   
        {   
            '0','1','2','3','4','5','6','7','8','9',  
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            '~','!','@','#','$','%','^','&','*','(',')','?','<','>','_','+','='
        };

        private static char[] numbers = 
        {
            '0','1','2','3','4','5','6','7','8','9'
        };

        private static char[] letters = 
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        public static string Number(int length = 6)
        {
            return Rdm(numbers, length);
        }

        public static string Letter(int length = 6)
        {
            return Rdm(letters, length);
        }

        public static string All(int length = 6)
        {
            return Rdm(all, length);
        }

        private static string Rdm(char[] source, int length = 6)
        {
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(source[rd.Next(length)]);
            }

            return sb.ToString();
        }
    }
}