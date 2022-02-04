using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assignment2.Filters;

public class CustomerAuth : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var customerID = context.HttpContext.Session.GetInt32(nameof(Customer.CustomerId));
        if (!customerID.HasValue) context.Result = new RedirectToActionResult("Index", "Home", null);
    }
}