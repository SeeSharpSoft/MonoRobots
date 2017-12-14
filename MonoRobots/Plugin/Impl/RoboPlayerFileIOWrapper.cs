using SeeSharpSoft.Plugin;
using System;
using System.Linq;

namespace SeeSharpSoft.MonoRobots.Plugin.Impl
{
    [PluginName("KI")]
    public class RoboPlayerFileIOWrapper : RoboPlayerFileIO
    {
        private String _executablePath;
        public String ExecutablePath {
            set
            {
                _executablePath = value.Replace("\\\\", "/").Replace("\\", "/").Replace("//", "/");
            }
            get
            {
                return _executablePath;
            }
        }

        protected override string LaunchFile
        {
            get
            {
                return ExecutablePath.Substring(ExecutablePath.LastIndexOf('/') + 1);
            }
        }

        protected override string WorkingDirectory
        {
            get
            {
                return ExecutablePath.Remove(ExecutablePath.LastIndexOf('/'));
            }
        }
    }
}
