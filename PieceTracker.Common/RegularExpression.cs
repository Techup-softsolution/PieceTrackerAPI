using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Common
{
    public static class RegularExpression
    {
        public const string RegExNumber = @"^[0-9]*$";

        /// <summary>
        /// The regular expression for telephone.
        /// </summary>
        public const string RegExTelephone = @"^[0-9 (,),-]{0,15}$";

        /// <summary>
        /// The regular expression email.
        /// </summary>
        public const string RegExEmail = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";

        /// <summary>
        /// The regular expression url.
        /// </summary>
        public const string RegExURl = @"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";

        /// <summary>
        /// The regular expression up to 2 decimal place.
        /// </summary>
        public const string RegExUpTo2Decimal = @"^\d{1,14}(\.\d{1,2})?$";       
      
    }
}
