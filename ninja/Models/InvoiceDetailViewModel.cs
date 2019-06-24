using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ninja.model.Entity;

namespace ninja.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceDetailViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceDetailViewModel"/> class.
        /// </summary>
        public InvoiceDetailViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceDetailViewModel"/> class.
        /// </summary>
        /// <param name="invoiceDetail">The invoice detail.</param>
        public InvoiceDetailViewModel(InvoiceDetail invoiceDetail)
        {
            this.InvoiceId = invoiceDetail.InvoiceId;
            this.Id = invoiceDetail.Id;
            this.Description = invoiceDetail.Description;
            this.Amount = invoiceDetail.Amount;
            this.UnitPrice = invoiceDetail.UnitPrice;
            //this.TotalPrice = invoiceDetail.TotalPrice;
            this.Taxes = invoiceDetail.Taxes;
            //this.GrandTotal = invoiceDetail.TotalPriceWithTaxes;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>
        /// The invoice identifier.
        /// </value>
        public long InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Converts to invoicedetail.
        /// </summary>
        /// <returns></returns>
        internal InvoiceDetail ToInvoiceDetail()
        {
            return new InvoiceDetail()
            {
                Amount = this.Amount,
                Description = this.Description,
                Id = this.Id,
                InvoiceId = this.InvoiceId,
                UnitPrice = this.UnitPrice
            };
        }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>
        /// The unit price.
        /// </value>
        public double UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the total price.
        /// </summary>
        /// <value>
        /// The total price.
        /// </value>
        public double TotalPrice => Amount * UnitPrice;

        /// <summary>
        /// Gets or sets the taxes.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        public double Taxes { get; set; }

        /// <summary>
        /// Gets or sets the taxes percent.
        /// </summary>
        /// <value>
        /// The taxes percent.
        /// </value>
        public double TaxesPercent => ((this.Taxes - 1) * 100);

        /// <summary>
        /// Gets or sets the grand total.
        /// </summary>
        /// <value>
        /// The grand total.
        /// </value>
        public double GrandTotal => this.TotalPrice * this.Taxes;
    }
}