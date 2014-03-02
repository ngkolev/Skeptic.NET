using System.Collections.Generic;

namespace Skeptic.Model
{
    public class Settings
    {
        private IDictionary<string, string> settings;

        public Settings(IDictionary<string, string> settings = null)
        {
            this.settings = settings ?? new Dictionary<string, string>();
        }

        public string this[string key]
        {
            // Microsoft convention is to return null if nothing was found
            get
            {
                try
                {
                    return settings[key];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
            set { settings[key] = value; }
        }
    }
}
