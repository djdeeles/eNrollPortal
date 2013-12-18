using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class AramaKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KategorileriVer();
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Kategori"] != null)
                    {
                        if (Request.QueryString["Code"] != null)
                        {
                            AramaSonuclariVer(Request.QueryString["Kategori"], Request.QueryString["Code"]);
                            TextBoxAra.Text = EnrollArama.QueryStringeTersCevir(Request.QueryString["Code"]);
                        }
                    }
                    else
                    {
                        if (Request.QueryString["Code"] != null)
                        {
                            AramaSonuclariVer("0", Request.QueryString["Code"]);
                            TextBoxAra.Text = EnrollArama.QueryStringeTersCevir(Request.QueryString["Code"]);
                        }
                    }
                }
            }
        }

        private void MenulerdeArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            List<Menuler> MenulerList = null;
            string Sql = "it.DilId = " + DilId + " and it.Durum = True and it.MenuTipId = 3";
            foreach (string AK in ArananKelimeler)
            {
                Sql += " and (it.Aciklama like '%"
                       + EnrollArama.QueryStringeTersCevir(AK)
                       + "%' or it.AnahtarKelimeler like '%"
                       + EnrollArama.QueryStringeTersCevir(AK)
                       + "%' or it.Icerik like '%"
                       + EnrollArama.QueryStringeTersCevir(AK)
                       + "%' or it.MenuAdi like '%"
                       + EnrollArama.QueryStringeTersCevir(AK)
                       + "%' or it.Ozet like '%"
                       + EnrollArama.QueryStringeTersCevir(AK)
                       + "%')";
            }
            MenulerList = Veriler.Menuler.Where(Sql).ToList();
            if (MenulerList.Count != 0)
            {
                Arama MenulerListArama = new Arama();
                MenulerListArama.Ara =
                    "<div class=\"searchtitle\">İçerik bölümünde "
                    + MenulerList.Count.ToString()
                    + " adet sonuç bulunmuştur.</div>";
                AramaList.Add(MenulerListArama);
            }
            foreach (Menuler M in MenulerList)
            {
                Arama A = new Arama();
                A.Ara = "<table class=\"searchresult\">"
                        + "<tr><td><div class=\"searchresulttitle\">"
                        + "<a href=\"Sayfa.aspx?Code="
                        + M.Id
                        + "\">"
                        + M.MenuAdi
                        + "</a></div><div class=\"searchresultozet\">"
                        + M.Ozet
                        + "</div></td></tr></table>";

                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void HaberlerdeArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var HaberlerList = new List<Haberler>();
            string S =
                "select H.Id as Id, H.Baslik as Baslik, H.Ozet as Ozet, H.GorselThumbnail1 as GorselThumbnail1 from HaberKategorileri as HK join HaberlerTablosu as HT on HK.Id==HT.KategoriId join Haberler as H on HT.HaberId==H.Id where HK.DilId==" +
                DilId + " and HK.Durum==True and H.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( H.AnahtarKelimeler like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or H.Baslik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or H.Ozet like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or H.Icerik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Haberler H = new Haberler();
                H.Id = Convert.ToInt32(item["Id"].ToString());
                H.Baslik = item["Baslik"].ToString();
                H.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    H.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    H.GorselThumbnail1 = null;
                }
                HaberlerList.Add(H);
            }
            if (HaberlerList.Count != 0)
            {
                Arama HaberlerListArama = new Arama();
                HaberlerListArama.Ara =
                    "<div class=\"searchtitle\">Haberler bölümünde "
                    + HaberlerList.Count.ToString()
                    + " adet sonuç bulunmuştur.</div>";
                AramaList.Add(HaberlerListArama);
            }
            foreach (Haberler H in HaberlerList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(H.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + H.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + H.Baslik
                        + "\"/></div>"
                        + "<div class=\"searchresulttitle\"><a href=\""
                        + "/HaberVeDuyuruDetay.aspx?HaberCode="
                        + H.Id
                        + "\">"
                        + H.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + H.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"><a href=\""
                        + "/HaberVeDuyuruDetay.aspx?HaberCode="
                        + H.Id
                        + "\">"
                        + H.Baslik
                        + "</a></div><divclass=\"searchresultozet\">"
                        + H.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void DuyurulardaArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var DuyurularList = new List<Duyurular>();
            string S =
                "select D.Id as Id, D.Baslik as Baslik, D.Ozet as Ozet, D.GorselThumbnail1 as GorselThumbnail1 from DuyuruKategorileri as DK join DuyurularTablosu as DT on DK.Id==DT.KategoriId join Duyurular as D on DT.DuyuruId==D.Id where DK.DilId==" +
                DilId + " and DK.Durum==True and D.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( D.AnahtarKelimeler like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or D.Baslik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or D.Ozet like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or D.Icerik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Duyurular D = new Duyurular();
                D.Id = Convert.ToInt32(item["Id"].ToString());
                D.Baslik = item["Baslik"].ToString();
                D.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    D.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    D.GorselThumbnail1 = null;
                }
                DuyurularList.Add(D);
            }
            if (DuyurularList.Count != 0)
            {
                Arama DuyurularListArama = new Arama();
                DuyurularListArama.Ara =
                    "<div class=\"searchtitle\">Duyurular bölümünde "
                    + DuyurularList.Count.ToString()
                    + " adet sonuç bulunmuştur.</div>";
                AramaList.Add(DuyurularListArama);
            }
            foreach (Duyurular D in DuyurularList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(D.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + D.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + D.Baslik
                        + "\"/></div><div class=\"searchresulttitle\"> <a href=\""
                        + "/HaberVeDuyuruDetay.aspx?DuyuruCode="
                        + D.Id
                        + "\">"
                        + D.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + D.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/HaberVeDuyuruDetay.aspx?DuyuruCode="
                        + D.Id
                        + "\">"
                        + D.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + D.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void PersonelDuyurulardaArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var PersonelDuyurularList = new List<PersonelDuyurular>();
            string S =
                "select PD.Id as Id, PD.Baslik as Baslik, PD.Ozet as Ozet, PD.GorselThumbnail1 as GorselThumbnail1 from PersonelDuyuruKategorileri as PDK join PersonelDuyurularTablosu as PDT on PDK.Id==PDT.KategoriId join PersonelDuyurular as PD on PDT.DuyuruId==PD.Id where PDK.DilId==" +
                DilId + " and PDK.Durum==True and PD.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( PD.AnahtarKelimeler like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or PD.Baslik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or PD.Ozet like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or PD.Icerik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                PersonelDuyurular PD = new PersonelDuyurular();
                PD.Id = Convert.ToInt32(item["Id"].ToString());
                PD.Baslik = item["Baslik"].ToString();
                PD.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    PD.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    PD.GorselThumbnail1 = null;
                }
                PersonelDuyurularList.Add(PD);
            }
            if (PersonelDuyurularList.Count != 0)
            {
                Arama PersonelDuyurularListArama = new Arama();
                PersonelDuyurularListArama.Ara =
                    "<div class=\"searchtitle\">Personel duyuruları bölümünde "
                    + PersonelDuyurularList.Count.ToString()
                    + " adet sonuç bulunmuştur.</div>";
                AramaList.Add(PersonelDuyurularListArama);
            }
            foreach (PersonelDuyurular PD in PersonelDuyurularList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(PD.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + PD.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + PD.Baslik
                        + "\"/></div><div class=\"searchresulttitle\"><a href=\""
                        + "/IlanVeDuyuruDetay.aspx?DuyuruCode="
                        + PD.Id
                        + "\">"
                        + PD.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + PD.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/IlanVeDuyuruDetay.aspx?DuyuruCode="
                        + PD.Id
                        + "\">"
                        + PD.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + PD.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void IlanlardaArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var IlanlarList = new List<Ilanlar>();
            string S =
                "select I.Id as Id, I.Baslik as Baslik, I.Ozet as Ozet, I.GorselThumbnail1 as GorselThumbnail1 from IlanKategorileri as IK join IlanlarTablosu as IT on IK.Id==IT.KategoriId join Ilanlar as I on IT.IlanId==I.Id where IK.DilId==" +
                DilId + " and IK.Durum==True and I.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( I.AnahtarKelimeler like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or I.Baslik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or I.Ozet like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or I.Icerik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Ilanlar I = new Ilanlar();
                I.Id = Convert.ToInt32(item["Id"].ToString());
                I.Baslik = item["Baslik"].ToString();
                I.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    I.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    I.GorselThumbnail1 = null;
                }
                IlanlarList.Add(I);
            }
            if (IlanlarList.Count != 0)
            {
                Arama IlanlarListArama = new Arama();
                IlanlarListArama.Ara =
                    "<div class=\"searchtitle\">İlanlar bölümünde "
                    + IlanlarList.Count.ToString()
                    + " adet sonuç bulunmuştur.</div>";
                AramaList.Add(IlanlarListArama);
            }
            foreach (Ilanlar I in IlanlarList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(I.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + I.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + I.Baslik
                        + "\"/></div><div class=\"searchresulttitle\"> <a href=\""
                        + "/IlanVeDuyuruDetay.aspx?DuyuruCode="
                        + I.Id
                        + "\">"
                        + I.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + I.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/IlanVeDuyuruDetay.aspx?DuyuruCode="
                        + I.Id
                        + "\">"
                        + I.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + I.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void EtkinliklerdeArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var EtkinliklerList = new List<Etkinlikler>();
            string S =
                "select E.Id as Id, E.Ad as Ad, E.Ozet as Ozet, E.GorselThumbnail1 as GorselThumbnail1 from EtkinlikKategorileri as EK join Etkinlikler as E on EK.Id==E.EtkinlikKategoriId where EK.DilId==" +
                DilId + " and EK.Durum==True and E.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( E.AnahtarKelimeler like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or E.Ad like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or E.Ozet like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%' or E.Icerik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Etkinlikler E = new Etkinlikler();
                E.Id = Convert.ToInt32(item["Id"].ToString());
                E.Ad = item["Ad"].ToString();
                E.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    E.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    E.GorselThumbnail1 = null;
                }
                EtkinliklerList.Add(E);
            }
            if (EtkinliklerList.Count != 0)
            {
                Arama EtkinliklerListArama = new Arama();
                EtkinliklerListArama.Ara =
                    "<div class=\"searchtitle\">Etkinlikler bölümünde " + EtkinliklerList.Count.ToString() +
                    " adet sonuç bulunmuştur.</div>";
                AramaList.Add(EtkinliklerListArama);
            }
            foreach (Etkinlikler E in EtkinliklerList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(E.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + E.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + E.Ad
                        + "\"/></div><div class=\"searchresulttitle\"> <a href=\""
                        + "/EtkinlikDetay.aspx?Code="
                        + E.Id
                        + "\">"
                        + E.Ad
                        + "</a></div><div class=\"searchresultozet\">"
                        + E.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/EtkinlikDetay.aspx?Code="
                        + E.Id
                        + "\">"
                        + E.Ad
                        + "</a></div><div class=\"searchresultozet\">"
                        + E.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void ForumlardaArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var ForumKonulariList = new List<ForumKonulari>();
            string S = "select FKO.Id as Id, FKO.Baslik as Ad from ForumKategorileri as FKA join ForumKonulari as FKO on FKA.Id==FKO.ForumKategoriId where FKA.DilId=="
                       + DilId
                       + " and FKA.Durum==True and FKO.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( FKO.Baslik like '%" + EnrollArama.QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                ForumKonulari FKO = new ForumKonulari();
                FKO.Id = Convert.ToInt32(item["Id"].ToString());
                FKO.Baslik = item["Ad"].ToString();
                ForumKonulariList.Add(FKO);
            }
            if (ForumKonulariList.Count != 0)
            {
                Arama EtkinliklerListArama = new Arama();
                EtkinliklerListArama.Ara =
                    "<div class=\"searchtitle\">Forumlar bölümünde " + ForumKonulariList.Count.ToString() +
                    " adet sonuç bulunmuştur.</div>";
                AramaList.Add(EtkinliklerListArama);
            }
            foreach (ForumKonulari FKO in ForumKonulariList)
            {
                Arama A = new Arama();
                A.Ara =
                    "<table class=\"searchresult\"><tr><td>"
                    + "<div class=\"searchresulttitle\"> <a href=\""
                    + "/Portal/ForumDetay.aspx?KonuId="
                    + FKO.Id
                    + "\">"
                    + FKO.Baslik
                    + "</a></div>"
                    + "</td></tr></table>";
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void AramaSonuclariVer(string Kategori, string ArananKelime)
        {
            var AraList = new List<Arama>();
            var ArananKelimeler = ArananKelime.Split('-').ToList();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            switch (Kategori)
            {
                case "0":
                    MenulerdeArama(DilId, ArananKelimeler, AraList);
                    HaberlerdeArama(DilId, ArananKelimeler, AraList);
                    DuyurulardaArama(DilId, ArananKelimeler, AraList);
                    PersonelDuyurulardaArama(DilId, ArananKelimeler, AraList);
                    IlanlardaArama(DilId, ArananKelimeler, AraList);
                    EtkinliklerdeArama(DilId, ArananKelimeler, AraList);
                    ForumlardaArama(DilId, ArananKelimeler, AraList);
                    break;
                case "1":
                    MenulerdeArama(DilId, ArananKelimeler, AraList);
                    break;
                case "2":
                    HaberlerdeArama(DilId, ArananKelimeler, AraList);
                    break;
                case "3":
                    DuyurulardaArama(DilId, ArananKelimeler, AraList);
                    break;
                case "4":
                    PersonelDuyurulardaArama(DilId, ArananKelimeler, AraList);
                    break;
                case "5":
                    IlanlardaArama(DilId, ArananKelimeler, AraList);
                    break;
                case "6":
                    EtkinliklerdeArama(DilId, ArananKelimeler, AraList);
                    break;
                case "7":
                    ForumlardaArama(DilId, ArananKelimeler, AraList);
                    break;
            }
            LiteralSiteMap.Text = "<a href=\"Ara.aspx\">Ara</a>";
            ListViewVeriler.DataSource = AraList;
            ListViewVeriler.DataBind();
        }

        private void KategorileriVer()
        {
            DropDownListKategoriler.Items.Add(new ListItem("Tümü", "0"));
            DropDownListKategoriler.Items.Add(new ListItem("İçerik", "1"));
            DropDownListKategoriler.Items.Add(new ListItem("Haberler", "2"));
            DropDownListKategoriler.Items.Add(new ListItem("Duyurular", "3"));
            DropDownListKategoriler.Items.Add(new ListItem("Personel Duyuruları", "4"));
            DropDownListKategoriler.Items.Add(new ListItem("İlanlar", "5"));
            DropDownListKategoriler.Items.Add(new ListItem("Etkinlikler", "6"));
            DropDownListKategoriler.Items.Add(new ListItem("Forumlar", "7"));
        }

        protected void LinkButtonAra_Click(object sender, EventArgs e)
        {
            string ArananKelime = EnrollArama.QueryStringeCevir(TextBoxAra.Text);
            TextBoxAra.Text = "Ne aramıştınız?";
            Response.Redirect("/Ara.aspx?Kategori=" + DropDownListKategoriler.SelectedValue + "&Code=" + ArananKelime +
                              "");
        }
    }
}