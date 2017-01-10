using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class BasicModel
    {
        public int ID { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }
    }
}