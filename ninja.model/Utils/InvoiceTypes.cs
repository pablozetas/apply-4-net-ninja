using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Utils
{
    /// <summary>
    /// Return the valid invoice types given by the model.
    /// </summary>
    public static class InvoiceTypes
    {
        /// <summary>
        /// Gets the valid invoice types.
        /// </summary>
        /// <returns></returns>
        public static string[] GetValidInvoiceTypes()
        {
            return Enum.GetNames(typeof(ninja.model.Entity.Invoice.Types));
        }
    }
}
