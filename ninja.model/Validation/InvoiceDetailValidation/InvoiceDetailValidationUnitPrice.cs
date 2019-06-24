using ninja.model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation.InvoiceDetailValidation
{
    /// <summary>
    /// Unit price validator.
    /// </summary>
    /// <seealso cref="ninja.model.Validation.AbstractValidationHandler" />
    public class InvoiceDetailValidationUnitPrice: AbstractValidationHandler
    {
        public override object Validate(string requestedProperty, object value)
        {
            if (requestedProperty.Equals("UnitPrice"))
            {
                if (Convert.ToDouble(value) <= 0)
                {
                    throw new BusinessException("The unit price must be greter than 0.");
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
