using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMcG.BinDays
{
    public class LicenseInformation
    {
        static bool? s_isTrial;
        public static bool IsTrial
        {
            get
            {
#if FREE_VERSION
                return true;
#else
                if (s_isTrial == null)
                    s_isTrial = new Microsoft.Phone.Marketplace.LicenseInformation().IsTrial();

                return s_isTrial.Value;
#endif
            }
        }

    }
}
