using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kutuphaneler
{
    /// <summary>
    ///   Summary description for WebServicePersonelAra
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class WebServicePersonelAra : WebService
    {
        //[WebMethod]
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] PersonelList(string prefixText)
        {
            ArrayList KullanicilarList = new ArrayList();
            if (HttpContext.Current.Session["ServiceToken"] != null)
            {
                EnrollPortalEntities Veriler = new EnrollPortalEntities();
                int Id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                var KList = (from K in Veriler.Kullanicilar
                             where K.SilindiMi == false
                                   && K.Id != Id
                                   && K.Ad.StartsWith(prefixText)
                             orderby K.Ad ascending
                             select new
                                        {
                                            K.Ad,
                                            K.Soyad,
                                            K.EPosta,
                                        }).Take(10).ToList();
                foreach (var Item in KList)
                {
                    if (Item.Ad.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase))
                    {
                        KullanicilarList.Add(Item.Ad + " " + Item.Soyad + "<\"" + Item.EPosta + "\">");
                    }
                }
            }
            return (string[]) (KullanicilarList.ToArray(typeof (string)));
        }

        //[WebMethod]
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string Test(string selectedMessageIds)
        {
            var data = selectedMessageIds.Split(',');
            if(data.Length>1)
                return data[1];
            else
            {
                return data[0];
            }
        }
    }

}