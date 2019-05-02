using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ResetPasswordViewModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string GUID { get; set; }
    }
}
