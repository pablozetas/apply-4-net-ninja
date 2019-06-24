using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation
{
    /// <summary>
    /// Abstract validation handler.
    /// </summary>
    /// <seealso cref="ninja.model.Validation.IValidationHandler" />
    public class AbstractValidationHandler : IValidationHandler
    {
        protected IValidationHandler _nextHandler;

        /// <summary>
        /// Sets the next.
        /// </summary>
        /// <param name="validationHandler">The validation handler.</param>
        /// <returns></returns>
        public IValidationHandler SetNext(IValidationHandler validationHandler)
        {
            this._nextHandler = validationHandler;

            return _nextHandler;
        }

        /// <summary>
        /// Return the next validation to execute.
        /// </summary>
        /// <param name="requestedProperty">The requested property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual object Validate(string requestedProperty, object value)
        {
            if (this._nextHandler == null)
            {
                return null;
            }
            else
            {
                return this._nextHandler.Validate(requestedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /// <value>
        /// The property value.
        /// </value>
        public object PropertyValue { get; set; }
    }
}
