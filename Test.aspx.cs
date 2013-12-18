using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.WebServices;
using EnrollPortal.WebServices.Types;

namespace EnrollPortal
{
    public partial class Test : System.Web.UI.Page
    {
        public SqlConnection OConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EnrollPortalConnectionString"].ToString());
        EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            gvTumAlbumler.DataSource = GetEvents(20, 0);
            gvTumAlbumler.DataBind();
        }

        class test
        {
            public String title;
            public int id;
            public string startTime;
            //public string endTime;
        }

        List<test> GetEvents(int itemCount, int pageCount)
        {
            var veriler = new EnrollPortalEntities();
            var data = new List<test>();
            var etkinlikler = veriler.Etkinlikler.ToList();
            foreach (var item in etkinlikler)
            {
                var t = new test();
                t.id = item.Id;
                t.title= item.Ad;
                t.startTime = item.BaslangicTarihi.Value.ToShortDateString();
                
                data.Add(t);
            }
            return data;
            /*
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
                                   //StartDate = e.BaslangicTarihi.Value.ToShortDateString() + " " + e.BaslangicTarihi.Value.ToShortTimeString(),
                                   EndDate = e.BitisTarihi,
                                   //EndDate = e.BitisTarihi.Value.ToShortDateString() + " " + e.BitisTarihi.Value.ToShortTimeString(),
                                   State = e.Durum,
                                   ReadCount = e.OkunmaSayisi,
                                   Keywords = e.AnahtarKelimeler,
                                   CreatedDate = e.KaydetmeTarihi,
                                   //CreatedDate = e.KaydetmeTarihi.ToShortDateString() + " " + e.KaydetmeTarihi.ToShortTimeString(),
                                   CreatedUser = e.KaydedenKullaniciId,
                                   UpdatedDate = e.DuzenlemeTarihi,
                                   //UpdatedDate = e.DuzenlemeTarihi.Value.ToShortDateString() + " " + e.DuzenlemeTarihi.Value.ToShortTimeString(),
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
                newData.StartDate = item.StartDate.ToString();
                newData.EndDate = item.EndDate.ToString();
                newData.State = item.State;
                newData.ReadCount = item.ReadCount;
                newData.Keywords = item.Keywords;
                newData.CreatedDate = item.CreatedDate.ToString();
                newData.CreatedUser = item.CreatedUser;
                newData.UpdatedDate = item.UpdatedDate.ToString();
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
            return returnData;*/
        }
    } 
}