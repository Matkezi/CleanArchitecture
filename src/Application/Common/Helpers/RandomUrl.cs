using System;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Helpers
{
    public class RandomUrl
    {
        // List of characters and numbers to be used...  
        private static List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private static List<char> characters = new List<char>()
    {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
    'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B',
    'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
    'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};

        public static string GetRandomUrl()
        {
            string url = "";
            Random rand = new Random();
            // run the loop till I get a string of 16 characters  
            for (int i = 0; i < 17; i++)
            {
                // Get random numbers, to get either a character or a number...  
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    // use a number  
                    random = rand.Next(0, numbers.Count);
                    url += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    url += characters[random].ToString();
                }
            }
            return url;
        }
    }

}
