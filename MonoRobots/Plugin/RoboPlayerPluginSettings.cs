using SeeSharpSoft.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpSoft.MonoRobots.Plugin
{
    public class RoboPlayerPluginSettings : PluginSettings
    {
        private Boolean _playerCollision = true;
        public Boolean PlayerCollision
        {
            get
            {
                return _playerCollision;
            }
            set
            {
                _playerCollision = value;
            }
        }
    }
}
