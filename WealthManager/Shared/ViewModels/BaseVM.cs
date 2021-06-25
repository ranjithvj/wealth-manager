using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Shared.Helpers;

namespace WealthManager.Shared.ViewModels
{
    public class BaseVM<T>
    {
        public T Id { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Updated By")]
        public DateTime LastUpdatedOnIST
        {
            get
            {
                //return LastUpdatedOn.UtcToIst(); todo : Waiting for .NET 6 Update. Only 14 timezone info available for client project. server project has 120 timezones.
                return LastUpdatedOn.ToLocalTime();
            }
        }
    }
}
