using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;
using Telerik.Web.UI;

namespace EnrollPortal.Kontroller
{
    public partial class IlanlarVeDuyurularListKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IlanlariVeDuyurulariVer();
            }
        }

        private void IlanlariVeDuyurulariVer()
        {
            // İlanları veya duyuruları ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IDList = new List<IlanlarVeDuyurularList>();
            var IList = (from I in Veriler.Ilanlar
                         join IT in Veriler.IlanlarTablosu
                             on I.Id equals IT.IlanId
                         join IK in Veriler.IlanKategorileri
                             on IT.KategoriId equals IK.Id
                         join K in Veriler.Kullanicilar
                             on I.KaydedenKullaniciId equals K.Id
                         where I.Durum
                               && IK.Durum
                               && IK.DilId == DilId
                         orderby I.BaslangicTarihi descending
                         select new
                                    {
                                        I.Id,
                                        I.Baslik,
                                        I.Ozet,
                                        Tarih = I.KaydetmeTarihi,
                                        Ekleyen = K.Ad + " " + K.Soyad,
                                        Resim =
                             I.GorselThumbnail1 != null
                                 ? I.GorselThumbnail1.Replace("~/", "")
                                 : "App_Themes/MainTheme/Images/noimage.png",
                                    }).Distinct().ToList();
            foreach (var Item in IList)
            {
                IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                ID.Id = Item.Id;
                ID.Baslik = Item.Baslik;
                ID.Ozet = Item.Ozet;
                ID.Tarih = Item.Tarih;
                ID.Ekleyen = "<b>Ekleyen: </b>" + Item.Ekleyen;
                ID.ResimThumbnail = Item.Resim;
                ID.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + Item.Id;
                IDList.Add(ID);
            }
            var PDList = (from PD in Veriler.PersonelDuyurular
                          join PDT in Veriler.PersonelDuyurularTablosu
                              on PD.Id equals PDT.DuyuruId
                          join PDK in Veriler.PersonelDuyuruKategorileri
                              on PDT.KategoriId equals PDK.Id
                          join K in Veriler.Kullanicilar
                              on PD.KaydedenKullaniciId equals K.Id
                          where PD.Durum
                                && PDK.Durum
                                && PDK.DilId == DilId
                          orderby PD.BaslangicTarihi descending
                          select new
                                     {
                                         PD.Id,
                                         PD.Baslik,
                                         PD.Ozet,
                                         Tarih = PD.KaydetmeTarihi,
                                         Ekleyen = K.Ad + " " + K.Soyad,
                                         ResimThumbnail =
                              PD.GorselThumbnail1 != null
                                  ? PD.GorselThumbnail1.Replace("~/", "")
                                  : "App_Themes/MainTheme/Images/noimage.png",
                                     }).Distinct().ToList();
            foreach (var Item in PDList)
            {
                IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                ID.Id = Item.Id;
                ID.Baslik = Item.Baslik;
                ID.Ozet = Item.Ozet;
                ID.Tarih = Item.Tarih;
                ID.Ekleyen = "<b>Ekleyen: </b>" + Item.Ekleyen;
                ID.ResimThumbnail = Item.ResimThumbnail;
                ID.Url = "IlanVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                IDList.Add(ID);
            }
            RadGridVeriler.DataSource = IDList;
            RadGridVeriler.DataBind();
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            IlanlariVeDuyurulariVer();
            RadGridVeriler.PageSize += 10;
            RadGridVeriler.Rebind();
        }
    }
}