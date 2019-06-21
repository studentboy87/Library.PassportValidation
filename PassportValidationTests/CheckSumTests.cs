using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PassportValidation;
using Xunit;

namespace PassportValidationTests
{
    public class ChecksumTests
    {
        private static IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection().AddScoped<IChecksum, Checksum>().BuildServiceProvider();
        }

        /*
        * the example given on high programmer
        * L898902C<3UTO6908061F9406236ZE184226B<<<<<14
        * L898902C< 3 UTO 690806 1 F 940623 6 ZE184226B<<<<< 1 4
        * 21,8,9,8,9,0,2,12,0,
        * 3,
        *
        * 30,29,24,
        *
        * 6,9,0,8,0,6,
        * 1,
        *
        * 15,
        *
        * 9,4,0,6,2,3,
        * 6,
        *
        * 35,14,1,8,4,2,2,6,11,0,0,0,0,0,
        * 1,
        * 4
        */

        #region TestingVariables

        #region PassingValues

        private static readonly Array HPPassportNumbers = new int[] {21, 8, 9, 8, 9, 0, 2, 12, 0,};
        private const int HPPassportNumberChecksum = 3;

        private static readonly Array HPNationalityNumbers = new int[] {30, 29, 24};

        private static readonly Array HPDateOfBirthNumbers = new int[] {6, 9, 0, 8, 0, 6};

        private const int HPDOBChecksum = 1;

        private const int HPSexNumber = 15;

        private static readonly Array HPDateOfExpirationNumbers = new int[] {9, 4, 0, 6, 2, 3};

        private const int HPExpirationChecksum = 6;

        private static readonly Array HPPersonalNumbers = new int[] {35, 14, 1, 8, 4, 2, 2, 6, 11, 0, 0, 0, 0, 0};

        private const int HPPersonalChecksum = 1;

        private static readonly Array HPFinalChecksumValues = new int[]
        {
            21, 8, 9, 8, 9, 0, 2, 12, 0, 3, 6, 9, 0, 8, 0, 6, 1, 9, 4, 0, 6, 2, 3, 6, 35, 14, 1, 8, 4, 2, 2, 6, 11,
            0, 0, 0, 0, 0, 1
        };

        private const int HPFinalChecksum = 4;

        #endregion

        #region SadPathValues

        private static readonly Array SPPassportNumbersSetOne = new int[] {22, 8, 9, 8, 9, 0, 2, 12, 2,};
        private const int SPPassportNumberChecksumSetOne = 2;

        private static readonly Array SPNationalityNumbersSetOne = new int[] {30, 29, 24};

        private static readonly Array SPDateOfBirthNumbersSetOne = new int[] {6, 9, 0, 8, 0, 6};

        private const int SPDOBChecksumSetOne = 1;

        private const int SPSexNumberSetOne = 15;

        private static readonly Array SPDateOfExpirationNumbersSetOne = new int[] {9, 4, 0, 6, 2, 3};

        private const int SPExpirationChecksumSetOne = 6;

        private static readonly Array SPPersonalNumbersSetOne = new int[] {35, 14, 1, 8, 4, 2, 2, 6, 11, 0, 0, 0, 0, 0};

        private const int SPPersonalChecksumSetOne = 1;
        private const int SPFinalChecksumSetOne = 4;

        #endregion

        #endregion

        [Fact]
        public void Correct_Digits_Give_Correct_Checksum()
        {
            var digitsToCheck = new int[] {10, 11, 2, 1, 3, 4, 0, 0, 0};

            var expectedChecksum = 5;
            var serviceProvider = BuildServiceProvider();
            var checksum = serviceProvider.GetService<IChecksum>();
            var result = checksum.PerformChecksum(digitsToCheck, expectedChecksum);
            Assert.True(result);
        }

        [Fact]
        public void Checksum_PassportNumber_Validation()
        {
            var expectedChecksum = 5;
            var digitsToCheck = new int[] {10, 11, 2, 1, 3, 4, 0, 0, 0};

            var serviceProvider = BuildServiceProvider();
            var checksum = serviceProvider.GetService<IChecksum>();
            var result = checksum.PerformChecksum(digitsToCheck, expectedChecksum);
            Assert.True(result);
        }

        [Fact]
        public void Checksum_HPPassportNumber_Validation()
        {
            var expectedChecksum = HPPassportNumberChecksum;
            var digitsToCheck = HPPassportNumbers.OfType<int>();

            var serviceProvider = BuildServiceProvider();
            var checksum = serviceProvider.GetService<IChecksum>();
            var result = checksum.PerformChecksum(digitsToCheck, expectedChecksum);
            Assert.True(result);
        }

        [Fact]
        public void Checksum_HPDOB_Validation()
        {
            var expectedChecksum = HPDOBChecksum;
            var digitsToCheck = HPDateOfBirthNumbers.OfType<int>();
            var serviceProvider = BuildServiceProvider();
            var checksum = serviceProvider.GetService<IChecksum>();
            var result = checksum.PerformChecksum(digitsToCheck, expectedChecksum);
            Assert.True(result);
        }

        [Fact]
        public void Checksum_HPPersonal_Validation()
        {
            var expectedChecksum = HPPersonalChecksum;
            var digitsToCheck = HPPersonalNumbers.OfType<int>();
            var serviceProvider = BuildServiceProvider();
            var checksum = serviceProvider.GetService<IChecksum>();
            var result = checksum.PerformChecksum(digitsToCheck, expectedChecksum);
            Assert.True(result);
        }

        [Fact]
        public void FinalChecksum()
        {
            var expectedChecksum = 4;
            var digitsToCheck = new int[]
            {
                21, 8, 9, 8, 9, 0, 2, 12, 0, 3, 6, 9, 0, 8, 0, 6, 1, 9, 4, 0, 6, 2, 3, 6, 35, 14, 1, 8, 4, 2, 2, 6,
                11, 0, 0, 0, 0, 0, 1
            };
            var serviceProvider = BuildServiceProvider();
            var checksum = serviceProvider.GetService<IChecksum>();
            var result = checksum.PerformChecksum(digitsToCheck, expectedChecksum);
            Assert.True(result);
        }

        [Fact]
        public void TestStringToInt()
        {
            string letter = "zABCD";
            var letters = letter.ToCharArray();
            List<int> letterValues = new List<int>();
            var serviceProvider = BuildServiceProvider();
            var checksum = serviceProvider.GetService<IChecksum>();
            foreach (var letter1 in letters)
            {
                letterValues.Add(checksum.GetIndexInAlphabet(letter1));
            }

            Assert.Equal(5, letterValues.Count);
        }

    }
}