using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Common
{
    public class MessageEnums
    {
        public enum GeneralActionMessage
        {
            [Display(Name = "Record Fetched Successfully!")]
            FetchSuccess = 1,
            [Display(Name = "Record Not Found.")]
            RecordNotFound = 2,
            [Display(Name = "Error in fetching record!!")]
            FetchError = 3,
            [Display(Name = "Record Created Successfully!!")]
            CreateMessage = 4,
            [Display(Name = "There is an Error Saving Record. Please try again with all required fields or contact System Administrator for more detail!!")]
            CreateErrorMessage = 5,
            [Display(Name = "Record Updated Successfully!!")]
            EditMessage = 6,
            [Display(Name = "There is an Error Updating Record. Please try again with all required fields or contact System Administrator for more detail!!")]
            EditErrorMessage = 7,
            [Display(Name = "Record Deleted Successfully!!")]
            DeleteMessage = 8,
            [Display(Name = "There is an Error Deleting Record. Please try again with all required fields or contact System Administrator for more detail!!")]
            DeleteErrorMessage = 9,
            [Display(Name = "Record Validation Falied. Please check all the required fields to be filled..!!")]
            ModelMessage = 10,
            [Display(Name = "Login Successful!!")]
            LoginSuccess = 11,
            [Display(Name = "You are not authorised to access the application. Please login with verified username/email and password or contact system administrator for more detail!")]
            NotAuthorised = 12,
            [Display(Name = "There is some error in validating records. Please try again with all required fields or contact System Administrator for more detail!!")]
            ModelInvalidMessage = 13,
            [Display(Name = "Record Verified Successfully!!")]
            VerifySuccessMessage = 14,
            [Display(Name = "The Account is not verified. Please contact your administrator to verify the account and try again later!!")]
            VerifyErrorMessage = 15,
            [Display(Name = "New Password and Confirm Password do not match!!")]
            ConfirmpasswordErrorMessage = 16,
            [Display(Name = "Reset Password Link has been shared on your email addrees!!")]
            ResetPasswordLinkSuccess = 17,
            [Display(Name = "Error in generating Reset Password Link.Pleasetry again later or contact system administrator!!")]
            ResetPasswordLinkError = 18,
        }
    }
}
