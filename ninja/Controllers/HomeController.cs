using Newtonsoft.Json;
using ninja.model.Manager;
using ninja.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ninja.Controllers
{

    /// <summary>
    /// Home controller
    /// </summary>
    /// <seealso cref="ninja.Controllers.BaseController" />
    [Authorize]
    public class HomeController : BaseController {

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {

            InvoiceManager manager = new InvoiceManager();

            List<SalesByInvoiceTypeViewModel> salesByInvoiceType = new List<SalesByInvoiceTypeViewModel>();
            int itemId = 0;
            foreach (var item in manager.GetAll().GroupBy(x => x.Type))
            {
                salesByInvoiceType.Add(new SalesByInvoiceTypeViewModel()
                {
                    label = $"Type {item.Key}",
                    data = item.Sum(x => x.CalculateInvoiceTotalPriceWithTaxes()),
                    color = ChartColors.Colors[itemId],
                    id = itemId.ToString()
                });
                itemId++;
            }

            var labels = salesByInvoiceType.Select(x => x.label).ToArray();
            var values = salesByInvoiceType.Select(x => x.data).ToArray();

            ViewBag.DataChart = JsonConvert.SerializeObject(values);
            ViewBag.LabelsChart = JsonConvert.SerializeObject(labels);
            return View();
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Contacts this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}