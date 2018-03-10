using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Synchronous
{
    public class RealTimeManager
    {
        Dictionary<string, IRealTimeMaterial> manager = null;
        public RealTimeManager()
        {
            manager = new Dictionary<string, IRealTimeMaterial>();
            manager.Add("folder", new FolderRealTimeMaterial());
        }

        public void Update(string key)
        {
            if (manager.ContainsKey(key))
            {
                manager[key].Update();
            }
        }
    }
}