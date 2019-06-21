using System.Collections.Generic;

namespace PassportValidation
{
    public interface IChecksum
    {
        bool PerformChecksum(IEnumerable<int> digitsToCheck, int checksum);
        int GetIndexInAlphabet(char value);
    }
}