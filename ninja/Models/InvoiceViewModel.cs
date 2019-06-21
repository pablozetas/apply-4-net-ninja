using ninja.model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ninja.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceViewModel"/> class.
        /// </summary>
        public InvoiceViewModel()
        {
            this.Details = new List<InvoiceDetailViewModel>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceViewModel"/> class.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        public InvoiceViewModel(Invoice invoice)
            : this()
        {
            this.Id = invoice.Id;
            this.Type = invoice.Type;
            this.Customer = invoice.CustomerName;
            this.PointOfSaleNumber = invoice.PointOfSale;
            this.InvoiceNumber = invoice.Number;
            this.TotalInvoice = invoice.CalculateInvoiceTotalPriceWithTaxes();
            this.InvoiceDate = invoice.Date;
            this.Details.AddRange(invoice.GetDetail().Select(x => new InvoiceDetailViewModel(x)));
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DisplayName("Type")]
        [Required(ErrorMessage = "The invoice type is required.")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the total inovie.
        /// </summary>
        /// <value>
        /// The total inovie.
        /// </value>
        [DisplayName("Total")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]        
        public double TotalInvoice { get; set; }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        /// <value>
        /// The invoice date.
        /// </value>
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        [Required(ErrorMessage = "The invoice date is required.") ]
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        [DisplayName("Customer")]
        [MaxLengthAttribute(60)]
        [Required(ErrorMessage = "The customer is required.")]
        public string Customer { get; set; } = "No name";

        /// <summary>
        /// Gets or sets the invoice description.
        /// </summary>
        /// <value>
        /// The invoice description.
        /// </value>
        [DisplayName("Description")]
        public string InvoiceDescription => $"{Type} {this.PointOfSaleNumber.ToString("00000")}-{this.InvoiceNumber.ToString("00000000")}";

        /// <summary>
        /// Gets or sets the point of sale number.
        /// </summary>
        /// <value>
        /// The point of sale number.
        /// </value>
        [DisplayName("POS #")]
        [Required(ErrorMessage = "The POS # is required.")]
        public int PointOfSaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        [DisplayName("Number")]
        [Required(ErrorMessage = "The invoice number is required.")]
        public long InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public List<InvoiceDetailViewModel> Details { get; set; }

        /// <summary>
        /// Copies to invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        internal void CopyToInvoice(Invoice invoice)
        {
            invoice.Type = this.Type;
            invoice.Number = this.InvoiceNumber;
            invoice.PointOfSale = this.PointOfSaleNumber;
            invoice.CustomerName = this.Customer;
            invoice.Date = this.InvoiceDate;
            invoice.DeleteDetails();
            this.Details.ForEach(x =>
            {
                invoice.AddDetail(x.ToInvoiceDetail());
            });
        }

        /// <summary>
        /// Converts to invoice.
        /// </summary>
        /// <returns></returns>
        internal Invoice ToInvoice()
        {
            Invoice invoice = new Invoice();
            this.CopyToInvoice(invoice);
            return invoice;
        }
    }
}