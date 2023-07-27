﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCParser.DTOs
{
    internal class PSUParse
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public ushort Power { get; set; }
        public decimal Price { get; set; }

    }
}