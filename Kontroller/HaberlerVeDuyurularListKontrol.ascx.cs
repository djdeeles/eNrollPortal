using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;
using Telerik.Web.UI;

namespace EnrollPortal.Kontroller
{
    public partial class HaberlerVeDuyurularListKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HaberleriVeDuyurulariVer();
            }
        }

        private void HaberleriVeDuyurulariVer()
        {
            // Haberleri ve duyuruları ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HDList = new List<HaberlerVeDuyurularList>();
            var HList = (from H in Veriler.Haberler
                         join HT in Veriler.HaberlerTablosu
                             on H.Id equals HT.HaberId
                         join HK in Veriler.HaberKategorileri
                             on HT.KategoriId equals HK.Id
                         where H.Durum
                               && HK.Durum
                               && HK.DilId == DilId
                         orderby H.KayitTarihi descending
                         select new
                                    {
                                        H.Id,
                                        H.Baslik,
                                        H.Ozet,
                                        Tarih = H.KaydetmeTarihi,
                                        Resim =
                             H.GorselThumbnail1 != null
                                 ? H.GorselThumbnail1.Replace("~/", "")
                                 : "App_Themes/MainTheme/Images/noimage.png",
                                    }).Distinct().ToList();
            foreach (var Item in HList)
            {
                HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                HD.Id = Item.Id;
                if (Item.Baslik.Length > 40)
                {
                    HD.Baslik = Item.Baslik.Substring(0, 40) + " ...";
                }
                else
                {
                    HD.Baslik = Item.Baslik;
                }
                HD.Ozet = Item.Ozet;
                HD.Resim = Item.Resim;
                HD.Tarih = Item.Tarih;
                HD.Url = "HaberVeDuyuruDetay.aspx?HaberCode=" + Item.Id;
                HDList.Add(HD);
            }
            var DList = (from D in Veriler.Duyurular
                         join DT in Veriler.DuyurularTablosu
                             on D.Id equals DT.DuyuruId
                         join DK in Veriler.DuyuruKategorileri
                             on DT.KategoriId equals DK.Id
                         where D.Durum
                               && DK.Durum
                               && DK.DilId == DilId
                         orderby D.BaslangicTarihi descending
                         select new
                                    {
                                        D.Id,
                                        D.Baslik,
                                        D.Ozet,
                                        Tarih = D.KaydetmeTarihi,
                                        Resim =
                             D.GorselThumbnail1 != null
                                 ? D.GorselThumbnail1.Replace("~/", "")
                                 : "App_Themes/MainTheme/Images/noimage.png",
                                    }).Distinct().ToList();
            foreach (var Item in DList)
            {
                HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                HD.Id = Item.Id;
                if (Item.Baslik.Length > 40)
                {
                    HD.Baslik = Item.Baslik.Substring(0, 40) + " ...";
                }
                else
                {
                    HD.Baslik = Item.Baslik;
                }
                HD.Ozet = Item.Ozet;
                HD.Tarih = Item.Tarih;
                HD.Resim = Item.Resim;
                HD.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                HDList.Add(HD);
            }
            RadGridVeriler.DataSource = HDList;
            RadGridVeriler.DataBind();
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            HaberleriVeDuyurulariVer();
            RadGridVeriler.PageSize += 10;
            RadGridVeriler.Rebind();
        }
    }
}