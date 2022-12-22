using System.Collections.Generic;
using System.Net;
using System.Text;
using static PieceTracker.Common.Enums;

namespace PieceTracker.Common
{
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {

        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public string JWTToken { get; set; }
        public string OTP { get; set; }
    }

    public class ApiResponse<T> : BaseApiResponse
    {
        public virtual IList<T> Data { get; set; }

    }
    public class ApiPostResponse<T> : BaseApiResponse
    {
        public virtual T Data { get; set; }

    }

    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ReturnResult
    {
        List<string> _Errors;
        List<string> _Warnings;

        public ReturnResult()
        {
        }

        public RESULT_TYPES Result { get; set; }
        public List<string> Errors
        {
            get
            {
                if (_Errors == null) _Errors = new List<string>();
                return _Errors;
            }
        }
        public List<string> Warnings
        {
            get
            {
                if (_Warnings == null) _Warnings = new List<string>();
                return _Warnings;
            }
        }

        public string ShortMessage { get; set; }

        public object ResultValue { get; set; }

        private string _ErrorStrings;
        public string ErrorStrings
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < _Errors.Count; i++)
                {
                    strBuilder.Append(_Errors[i]);
                }
                return strBuilder.ToString();
            }
            set
            {
                _ErrorStrings = value;
            }
        }
        /// <summary>
        /// overwrite
        /// </summary>
        public override string ToString()
        {
            return ShortMessage;
        }

        public int ToInteger()
        {
            throw new System.NotImplementedException();
        }
    }
}