using System;
using Microsoft.Extensions.DependencyInjection;
using PassportValidation;
using PassportValidation.Models;
using Xunit;

namespace PassportValidationTests
{
    public class ValidatePassportTests
    {
        private static IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection().AddScoped<IChecksum, Checksum>()
                .AddScoped<IValidatePassport, ValidatePassport>()
                .BuildServiceProvider();
        }

        private static PassportModel _correctValuesPassportModel = new PassportModel
        {
            PassportNumber = "L898902C<",
            Nationality = "GBR",
            Gender = "F",
            DateOfBirth = "690806",
            DateOfExpiration = "940623",
            MzrPassportNumber = "L898902C<",
            PassportNumberChecksum = "3",
            MzrNationalityCode = "GBR",
            MzrDateOfBirth = "690806",
            DOBChecksum = "1",
            Sex = "F",
            PassportExpiration = "940623",
            ExpirationChecksum = "6",
            PersonalNumber = "ZE184226B<<<<<",
            PersonalNumberChecksum = "1",
            FinalChecksum = "4",
            PassportNumberChecksumValid = false,
            DateOfBirthChecksumValid = false,
            PassportExpirationChecksumValid = false,
            PersonalNumberChecksumValid = false,
            FinalChecksumValid = false,
            GenderCrossCheckValid = false,
            DateOfBirthCrossCheckValid = false,
            PassportExpCrossCheckValid = false,
            NationalityCrossCheckValid = false,
            PassportNoCrossCheckValid = false
        };

        private static PassportModel _incorrectValuesPassportModelSafeCharacters = new PassportModel
        {
            PassportNumber = "L8989024<",
            Nationality = "GBt",
            Gender = "M",
            DateOfBirth = "690906",
            DateOfExpiration = "890624",
            MzrPassportNumber = "L898902C<",
            PassportNumberChecksum = "9",
            MzrNationalityCode = "GBR",
            MzrDateOfBirth = "690807",
            DOBChecksum = "1",
            Sex = "F",
            PassportExpiration = "940523",
            ExpirationChecksum = "6",
            PersonalNumber = "ZE184226B<<<3<",
            PersonalNumberChecksum = "1",
            FinalChecksum = "4",
            PassportNumberChecksumValid = false,
            DateOfBirthChecksumValid = false,
            PassportExpirationChecksumValid = false,
            PersonalNumberChecksumValid = false,
            FinalChecksumValid = false,
            GenderCrossCheckValid = false,
            DateOfBirthCrossCheckValid = false,
            PassportExpCrossCheckValid = false,
            NationalityCrossCheckValid = false,
            PassportNoCrossCheckValid = false
        };

        [Fact]
        public void Service_Builds()
        {
            var serviceProvider = BuildServiceProvider();
            var validate = serviceProvider.GetService<IValidatePassport>();
            Assert.NotNull(validate);
        }

        [Fact]
        public void GetNations_Returns_List()
        {
            var serviceProvider = BuildServiceProvider();
            var validate = serviceProvider.GetService<IValidatePassport>();
            Assert.NotEmpty(validate.GetNations());
        }

        [Fact]
        public void Checksum_Returns_True_With_Correct_Values()
        {
            var serviceProvider = BuildServiceProvider();
            var validate = serviceProvider.GetService<IValidatePassport>();
        }

        [Fact]
        public void Validate_Returns_All_Checks_True_Correct_Details()
        {
            var serviceProvider = BuildServiceProvider();
            var validate = serviceProvider.GetService<IValidatePassport>();
            var passport = _correctValuesPassportModel;
            validate.Validate(passport);
            Assert.True(passport.NationalityCrossCheckValid);
            Assert.True(passport.PassportExpCrossCheckValid);
            Assert.True(passport.PassportNoCrossCheckValid);
            Assert.True(passport.DateOfBirthCrossCheckValid);
            Assert.True(passport.GenderCrossCheckValid);
            Assert.True(passport.DateOfBirthChecksumValid);
            Assert.True(passport.FinalChecksumValid);
            Assert.True(passport.PassportNumberChecksumValid);
            Assert.True(passport.PassportExpirationChecksumValid);
            Assert.True(passport.PersonalNumberChecksumValid);
        }

        [Fact]
        public void Validate_Returns_All_Checks_False_Incorrect_Details()
        {
            var serviceProvider = BuildServiceProvider();
            var validate = serviceProvider.GetService<IValidatePassport>();
            var passport = _incorrectValuesPassportModelSafeCharacters;
            validate.Validate(passport);
            Assert.False(passport.NationalityCrossCheckValid);
            Assert.False(passport.PassportExpCrossCheckValid);
            Assert.False(passport.PassportNoCrossCheckValid);
            Assert.False(passport.DateOfBirthCrossCheckValid);
            Assert.False(passport.GenderCrossCheckValid);
            Assert.False(passport.DateOfBirthChecksumValid);
            Assert.False(passport.FinalChecksumValid);
            Assert.False(passport.PassportNumberChecksumValid);
            Assert.False(passport.PassportExpirationChecksumValid);
            Assert.False(passport.PersonalNumberChecksumValid);
        }
    }
}