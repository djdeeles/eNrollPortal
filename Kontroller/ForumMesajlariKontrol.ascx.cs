using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class ForumMesajlariKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ForumSonMesajlariVer();
            }
        }

        private void ForumSonMesajlariVer()
        {
            // Son mesajları ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FMAnaList = new List<ForumAnaSayfaMesajlarList>();
            var FMList = (from FM in Veriler.ForumMesajlari
                          join FKO in Veriler.ForumKonulari
                              on FM.ForumKonuId equals FKO.Id
                          join FKA in Veriler.ForumKategorileri
                              on FKO.ForumKategoriId equals FKA.Id
                          join K in Veriler.Kullanicilar
                              on FM.KaydedenKullaniciId equals K.Id
                          where FKA.Durum
                                && FKA.DilId == DilId
                                && FKO.Durum
                                && FM.Durum
                          orderby FM.KaydetmeTarihi descending
                          select new
                                     {
                                         KonuId = FKO.Id,
                                         MesajId = FM.Id,
                                         KonuBaslik = FKO.Baslik.Substring(0, 50),
                                         Tarih = FM.KaydetmeTarihi,
                                         KaydedenKullanici = K.Ad + "<br/>" + K.Soyad,
                                     }).Distinct().Take(20).ToList();
            foreach (var Item in FMList)
            {
                ForumAnaSayfaMesajlarList FAML = new ForumAnaSayfaMesajlarList();
                FAML.KonuId = Item.KonuId;
                FAML.KonuBaslik = Item.KonuBaslik;
                FAML.Link = "Portal/ForumDetay.aspx?KonuId=" + Item.KonuId + "&Sayfa=" +
                            SayfaSayisiVer(Item.KonuId, Item.MesajId);
                FAML.Tarih = Item.Tarih;
                FAML.KaydedenKullanici = Item.KaydedenKullanici;
                FMAnaList.Add(FAML);
            }
            var FMAnaListSort = new List<ForumAnaSayfaMesajlarList>();
            FMAnaListSort = FMAnaList.OrderByDescending(p => p.Tarih).ToList();
            ListViewVeriler.DataSource = FMAnaListSort;
            ListViewVeriler.DataBind();
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

        private int SayfaSayisiVer(int ForumKonuId, int MesajId)
        {
            // Mesaj sayısı ver
            int Sayi = 0;
            var FMList = (from FM in Veriler.ForumMesajlari
                          where FM.ForumKonuId == ForumKonuId
                          select FM).Count();

            var MList =
                Veriler.ForumMesajlari.Where(p => p.ForumKonuId == ForumKonuId).OrderBy(p => p.KaydetmeTarihi).ToList();
            int MesajIdIndex = 0;
            for (int i = 0; i <= MList.Count - 1; i++)
            {
                if (MList[i].Id == MesajId)
                {
                    MesajIdIndex = i + 1;
                }
            }
            if (FMList != 0)
            {
                Sayi = Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(MesajIdIndex)/6)));
            }
            return Sayi;
        }
    }
}