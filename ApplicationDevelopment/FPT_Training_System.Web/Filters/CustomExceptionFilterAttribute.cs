using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FPT_Training_System.Web.Filters
{
    public class MyErrorHandler : HandleErrorAttribute, IExceptionFilter
    {
        #region Private Members

        private Exception _serverException;
        private Exception _baseException;

        private const string ERROR_CONTROLLER = "Error";
        private const string GENERAL_ERROR_ACTION = "Error500";
        private const string PAGE_NOT_FOUND_ACTION = "Error404";

        #endregion

        #region Methods

        #region Protected

        public override void OnException(ExceptionContext filterContext)
        {
            _serverException = filterContext.Exception;

            //log
            logErrorRecursive(_serverException);

            //handle ajax
            if (
                filterContext.Exception != null
                && filterContext.HttpContext.Request.IsAjaxRequest()
            )
            {
                handleAjaxRequestError(filterContext);
            }
            //handle regular requsts
            else
            {
                redirectToErrorPage(filterContext);
            }

            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }

        #endregion

        #region Private

        /// <summary>
        /// Logs exception recursivty, starting with the inner most exception
        /// </summary>
        private void logErrorRecursive(Exception ex)
        {
            if (ex.InnerException != null)
            {
                logErrorRecursive(ex.InnerException);
            }
            else
            {
                _baseException = ex;
            }
        }

        /// <summary>
        /// Handles errors on ajax requests - returns internal server error and error message to caller
        /// </summary>
        private static void handleAjaxRequestError(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //filterContext.HttpContext.Response.StatusCode = (int) System.Net.HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            var message = filterContext.Exception.Message;
            filterContext.Result = new JsonNetResult
            {
                //Data = ResponseMessage.CreateErrorReponse(filterContext.Exception.Message),
                Data = new
                {
                    success = false,
                    message,
                    serverError = "500"
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// redirects to error page based on internal server error
        /// (authorization, page not found and general error)
        /// </summary>
        private void redirectToErrorPage(ExceptionContext filterContext)
        {
            string errorAction;
            if (_baseException.GetType() == typeof(HttpException))
            {
                var httpException = (HttpException)_baseException;
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        errorAction = PAGE_NOT_FOUND_ACTION;
                        break;
                    default:
                        errorAction = GENERAL_ERROR_ACTION;
                        break;
                }
            }
            else
            {
                errorAction = GENERAL_ERROR_ACTION;
            }

            filterContext.Controller.ViewData["message"] = _baseException.Message;
            //filterContext.Result = new RedirectToRouteResult(errorAction, null);
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = errorAction, controller = ERROR_CONTROLLER }));
        }

        #endregion

        #endregion
    }
}
