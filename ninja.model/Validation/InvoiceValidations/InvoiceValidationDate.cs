using ninja.model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation.InvoiceValidations
{
    /// <summary>
    /// Invoice validation date.
    /// </summary>
    /// <seealso cref="ninja.model.Validation.AbstractValidationHandler" />
    public class InvoiceValidationDate: AbstractValidationHandler
    {
        /// <summary>
        /// Return the next validation to execute.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ninja.model.Exceptions.BusinessException">
        /// The invoice date must not be null.
        /// or
        /// The invoice date must not be less than {DateTime.Now.Date}
        /// or
        /// The invoice date must not be greater than {DateTime.Now.Date}
        /// </exception>
        public override object Validate(string requestedProperty, object value)
        {
            if (requestedProperty.Equals("Date"))
            {
                if (value == null)
                {
                    throw new BusinessException($"The invoice date must not be null.");
                }

                if (Convert.ToDateTime(value).Date < DateTime.Now.Date)
                {
                    throw new BusinessException($"The invoice date must not be less than {DateTime.Now.Date}");
                }
                if (Convert.ToDateTime(value).Date > DateTime.Now.Date)
                {
                    throw new BusinessException($"The invoice date must not be greater than {DateTime.Now.Date}");
                }
                return base._nextHandler;
            }
            else
            {
                return base.Validate(requestedProperty, value);
            }
        }
    }
}
