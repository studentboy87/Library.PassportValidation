namespace PassportValidation.Models
{
    public class PassportModel
    {
        public string PassportNumber { get; set; }

        public string Nationality { get; set; }

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string DateOfExpiration { get; set; }

        public string MzrPassportNumber { get; set; }

        public string PassportNumberChecksum { get; set; }

        public string MzrNationalityCode { get; set; }

        public string MzrDateOfBirth { get; set; }

        public string DOBChecksum { get; set; }

        public string Sex { get; set; }

        public string PassportExpiration { get; set; }

        public string ExpirationChecksum { get; set; }

        public string PersonalNumber { get; set; }

        public string PersonalNumberChecksum { get; set; }

        public string FinalChecksum { get; set; }

        public bool PassportNumberChecksumValid { get; set; }

        public bool DateOfBirthChecksumValid { get; set; }

        public bool PassportExpirationChecksumValid { get; set; }

        public bool PersonalNumberChecksumValid { get; set; }

        public bool FinalChecksumValid { get; set; }

        public bool GenderCrossCheckValid { get; set; }

        public bool DateOfBirthCrossCheckValid { get; set; }

        public bool PassportExpCrossCheckValid { get; set; }

        public bool NationalityCrossCheckValid { get; set; }

        public bool PassportNoCrossCheckValid { get; set; }
    }
}