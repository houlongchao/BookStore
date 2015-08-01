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
            
            return customer.role == 0;
        }
    }
}