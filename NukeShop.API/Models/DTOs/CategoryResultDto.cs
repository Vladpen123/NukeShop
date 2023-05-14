﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.API.Models.DTOs
{
    public class CategoryResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductResultDto>? Products { get; set; }
    }
}
