using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Exceptions
{
    /// <summary>
    /// Business logic exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BusinessException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}
