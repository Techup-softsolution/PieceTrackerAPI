using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PieceTracker.Common
{
    public static class ConfigItems
    {
        /// <summary>
        /// Numeric Validation
        /// </summary>
        public const string NumericExpression = @"^[0-9]*$";

        /// <summary>
        /// allow multiple email address with comma(,) speperation
        /// </summary>
        public const string MultipleEmailRegularExpression = @"(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*,\s*|\s*$))*";
        /// <summary>
        /// The text box regular expression
        /// </summary>
        public const string TextBoxRegularExpression = @"[^<>]*";

        /// <summary>
        /// The regular expression for file name
        /// </summary>
        public const string RegularExpressionForFileName = @"[<>?/\|*:]*";

        /// <summary>
        /// The name validation expression
        /// </summary>
        public const string NameValidationExpression = @"([a-zA-Z0-9&#32;.&amp;amp;&amp;#39;-]+)";

        /// <summary>
        /// The special character validation expression
        /// </summary>
        public const string SpecialCharacterValidationExpression = @"^[^<>.!@#%/']+$";

        /// <summary>
        /// The decimal validation expression
        /// </summary>
        public const string RegularExprssionForDecimal = @"\d+(\.\d{1,2})?";

        /// <summary>
        /// The website validation expression
        /// </summary>
        /// 

        public const string RegularExprssionForWebsite = @"^(http|http(s)?://)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?";
        //public const string RegularExprssionForWebsite = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";

        /// <summary>
        /// The longitude validation expression
        /// </summary>

        public const string RegularExprssionForLongitude = @"^-?([1]?[0-7][0-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}";

        /// <summary>
        /// The Latitude validation expression
        /// </summary>

        public const string RegularExprssionForLatitude = @"^-?([0-8]?[0-9]|[0-9]0)\.{1}\d{1,6}";
        /// <summary>
        /// The Date Time Format Without Second
        /// </summary>


        /// <summary>
        /// Maximum Amount Price
        /// </summary>
        public const double MaxAmount = 999999999.99;

        public const string DateFormate = "dd/MM/yyyy";
        public const string DateTimeFormate = "dd/MM/yyyy HH:mm tt";
        public const string DateTimeFormateZone = "dd/MM/yyyy HH:mm";

        //public const string DateFormate = "MM/dd/yyyy";
        //public const string DateTimeFormate = "MM/dd/yyyy hh:mm tt";
        public static readonly Dictionary<string, object> CenterColumnStyle = new Dictionary<string, object> { { "align", "center" }, { "style", "text-align:center;vertical-align:middle !important;" } };

        /// <summary>
        /// Minimum Amount Price
        /// </summary>
        public const double MinAmount = 1.0;

        /// <summary>
        /// Gets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        public static string DateFormat
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            }
        }

        /// <summary>
        /// Gets the date format grid.
        /// </summary>
        /// <value>
        /// The date format grid.
        /// </value>
        public static string DateFormatGrid
        {
            get
            {
                return "{0: " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + "}";
            }
        }

        /// <summary>
        /// Gets the date time format.
        /// </summary>
        /// <value>
        /// The date time format.
        /// </value>
        public static string DateTimeFormat
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern;
            }
        }

        public static string DateTimeFormatGrid
        {
            get
            {
                return "{0: " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern + "}";
            }
        }

        /// <summary>
        /// Gets the date time format single digit.
        /// </summary>
        /// <value>The date time format single digit.</value>
        public static string DateTimeFormatSingleDigit
        {
            get
            {
                return ConfigurationManager.AppSettings["DateTimeFormat"].Replace(":ss", string.Empty).Replace("hh", "h").Replace("MM", "M").Replace("dd", "d");
            }
        }

        /// <summary>
        /// Gets the date format for date picker.
        /// </summary>
        /// <value>
        /// The date format for date picker.
        /// </value>
        public static string DateFormatForDatePicker
        {
            get
            {
                return ConfigurationManager.AppSettings["DateFormatForDatePicker"];
            }
        }

        
        /// <summary>
        /// Gets the current site URL.
        /// </summary>
        /// <value>
        /// The current site URL.
        /// </value>
        public static string CurrentSiteUrl
        {
            get
            {
                var strUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
                return strUrl;
            }
        }

        /// <summary>
        /// Converts the UTC to local.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="timeDifference">The time difference.</param>
        /// <returns>Return DateTime</returns>
        public static DateTime? ConvertUtcToLocal(DateTime? date, string timeDifference)
        {
            if (date != null)
            {
                var hourDifference = Convert.ToInt32(timeDifference.Split(':')[0]);
                var minDifference = Convert.ToInt32(timeDifference.Split(':')[1]);
                return date.Value.AddHours(-hourDifference).AddMinutes(-minDifference);
            }
            else
            {
                return null;
            }
        }

        public static DateTime DateTimezone(object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField), out dateReturn))
                    {
                        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                        if (tzi != null)
                        {
                            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateReturn, tzi);

                            return localTime;
                        }
                    }
                    else
                    {
                        return DateTime.UtcNow;
                    }
                }
            }
            return DateTime.UtcNow;
        }
    }
}
