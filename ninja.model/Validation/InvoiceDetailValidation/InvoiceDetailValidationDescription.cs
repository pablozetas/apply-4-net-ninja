using ninja.model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation.InvoiceDetailValidation
{
    /// <summary>
    /// Validation for description's rows.
    /// </summary>
    /// <seealso cref="ninja.model.Validation.AbstractValidationHandler" />
    public class InvoiceDetailValidationDescription: AbstractValidationHandler
    {
        /// <summary>
        /// Return the next validation to execute.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ninja.model.Exceptions.BusinessException">
        /// The detail description must not be empty.
        /// or
        /// The detail description lenght must be less or equal to 60 characters.
        /// </exception>
        /// <exception cref="BusinessException">The point of sale number must be between 1 and 99998</exception>
        public override object Validate(string requestedProperty, object value)
        {
            if (requestedProperty.Equals("Description"))
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new BusinessException("The detail description must not be empty.");
                }

                if (Convert.ToString(value).Length > 30)
                {
                    throw new BusinessException("The detail description lenght must be less or equal to 60 characters.");
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
