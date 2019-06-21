using System;
using System.Collections.Generic;
using System.Linq;

namespace PassportValidation
{
    public class Checksum : IChecksum
    {
        public bool PerformChecksum(IEnumerable<int> digitsToCheck, int checksum)
        {
            IEnumerable<int> toCheck = digitsToCheck as int[] ?? digitsToCheck.ToArray();

            return checksum == toCheck.Select((x, d) => d % 3 == 0 ? 7 * x : d % 3 == 1 ? 3 * x : x).Sum() % 10;
        }

        public int GetIndexInAlphabet(char value)
        {
            // Uses the uppercase character unicode code point. 'A' = U+0042 = 65, 'Z' = U+005A = 90
            char upper = char.ToUpper(value);
            if (upper < 'A' || upper > 'Z')
            {
                throw new ArgumentOutOfRangeException("value", "This method only accepts standard Latin characters.");
            }

            return (int) upper - ((int) 'A' - 10);
        }
    }
}