using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public class EngineContext
    {
        protected static IEngine CreateEngineInstance()
        {
            return new Engine();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = CreateEngineInstance();
                Singleton<IEngine>.Instance.Initialize();
            }
            return Singleton<IEngine>.Instance;
        }

        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }
    }
}
