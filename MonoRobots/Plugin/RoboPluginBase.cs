using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeeSharpSoft.Plugin;

namespace SeeSharpSoft.MonoRobots.Plugin
{
    public abstract class RoboPluginBase : IPlugin
    {
        public RoboPluginBase()
        {
            _pluginSettings = CreatePluginSettings();
        }

        private RoboPlayerPluginSettings _pluginSettings;
        public PluginSettings PluginSettings
        {
            get { return _pluginSettings; }
        }

        protected virtual RoboPlayerPluginSettings CreatePluginSettings()
        {
            return new RoboPlayerPluginSettings();
        }

        protected PluginContext PluginContext { private set; get; }

        protected virtual object CreatePluginService()
        {
            return null;
        }

        public virtual void ActivatePlugin(PluginContext pluginContext)
        {
            PluginContext = pluginContext;
        }

        public virtual void DeactivatePlugin(PluginContext pluginContext)
        {
            if (pluginContext != PluginContext)
            {
                throw new PluginException("Deactivation of plugin '" + this.GetName() + "' requested with wrong plugincontext!");
            }
        }

        public virtual void Dispose() { }

        public virtual void OnPluginNotify(object sender, PluginNotification notification) { }
    }
}