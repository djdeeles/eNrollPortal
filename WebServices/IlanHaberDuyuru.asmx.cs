using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using EnrollPortal.Kutuphaneler.DataModels;
using EnrollPortal.Kutuphaneler;

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
    public class IlanHaberDuyuru : WebService
    {
        public const int DilId = 1;
        public SqlConnection OConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["EnrollPortalConnectionString"].ToString());

        private EnrollPortalEntities Veriler = new EnrollPortalEntities();

        #region Login
        [WebMethod]
        public List<Kullanicilar> LoginUser(string username, string password)
        {
            var entities = new EnrollPortalEntities();
            try
            {
                var user =
                    entities.Kullanicilar.Where(
                        p => (p.TCNo == username || p.EPosta == username) && p.Parola == password).ToList();
                if (user.Count > 0) return user;
            }
            catch (Exception)
            {
            }
            return null;
        }

        #endregion

        #region Kurumdan Tüm Haberler Ve Duyurular
        [WebMethod]
        public List<HaberlerVeDuyurularList> HaberleriVeDuyurulariVer(int itemCount, int pageCount)
        {
            #region Haberler
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
                             H.Icerik,
                             Tarih = H.KaydetmeTarihi,
                             ResimThumbnail =
                  H.GorselThumbnail1 != null
                      ? H.GorselThumbnail1.Replace("~/", "")
                      : "App_Themes/MainTheme/Images/noimage.png",
                             ResimOriginal =
                  H.Gorsel1 != null ? H.Gorsel1.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png",
                         }).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
            foreach (var Item in HList)
            {
                var haberlerVeDuyurularList = new HaberlerVeDuyurularList();
                haberlerVeDuyurularList.Id = Item.Id;
                haberlerVeDuyurularList.Baslik = Item.Baslik.Length > 40 ? Item.Baslik.Substring(0, 40) + " ..." : Item.Baslik;
                haberlerVeDuyurularList.Ozet = Item.Ozet;
                haberlerVeDuyurularList.ResimThumbnail = Item.ResimThumbnail;
                haberlerVeDuyurularList.ResimOriginal = Item.ResimOriginal;
                haberlerVeDuyurularList.Icerik = Item.Icerik;
                haberlerVeDuyurularList.Tarih = Item.Tarih;
                haberlerVeDuyurularList.Url = "HaberVeDuyuruDetay.aspx?HaberCode=" + Item.Id;
                HDList.Add(haberlerVeDuyurularList);
            }
            #endregion

            #region Duyurular
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
                             D.Icerik,
                             Tarih = D.KaydetmeTarihi,
                             ResimThumbnail =
                  D.GorselThumbnail1 != null
                      ? D.GorselThumbnail1.Replace("~/", "")
                      : "App_Themes/MainTheme/Images/noimage.png",
                             ResimOriginal =
                  D.Gorsel1 != null ? D.Gorsel1.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png"
                         }).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
            foreach (var Item in DList)
            {
                HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                HD.Id = Item.Id;
                HD.Baslik = Item.Baslik.Length > 40 ? Item.Baslik.Substring(0, 40) + " ..." : Item.Baslik;
                HD.Ozet = Item.Ozet;
                HD.Icerik = Item.Icerik;
                HD.Tarih = Item.Tarih;
                HD.ResimThumbnail = Item.ResimThumbnail;
                HD.ResimOriginal = Item.ResimOriginal;
                HD.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                HDList.Add(HD);
            }
            #endregion

            return HDList;
        }
        #endregion

        #region Personelden Tüm İlanlar Ve Duyurular
        [WebMethod]
        public List<IlanlarVeDuyurularList> IlanlariVeDuyurulariVer(int itemCount, int pageCount)
        {
            // İlanları veya duyuruları ver 
            #region İlanlar
            var idList = new List<IlanlarVeDuyurularList>();
            var list = (from ilanlar in Veriler.Ilanlar
                        join ilanlarTablosu in Veriler.IlanlarTablosu
                            on ilanlar.Id equals ilanlarTablosu.IlanId
                        join ilanKategorileri in Veriler.IlanKategorileri
                            on ilanlarTablosu.KategoriId equals ilanKategorileri.Id
                        join kullanicilar in Veriler.Kullanicilar
                            on ilanlar.KaydedenKullaniciId equals kullanicilar.Id
                        where ilanlar.Durum
                              && ilanKategorileri.Durum
                              && ilanKategorileri.DilId == DilId
                        orderby ilanlar.BaslangicTarihi descending

                        select new
                        {
                            ilanlar.Id,
                            ilanlar.Baslik,
                            ilanlar.Ozet,
                            ilanlar.Icerik,
                            Tarih = ilanlar.KaydetmeTarihi,
                            Ekleyen = kullanicilar.Ad + " " + kullanicilar.Soyad,
                            ResimThumbnail =
                 ilanlar.GorselThumbnail1 != null
                     ? ilanlar.GorselThumbnail1.Replace("~/", "")
                     : "App_Themes/MainTheme/Images/noimage.png",
                            ResimOriginal =
                 ilanlar.Gorsel1 != null ? ilanlar.Gorsel1.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png",
                        }).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
            foreach (var item in list)
            {
                var ilanlarVeDuyurularList = new IlanlarVeDuyurularList();
                ilanlarVeDuyurularList.Id = item.Id;
                ilanlarVeDuyurularList.Baslik = item.Baslik;
                ilanlarVeDuyurularList.Ozet = item.Ozet;
                ilanlarVeDuyurularList.Icerik = item.Icerik;
                ilanlarVeDuyurularList.Tarih = item.Tarih;
                ilanlarVeDuyurularList.Ekleyen = "<b>Ekleyen: </b>" + item.Ekleyen;
                ilanlarVeDuyurularList.ResimThumbnail = item.ResimThumbnail;
                ilanlarVeDuyurularList.ResimOriginal = item.ResimOriginal;
                ilanlarVeDuyurularList.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + item.Id;
                idList.Add(ilanlarVeDuyurularList);
            }
            #endregion

            #region Duyurular
            var pdList = (from personelDuyurular in Veriler.PersonelDuyurular
                          join personelDuyurularTablosu in Veriler.PersonelDuyurularTablosu
                              on personelDuyurular.Id equals personelDuyurularTablosu.DuyuruId
                          join personelDuyuruKategorileri in Veriler.PersonelDuyuruKategorileri
                              on personelDuyurularTablosu.KategoriId equals personelDuyuruKategorileri.Id
                          join K in Veriler.Kullanicilar
                              on personelDuyurular.KaydedenKullaniciId equals K.Id
                          where personelDuyurular.Durum
                                && personelDuyuruKategorileri.Durum
                                && personelDuyuruKategorileri.DilId == DilId
                          orderby personelDuyurular.BaslangicTarihi descending
                          select new
                          {
                              personelDuyurular.Id,
                              personelDuyurular.Baslik,
                              personelDuyurular.Ozet,
                              personelDuyurular.Icerik,
                              Tarih = personelDuyurular.KaydetmeTarihi,
                              Ekleyen = K.Ad + " " + K.Soyad,
                              ResimThumbnail =
                   personelDuyurular.GorselThumbnail1 != null
                       ? personelDuyurular.GorselThumbnail1.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png",
                              ResimOriginal =
                   personelDuyurular.Gorsel1 != null
                       ? personelDuyurular.Gorsel1.Replace("~/", "")
                       : "App_Themes/MainTheme/Images/noimage.png",
                          }).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
            foreach (var item in pdList)
            {
                var ilanlarVeDuyurularList = new IlanlarVeDuyurularList();
                ilanlarVeDuyurularList.Id = item.Id;
                ilanlarVeDuyurularList.Baslik = item.Baslik;
                ilanlarVeDuyurularList.Ozet = item.Ozet;
                ilanlarVeDuyurularList.Icerik = item.Icerik;
                ilanlarVeDuyurularList.Tarih = item.Tarih;
                ilanlarVeDuyurularList.Ekleyen = "<b>Ekleyen: </b>" + item.Ekleyen;
                ilanlarVeDuyurularList.ResimThumbnail = item.ResimThumbnail;
                ilanlarVeDuyurularList.ResimOriginal = item.ResimOriginal;
                ilanlarVeDuyurularList.Url = "IlanVeDuyuruDetay.aspx?DuyuruCode=" + item.Id;
                idList.Add(ilanlarVeDuyurularList);
            }
            #endregion

            return idList;
        }
        #endregion

        #region Kurumdan Kategoriler

        [WebMethod]
        public List<DuyuruKategorileri> KurumdanDuyuruListesiniVer(int itemCount, int pageCount)
        {
            var duyuruKategorileriList =
                Veriler.DuyuruKategorileri.Where(p => p.Durum && p.DilId == DilId)
                    .OrderBy(p => p.SiraNo).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
            return duyuruKategorileriList;
        }

        [WebMethod]
        public List<HaberKategorileri> KurumdanHaberListesiniVer(int itemCount, int pageCount)
        {
            var haberKategorileriList =
                Veriler.HaberKategorileri.Where(p => p.Durum && p.DilId == DilId)
                    .OrderBy(p => p.SiraNo).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
            return haberKategorileriList;
        }

        #endregion

        #region Personelden Kategoriler

        [WebMethod]
        public List<IlanKategorileri> PersoneldenIlanListesiniVer(int itemCount, int pageCount)
        {
            var ilanKategorileriList = Veriler.IlanKategorileri.Where(p => p.Durum && p.DilId == DilId)
                .OrderBy(p => p.SiraNo).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();

            return ilanKategorileriList;
        }

        [WebMethod]
        public List<PersonelDuyuruKategorileri> PersoneldenDuyuruListesiniVer(int itemCount, int pageCount)
        {
            var duyuruKategorileriList = Veriler.PersonelDuyuruKategorileri.Where(p => p.Durum && p.DilId == DilId)
                .OrderBy(p => p.SiraNo).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
            return duyuruKategorileriList;
        }

        #endregion

        #region Kurumdan Duyuru ve Haberler
        [WebMethod]
        public List<HaberlerVeDuyurularList> KurumdanHaberler(int itemCount, int pageCount,
                                                                                      int KategoriId, String Type)
        {
            var haberlerVeDuyurularLists = new List<HaberlerVeDuyurularList>();
            if (Type == "0")
            {
                #region Tüm Haberler
                var hList = (from haberler in Veriler.Haberler
                             join haberlerTablosu in Veriler.HaberlerTablosu
                                 on haberler.Id equals haberlerTablosu.HaberId
                             join haberKategorileri in Veriler.HaberKategorileri
                                 on haberlerTablosu.KategoriId equals haberKategorileri.Id
                             where haberler.Durum
                                   && haberKategorileri.Durum
                                   && haberKategorileri.DilId == DilId
                             orderby haberler.KayitTarihi descending
                             select new
                             {
                                 haberler.Id,
                                 haberler.Baslik,
                                 haberler.Ozet,
                                 haberler.Icerik,
                                 Tarih = haberler.KayitTarihi,
                                 ResimThumbnail =
                      haberler.GorselThumbnail1 != null
                          ? haberler.GorselThumbnail1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                                 ResimOriginal =
                      haberler.Gorsel1 != null
                          ? haberler.Gorsel1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                                 haberler
                             }).Skip(itemCount * pageCount).Take(itemCount).ToList();
                foreach (var Item in hList)
                {
                    var haberlerVeDuyurularList = new HaberlerVeDuyurularList();
                    haberlerVeDuyurularList.Id = Item.Id;
                    haberlerVeDuyurularList.Baslik = Item.Baslik;
                    haberlerVeDuyurularList.Ozet = Item.Ozet;
                    haberlerVeDuyurularList.Icerik = Item.Icerik;
                    haberlerVeDuyurularList.Tarih = Item.Tarih.Value;
                    haberlerVeDuyurularList.ResimThumbnail = Item.ResimThumbnail;
                    haberlerVeDuyurularList.ResimOriginal = Item.ResimOriginal;
                    haberlerVeDuyurularList.Url = "HaberVeDuyuruDetay.aspx?HaberCode=" + Item.Id;
                    haberlerVeDuyurularLists.Add(haberlerVeDuyurularList);
                }
                #endregion
            }
            else if (Type == "2" && KategoriId != 0) // Seçilen Kategorinin İlanları
            {
                #region Seçilen Kategorinin Haberlerini Ver
                var hList = (from haberler in Veriler.Haberler
                             join haberlerTablosu in Veriler.HaberlerTablosu
                                 on haberler.Id equals haberlerTablosu.HaberId
                             join haberKategorileri in Veriler.HaberKategorileri
                                 on haberlerTablosu.KategoriId equals haberKategorileri.Id
                             where haberler.Durum
                                   && haberKategorileri.Durum
                                   && haberKategorileri.DilId == DilId
                                   && haberKategorileri.Id == KategoriId
                             orderby haberler.KayitTarihi descending
                             select new
                             {
                                 haberler.Id,
                                 haberler.Baslik,
                                 haberler.Ozet,
                                 haberler.Icerik,
                                 Tarih = haberler.KayitTarihi,
                                 ResimThumbnail =
                      haberler.GorselThumbnail1 != null
                          ? haberler.GorselThumbnail1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                                 ResimOriginal =
                      haberler.Gorsel1 != null
                          ? haberler.Gorsel1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png"
                             }).Skip(itemCount * pageCount).Take(itemCount).ToList();
                foreach (var Item in hList)
                {
                    var haberlerVeDuyurularList = new HaberlerVeDuyurularList();
                    haberlerVeDuyurularList.Id = Item.Id;
                    haberlerVeDuyurularList.Baslik = Item.Baslik;
                    haberlerVeDuyurularList.Icerik = Item.Icerik;
                    haberlerVeDuyurularList.Ozet = Item.Ozet;
                    haberlerVeDuyurularList.Tarih = Item.Tarih.Value;
                    haberlerVeDuyurularList.ResimThumbnail = Item.ResimThumbnail;
                    haberlerVeDuyurularList.ResimOriginal = Item.ResimOriginal;
                    haberlerVeDuyurularList.Url = "HaberVeDuyuruDetay.aspx?HaberCode=" + Item.Id;
                    haberlerVeDuyurularLists.Add(haberlerVeDuyurularList);
                }
                #endregion
            }
            return haberlerVeDuyurularLists;
        }

        [WebMethod]
        public List<HaberlerVeDuyurularList> KurumdanDuyurular(int itemCount, int pageCount,
                                                                                       int KategoriId, String Type, String userId)
        {
            var haberlerVeDuyurularLists = new List<HaberlerVeDuyurularList>();
            if (Type == "0")
            {
                #region Tüm Duyrular
                var dList = (from duyurular in Veriler.Duyurular
                             join duyurularTablosu in Veriler.DuyurularTablosu
                                 on duyurular.Id equals duyurularTablosu.DuyuruId
                             join duyuruKategorileri in Veriler.DuyuruKategorileri
                                 on duyurularTablosu.KategoriId equals duyuruKategorileri.Id
                             where duyurular.Durum
                                   && duyuruKategorileri.Durum
                                   && duyuruKategorileri.DilId == DilId
                             orderby duyurular.BaslangicTarihi descending
                             select new
                             {
                                 duyurular.Id,
                                 duyurular.Baslik,
                                 duyurular.Ozet,
                                 duyurular.Icerik,
                                 Tarih = duyurular.BaslangicTarihi,
                                 ResimThumbnail =
                      (duyurular.GorselThumbnail1 != null && !string.IsNullOrEmpty(duyurular.GorselThumbnail1))
                          ? duyurular.GorselThumbnail1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                                 ResimOriginal =
                      duyurular.Gorsel1 != null
                          ? duyurular.Gorsel1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                             }).OrderByDescending(p => p.Tarih).Skip(itemCount * pageCount).Take(itemCount).
                    Distinct().ToList();
                foreach (var item in dList)
                {
                    var haberlerVeDuyurularList = new HaberlerVeDuyurularList();
                    haberlerVeDuyurularList.Id = item.Id;
                    haberlerVeDuyurularList.Baslik = item.Baslik;
                    haberlerVeDuyurularList.Ozet = item.Ozet;
                    haberlerVeDuyurularList.Icerik = item.Icerik;
                    haberlerVeDuyurularList.Tarih = item.Tarih.Value;
                    haberlerVeDuyurularList.ResimThumbnail = item.ResimThumbnail;
                    haberlerVeDuyurularList.ResimOriginal = item.ResimOriginal;
                    haberlerVeDuyurularList.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + item.Id;
                    haberlerVeDuyurularLists.Add(haberlerVeDuyurularList);
                }
                #endregion
            }
            else if (Type == "2" && KategoriId != 0) // Seçilen Kategorinin İlanları
            {
                #region Seçilen Kategorinin Duyrularını Ver

                var dList = (from duyurular in Veriler.Duyurular
                             join duyurularTablosu in Veriler.DuyurularTablosu
                                 on duyurular.Id equals duyurularTablosu.DuyuruId
                             join duyuruKategorileri in Veriler.DuyuruKategorileri
                                 on duyurularTablosu.KategoriId equals duyuruKategorileri.Id
                             where duyurular.Durum
                                   && duyuruKategorileri.Durum
                                   && duyuruKategorileri.DilId == DilId
                                   && duyuruKategorileri.Id == KategoriId
                             orderby duyurular.BaslangicTarihi descending
                             select new
                             {
                                 duyurular.Id,
                                 duyurular.Baslik,
                                 duyurular.Ozet,
                                 duyurular.Icerik,
                                 Tarih = duyurular.BaslangicTarihi,
                                 ResimThumbnail =
                      (duyurular.GorselThumbnail1 != null && !string.IsNullOrEmpty(duyurular.GorselThumbnail1))
                          ? duyurular.GorselThumbnail1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                                 ResimOriginal =
                      duyurular.Gorsel1 != null
                          ? duyurular.Gorsel1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                             }).OrderByDescending(p => p.Tarih).Skip(itemCount * pageCount).Take(itemCount).
                    Distinct().ToList();
                foreach (var item in dList)
                {
                    var haberlerVeDuyurularList = new HaberlerVeDuyurularList();
                    haberlerVeDuyurularList.Id = item.Id;
                    haberlerVeDuyurularList.Baslik = item.Baslik;
                    haberlerVeDuyurularList.Ozet = item.Ozet;
                    haberlerVeDuyurularList.Icerik = item.Icerik;
                    haberlerVeDuyurularList.Tarih = item.Tarih.Value;
                    haberlerVeDuyurularList.ResimThumbnail = item.ResimThumbnail;
                    haberlerVeDuyurularList.ResimOriginal = item.ResimOriginal;
                    haberlerVeDuyurularList.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + item.Id;
                    haberlerVeDuyurularLists.Add(haberlerVeDuyurularList);
                }
                #endregion
            }
            return haberlerVeDuyurularLists;
        }

        #endregion

        #region Personelden İlan ve Duyurular

        [WebMethod]
        public List<IlanlarVeDuyurularList> PersoneldenIlanlar(int itemCount, int pageCount,
                                                                                       int KategoriId, String Type, String userId)
        {
            var ilanlarVeDuyurularLists = new List<IlanlarVeDuyurularList>();

            if (Type == "0") //Tüm İlanlar
            {
                #region Tüm İlanlar

                var ilanList = (from ilanlar in Veriler.Ilanlar
                                join ilanlarTablosu in Veriler.IlanlarTablosu
                                    on ilanlar.Id equals ilanlarTablosu.IlanId
                                //where ilanlar.Durum
                                orderby ilanlar.BaslangicTarihi descending
                                select new
                                {
                                    ilanlar.Id,
                                    ilanlar.Baslik,
                                    ilanlar.Ozet,
                                    ilanlar.Icerik,
                                    Tarih = ilanlar.BaslangicTarihi,
                                    Ekleyen = "",
                                    ResimThumbnail =
                         ilanlar.GorselThumbnail1 != null
                             ? ilanlar.GorselThumbnail1.Replace("~/", "")
                             : "App_Themes/MainTheme/Images/noimage.png",
                                    ResimOriginal =
                         ilanlar.Gorsel1 != null
                             ? ilanlar.Gorsel1.Replace("~/", "")
                             : "App_Themes/MainTheme/Images/noimage.png",
                                }).Skip(itemCount * pageCount).Take(itemCount).ToList();
                                //}).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
                foreach (var item in ilanList)
                {
                    var ilanlarVeDuyurularList = new IlanlarVeDuyurularList();
                    ilanlarVeDuyurularList.Id = item.Id;
                    ilanlarVeDuyurularList.Baslik = item.Baslik;
                    ilanlarVeDuyurularList.Ozet = item.Ozet;
                    ilanlarVeDuyurularList.Icerik = item.Icerik;
                    ilanlarVeDuyurularList.Tarih = item.Tarih.Value;
                    ilanlarVeDuyurularList.Ekleyen = item.Ekleyen;
                    ilanlarVeDuyurularList.ResimThumbnail = item.ResimThumbnail;
                    ilanlarVeDuyurularList.ResimOriginal = item.ResimOriginal;
                    ilanlarVeDuyurularList.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + item.Id;
                    ilanlarVeDuyurularLists.Add(ilanlarVeDuyurularList);
                }

                #endregion
            }
            else if (Type == "1") //İlanlarım
            {
                #region İlanlarım
                if (!string.IsNullOrEmpty(userId))
                {
                    var userid = Convert.ToInt32(userId);
                    var ilanList = (from ilanlar in Veriler.Ilanlar
                                    join ilanlarTablosu in Veriler.IlanlarTablosu
                                        on ilanlar.Id equals ilanlarTablosu.IlanId
                                    join kullanicilar in Veriler.Kullanicilar
                                        on ilanlar.KaydedenKullaniciId equals kullanicilar.Id
                                    where ilanlar.Durum && ilanlar.KaydedenKullaniciId == userid
                                    orderby ilanlar.BaslangicTarihi descending
                                    select new
                                    {
                                        ilanlar.Id,
                                        ilanlar.Baslik,
                                        ilanlar.Ozet,
                                        ilanlar.Icerik,
                                        Tarih = ilanlar.BaslangicTarihi,
                                        Ekleyen = kullanicilar.Ad + " " + kullanicilar.Soyad,
                                        ResimThumbnail =
                             ilanlar.GorselThumbnail1 != null
                                 ? ilanlar.GorselThumbnail1.Replace("~/", "")
                                 : "App_Themes/MainTheme/Images/noimage.png",
                                        ResimOriginal =
                             ilanlar.Gorsel1 != null
                                 ? ilanlar.Gorsel1.Replace("~/", "")
                                 : "App_Themes/MainTheme/Images/noimage.png",
                                    //}).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
                                    }).Skip(itemCount * pageCount).Take(itemCount).ToList();
                    foreach (var item in ilanList)
                    {
                        var ilanlarVeDuyurularList = new IlanlarVeDuyurularList();
                        ilanlarVeDuyurularList.Id = item.Id;
                        ilanlarVeDuyurularList.Baslik = item.Baslik;
                        ilanlarVeDuyurularList.Ozet = item.Ozet;
                        ilanlarVeDuyurularList.Icerik = item.Icerik;
                        ilanlarVeDuyurularList.Tarih = item.Tarih.Value;
                        ilanlarVeDuyurularList.Ekleyen = item.Ekleyen;
                        ilanlarVeDuyurularList.ResimThumbnail = item.ResimThumbnail;
                        ilanlarVeDuyurularList.ResimOriginal = item.ResimOriginal;
                        ilanlarVeDuyurularList.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + item.Id;
                        ilanlarVeDuyurularLists.Add(ilanlarVeDuyurularList);
                    }
                }
                #endregion
            }
            else if (Type == "2" && KategoriId != 0)// Kategorinin ilanları
            {
                #region Seçilen Kategorinin İlanları

                var ilanList = (from ilanlar in Veriler.Ilanlar
                                join ilanlarTablosu in Veriler.IlanlarTablosu
                                    on ilanlar.Id equals ilanlarTablosu.IlanId
                                join ilanKategorileri in Veriler.IlanKategorileri
                                    on ilanlarTablosu.KategoriId equals ilanKategorileri.Id
                                join kullanicilar in Veriler.Kullanicilar
                                    on ilanlar.KaydedenKullaniciId equals kullanicilar.Id
                                where ilanlar.Durum
                                      && ilanKategorileri.Durum
                                      && ilanKategorileri.DilId == DilId
                                      && ilanKategorileri.Id == KategoriId
                                orderby ilanlar.BaslangicTarihi descending
                                select new
                                {
                                    ilanlar.Id,
                                    ilanlar.Baslik,
                                    ilanlar.Ozet,
                                    ilanlar.Icerik,
                                    Tarih = ilanlar.BaslangicTarihi,
                                    Ekleyen = kullanicilar.Ad + " " + kullanicilar.Soyad,
                                    ResimThumbnail =
                         ilanlar.GorselThumbnail1 != null
                             ? ilanlar.GorselThumbnail1.Replace("~/", "")
                             : "App_Themes/MainTheme/Images/noimage.png",
                                    ResimOriginal =
                         ilanlar.Gorsel1 != null
                             ? ilanlar.Gorsel1.Replace("~/", "")
                             : "App_Themes/MainTheme/Images/noimage.png",
                                }).Skip(itemCount * pageCount).Take(itemCount).ToList();
                                //}).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
                foreach (var Item in ilanList)
                {
                    var ilanlarVeDuyurularList = new IlanlarVeDuyurularList();
                    ilanlarVeDuyurularList.Id = Item.Id;
                    ilanlarVeDuyurularList.Baslik = Item.Baslik;
                    ilanlarVeDuyurularList.Ozet = Item.Ozet;
                    ilanlarVeDuyurularList.Icerik = Item.Icerik;
                    ilanlarVeDuyurularList.Tarih = Item.Tarih.Value;
                    ilanlarVeDuyurularList.Ekleyen = Item.Ekleyen;
                    ilanlarVeDuyurularList.ResimThumbnail = Item.ResimThumbnail;
                    ilanlarVeDuyurularList.ResimOriginal = Item.ResimOriginal;
                    ilanlarVeDuyurularList.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + Item.Id;
                    ilanlarVeDuyurularLists.Add(ilanlarVeDuyurularList);
                }

                #endregion
            }

            return ilanlarVeDuyurularLists;
        }
        [WebMethod]
        public List<HaberlerVeDuyurularList> PersoneldenDuyurular(int itemCount, int pageCount,
                                                                                          int KategoriId, string Type, String userId)
        {
            var haberlerVeDuyurularLists = new List<HaberlerVeDuyurularList>();
            int DilId = 1;
            if (Type == "0")
            {
                #region Tüm Duyurular
                var DList = (from D in Veriler.PersonelDuyurular
                             join DT in Veriler.PersonelDuyurularTablosu
                                 on D.Id equals DT.DuyuruId
                             where D.Durum
                             orderby D.BaslangicTarihi descending
                             select new
                             {
                                 D.Id,
                                 D.Baslik,
                                 D.Ozet,
                                 D.Icerik,
                                 Tarih = D.BaslangicTarihi,
                                 ResimThumbnail =
                      D.GorselThumbnail1 != null
                          ? D.GorselThumbnail1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                                 ResimOriginal =
                      D.Gorsel1 != null
                          ? D.Gorsel1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                             }).OrderByDescending(p => p.Tarih).Skip(itemCount * pageCount).Take(itemCount)
                        .Distinct().ToList();
                foreach (var Item in DList)
                {
                    HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                    HD.Id = Item.Id;
                    HD.Baslik = Item.Baslik;
                    HD.Ozet = Item.Ozet;
                    HD.Icerik = Item.Icerik;
                    HD.Tarih = Item.Tarih.Value;
                    HD.ResimThumbnail = Item.ResimThumbnail;
                    HD.ResimOriginal = Item.ResimOriginal;
                    HD.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                    haberlerVeDuyurularLists.Add(HD);
                }
                #endregion
            }
            else if (Type == "1")
            {
                #region Duyurularım
                if (!string.IsNullOrEmpty(userId))
                {
                    var userid = Convert.ToInt32(userId);
                    var dList = (from personelDuyurular in Veriler.PersonelDuyurular
                                 join personelDuyurularTablosu in Veriler.PersonelDuyurularTablosu
                                     on personelDuyurular.Id equals personelDuyurularTablosu.DuyuruId
                                 where personelDuyurular.Durum && personelDuyurular.KaydedenKullaniciId == userid
                                 orderby personelDuyurular.BaslangicTarihi descending
                                 select new
                                 {
                                     personelDuyurular.Id,
                                     personelDuyurular.Baslik,
                                     personelDuyurular.Ozet,
                                     personelDuyurular.Icerik,
                                     Tarih = personelDuyurular.BaslangicTarihi,
                                     ResimThumbnail =
                          personelDuyurular.GorselThumbnail1 != null
                              ? personelDuyurular.GorselThumbnail1.Replace("~/", "")
                              : "App_Themes/MainTheme/Images/noimage.png",
                                     ResimOriginal =
                          personelDuyurular.Gorsel1 != null
                              ? personelDuyurular.Gorsel1.Replace("~/", "")
                              : "App_Themes/MainTheme/Images/noimage.png",
                                 }).OrderByDescending(p => p.Tarih).Skip(itemCount * pageCount).Take(itemCount)
                        .Distinct().ToList();
                    foreach (var item in dList)
                    {
                        var haberlerVeDuyurularList = new HaberlerVeDuyurularList();
                        haberlerVeDuyurularList.Id = item.Id;
                        haberlerVeDuyurularList.Baslik = item.Baslik;
                        haberlerVeDuyurularList.Ozet = item.Ozet;
                        haberlerVeDuyurularList.Icerik = item.Icerik;
                        haberlerVeDuyurularList.Tarih = item.Tarih.Value;
                        haberlerVeDuyurularList.ResimThumbnail = item.ResimThumbnail;
                        haberlerVeDuyurularList.ResimOriginal = item.ResimOriginal;
                        haberlerVeDuyurularList.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + item.Id;
                        haberlerVeDuyurularLists.Add(haberlerVeDuyurularList);
                    }
                }
                #endregion
            }
            else
            {
                #region Seçilen Kategorinin duyuruları
                var DList = (from D in Veriler.PersonelDuyurular
                             join DT in Veriler.PersonelDuyurularTablosu
                                 on D.Id equals DT.DuyuruId
                             join DK in Veriler.PersonelDuyuruKategorileri
                                 on DT.KategoriId equals DK.Id
                             where D.Durum
                                   && DK.Durum
                                   && DK.DilId == DilId
                                   && DK.Id == KategoriId
                             orderby D.BaslangicTarihi descending
                             select new
                             {
                                 D.Id,
                                 D.Baslik,
                                 D.Ozet,
                                 D.Icerik,
                                 Tarih = D.BaslangicTarihi,
                                 ResimThumbnail =
                      D.GorselThumbnail1 != null
                          ? D.GorselThumbnail1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                                 ResimOriginal =
                      D.Gorsel1 != null
                          ? D.Gorsel1.Replace("~/", "")
                          : "App_Themes/MainTheme/Images/noimage.png",
                             }).OrderByDescending(p => p.Tarih).Skip(itemCount * pageCount).Take(itemCount)
                        .Distinct().ToList();
                foreach (var Item in DList)
                {
                    HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                    HD.Id = Item.Id;
                    HD.Baslik = Item.Baslik;
                    HD.Ozet = Item.Ozet;
                    HD.Icerik = Item.Icerik;
                    HD.Tarih = Item.Tarih.Value;
                    HD.ResimThumbnail = Item.ResimThumbnail;
                    HD.ResimOriginal = Item.ResimOriginal;
                    HD.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                    haberlerVeDuyurularLists.Add(HD);
                }

                #endregion
            }
            return haberlerVeDuyurularLists;
        }
        #endregion

    }
}

