using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ninja.CustomFilters
{
    /// <summary>
    /// Custom filter action to manipulate the business error.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.ActionFilterAttribute" />
    public class OnValidateExceptionAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnValidateExceptionAttribute"/> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        public OnValidateExceptionAttribute(Type exceptionType) 
            : this(exceptionType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnValidateExceptionAttribute" /> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="viewName">Name of the view.</param>
        public OnValidateExceptionAttribute(Type exceptionType, string viewName) 
            : this(exceptionType, viewName, "model")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnValidateExceptionAttribute"/> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="modelParameterName">Name of the model parameter.</param>
        public OnValidateExceptionAttribute(Type exceptionType, string viewName, string modelParameterName)
        {
            ViewName = viewName;
            ExceptionType = exceptionType;
            ModelParameterName = modelParameterName;
        }

        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        /// <value>
        /// The name of the view.
        /// </value>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the type of the exception to handle (Business Logic Exceptions are spected).
        /// </summary>
        /// <value>
        /// The type of the exception.
        /// </value>
        public Type ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the name of the model parameter in case of model param name is different from model.
        /// </summary>
        /// <value>
        /// The name of the model parameter.
        /// </value>
        public string ModelParameterName { get; set; }

        /// <summary>
        /// Gets or sets the name of the redirect to action when exception occurs.
        /// </summary>
        /// <value>
        /// The name of the redirect to action.
        /// </value>
        public string RedirectToActionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the redirect to controller.
        /// </summary>
        /// <value>
        /// The name of the redirect to controller.
        /// </value>
        public string RedirectToControllerName { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        private object Model { get; set; }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <remarks>
        /// Store de Model into the model property.
        /// </remarks>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Model = filterContext.ActionParameters[ModelParameterName];
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null && ExceptionType.IsAssignableFrom(filterContext.Exception.GetType()))
            {
                filterContext.Controller.TempData["MessageError"] = filterContext.Exception.Message;

                if (!string.IsNullOrEmpty(RedirectToActionName))
                {
                    var routeValueDictionary = new System.Web.Routing.RouteValueDictionary();
                    routeValueDictionary.Add("Action", RedirectToActionName);
                    routeValueDictionary.Add("Controller", RedirectToControllerName ?? filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
                    filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = ViewName ?? filterContext.ActionDescriptor.ActionName,
                        ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                        {
                            Model = Model
                        },
                        TempData = filterContext.Controller.TempData,
                    };
                }

                filterContext.ExceptionHandled = true;
                base.OnActionExecuted(filterContext);
            }
        }
    }
}