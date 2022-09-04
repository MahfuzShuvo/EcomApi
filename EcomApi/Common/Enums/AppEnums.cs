using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Common.Enums
{
    public class AppEnums
    {
        public enum ResponseCode
        {
            Success = 200,
            InternalServerError = 500,
            Failed = 404,
            Warning = 400,
            UnAuthorize = 401
        }
        public enum Status
        {
            Active = 1,
            InActive = 2,
            Deleted = 3
        }
        public enum Gender
        {
            Male = 1,
            Female = 2,
            Other = 3
        }
        public enum ValidationType
        {
            IsNullOrEmpty = 1,
            IsNullOrWhiteSpace = 2,
            IsLessThanOrEqual = 3,
            IsGreaterThan = 4,
            IsGreaterThanOrEqual = 5,
            IsEqual = 6,
            IsNotEqual = 7,
            IsValidEmail = 8,
            IsNull = 9,
            IsDuplicateItem = 10,
            IsPositiveInteger = 11,
            IsPositiveIntegerOrNull = 12,
            IsLengthLessThan = 13,
            IsLengthLessThanOrEqual = 14,
            IsLengthGreaterThan = 15,
            IsLengthGreaterThanOrEqual = 16,
            IsValidDate = 17,
            IsNullObject = 18
        }
    }
}
