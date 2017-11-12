using Core;
using log4net;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Web.Framework.Exceptions
{
    public class GlobalErrorLogger : IExceptionLogger
    {
        public async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var workContext = context.Request.GetDependencyScope().GetService(typeof(IWorkContext)) as IWorkContext;
            var logger = LogManager.GetLogger(
                //workContext.CurrentRequestId.ToString()
                Guid.NewGuid().ToString()
                );

            logger.Error(context.Exception.Message, context.Exception);
        }
    }
}
