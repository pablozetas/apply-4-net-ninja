using ninja.model.Exceptions;
using ninja.model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation.InvoiceValidations
{
    /// <summary>
    /// Invoice validation for Type.
    /// </summary>
    /// <seealso cref="ninja.model.Validation.AbstractValidationHandler" />
    public class InvoiceValidationType: AbstractValidationHandler
    {
        /// <summary>
        /// Return the next validation to execute.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ninja.model.Exceptions.BusinessException">The type of invoice must be a value of these {string.Join(",", validInvoiceTypes)}</exception>
        public override object Validate(string requestedProperty, object value)
        {
            if (requestedProperty.Equals("Type"))
            {
                string[] validInvoiceTypes = InvoiceTypes.GetValidInvoiceTypes();
                if (!validInvoiceTypes.Any(x => x.Equals(Convert.ToString(value))))
                {
                    throw new BusinessException($"The type of invoice must be a value of these {string.Join(",", validInvoiceTypes)}");
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
