using ninja.Helpers;
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
            this.InvoiceDate = DateTime.Now;
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
        public double TotalInvoice { get => this.Details.Sum(x => x.GrandTotal); }

        /// <summary>
        /// Gets or sets the taxes.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        public string Taxes
        {
            get
            {
                string taxes = string.Empty;
                foreach(var item in this.Details.Select(x => x.Taxes).Distinct())
                {
                    taxes += $"{((item - 1) * 100).ToPercentageString()}";
                }
                return taxes;
            }
        }

        /// <summary>
        /// Gets the total taxes.
        /// </summary>
        /// <value>
        /// The total taxes.
        /// </value>
        public double TotalTaxes => this.Details.Sum(x => x.TotalPrice * (x.Taxes - 1));


        /// <summary>
        /// Gets or sets the sub total.
        /// </summary>
        /// <value>
        /// The sub total.
        /// </value>
        public double SubTotal => this.Details.Sum(x => x.TotalPrice);

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        /// <value>
        /// The invoice date.
        /// </value>
        [DisplayName("Date")]
        [Required(ErrorMessage = "The invoice date is required.") ]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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
        [Range(1, 99999, ErrorMessage = "The POS # must be between 1 and 99998")]
        public int PointOfSaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        [DisplayName("Number")]
        [Required(ErrorMessage = "The invoice number is required.")]
        [Range(1, 9999999, ErrorMessage = "The invoice number must be between 1 and 9999999")]
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
            invoice.Id = this.Id;
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