using ninja.model.Validation;
using ninja.model.Validation.InvoiceValidations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ninja.model.Entity
{

    public class Invoice {

        /// <summary>
        /// Initializes a new instance of the <see cref="Invoice"/> class.
        /// </summary>
        public Invoice() {

            this.Detail = new List<InvoiceDetail>();

        }

        /// <summary>
        /// Enum types
        /// </summary>
        public enum Types {
            A,
            B,
            C
        }

        /// <summary>
        /// Numero de factura
        /// </summary>      
        public long Id { get; set; }

        /// <summary>
        /// Does the validate.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void DoValidate()
        {
            AbstractValidationHandler invoiceValidationType = new InvoiceValidationType() { PropertyName = "Type", PropertyValue = this.Type };
            AbstractValidationHandler invoiceValidationPOS = new InvoiceValidationPointOfSale() { PropertyName = "PointOfSale", PropertyValue = this.PointOfSale };
            AbstractValidationHandler invoiceValiationNumber = new InvoiceValidationNumber() { PropertyName = "Number", PropertyValue = this.Number };
            AbstractValidationHandler invoiceValidationDate = new InvoiceValidationDate() { PropertyName = "Date", PropertyValue = this.Date };
            AbstractValidationHandler invoiceValidationDetail = new InvoiceValidationDetails() { PropertyName = "Detail", PropertyValue = this.Detail.Count };

            invoiceValidationType
                .SetNext(invoiceValidationPOS)
                .SetNext(invoiceValiationNumber)
                .SetNext(invoiceValidationDate)
                .SetNext(invoiceValidationDetail);

            ValidationChain.ExecuteValidationChain(invoiceValidationType);

            foreach(InvoiceDetail detail in Detail)
            {
                detail.Validate();
            }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        private IList<InvoiceDetail> Detail { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the point of sale.
        /// </summary>
        /// <value>
        /// The point of sale.
        /// </value>
        public int PointOfSale { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public long Number { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the detail.
        /// </summary>
        /// <returns></returns>
        public IList<InvoiceDetail> GetDetail() {

            return this.Detail;

        }

        /// <summary>
        /// Adds the detail.
        /// </summary>
        /// <param name="detail">The detail.</param>
        public void AddDetail(InvoiceDetail detail) {

            this.Detail.Add(detail);

        }

        /// <summary>
        /// Deletes the details.
        /// </summary>
        public void DeleteDetails() {

            this.Detail.Clear();

        }

        /// <summary>
        /// Sumar el TotalPrice de cada elemento del detalle
        /// </summary>
        /// <returns></returns>
        public double CalculateInvoiceTotalPriceWithTaxes()
        {
            return this.Detail
                .Sum(x => x.TotalPriceWithTaxes);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        internal void Update(Invoice model)
        {
            this.Type = model.Type;
            this.CustomerName = model.CustomerName;
            this.PointOfSale = model.PointOfSale;
            this.Number = model.Number;
            this.Date = model.Date;
            this.DeleteDetails();
            model.Detail.ToList().ForEach(x =>
            {
                this.AddDetail(x);
            });
        }
    }
}