using System.Web;
using System.Web.Mvc;
using Models;

namespace Web.Attribute
{
    public class RolesAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Customer customer = httpContext.Session["user"] as Customer;
            if (customer==null)
            {
                return false; 
            }
            Users = customer.username;
            Roles = customer.role == 0 ? "user" : "admin";
            return customer.role == 0;
        }
    }
}