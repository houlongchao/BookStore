using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Attribute
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class,Inherited = true,AllowMultiple = true)]
    public class AuthorizationAttribute:FilterAttribute,IAuthorizationFilter
    {
        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AuthorizationAttribute()
        {
            string authUrl = ConfigurationManager.AppSettings["AuthUrl"];
            string saveKey = ConfigurationManager.AppSettings["AuthSaveKey"];
            string saveType = ConfigurationManager.AppSettings["AuthSaveType"];

            if (string.IsNullOrEmpty(authUrl))
            {
                this._authUrl = "~/Home/Login";
            }
            else
            {
                this._authUrl = authUrl;
            }
            if (string.IsNullOrEmpty(saveKey))
            {
                this._authSaveKey = "LoginedUser";
            }
            else
            {
                this._authSaveKey = saveKey;
            }
            if (string.IsNullOrEmpty(saveType))
            {
                this._authSaveType = "Session";
            }
            else
            {
                this._authSaveType = saveType;
            }

        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="authUrl">表示没有登录跳转的登录地址</param>
        public AuthorizationAttribute(string authUrl)
            : this()
        {
            this._authUrl = authUrl;
        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="authUrl">表示没有登录跳转的登录地址</param>
        /// <param name="saveKey">表示登录用来保存登陆信息的键名</param>
        public AuthorizationAttribute(string authUrl, string saveKey)
            : this(authUrl)
        {
            this._authSaveKey = saveKey;
            this._authSaveType = "Session";
        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="authUrl">表示没有登录跳转的登录地址</param>
        /// <param name="saveKey">表示登录用来保存登陆信息的键名</param>
        /// <param name="saveType">表示登录用来保存登陆信息的方式</param>
        public AuthorizationAttribute(string authUrl, string saveKey, string saveType)
            : this(authUrl, saveKey)
        {
            this._authSaveType = saveType;
        } 
        #endregion

        #region 字段
        /// <summary>
        /// 字段
        /// </summary>
        private string _authUrl = string.Empty;
        private string _saveKey = string.Empty;
        private string _saveType = string.Empty; 
        #endregion

        #region 属性
        /// <summary>
        /// 获取或者设置一个值，该值表示登录地址
        /// 如果web.config中末定义AuthUrl的值，则默认为：/waste/user/login
        /// </summary>
        public string AuthUrl
        {
            get { return _authUrl.Trim(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用于验证用户登录信息的登录地址不能为空！");
                }
                else
                {
                    _authUrl = value.Trim();
                }
            }
        }
        /// <summary>
        /// 获取或者设置一个值，该值表示登录用来保存登陆信息的键名
        /// 如果web.config中末定义AuthSaveKey的值，则默认为LoginedUser
        /// </summary>
        private string _authSaveKey = string.Empty;
        public string AuthSaveKey
        {
            get { return _authSaveKey.Trim(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用于保存登陆信息的键名不能为空！");
                }
                else
                {
                    this._authSaveKey = value.Trim();
                }
            }
        }
        /// <summary>
        /// 获取或者设置一个值，该值用来保存登录信息的方式
        /// 如果web.config中末定义AuthSaveType的值，则默认为Session保存
        /// </summary>
        private string _authSaveType = string.Empty;
        public string AuthSaveType
        {
            get { return _authSaveType.Trim().ToUpper(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用于保存登陆信息的方式不能为空，只能为【Cookie】或者【Session】！");
                }
                else
                {
                    _authSaveType = value.Trim();
                }
            }
        } 
        #endregion

        #region 方法
        /// <summary>
        /// void OnAuthorization(AuthorizationContext filterContext)
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new Exception("此特性只适合于Web应用程序使用！");
            }
            else
            {
                switch (AuthSaveType)
                {
                    case "SESSION":
                        if (filterContext.HttpContext.Session == null)
                        {
                            throw new Exception("服务器Session不可用！");
                        }
                        else if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                        {
                            if (filterContext.HttpContext.Session[_authSaveKey] == null)
                            {
                                filterContext.Result = new RedirectResult(_authUrl);
                            }
                        }
                        break;
                    case "COOKIE":
                        if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                        {
                            filterContext.Result = new RedirectResult(_authUrl);
                        }
                        break;
                    default:
                        throw new ArgumentNullException("用于保存登陆信息的方式不能为空，只能为【Cookie】或者【Session】！");
                }
            }
        } 

       
        #endregion
    }
}