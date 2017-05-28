﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wilson.Web.Areas.Accounting.Models.HomeViewModels
{
    public class PayrollViewModel
    {
        [Required]
        [StringLength(36)]
        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }

        [Required]
        [Display(Name = "From")]
        [DataType(DataType.Duration)]
        public DateTime From { get; set; }

        [Required]
        [Display(Name = "To")]
        public DateTime To { get; set; }

        public IEnumerable<SelectListItem> EmployeeOptions { get; set; }
    }
}