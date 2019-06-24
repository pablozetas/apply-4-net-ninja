using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ninja.Controllers
{
    public class BaseController : Controller
    {
        private readonly Regex m_regex = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}");

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <param name="master">The master.</param>
        /// <returns></returns>
        public string RenderView(string viewName, object model = null, string master = null)
        {
            var vd = new ViewDataDictionary();

            foreach (var item in this.ViewData.Keys)
            {
                vd.Add(item, this.ViewData[item]);
            }
            vd.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(this.ControllerContext, viewName, master);
                ViewContext viewContext = new ViewContext(this.ControllerContext, viewResult.View, vd, this.TempData, sw);

                viewResult.View.Render(viewContext, sw);

                return m_regex.Replace(sw.GetStringBuilder().ToString(), string.Empty);
            }
        }
    }
}