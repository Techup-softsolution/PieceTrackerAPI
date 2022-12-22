using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Common
{
    public class UserEnums
    {
        public enum EnumUserRole
        {
            [Display(Name = "Assert Garage Admin")]
            AssertAdmin = 1,

            [Display(Name = "Agent")]
            Agent = 2,

            [Display(Name = "Customer")]
            Customer = 3,
        }
    }
}
