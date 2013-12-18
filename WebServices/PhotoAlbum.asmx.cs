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
    public class PhotoAlbum : WebService
    {

        public SqlConnection OConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EnrollPortalConnectionString"].ToString());
        EnrollPortalEntities Veriler = new EnrollPortalEntities();
         
        #region Personel Albümleri

        [WebMethod]
        public List<PhotoGallery> PersonelAlbumleri(int itemCount, int pageCount, int kategoriId, String type, String userId)
        {
            var returnData = new List<PhotoGallery>();

            if (type == "0")
            {
                #region Personelden Tüm Albümler
                var albumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    join K in Veriler.Kullanicilar
                                        on p1.KaydedenKullaniciId equals K.Id
                                    where p.DilId == 1
                                            && p.Durum
                                            && p1.Durum
                                            && p.PersonelKategorisiMi
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                    {
                                        Id = p1.Id,
                                        fakAdi = p.FotoAlbumKategoriAdi,
                                        fAdi = p1.FotoAlbumAdi,
                                        kAd = K.Ad,
                                        kSoyad = K.Soyad,
                                        createdUser = K.Id,
                                        faKaydetmeTarihi = p1.KaydetmeTarihi,
                                        Ekleyen = "",
                                        faAciklama = p1.Aciklama,
                                        ResimThumbnail =
                                        p1.GorselThumbnail ?? "/App_Themes/MainTheme/Images/noimage.png",
                                        ResimOriginal = p1.Gorsel != null ? p1.Gorsel.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png"
                                    }).OrderByDescending(p => p.faKaydetmeTarihi).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();

                foreach (var item in albumlerList)
                {
                    var newData = new PhotoGallery();
                    newData.Id = item.Id.ToString();
                    newData.CategoryName = item.fakAdi;
                    newData.AlbumName = item.fAdi;
                    newData.Aciklama = item.faAciklama;
                    newData.ResimThumbnail = item.ResimThumbnail != null
                           ? item.ResimThumbnail.Replace("~/", "")
                           : "App_Themes/MainTheme/Images/noimage.png";
                    newData.ResimOriginal = item.ResimOriginal != null
                           ? item.ResimOriginal.Replace("~/", "")
                           : "App_Themes/MainTheme/Images/noimage.png";
                    newData.CreatedUser = item.createdUser.ToString();
                    newData.KAd = item.kAd;
                    newData.KSoyad = item.kSoyad;
                    newData.KaydedilmeTarihi = item.faKaydetmeTarihi.ToString();
                    returnData.Add(newData);
                }

                #endregion
            }
            else if (type == "1")
            {
                #region Tüm Albümlerim
                if(!string.IsNullOrEmpty(userId))
                {
                    var userid = Convert.ToInt32(userId);
                    var albumlerList = (from fotoAlbumKategorileri in Veriler.FotoAlbumKategorileri
                                        join fotoAlbumler in Veriler.FotoAlbumler
                                            on fotoAlbumKategorileri.Id equals fotoAlbumler.FotoAlbumKategoriId
                                        join kullanicilar in Veriler.Kullanicilar
                                            on fotoAlbumler.KaydedenKullaniciId equals kullanicilar.Id
                                        where fotoAlbumKategorileri.DilId == 1
                                                && fotoAlbumKategorileri.Durum
                                                && fotoAlbumler.Durum
                                                && fotoAlbumKategorileri.PersonelKategorisiMi
                                                && fotoAlbumler.KaydedenKullaniciId == userid
                                        orderby fotoAlbumKategorileri.FotoAlbumKategoriAdi ascending
                                        select new
                                        {
                                            Id = fotoAlbumler.Id,
                                            fakAdi = fotoAlbumKategorileri.FotoAlbumKategoriAdi,
                                            fAdi = fotoAlbumler.FotoAlbumAdi,
                                            kAd = kullanicilar.Ad,
                                            kSoyad = kullanicilar.Soyad,
                                            createdUser = kullanicilar.Id,
                                            faKaydetmeTarihi = fotoAlbumler.KaydetmeTarihi,
                                            Ekleyen = "",
                                            faAciklama = fotoAlbumler.Aciklama,
                                            ResimThumbnail =
                                            fotoAlbumler.GorselThumbnail ?? "/App_Themes/MainTheme/Images/noimage.png",
                                            ResimOriginal = fotoAlbumler.Gorsel != null ? fotoAlbumler.Gorsel.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png"
                                        }).OrderByDescending(p => p.faKaydetmeTarihi).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();

                    foreach (var item in albumlerList)
                    {
                        var newData = new PhotoGallery();
                        newData.Id = item.Id.ToString();
                        newData.CategoryName = item.fakAdi;
                        newData.AlbumName = item.fAdi;
                        newData.Aciklama = item.faAciklama;
                        newData.ResimThumbnail = item.ResimThumbnail != null
                               ? item.ResimThumbnail.Replace("~/", "")
                               : "App_Themes/MainTheme/Images/noimage.png";
                        newData.ResimOriginal = item.ResimOriginal != null
                               ? item.ResimOriginal.Replace("~/", "")
                               : "App_Themes/MainTheme/Images/noimage.png";
                        newData.CreatedUser = item.createdUser.ToString();
                        newData.KAd = item.kAd;
                        newData.KSoyad = item.kSoyad;
                        newData.KaydedilmeTarihi = item.faKaydetmeTarihi.ToString();
                        returnData.Add(newData);
                    } 
                }
                
                #endregion
            }
            else if (type == "2" && kategoriId != 0)
            {
                #region Seçilen Kategorinin Albümleri
                var albumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    join K in Veriler.Kullanicilar
                                        on p1.KaydedenKullaniciId equals K.Id
                                    where p.DilId == 1
                                          && p.Durum
                                          && p1.Durum
                                          && p.PersonelKategorisiMi
                                          && p1.FotoAlbumKategoriId == kategoriId
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                    {
                                        Id = p1.Id,
                                        fakAdi = p.FotoAlbumKategoriAdi,
                                        fAdi = p1.FotoAlbumAdi,
                                        kAd = K.Ad,
                                        kSoyad = K.Soyad,
                                        createdUser = K.Id,
                                        faKaydetmeTarihi = p1.KaydetmeTarihi,
                                        Ekleyen = "",
                                        faAciklama = p1.Aciklama,
                                        faGorsel =
                                        p1.GorselThumbnail ?? "App_Themes/MainTheme/Images/noimage.png",
                                        ResimOriginal = p1.Gorsel != null ? p1.Gorsel.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png"
                                    }).OrderByDescending(p => p.faKaydetmeTarihi).Skip(itemCount * pageCount).Take(itemCount).Distinct().ToList();
                if (albumlerList.Count != 0)
                {
                    foreach (var item in albumlerList)
                    {
                        var newData = new PhotoGallery();
                        newData.Id = item.Id.ToString();
                        newData.CategoryName = item.fakAdi;
                        newData.AlbumName = item.fAdi;
                        newData.Aciklama = item.faAciklama;
                        newData.ResimThumbnail = item.faGorsel;
                        newData.ResimOriginal = item.ResimOriginal;
                        newData.CreatedUser = item.createdUser.ToString();
                        newData.KAd = item.kAd;
                        newData.KSoyad = item.kSoyad;
                        newData.KaydedilmeTarihi = item.faKaydetmeTarihi.ToString();
                        returnData.Add(newData);
                    }
                }
                #endregion
            }
             
            return returnData;
        }

        #endregion

        #region Kurum Albümleri
        [WebMethod]
        public List<PhotoGallery> KurumAlbumleri(int itemCount, int pageCount, int kategoriId, String type)
        {
            var returnData = new List<PhotoGallery>();
            if (type == "0")
            {
                #region Kurumdan Tüm Albümler
                var albumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    join K in Veriler.Kullanicilar
                                        on p1.KaydedenKullaniciId equals K.Id
                                    where p.DilId == 1
                                          && p.Durum
                                          && p1.Durum
                                          && p.PersonelKategorisiMi == false
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                    {
                                        Id = p1.Id,
                                        fakAdi = p.FotoAlbumKategoriAdi,
                                        fAdi = p1.FotoAlbumAdi,
                                        kAd = K.Ad,
                                        kSoyad = K.Soyad,
                                        createdUser = K.Id,
                                        faKaydetmeTarihi = p1.KaydetmeTarihi,
                                        Ekleyen = "",
                                        faAciklama = p1.Aciklama,
                                        ResimThumbnail =
                             p1.GorselThumbnail ?? "/App_Themes/MainTheme/Images/noimage.png",
                                        ResimOriginal = p1.Gorsel != null ? p1.Gorsel.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png"
                                    }).OrderByDescending(p => p.faKaydetmeTarihi).Skip(itemCount * pageCount).
                   Take(itemCount).Distinct().ToList();
                //.ToList();
                if (albumlerList.Count != 0)
                {
                    foreach (var item in albumlerList)
                    {
                        var newData = new PhotoGallery();
                        newData.Id = item.Id.ToString();
                        newData.CategoryName = item.fakAdi;
                        newData.AlbumName = item.fAdi;
                        newData.Aciklama = item.faAciklama;
                        newData.ResimThumbnail = item.ResimThumbnail;
                        newData.ResimOriginal = item.ResimOriginal;
                        newData.CreatedUser = item.createdUser.ToString();
                        newData.KAd = item.kAd;
                        newData.KSoyad = item.kSoyad;
                        newData.KaydedilmeTarihi = item.faKaydetmeTarihi.ToString();
                        returnData.Add(newData);
                    }
                }
                #endregion
            }
            else if (type == "2" && kategoriId != 0)
            {
                #region Seçilen Kategorinin Albümleri
                var albumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    join K in Veriler.Kullanicilar
                                        on p1.KaydedenKullaniciId equals K.Id
                                    where p.DilId == 1
                                          && p.Durum
                                          && p1.Durum
                                          && p.PersonelKategorisiMi == false
                                          && p1.FotoAlbumKategoriId == kategoriId
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                    {
                                        Id = p1.Id,
                                        fakAdi = p.FotoAlbumKategoriAdi,
                                        fAdi = p1.FotoAlbumAdi,
                                        kAd = K.Ad,
                                        kSoyad = K.Soyad,
                                        createdUser = K.Id,
                                        faKaydetmeTarihi = p1.KaydetmeTarihi,
                                        Ekleyen = "",
                                        faAciklama = p1.Aciklama,
                                        ResimThumbnail =
                             p1.GorselThumbnail ?? "/App_Themes/MainTheme/Images/noimage.png",
                                        ResimOriginal = p1.Gorsel != null ? p1.Gorsel.Replace("~/", "") : "App_Themes/MainTheme/Images/noimage.png"
                                    }).OrderByDescending(p => p.faKaydetmeTarihi).Skip(itemCount * pageCount).
                   Take(itemCount).Distinct().ToList();
                //.ToList();
                if (albumlerList.Count != 0)
                {
                    foreach (var item in albumlerList)
                    {
                        var newData = new PhotoGallery();
                        newData.Id = item.Id.ToString();
                        newData.CategoryName = item.fakAdi;
                        newData.AlbumName = item.fAdi;
                        newData.Aciklama = item.faAciklama;
                        newData.ResimThumbnail = item.ResimThumbnail;
                        newData.ResimOriginal = item.ResimOriginal;
                        newData.CreatedUser = item.createdUser.ToString();
                        newData.KAd = item.kAd;
                        newData.KSoyad = item.kSoyad;
                        newData.KaydedilmeTarihi = item.faKaydetmeTarihi.ToString();
                        returnData.Add(newData);
                    }
                }
                #endregion
            }
            return returnData;
        }
        #endregion

        #region Personelden Album Kategorileri

        [WebMethod]
        public List<FotoAlbumKategorileri> PersoneldenAlbumKategorileriniVer()
        {
            // Kategorileri ver 
            var fakList = Veriler.FotoAlbumKategorileri.Where(p => p.Durum && p.DilId == 1 && p.PersonelKategorisiMi).ToList();

            return fakList;

        }

        #endregion
         
        #region Kurumdan Album Kategori

        [WebMethod]
        public List<FotoAlbumKategorileri> KurumdanAlbumKategorileriniVer()
        {
            // Kategorileri ver 
            var fakList = Veriler.FotoAlbumKategorileri.Where(p => p.Durum && p.DilId == 1 && p.PersonelKategorisiMi == false).ToList();

            return fakList;

        }
        #endregion

        #region Seçilen albümün fotoğraflarını çek
        [WebMethod]
        public List<FotoAlbumGorselleri> AlbumFotolariniVer(int albumId)
        {
            var albumGorselleri = Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == albumId && p.Durum).OrderByDescending(p => p.KaydetmeTarihi).ToList();

            return albumGorselleri;
        }

        #endregion

        public class PhotoGallery
        {
            public string Id { get; set; }
            public string CategoryName { get; set; }
            public string AlbumName { get; set; }
            public string Aciklama { get; set; }
            public string Resim { get; set; }
            public string ResimThumbnail { get; set; }
            public string ResimOriginal { get; set; }
            public string CreatedUser { get; set; }
            public string KAd { get; set; }
            public string KSoyad { get; set; }
            public string KaydedilmeTarihi { get; set; }
        }

    }
}
