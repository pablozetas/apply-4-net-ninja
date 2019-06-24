using ninja.model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation.InvoiceValidations
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ninja.model.Validation.AbstractValidationHandler" />
    public class InvoiceValidationNumber: AbstractValidationHandler
    {
        /// <summary>
        /// Return the next validation to execute.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ninja.model.Exceptions.BusinessException">The point of sale number must be between 1 and 99999999</exception>
        public override object Validate(string requestedProperty, object value)
        {
            if (requestedProperty.Equals("Number"))
            {
                if (Convert.ToInt32(value) <= 0 || Convert.ToInt32(value) > 99998)
                {
                    throw new BusinessException("The invoice number must be between 1 and 99999999");
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
