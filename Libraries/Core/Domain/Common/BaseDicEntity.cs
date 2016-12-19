using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public abstract class BaseDicEntity : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        private int? _seq;
        public int? Seq
        {
            get
            {
                return _seq;
            }
            set
            {
                _seq = value ?? 0;
            }
        }
    }
}
