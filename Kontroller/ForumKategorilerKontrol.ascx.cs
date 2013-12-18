using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class ForumKategorilerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["KategoriId"] != null)
                    {
                        ForumKonulariVer(Convert.ToInt32(Request.QueryString["KategoriId"]));
                    }
                }
                else
                {
                    ForumKategorileriVer();
                }
            }
        }

        private void ForumKategorileriVer()
        {
            // Kategoriler ver
            MultiView1.ActiveViewIndex = 0;
            var FList = new List<ForumKategorilerList>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FKList = Veriler.ForumKategorileri.Where(p => p.DilId == DilId && p.Durum).ToList();
            foreach (var Item in FKList)
            {
                ForumKategorilerList FKL = new ForumKategorilerList();
                FKL.Id = Item.Id;
                FKL.KategoriAdi = Item.KategoriAdi;
                FKL.Aciklama = Item.Aciklama;
                FKL.KonuSayisi = KonuSayisiVer(Item.Id);
                FKL.MesajSayisi = MesajSayisiVer(Item.Id);
                FKL.SonMesaj = SonMesajVer(Item.Id);
                FList.Add(FKL);
            }
            ListViewVeriler.DataSource = FList;
            ListViewVeriler.DataBind();
            LiteralSiteMap.Text = "<a href=\"../Forumlar.aspx\">Tüm Forumlar</a>";
        }

        private int KonuSayisiVer(int ForumKategoriId)
        {
            // Konu sayısı ver
            return Veriler.ForumKonulari.Where(p => p.ForumKategoriId == ForumKategoriId).Count();
        }

        private int MesajSayisiVer(int ForumKategoriId)
        {
            // Mesaj sayısı ver
            int Sayi = 0;
            var FKList = (from FK in Veriler.ForumKonulari
                          join FM in Veriler.ForumMesajlari
                              on FK.Id equals FM.ForumKonuId
                          where FK.ForumKategoriId == ForumKategoriId
                          select FK).Count();
            if (FKList != 0)
            {
                Sayi = FKList;
            }
            return Sayi;
        }

        private int SayfaSayisiVer(int ForumKonuId)
        {
            // Mesaj sayısı ver
            int Sayi = 0;
            var FMList = (from FM in Veriler.ForumMesajlari
                          where FM.ForumKonuId == ForumKonuId
                          select FM).Count();
            if (FMList != 0)
            {
                Sayi = Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(FMList)/6)));
            }
            return Sayi;
        }

        private string SonMesajVer(int ForumKategoriId)
        {
            // Son mesajı ver

            #region Konular

            var Konular = (from FK in Veriler.ForumKonulari
                           join K in Veriler.Kullanicilar
                               on FK.KaydedenKullaniciId equals K.Id
                           where FK.ForumKategoriId == ForumKategoriId
                           orderby FK.KaydetmeTarihi descending
                           select new
                                      {
                                          Konu = FK.Baslik,
                                          KonuId = FK.Id,
                                          Ekleyen = K.Ad + " " + K.Soyad,
                                          Tarih = FK.KaydetmeTarihi,
                                      }).FirstOrDefault();

            #endregion

            #region Mesajlar

            var Mesajlar = (from FM in Veriler.ForumMesajlari
                            join FK in Veriler.ForumKonulari
                                on FM.ForumKonuId equals FK.Id
                            join K in Veriler.Kullanicilar
                                on FM.KaydedenKullaniciId equals K.Id
                            where FK.ForumKategoriId == ForumKategoriId
                            orderby FM.KaydetmeTarihi descending
                            select new
                                       {
                                           Konu = FK.Baslik,
                                           KonuId = FK.Id,
                                           Ekleyen = K.Ad + " " + K.Soyad,
                                           Tarih = FM.KaydetmeTarihi,
                                       }).FirstOrDefault();

            #endregion

            # region  Konular Ve Mesajlar

            if (Konular != null && Mesajlar != null)
            {
                #region KonuTarihiBüyükse

                if (Konular.Tarih > Mesajlar.Tarih)
                {
                    if (Konular.Konu.Length > 25)
                    {
                        return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                               + Konular.KonuId
                               + "\">"
                               +
                               Konular.Konu.Substring(0, 25)
                               +
                               " ...  <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br> "
                               + "<div class=\"listtarih\">"
                               + Konular.Ekleyen
                               + " - "
                               + Konular.Tarih
                               + "</div>";
                    }
                    else
                    {
                        return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                               + Konular.KonuId
                               + "\">"
                               +
                               Konular.Konu
                               +
                               " <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br> "
                               + "<div class=\"listtarih\">"
                               + Konular.Ekleyen
                               + " - "
                               + Konular.Tarih
                               + "</div>";
                    }
                }
                    #endregion
                    #region KonuTarihiKüçükse

                else
                {
                    if (Mesajlar.Konu.Length > 25)
                    {
                        return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                               + Mesajlar.KonuId
                               + "&Sayfa="
                               + SayfaSayisiVer(Mesajlar.KonuId)
                               + "\">"
                               +
                               Mesajlar.Konu.Substring(0, 25)
                               +
                               " ...  <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br> "
                               + "<div class=\"listtarih\">"
                               + Mesajlar.Ekleyen
                               + " - "
                               + Mesajlar.Tarih
                               + "</div>";
                    }
                    else
                    {
                        return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                               + Mesajlar.KonuId
                               + "\">"
                               +
                               Mesajlar.Konu
                               +
                               " <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br> "
                               + "<div class=\"listtarih\">"
                               + Mesajlar.Ekleyen
                               + " - "
                               + Mesajlar.Tarih
                               + "</div>";
                    }
                }

                #endregion
            }
                # endregion
                # region  Konular

            else if (Konular != null && Mesajlar == null)
            {
                if (Konular.Konu.Length > 25)
                {
                    return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                           + Konular.KonuId
                           + "\">"
                           +
                           Konular.Konu.Substring(0, 25)
                           +
                           " ...  <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br> "
                           + "<div class=\"listtarih\">"
                           + Konular.Ekleyen
                           + " - "
                           + Konular.Tarih
                           + "</div>";
                }
                else
                {
                    return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                           + Konular.KonuId
                           + "\">"
                           +
                           Konular.Konu
                           +
                           " <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br> "
                           + "<div class=\"listtarih\">"
                           + Konular.Ekleyen
                           + " - "
                           + Konular.Tarih
                           + "</div>";
                }
            }
                # endregion
                # region İkiside Null ise

            else
            {
                return "Mesaj yok.";
            }

            # endregion
        }

        private void ForumKonulariVer(int ForumKategoriId)
        {
            // Konuları ver
            MultiView1.ActiveViewIndex = 1;
            var FList = new List<ForumKonularList>();
            var FKList = Veriler.ForumKonulari.Where(p => p.ForumKategoriId == ForumKategoriId && p.Durum).ToList();
            foreach (var Item in FKList)
            {
                ForumKonularList FL = new ForumKonularList();
                FL.Id = Item.Id;
                FL.Baslik = Item.Baslik;
                FL.Baslatan = KullaniciAdiSoyadiVer(Item.KaydedenKullaniciId);
                FL.CevapSayisi = CevapSayisiVer(Item.Id);
                FL.OkunmaSayisi = Item.OkunmaSayisi;
                FL.SonMesaj = FormSonMesaj(Item.Id);
                FList.Add(FL);
            }
            ListViewKonular.DataSource = FList;
            ListViewKonular.DataBind();
            LiteralSiteMap.Text = "<a href=\"../Forumlar.aspx\">Tüm Forumlar</a> / ";
            LiteralSiteMap.Text
                += "<a href=\"../Forumlar.aspx?KategoriId="
                   + ForumKategoriId
                   + "\">"
                   + Veriler.ForumKategorileri.Where(p => p.Id == ForumKategoriId).FirstOrDefault().KategoriAdi
                   + "</a>";
        }

        private string KullaniciAdiSoyadiVer(int KaydedenKullaniciId)
        {
            // Kullanısı bilgisini ver
            Kullanicilar K = Veriler.Kullanicilar.Where(p => p.Id == KaydedenKullaniciId).FirstOrDefault();
            return K.Ad + " " + K.Soyad;
        }

        private int CevapSayisiVer(int ForumKonuId)
        {
            // Cevap sayısını ver
            return Veriler.ForumMesajlari.Where(p => p.ForumKonuId == ForumKonuId).Count();
        }

        private string FormSonMesaj(int ForumKonuId)
        {
            // Son mesajı ver
            var FMList = (from FM in Veriler.ForumMesajlari
                          join K in Veriler.Kullanicilar
                              on FM.KaydedenKullaniciId equals K.Id
                          where FM.ForumKonuId == ForumKonuId
                          orderby FM.KaydetmeTarihi descending
                          select new
                                     {
                                         Ekleyen = K.Ad + " " + K.Soyad,
                                         Tarih = FM.KaydetmeTarihi,
                                     }).FirstOrDefault();
            if (FMList != null)
            {
                return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                       + ForumKonuId
                       + "&Sayfa="
                       + SayfaSayisiVer(ForumKonuId)
                       + "\">"
                       + FMList.Ekleyen
                       +
                       " <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br>"
                       + "<div class=\"listtarih\">"
                       + FMList.Tarih
                       + "</div>";
            }
            else
            {
                var F = (from FK in Veriler.ForumKonulari
                         join K in Veriler.Kullanicilar
                             on FK.KaydedenKullaniciId equals K.Id
                         where FK.Id == ForumKonuId
                         orderby FK.KaydetmeTarihi descending
                         select new
                                    {
                                        KonuId = FK.Id,
                                        Ekleyen = K.Ad + " " + K.Soyad,
                                        Tarih = FK.KaydetmeTarihi,
                                    }).FirstOrDefault();
                return "<a href=\"../Portal/ForumDetay.aspx?KonuId="
                       + F.KonuId
                       + "&Sayfa="
                       + SayfaSayisiVer(F.KonuId)
                       + "\">"
                       + F.Ekleyen
                       +
                       " <img style=\"vertical-align:middle;\" src=\"../App_Themes/MainTheme/Images/mesajagit.png\" /></a></br>"
                       + "<div class=\"listtarih\">"
                       + F.Tarih
                       + "</div>";
            }
        }
    }
}