using ninja.model.Entity;
using ninja.model.Manager;
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
    public class InvoiceController : Controller
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
            Invoice model = Manager.GetById(id);
            return View(new InvoiceViewModel(model));
        }

        [HttpPost]
        public ActionResult Update(InvoiceViewModel model)
        {
            Invoice invoice = Manager.GetById(model.Id);
            model.CopyToInvoice(invoice);
            Manager.Update(invoice);
            return RedirectToAction("Details", new { @id = model.Id });
        }

        /// <summary>
        /// News this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            InvoiceViewModel model = new InvoiceViewModel();
            return View(model);
        }

        /// <summary>
        /// News the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult New(InvoiceViewModel model)
        {
            Manager.Insert(model.ToInvoice());
            return RedirectToAction("Details", new { @id = model.Id });
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
    }
}