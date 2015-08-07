using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Security;
using Common;
using Microsoft.Ajax.Utilities;
using Models;
using Web.Attribute;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        Models.BookStoreEntities bse = new BookStoreEntities();

        /// <summary>
        /// 页面大小值
        /// </summary>
        private int pageSize = SettingHelper.PageSize;

        #region 首页
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获取图书类别和当前页
            int categoryId = int.Parse(Request.QueryString["category"] ?? "1");
            int currentPage = int.Parse(Request.QueryString["page"] ?? "1");

            //更具当前页，页大小，图书类别获取分页图书
            List<Book> books = GetPageBooks(currentPage, pageSize, b => b.categoryId == categoryId);
            int total = bse.Books.Count(b => b.categoryId == categoryId);

            //获取图书分类
            List<Category> categories = bse.Categories.Where(c => true).ToList();
            ViewBag.Categories = categories;
            
            //动态跟新分页组件
            ViewBag.Page = GetPage(currentPage, pageSize, total, "&category=" + categoryId);
            return View(books);
        } 
        #endregion
        #region 图书详情
        /// <summary>
        /// 图书详情
        /// </summary>
        /// <returns></returns>
        public ActionResult BookInfo()
        {
            int bookId = int.Parse(Request.QueryString["bookId"] ?? "1");
            Book book = bse.Books.Where(b => b.id == bookId).ToList().FirstOrDefault();
            return View(book);
        } 
        #endregion

        #region 登录
        /// <summary>
        /// 显示登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string username, string password,string returnUrl)
        {
            //判断传过来的值是否为空
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return View();
            }
            //得到登录用户实体
            Customer customer = bse.Customers.Where(c => c.username == username).FirstOrDefault();
            if (customer != null && password.ToMD5().Equals(customer.password))
            {
                if (customer.role==1)
                {
                    ModelState.AddModelError("errorMessage", "用户没有权限!");
                    return View();
                }

                Session["user"] = customer;
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
               
            }
            else
            {
                ModelState.AddModelError("errorMessage", "用户名或密码错误！");
                return View();
            }
        }
        #endregion

        #region 友情链接
        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        public ActionResult FriendLink()
        {
            List<FriendLink> links = bse.FriendLinks.Where(f => true).OrderBy(f=>f.sort).ToList();
            return View(links);
        } 
        #endregion

        #region 退出
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index", "Home");
        } 
        #endregion

        #region 个人中心
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [RolesAuthorize]
        public ActionResult UserInfo()
        {
            Customer customer = Session["user"] as Customer;
            return View(customer);
        } 
        #endregion

        #region 我的订单
        /// <summary>
        /// 我的订单
        /// </summary>
        /// <returns></returns>
        [RolesAuthorize]
        public ActionResult Order()
        {
            Customer customer = Session["user"] as Customer;
            List<Order> orders = bse.Orders.Where(o => o.customer == customer.id).ToList();

            return View(orders);
        } 
        #endregion

        /// <summary>
        /// 购物车
        /// </summary>
        /// <returns></returns>
        [RolesAuthorize]
        public ActionResult Cart()
        {
            //当前用户对象
            Customer customer = Session["user"] as Customer;
           //当前用户的购物车
            Cart cart = bse.Carts.Where(c => c.customerId == customer.id).FirstOrDefault();
            return View(cart);
        }

        [Authorize]
        public ActionResult AddCartItem()
        {
            string bookId = Request.QueryString["bookId"];

           //得到当前用户id
           
            //判断当前用户的购物车中是否有该书
            //有则更新，无则添加

            return RedirectToAction("Cart", "Home");
        }

        #region 获得分页组件
        /// <summary>
        /// 获得分页组件
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示的数目</param>
        /// <param name="total">总条目数</param>
        /// <param name="query">参数</param>
        /// <returns>分页html代码</returns>
        public MvcHtmlString GetPage(int currentPage, int pageSize, int total,string query)
        {
            if (total<=0)
            {
                return new MvcHtmlString("");
            }
            int len = (int)Math.Ceiling(total * 1.0 / pageSize);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" <ul class=\"{0}\">", "pagination  pagination-lg");//分页样式
            sb.AppendFormat(" <li><a href=\"?page=1{0}\"><span>首页</span></a></li>", query);//首页地址

            if (len <= 9)
            {
                for (int i = 1; i <= len; i++)
                {
                    if (i == currentPage)
                    {
                        sb.AppendFormat(" <li class=\"active\"><a href=\"?page={1}{0}\">{1}</a></li>", query, i);
                    }
                    else
                    {
                        sb.AppendFormat(" <li><a href=\"?page={1}{0}\">{1}</a></li>", query, i);
                    }
                }
            }
            else
            {
                int start = currentPage - 4;
                if (start > 0)
                {
                    for (int i = start; i <= start + 8; i++)
                    {
                        if (i == currentPage)
                        {
                            sb.AppendFormat(" <li class=\"active\"><a href=\"?page={1}{0}\">{1}</a></li>", query, i);
                        }
                        else
                        {
                            sb.AppendFormat(" <li><a href=\"?page={1}{0}\">{1}</a></li>", query, i);
                        }

                    }
                }
                else
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (i == currentPage)
                        {
                            sb.AppendFormat(" <li class=\"active\"><a href=\"?page={1}{0}\">{1}</a></li>", query, i);
                        }
                        else
                        {
                            sb.AppendFormat(" <li><a href=\"?page={1}{0}\">{1}</a></li>", query, i);
                        }

                    }
                }
            }
            sb.AppendFormat(" <li><a href=\"?page={1}{0}\"><span>末页</span></a></li>", query,len);//尾页地址
            sb.Append(" </ul>");

            return new MvcHtmlString(sb.ToString());
        } 
        #endregion

        #region 获得分页图书
        /// <summary>
        /// 获得分页图书
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        private List<Book> GetPageBooks(int currentPage, int pageSize, Expression<Func<Book, bool>> whereLambda)
        {
            return
                bse.Books.Where(whereLambda).OrderBy(b => b.id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        } 
        #endregion
    }
}
