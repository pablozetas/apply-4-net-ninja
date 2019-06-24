using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Validation
{
    /// <summary>
    /// Validation change executer.
    /// </summary>
    public static class ValidationChain
    {
        /// <summary>
        /// Executes the validation chain.
        /// </summary>
        /// <param name="abstractValidation">The abstract validation.</param>
        public static void ExecuteValidationChain(AbstractValidationHandler abstractValidation)
        {
            AbstractValidationHandler validationHandler = abstractValidation;
            while (validationHandler != null)
            {
                validationHandler = validationHandler.Validate(validationHandler.PropertyName, validationHandler.PropertyValue) as AbstractValidationHandler;
            }            
        }
    }
}
