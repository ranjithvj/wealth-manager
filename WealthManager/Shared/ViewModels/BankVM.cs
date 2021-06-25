﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Shared.ViewModels
{
    public class BankVM
    {
        public int Id { get; set; }
        [Display(Name="Bank Name")]
        public string Name { get; set; }
    }
}
