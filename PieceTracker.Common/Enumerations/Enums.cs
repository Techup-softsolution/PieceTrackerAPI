using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PieceTracker.Common
{
    public class Enums
    {
        public enum ActionName
        {
            [Display(Name = "Add Update Record!")]
            AddUpdate = 1,
            [Display(Name = "Delete/Deactivate Record!")]
            Delete = 2,
            [Display(Name = "Verify Record!")]
            Verify = 3,
            [Display(Name = "Reset Password Link!")]
            ResetPasswordLink = 4,
        }
        public enum RESULT_TYPES
        {
            SUCCESS = 0,
            SUCCESS_WITH_WARNINGS = 1,
            FAILED = -1,
        }
        public enum Status
        {
            [Description("InActive")]
            [Display(Name = "InActive")]
            InActive = 0,

            [Description("Active")]
            [Display(Name = "Active")]
            Active = 1,

            [Description("Deleted")]
            [Display(Name = "Deleted")]
            Deleted = 2,
        }
        public enum PropertyStatus
        {
            [Description("Pending")]
            Pending = 1,

            [Description("Approved")]
            Approved = 2,
            
            [Description("Rejected")]
            Rejected = 3,

            [Description("Closed")]
            Closed = 4
        }

        public enum NotifyType
        {
            [Display(Name = "Success")]
            [Description("Success")]
            Success = 1,

            [Description("System Error Message")]
            SystemErrorMessage = 4,

            /// <summary>
            /// Error Enum Value setting.
            /// </summary>
            [Display(Name = "Error")]
            [Description("Error")]
            Error = 0
        }

        public enum TaskStatus
        {
            Pending = 0,
            Completed = 1
        }

        public enum AccessRight
        {
            [Description("View")]
            View = 1,

            [Description("Create")]
            Create = 2,

            [Description("Edit")]
            Edit = 3,

            [Description("Delete")]
            Delete = 4
        }

        public enum ListingType
        {
            [Description("For Sale")]
            Sale = 1,

            [Description("For Rent")]
            Rent = 2,

            [Description("For Lease")]
            Lease = 3,
        }

        public enum EnumUserRole
        {
            [Display(Name = "ASSERTSECURE ADMINISTRATOR")]
            AssertSecureAdministrator = 1,

            //[Display(Name = "Builder")]
            //BUILDER = 2,

            [Display(Name = "VendorAdmin")]
            VendorAdmin = 2,

            [Display(Name = "RESIDENT OWNER")]
            RESIDENTOWNER = 3,

            [Display(Name = "SITE MANAGER")]
            SITEMANAGER = 4,

            [Display(Name = "CHAIRMAN")]
            CHAIRMAN = 5,

            [Display(Name = "SECURITY GUARD")]
            SECURITYGUARD = 6,

            [Display(Name = "Receptionist")]
            Receptionist = 7,

            [Display(Name = "Hospital Doctor")]
            HospitalDoctor = 8,

            [Display(Name = "Customer")]
            Customer = 9,

            [Display(Name = "Assistant Doctor")]
            AssistantDoctor = 10,

            [Display(Name = "Hospital CA")]
            HospitalCA = 17,

            [Display(Name = "Ambulance Driver")]
            AmbulanceDriver = 18,

            [Display(Name = "Health Worker")]
            HealthWorker = 19,

            [Display(Name = "HXN Auditor")]
            HXNAuditor = 20,
        }

        public enum GeneralActionMessage
        {
            [Display(Name = "Record Created Successfully!!")]
            CreateMessage = 1,

            [Display(Name = "There is an Error Saving Record. Please try again with all required fields or contact System Administrator for more detail!!")]
            CreateErrorMessage = 2,

            [Display(Name = "Record Updated Successfully!!")]
            EditMessage = 3,

            [Display(Name = "There is an Error Updating Record. Please try again with all required fields or contact System Administrator for more detail!!")]
            EditErrorMessage = 4,

            [Display(Name = "Record Deleted Successfully!!")]
            DeleteMessage = 5,

            [Display(Name = "There is an Error Deleting Record. Please try again with all required fields or contact System Administrator for more detail!!")]
            DeleteErrorMessage = 6,

            [Display(Name = "Record Validation Falied. Please check all the required fields to be filled..!!")]
            ModelMessage = 7,

            [Display(Name = "Record Already Exists. Please try different Email and Mobile No or try login with your credentials!!")]
            ExistMessge = 8,

            [Display(Name = "Your Account detail saved successfully!")]
            SuccessMessage = 9,

            [Display(Name = "Your Profile Picture Updated Succesfully.")]
            ProfileSucess = 10,

            [Display(Name = "Error in updating Profile Picture. Please contact System Administrator for more detail!!")]
            ProfileError = 11,

            [Display(Name = "OTP Verified Succesfully. Please log in to system for access.")]
            OTPSuccess = 12,

            [Display(Name = "Error in OTP verification. Please enter correct OTP or contact System Administrator for more detail!!")]
            OTPError = 13,

            [Display(Name = "Your Account is not verified. Please verify with OTP recieved over regsitered Email or contact System Administrator for more detail!!")]
            NotVerified = 14,

            [Display(Name = "Login Successful!!")]
            LoginSuccess = 15,

            [Display(Name = "Sequence Record Already Exists. Please try different Sequence No!!")]
            SequenceExistMessge = 16,

            [Display(Name = "There is some error in validating records. Please try again with all required fields or contact System Administrator for more detail!!")]
            ModelInvalidMessage = 17,

            [Display(Name = "There is some error in fetching records. Please try again or contact System Administrator for more detail!!")]
            DataFetchError = 18,

            [Display(Name = "You are not authorised to access the application. Please login with verified username/email and password or contact system administrator for more detail!")]
            NotAuthorised = 19,

            [Display(Name = "Property Status has been updated successfully!!")]
            ChangeStatusSuccess = 20,

            [Display(Name = "There is some error in changing Property Status. Please try again or contact System Administrator for more detail!!")]
            ChangeStatusError = 21,
        }
    }
}
