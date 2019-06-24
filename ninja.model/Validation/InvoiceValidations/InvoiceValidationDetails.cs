using ninja.model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation.InvoiceValidations
{
    /// <summary>
    /// Check if invoice has details.
    /// </summary>
    /// <seealso cref="ninja.model.Validation.AbstractValidationHandler" />
    public class InvoiceValidationDetails: AbstractValidationHandler
    {
        /// <summary>
        /// Return the next validation to execute.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ninja.model.Exceptions.BusinessException">The invoice must have at least 1 detail.</exception>
        public override object Validate(string requestedProperty, object value)
        {
            if (requestedProperty.Equals("Detail"))
            {
                if (Convert.ToInt32(value) <= 0)
                {
                    throw new BusinessException("The invoice must have at least 1 detail.");
                }
                return null;
            }
            else
            {
                return base.Validate(requestedProperty, value);
            }
        }
    }
}
