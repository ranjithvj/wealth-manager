using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WealthManager.Shared.Helpers;

namespace WealthManager.Server.Models
{
    public class BaseEntity<T>
    {
        [Required]
        public T Id { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOnUtc { get; set; }
    }
}
