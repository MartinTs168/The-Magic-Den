﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesShop.Infrastructure.Data.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
