using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ninja.Binders
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if ((valueProviderResult == null) || (string.IsNullOrEmpty(valueProviderResult.AttemptedValue)))
            {
                return base.BindModel(controllerContext, bindingContext);
            }
            else
            {
                try
                {
                    CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings.Get("CultureInfoName"));
                    var dateTime = Convert.ToDateTime(valueProviderResult.AttemptedValue, culture);
                    return dateTime;
                }
                catch (FormatException)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Fecha no válida.");
                    return base.BindModel(controllerContext, bindingContext);
                }
            }
        }
    }
}