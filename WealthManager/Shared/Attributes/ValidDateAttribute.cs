using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Shared.Attributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        private readonly DateTime _minValue = new DateTime(1, 1, 1900);
        private readonly DateTime _maxValue = new DateTime(1, 1, 2100);

        public override bool IsValid(object value)
        {
            DateTime val = (DateTime)value;
            return val >= _minValue && val <= _maxValue;
        }

    }
}
