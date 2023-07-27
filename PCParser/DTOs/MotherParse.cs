﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCParser.DTOs
{
    internal class MotherParse
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Socket { get; set; }
        public string Form { get; set; }
        public decimal Price { get; set; }

    }
}