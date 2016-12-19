using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Core.Infrastructure
{
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        public WebAppTypeFinder()
        {
        }

        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return _ensureBinFolderAssembliesLoaded; }
            set { _ensureBinFolderAssembliesLoaded = value; }
        }

        public virtual string GetBinDirectory()
        {
            return HostingEnvironment.IsHosted ? HttpRuntime.BinDirectory : AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }
    }
}
