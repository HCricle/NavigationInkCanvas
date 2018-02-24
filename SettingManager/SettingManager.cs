using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingManager
{
    public class SettingManager
    {

        private static SettingManager Instance = null;
        private SettingManager() { }

        public static SettingManager GetInstance()
        {
            return Instance == null ? (Instance = new SettingManager()) : Instance;
        }

    }
}
