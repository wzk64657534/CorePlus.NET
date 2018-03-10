using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;

namespace Core
{
    public sealed class DbContextScope : IDisposable
    {
        private static Dictionary<int, DbContextScope> dict = new Dictionary<int, DbContextScope>();

        public static DbContextScope Begin()
        {
            return new DbContextScope();
        }

        public CoreDBContext Context { get; private set; }

        private DbContextScope()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            if (!dict.ContainsKey(id))
            {
                Context = new CoreDBContext();
                dict.Add(id, this);
            }
        }

        public static DbContextScope Current
        {
            get
            {
                DbContextScope scope = null;
                var id = Thread.CurrentThread.ManagedThreadId;
                if (dict.TryGetValue(id, out scope))
                {
                    return scope;
                }
                return null;
            }
        }

        public void Dispose()
        {
            dict.Remove(Thread.CurrentThread.ManagedThreadId);
        }
    }
}
