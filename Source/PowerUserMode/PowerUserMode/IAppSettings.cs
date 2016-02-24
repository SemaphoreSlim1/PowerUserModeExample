using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUserMode
{
    public interface IAppSettings
    {
        bool GetIsSubscribed(PowerSetting setting);
        void SetIsSubscribed(PowerSetting setting, bool value);

        bool GetIsEnabled(PowerSetting setting);
        void SetIsEnabled(PowerSetting setting, bool value);
    }
}
