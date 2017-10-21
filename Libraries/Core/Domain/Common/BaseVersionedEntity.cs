﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class BaseVersionedEntity : BaseEntity
    {
        public byte[] Version { get; set; }
    }
}
