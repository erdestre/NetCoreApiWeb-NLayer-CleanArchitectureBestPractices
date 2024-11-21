﻿using System.Net;
using App.Application;
using Microsoft.AspNetCore.Mvc;

namespace CApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
			//Switch Expression
			return result.Status switch
			{
				HttpStatusCode.NoContent => NoContent(),
				HttpStatusCode.Created => Created(result.UrlAsCreated, result),
				_ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
			};


			//if (result.Status == HttpStatusCode.NoContent)
   //         {
			//	return NoContent();
   //             //return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
   //         }
			//if (result.Status == HttpStatusCode.Created)
			//{
			//	return Created(urlAsCreated, result.Data);
			//}
   //         return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
			//Switch Expression
			return result.Status switch
			{
				HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = result.Status.GetHashCode() },
				_ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
			};

			//if (result.Status == HttpStatusCode.NoContent)
   //         {
   //             return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
   //         }
   //         return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }
    }
}
