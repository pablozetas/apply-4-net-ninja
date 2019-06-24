using ninja.model.Exceptions;
using ninja.model.Validation;
using ninja.model.Validation.InvoiceDetailValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Entity {

    public class InvoiceDetail {

        /// <summary>
        /// Valor del IVA para todo el dominio
        /// </summary>
        public double Taxes { get { return 1.21; } }

        public long InvoiceId { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get { return this.Amount * this.UnitPrice; } }
        public double TotalPriceWithTaxes { get { return this.TotalPrice * this.Taxes; } }

        /// <summary>
        /// Validation rules for details.
        /// </summary>
        /// <exception cref="ninja.model.Exceptions.BusinessException"></exception>
        internal void Validate()
        {
            AbstractValidationHandler detailValidationDescription = new InvoiceDetailValidationDescription() { PropertyName = "Description", PropertyValue = this.Description };
            AbstractValidationHandler detailValidationAmount = new InvoiceDetailValidationAmount() { PropertyName = "Amount", PropertyValue = this.Amount  };
            AbstractValidationHandler detailValidationUnitPrice = new InvoiceDetailValidationUnitPrice() { PropertyName = "UnitPrice", PropertyValue = this.UnitPrice };

            detailValidationDescription
                .SetNext(detailValidationAmount)
                .SetNext(detailValidationUnitPrice)
                ;
            try
            {
                ValidationChain.ExecuteValidationChain(detailValidationDescription);
            }
            catch (BusinessException be)
            {
                throw new BusinessException($"{be.Message} Please check the row {Id}");
            }

        }
    }

}