using System.Collections.Generic;
using PassportValidation.Models;

namespace PassportValidation
{
    public interface IValidatePassport
    {
        void Validate(PassportModel model);
        List<NationCode> GetNations();
    }
}
