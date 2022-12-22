using System.Collections.Generic;
using System.Web;
using System;
namespace PieceTracker.Common
{
    public class SessionHelper
    {
        public static int UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] == null ? 0 : (int)HttpContext.Current.Session["UserId"];
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }

        
        public static int RoleId
        {
            get
            {
                return HttpContext.Current.Session["UserRoleId"] == null ? 0 : (int)HttpContext.Current.Session["UserRoleId"];
            }
            set
            {
                HttpContext.Current.Session["UserRoleId"] = value;
            }
        }
        public static string UserEmail
        {
            get
            {
                return HttpContext.Current.Session["UserEmail"] == null ? "" : (string)HttpContext.Current.Session["UserEmail"];
            }
            set
            {
                HttpContext.Current.Session["UserEmail"] = value;
            }
        }
        public static string UserMobile
        {
            get
            {
                return (string)HttpContext.Current.Session["UserMobile"];
            }
            set
            {
                HttpContext.Current.Session["UserMobile"] = value;
            }
        }
        public static string RoleName
        {
            get
            {
                return HttpContext.Current.Session["RoleName"] == null ? "" : (string)HttpContext.Current.Session["RoleName"];
            }
            set
            {
                HttpContext.Current.Session["RoleName"] = value;
            }
        }
        public static string RoleAlias
        {
            get
            {
                return HttpContext.Current.Session["RoleAlias"] == null ? "" : (string)HttpContext.Current.Session["RoleAlias"];
            }
            set
            {
                HttpContext.Current.Session["RoleAlias"] = value;
            }
        }
        public static void RememberLoginDetails(string Email, string Password, string key)
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[key] ?? new HttpCookie(key); //_mblta -- Admin _mbltu---Users(Customer)
            objCookie.Values["LastVisit"] = DateTime.Now.ToString();
            objCookie.Values["Email"] = Email;
            objCookie.Values["Password"] = Password;
            objCookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        public static HttpCookie GetLoginDetails(string key)
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[key];
            if (objCookie != null)
            {
                return objCookie;
            }
            return null;
        }

        public static void RememberLoginDetails(string EmailID, string Password)
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["UserLoginDetails"] ?? new HttpCookie("UserLoginDetails");
            objCookie.Values["LastVisit"] = DateTime.Now.ToString();
            objCookie.Values["EmailID"] = EmailID;
            objCookie.Values["Password"] = Password;
            objCookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        public static HttpCookie GetLoginDetails()
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["UserLoginDetails"];
            if (objCookie != null)
            {
                return objCookie;
            }
            return null;
        }

        public static void ClearCookie(string Key)
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[Key] ?? new HttpCookie(Key);
            objCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        public static string ZipCode
        {
            get
            {
                return HttpContext.Current.Session["ZipCode"] == null ? "" : (string)HttpContext.Current.Session["ZipCode"];
            }
            set
            {
                HttpContext.Current.Session["ZipCode"] = value;
            }
        }

        public static string WelcomeUser
        {
            get
            {
                return HttpContext.Current.Session["WelcomeUser"] == null
                    ? "Guest" : (string)HttpContext.Current.Session["WelcomeUser"];
            }
            set
            {
                HttpContext.Current.Session["WelcomeUser"] = value;
            }
        }
        

        public static string SubcontractorCode
        {
            get
            {
                return (string)HttpContext.Current.Session["SubcontractorCode"];
            }
            set
            {
                HttpContext.Current.Session["SubcontractorCode"] = value;
            }
        }

        public static string ProjectIndexName
        {
            get
            {
                return (string)HttpContext.Current.Session["ProjectIndexName"];
            }
            set
            {
                HttpContext.Current.Session["ProjectIndexName"] = value;
            }
        }

        public static int ProjectId
        {
            get
            {
                return (int)HttpContext.Current.Session["ProjectId"];
            }
            set
            {
                HttpContext.Current.Session["ProjectId"] = value;
            }
        }

        public static int ControlIndex
        {
            get
            {
                return (int)HttpContext.Current.Session["ControlIndex"];
            }

            set
            {
                HttpContext.Current.Session["ControlIndex"] = value;
            }
        }

        public static string TreeView
        {
            get
            {
                return (string)HttpContext.Current.Session["TreeView"];
            }

            set
            {
                HttpContext.Current.Session["TreeView"] = value;
            }
        }



        public static string WeeklyDetails
        {
            get
            {
                return (string)HttpContext.Current.Session["WeeklyDetails"];
            }
            set
            {
                HttpContext.Current.Session["WeeklyDetails"] = value;
            }
        }

        public static string MonthlyDetails
        {
            get
            {
                return (string)HttpContext.Current.Session["MonthlyDetails"];
            }
            set
            {
                HttpContext.Current.Session["MonthlyDetails"] = value;
            }
        }

        [Serializable()]
        public class clsUserAccessRights
        {
            public int MenuId { get; set; }
            public int ParentMenuId { get; set; }
            public String Controller { get; set; }
            public String Action { get; set; }
            public int RightId { get; set; }
        }
    }
}
