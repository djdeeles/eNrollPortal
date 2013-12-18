using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;

namespace EnrollPortal.Kutuphaneler
{
    public class EnrollContext
    {
        private readonly CookieeLanguage dataLanguage;
        private readonly CookieeLanguage workingLanguage;

        public EnrollContext()
        {
            workingLanguage = new CookieeLanguage("EnrollWorkingLanguage");
            dataLanguage = new CookieeLanguage("EnrollDataLanguage");
        }

        public static EnrollContext Current
        {
            get { return (EnrollContext) HttpContext.Current.Session["EnrollContext"]; }
        }

        public CookieeLanguage WorkingLanguage
        {
            get { return workingLanguage; }
        }

        public CookieeLanguage DataLanguage
        {
            get { return dataLanguage; }
        }

        private Int32 GetWorkingLanguageIdFromCookie()
        {
            return Convert.ToInt32(HttpContext.Current.Response.Cookies["EnrollWorkingLanguage"].Value);
        }

        private Int32 GetDataLanguageIdFromCookie()
        {
            return Convert.ToInt32(HttpContext.Current.Response.Cookies["EnrollDataLanguage"].Value);
        }

        public void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }

    public class CookieeLanguage
    {
        private readonly string strcookieName;
        private int intlanguageId;

        public CookieeLanguage(String cookieName)
        {
            strcookieName = cookieName;
            if (CheckCookieNull())
            {
                CreateCookie();
                intlanguageId = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultWorkingLanguage"]);
            }
            else
            {
                intlanguageId = Convert.ToInt32(HttpContext.Current.Request.Cookies[strcookieName].Value);
            }
        }

        public Int32 languageId
        {
            get { return intlanguageId; }
            set
            {
                HttpCookie oCookie = HttpContext.Current.Request.Cookies[strcookieName];
                oCookie.Value = value.ToString();
                HttpContext.Current.Response.Cookies.Set(oCookie);
                intlanguageId = value;
            }
        }

        private void SetCulture(String culture)
        {
            CultureInfo oCultureInfo = new CultureInfo(culture);
            EnrollContext.Current.SetCulture(oCultureInfo);
        }

        private Boolean CheckCookieNull()
        {
            Boolean blnReturn = false;
            if (HttpContext.Current.Request.Cookies[strcookieName] == null)
            {
                blnReturn = true;
            }
            return blnReturn;
        }

        private void CreateCookie()
        {
            string strDefaultLanguageId = ConfigurationManager.AppSettings["DefaultWorkingLanguage"];
            HttpCookie oCookie = new HttpCookie(strcookieName, strDefaultLanguageId);
            oCookie.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(oCookie);
        }
    }
}