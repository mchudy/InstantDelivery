using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InstantDelivery.Core
{
    public abstract class ValidationBase : IDataErrorInfo
    {
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        string IDataErrorInfo.this[string propertyName] => OnValidate(propertyName);

        protected virtual string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }
            string error = string.Empty;
            var value = GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<ValidationResult>(1);
            var context = new ValidationContext(this, null, null) { MemberName = propertyName };

            var result = Validator.TryValidateProperty(value, context, results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }
            return error;
        }
    }
}
