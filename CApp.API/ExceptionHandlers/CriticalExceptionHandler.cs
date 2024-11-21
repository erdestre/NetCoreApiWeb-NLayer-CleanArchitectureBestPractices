using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CApp.API.ExceptionHandlers
{
    internal class CriticalExceptionHandler : IExceptionHandler
    {
	    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	    {
			if (exception is CriticalException)
			{
				Console.WriteLine("Hata ile ilgili sms gönderildi.");
			}
		    //business logic
		    return ValueTask.FromResult(false);
	    }
    }
}
