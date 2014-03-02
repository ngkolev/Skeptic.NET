using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Core.Model
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
