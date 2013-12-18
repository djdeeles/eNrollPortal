using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using EnrollPortal.Kutuphaneler.DataModels;
using EnrollPortal.WebServices.Types;

namespace EnrollPortal.WebServices
{
    /// <summary>
    ///   Summary description for WebServicePersonelAra
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class EventsActions : WebService
    {
        [WebMethod]
        //public List<EventsType> GetEvents(int categoryId, int itemCount, int pageCount)
        public List<EventsType> GetEvents(int categoryId, int itemCount, int pageCount)
        {
            var veriler = new EnrollPortalEntities();
            var returnData = new List<EventsType>();
             
            var etkinlikler = (from e in veriler.Etkinlikler
                                   where e.Durum && (categoryId>0 ? e.EtkinlikKategoriId == categoryId:true)
                                   orderby e.BaslangicTarihi
                                   select new
                                   {
                                       Id = e.Id,
                                       Title = e.Ad,
                                       Description = e.Ozet,
                                       Content = e.Icerik,
                                       StartDate = e.BaslangicTarihi,
                                       EndDate = e.BitisTarihi,
                                       State = e.Durum,
                                       ReadCount = e.OkunmaSayisi,
                                       Keywords = e.AnahtarKelimeler,
                                       CreatedDate = e.KaydetmeTarihi,
                                       CreatedUser = e.KaydedenKullaniciId,
                                       UpdatedDate = e.DuzenlemeTarihi,
                                       UpdatedUser = e.DuzenleyenKullaniciId,
                                       Image1 = e.Gorsel1,
                                       ImageThumbnail1 = e.GorselThumbnail1,
                                       Image2 = e.Gorsel2,
                                       ImageThumbnail2 = e.GorselThumbnail2,
                                       Image3 = e.Gorsel3,
                                       ImageThumbnail3 = e.GorselThumbnail3,
                                       Image4 = e.Gorsel4,
                                       ImageThumbnail4 = e.GorselThumbnail4,

                                   }).OrderBy(p => p.StartDate).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList(); 
            
            
            foreach (var item in etkinlikler)
            {
                var newData = new EventsType();
                newData.Id = item.Id;
                newData.Title = item.Title;
                newData.Description = item.Description;
                newData.Content = item.Content;
                newData.StartDate = item.StartDate != null ? item.StartDate.Value.ToShortDateString() + " " + item.StartDate.Value.ToShortTimeString() : " ";
                newData.EndDate = item.EndDate != null ? item.EndDate.Value.ToShortDateString() + " " + item.EndDate.Value.ToShortTimeString() : " ";
                newData.State = item.State;
                newData.ReadCount = item.ReadCount;
                newData.Keywords = item.Keywords;
                newData.CreatedDate = item.CreatedDate.ToShortDateString() + " " + item.CreatedDate.ToShortTimeString();
                newData.CreatedUser = item.CreatedUser;
                newData.UpdatedDate = item.UpdatedDate != null ? item.UpdatedDate.Value.ToShortDateString() + " " + item.UpdatedDate.Value.ToShortTimeString() : " ";
                newData.UpdatedUser = item.UpdatedUser != null ? Convert.ToInt32(item.UpdatedUser) : 0;
                newData.Image1 = item.Image1 != null
                       ? item.Image1.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail1 = item.ImageThumbnail1 != null
                       ? item.ImageThumbnail1.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.Image2 = item.Image2 != null
                       ? item.Image2.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail2 = item.ImageThumbnail2 != null
                       ? item.ImageThumbnail2.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.Image3 = item.Image3 != null
                       ? item.Image3.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail3 = item.ImageThumbnail3 != null
                       ? item.ImageThumbnail3.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.Image4 = item.Image4 != null
                       ? item.Image4.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail4 = item.ImageThumbnail4 != null
                       ? item.ImageThumbnail4.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";

                returnData.Add(newData);
            }
            return returnData;
        }

        [WebMethod]
        public List<EtkinlikKategorileri> GetEventCategories(int itemCount, int pageCount)
        {
            var veriler = new EnrollPortalEntities();
            var etkinlikKategorileriList = veriler.EtkinlikKategorileri.Where(p => p.Durum)
                .OrderBy(p => p.SiraNo).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();

            return etkinlikKategorileriList;
        }

        [WebMethod]
        public List<EventsType> GetClosestEvent()
        {
            var veriler = new EnrollPortalEntities();

            var returnData = new List<EventsType>();

            var etkinlikler = (from e in veriler.Etkinlikler
                               where e.Durum
                               orderby e.BaslangicTarihi
                               select new
                               {
                                   Id = e.Id,
                                   Title = e.Ad,
                                   Description = e.Ozet,
                                   Content = e.Icerik,
                                   StartDate = e.BaslangicTarihi,
                                   EndDate = e.BitisTarihi,
                                   State = e.Durum,
                                   ReadCount = e.OkunmaSayisi,
                                   Keywords = e.AnahtarKelimeler,
                                   CreatedDate = e.KaydetmeTarihi,
                                   CreatedUser = e.KaydedenKullaniciId,
                                   UpdatedDate = e.DuzenlemeTarihi,
                                   UpdatedUser = e.DuzenleyenKullaniciId,
                                   Image1 = e.Gorsel1,
                                   ImageThumbnail1 = e.GorselThumbnail1,
                                   Image2 = e.Gorsel2,
                                   ImageThumbnail2 = e.GorselThumbnail2,
                                   Image3 = e.Gorsel3,
                                   ImageThumbnail3 = e.GorselThumbnail3,
                                   Image4 = e.Gorsel4,
                                   ImageThumbnail4 = e.GorselThumbnail4,
                               }).OrderByDescending(p => p.StartDate).Take(1).Distinct().ToList();
            //}).Take(1).Distinct().ToList();
            foreach (var item in etkinlikler)
            {
                var newData = new EventsType();
                newData.Id = item.Id;
                newData.Title = item.Title;
                newData.Description = item.Description;
                newData.Content = item.Content;
                newData.StartDate = item.StartDate != null ? item.StartDate.Value.ToShortDateString() + " " + item.StartDate.Value.ToShortTimeString() : "";
                newData.EndDate = item.EndDate != null ? item.EndDate.Value.ToShortDateString() + " " + item.EndDate.Value.ToShortTimeString() : " ";
                newData.State = item.State;
                newData.ReadCount = item.ReadCount;
                newData.Keywords = item.Keywords;
                newData.CreatedDate = item.CreatedDate.ToShortDateString() + " " + item.CreatedDate.ToShortTimeString();
                newData.CreatedUser = item.CreatedUser;
                newData.UpdatedDate = item.UpdatedDate != null ? item.UpdatedDate.Value.ToShortDateString() + " " + item.UpdatedDate.Value.ToShortTimeString() : "";
                newData.UpdatedUser = item.UpdatedUser != null ? Convert.ToInt32(item.UpdatedUser) : 0;
                newData.Image1 = item.Image1 != null
                       ? item.Image1.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail1 = item.ImageThumbnail1 != null
                       ? item.ImageThumbnail1.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.Image2 = item.Image2 != null
                       ? item.Image2.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail2 = item.ImageThumbnail2 != null
                       ? item.ImageThumbnail2.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.Image3 = item.Image3 != null
                       ? item.Image3.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail3 = item.ImageThumbnail3 != null
                       ? item.ImageThumbnail3.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.Image4 = item.Image4 != null
                       ? item.Image4.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";
                newData.ImageThumbnail4 = item.ImageThumbnail4 != null
                       ? item.ImageThumbnail4.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png";

                returnData.Add(newData);
            }
            return returnData;
        }
    }
}
