using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation
{
    /// <summary>
    /// Validation contract for business logic.
    /// </summary>
    public interface IValidationHandler
    {
        /// <summary>
        /// Sets the next.
        /// </summary>
        /// <param name="validationHandler">The validation handler.</param>
        /// <returns></returns>
        IValidationHandler SetNext(IValidationHandler validationHandler);

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        object Validate(string requestedProperty, object value);
    }
}
