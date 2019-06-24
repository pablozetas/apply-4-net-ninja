using ninja.model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation.InvoiceDetailValidation
{
    /// <summary>
    /// The validation for amount.
    /// </summary>
    /// <seealso cref="ninja.model.Validation.AbstractValidationHandler" />
    public class InvoiceDetailValidationAmount: AbstractValidationHandler
    {
        /// <summary>
        /// Return the next validation to execute.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ninja.model.Exceptions.BusinessException">The amout must be greter than 0.</exception>
        /// <exception cref="BusinessException">The detail description must not be empty.
        /// or
        /// The detail description lenght must be less or equal to 60 characters.</exception>
        public override object Validate(string requestedProperty, object value)
        {
            if (requestedProperty.Equals("Amount"))
            {
                if (Convert.ToDouble(value) <= 0)
                {
                    throw new BusinessException("The amout must be greter than 0.");
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
