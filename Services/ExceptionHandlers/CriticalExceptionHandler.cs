using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.Services.ExceptionHandlers
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
