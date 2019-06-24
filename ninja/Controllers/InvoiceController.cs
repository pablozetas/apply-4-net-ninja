using ninja.CustomFilters;
using ninja.model.Entity;
using ninja.model.Exceptions;
using ninja.model.Manager;
using ninja.model.Utils;
using ninja.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ninja.Controllers
{
    /// <summary>
    /// Invoice controller.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class InvoiceController : BaseController
    {
        InvoiceManager Manager = new InvoiceManager();
        // GET: Invoice
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IList<InvoiceViewModel> model = new List<InvoiceViewModel>();
            Manager.GetAll().ToList().ForEach(x =>
            {
                model.Add(new InvoiceViewModel(x));
            });

            return View(model);
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(long id)
        {
            Invoice model = Manager.GetById(id);
            return View(new InvoiceViewModel(model));
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Update(long id)
        {
            Invoice invoice = Manager.GetById(id);
            SetTempData(invoice.Type);
            return View(new InvoiceViewModel(invoice));
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(InvoiceViewModel model)
        {
            Invoice invoice = new Invoice();
            SetTempData(invoice.Type);
            model.CopyToInvoice(invoice);            
            Manager.Update(invoice);
            return RedirectToAction("Details", new { @id = model.Id });
        }

        /// <summary>
        /// Gets the invoice types.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetInvoiceTypes(string current)
        {
            string[] invoiceTypes = InvoiceTypes.GetValidInvoiceTypes();
            SelectListGroup group = new SelectListGroup()
            {
                Disabled = false,
                Name = "Invoice types"
            };
            var items = invoiceTypes.Select(x => new SelectListItem()
            {
                Disabled = false,
                Group = group,
                Selected = x == current,
                Text = x,
                Value = x
            });

            return items;
        }

        /// <summary>
        /// News this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            InvoiceViewModel model = new InvoiceViewModel();
            SetTempData(ninja.model.Entity.Invoice.Types.A.ToString());
            return View(model);
        }

        /// <summary>
        /// News the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [OnValidateExceptionAttribute(typeof(BusinessException))]
        public ActionResult New(InvoiceViewModel model)
        {
            SetTempData(model.Type);
            Manager.Insert(model.ToInvoice());            
            return RedirectToAction("Details", new { @id = model.Id });
        }

        /// <summary>
        /// Sets the temporary data thats used by de ui.
        /// </summary>
        /// <param name="defaultInvoiceType">Default type of the invoice.</param>
        private void SetTempData(string defaultInvoiceType)
        {
            if (TempData["SelectListInvoiceTypes"] != null)
            {
                TempData.Remove("SelectListInvoiceTypes");
            }

            TempData.Add("SelectListInvoiceTypes", this.GetInvoiceTypes(defaultInvoiceType));
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(long id)
        {
            Manager.Delete(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Gets the new row for the invoice.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public JsonResult GetRow(int id)
        {
            ViewBag.CurrentItemID = id;
            InvoiceDetailViewModel invoiceDetail = new InvoiceDetailViewModel(new InvoiceDetail());
            invoiceDetail.Id = id;

            var rowView = RenderView("DetaiInvoicelRow", invoiceDetail);

            return Json(new { rowId = id, rowData = rowView }, JsonRequestBehavior.AllowGet);
        }
    }
}