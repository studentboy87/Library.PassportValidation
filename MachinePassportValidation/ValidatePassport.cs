using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using PassportValidation.Models;

namespace PassportValidation
{
    public class ValidatePassport : IValidatePassport
    {
        private readonly IChecksum _checksum;

        public ValidatePassport(IChecksum checksum)
        {
            _checksum = checksum;
        }

        public void Validate(PassportModel model)
        {
            var finalCheck = string.Concat(model.MzrPassportNumber, model.PassportNumberChecksum, model.MzrDateOfBirth,
                model.DOBChecksum, model.PassportExpiration, model.ExpirationChecksum, model.PersonalNumber,
                model.PersonalNumberChecksum);

            model.PassportNumberChecksumValid = Checksum(model.MzrPassportNumber, model.PassportNumberChecksum);
            model.DateOfBirthChecksumValid = Checksum(model.MzrDateOfBirth, model.DOBChecksum);
            model.PassportExpirationChecksumValid = Checksum(model.PassportExpiration, model.ExpirationChecksum);
            model.PersonalNumberChecksumValid = Checksum(model.PersonalNumber, model.PersonalNumberChecksum);
            model.FinalChecksumValid = Checksum(finalCheck, model.FinalChecksum);

            model.GenderCrossCheckValid = CrossCheck(model.Gender, model.Sex);
            model.DateOfBirthCrossCheckValid = CrossCheck(model.DateOfBirth, model.MzrDateOfBirth);
            model.PassportExpCrossCheckValid = CrossCheck(model.DateOfExpiration, model.PassportExpiration);
            model.NationalityCrossCheckValid = CrossCheck(model.Nationality, model.MzrNationalityCode);
            model.PassportNoCrossCheckValid = CrossCheck(model.PassportNumber, model.MzrPassportNumber);
        }

        public List<NationCode> GetNations()
        {
            using (StreamReader r = File.OpenText("ISO3166.json"))
            {
                string json = r.ReadToEnd();
                List<NationCode> codes = JsonConvert.DeserializeObject<List<NationCode>>(json);
                return codes;
            }
        }

        internal bool CrossCheck(string propertyOne, string propertyTwo)
        {
            return string.Equals(propertyOne, propertyTwo, StringComparison.OrdinalIgnoreCase);
        }

        internal bool Checksum(string value, string checksum)
        {
            value = ConvertToNumeric(value);

            char[] charArr = value.Where(char.IsLetterOrDigit).ToArray();

            List<int> digitsToCheck = charArr.Select(character =>
                    char.IsLetter(character)
                        ? _checksum.GetIndexInAlphabet(character)
                        : (int) char.GetNumericValue(character))
                .ToList();

            return _checksum.PerformChecksum(digitsToCheck, int.Parse(checksum));
        }

        internal string ConvertToNumeric(string property)
        {
            property = Regex.Unescape(property);
            property = property.Replace("<", "0");
            return property;
        }
    }
}