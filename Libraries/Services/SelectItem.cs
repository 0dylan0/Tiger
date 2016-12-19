using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SelectItem
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public bool Disabled { get; set; }

        public string Group { get; set; }
    }
}
