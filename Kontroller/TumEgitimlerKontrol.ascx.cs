using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class TumEgitimlerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Code"] != null)
                    {
                        EgitimleriVer(Convert.ToInt32(Request.QueryString["Code"]));
                    }
                    else
                    {
                        EgitimleriVer();
                    }
                }
                else
                {
                    EgitimleriVer();
                }
                MenuEgitimlerimKategoriler.Items.Clear();
                EgitimlerimKategorileriniVer(MenuEgitimlerimKategoriler.Items);
                MenuEgitimKategoriler.Items.Clear();
                EgitimKategorileriniVer(MenuEgitimKategoriler.Items);
            }
        }

        private void EgitimleriVer()
        {
            // Eğitimleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EList = new List<EgitimlerList>();
            var EL = (from E in Veriler.Egitimler
                      where E.Durum
                            && E.DilId == DilId
                            && E.PersonelTalebiMi == false
                      orderby E.BaslangicTarihi descending
                      select new
                                 {
                                     E.Id,
                                     E.Baslik,
                                     E.Icerik,
                                     E.BaslangicTarihi,
                                     E.BitisTarihi,
                                     E.KatilimciSayisi,
                                     KullaniciAdiSoyadi = E.KaydedenKullaniciId
                                 }).Distinct().ToList();
            foreach (var Item in EL)
            {
                EgitimlerList E = new EgitimlerList();
                E.Id = Item.Id;
                E.Baslik = Item.Baslik;
                E.Icerik = Item.Icerik;
                string Text = string.Empty;
                if (Item.BaslangicTarihi != null)
                {
                    Text += "<b>Başlangiç Tarihi:</b> " + Item.BaslangicTarihi.Value.ToString("yyyy-MM-dd hh:mm") + " ";
                }
                if (Item.BitisTarihi != null)
                {
                    Text += "<b>Bitiş Tarihi:</b> " + Item.BitisTarihi.Value.ToString("yyyy-MM-dd hh:mm") + " ";
                }
                if (Item.KatilimciSayisi != null)
                {
                    Text += "<b>Katılımcı Sayısı:</b> " + Item.KatilimciSayisi.Value.ToString() + " ";
                }
                E.KayitBilgileri = Text;
                EList.Add(E);
            }
            ListViewVeriler.DataSource = EList;
            ListViewVeriler.DataBind();
            LiteralSiteMap.Text = "<a href=\"../TumEgitimler.aspx\">Tüm Eğitimler</a>";
        }

        private void EgitimleriVer(int PersonelTalebiMi)
        {
            // Eğitimleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EList = new List<EgitimlerList>();
            if (PersonelTalebiMi == 1)
            {
                var EL = (from E in Veriler.Egitimler
                          join K in Veriler.Kullanicilar
                              on E.KaydedenKullaniciId equals K.Id
                          where E.Durum
                                && E.DilId == DilId
                                && E.PersonelTalebiMi
                          orderby E.BaslangicTarihi descending
                          select new
                                     {
                                         E.Id,
                                         E.Baslik,
                                         E.Icerik,
                                         E.BaslangicTarihi,
                                         E.BitisTarihi,
                                         E.KatilimciSayisi,
                                         KayitTarihi = E.KaydetmeTarihi,
                                         KullaniciAdiSoyadi = K.Ad + " " + K.Soyad,
                                     }).Distinct().ToList();
                foreach (var Item in EL)
                {
                    EgitimlerList E = new EgitimlerList();
                    E.Id = Item.Id;
                    E.Baslik = Item.Baslik;
                    E.Icerik = Item.Icerik;
                    string Text = string.Empty;
                    if (Item.BaslangicTarihi != null)
                    {
                        Text += "<b>Başlangiç Tarihi:</b> " + Item.BaslangicTarihi.Value.ToString("yyyy-MM-dd hh:mm") +
                                " ";
                    }
                    if (Item.BitisTarihi != null)
                    {
                        Text += "<b>Bitiş Tarihi:</b> " + Item.BitisTarihi.Value.ToString("yyyy-MM-dd hh:mm") + " ";
                    }
                    if (Item.KatilimciSayisi != null)
                    {
                        Text += "<b>Katılımcı Sayısı:</b> " + Item.KatilimciSayisi.Value.ToString() + " ";
                    }
                    Text += "<b>Ekleyen:</b>" + Item.KullaniciAdiSoyadi;
                    Text += " <b>Talep Tarihi:</b>" + Item.KayitTarihi;
                    E.KayitBilgileri = Text;
                    EList.Add(E);
                }
                ListViewVeriler.DataSource = EList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text = "<a href=\"../TumEgitimler.aspx?Code=1\">Personel'den Talepler</a>";
            }
            else if (PersonelTalebiMi == 0)
            {
                var EL = (from E in Veriler.Egitimler
                          where E.Durum
                                && E.DilId == DilId
                                && E.PersonelTalebiMi == false
                          orderby E.BaslangicTarihi descending
                          select new
                                     {
                                         E.Id,
                                         E.Baslik,
                                         E.Icerik,
                                         E.BaslangicTarihi,
                                         E.BitisTarihi,
                                         E.KatilimciSayisi,
                                         KullaniciAdiSoyadi = E.KaydedenKullaniciId
                                     }).Distinct().ToList();
                foreach (var Item in EL)
                {
                    EgitimlerList E = new EgitimlerList();
                    E.Id = Item.Id;
                    E.Baslik = Item.Baslik;
                    E.Icerik = Item.Icerik;
                    string Text = string.Empty;
                    if (Item.BaslangicTarihi != null)
                    {
                        Text += "<b>Başlangiç Tarihi:</b> " + Item.BaslangicTarihi.Value.ToString("yyyy-MM-dd hh:mm") +
                                " ";
                    }
                    if (Item.BitisTarihi != null)
                    {
                        Text += "<b>Bitiş Tarihi:</b> " + Item.BitisTarihi.Value.ToString("yyyy-MM-dd hh:mm") + " ";
                    }
                    if (Item.KatilimciSayisi != null)
                    {
                        Text += "<b>Katılımcı Sayısı:</b> " + Item.KatilimciSayisi.Value.ToString() + " ";
                    }
                    E.KayitBilgileri = Text;
                    EList.Add(E);
                }
                ListViewVeriler.DataSource = EList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text = "<a href=\"../TumEgitimler.aspx?Code=0\">Kurum'dan Eğitimler</a>";
            }
        }

        private void EgitimKategorileriniVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            MenuItem MI = new MenuItem();
            MI.NavigateUrl = "../TumEgitimler.aspx?Code=0";
            MI.Text = "Kurum'dan Eğitimler";
            Items.Add(MI);

            MenuItem MI1 = new MenuItem();
            MI1.NavigateUrl = "../TumEgitimler.aspx?Code=1";
            MI1.Text = "Personel'den Talepler";
            Items.Add(MI1);
        }

        private void EgitimlerimKategorileriniVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            MenuItem MI = new MenuItem();
            MI.NavigateUrl = "../Portal/EgitimListeleri.aspx?Code=0";
            MI.Text = "Yeni Eğitim Talebi";
            Items.Add(MI);

            MenuItem MI1 = new MenuItem();
            MI1.NavigateUrl = "../Portal/EgitimListeleri.aspx?Code=1";
            MI1.Text = "Katıldığım Kurum Eğitimleri";
            Items.Add(MI1);

            MenuItem MI2 = new MenuItem();
            MI2.NavigateUrl = "../Portal/EgitimListeleri.aspx?Code=2";
            MI2.Text = "Katıldığım Personel Talepleri";
            Items.Add(MI2);

            MenuItem MI3 = new MenuItem();
            MI3.NavigateUrl = "../Portal/EgitimListeleri.aspx?Code=3";
            MI3.Text = "Eğitim Taleplerim";
            Items.Add(MI3);
        }

        protected void ListViewVeriler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Katil")
            {
                // Eğitime katıl
                int EgitimId = Convert.ToInt32(e.CommandArgument);
                int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                EgitimKatilimlari EK;
                EK =
                    Veriler.EgitimKatilimlari.Where(p => p.EgitimId == EgitimId && p.KullaniciId == KullaniciId).
                        FirstOrDefault();
                if (EK != null)
                {
                    EK.KatilimTarihi = DateTime.Now;
                    EK.IptalEdilmisMi = false;
                    Veriler.SaveChanges();
                }
                else
                {
                    EK = new EgitimKatilimlari();
                    EK.EgitimId = EgitimId;
                    EK.KullaniciId = KullaniciId;
                    EK.KatilimTarihi = DateTime.Now;
                    EK.IptalEdilmisMi = false;
                    Veriler.AddToEgitimKatilimlari(EK);
                    Veriler.SaveChanges();
                }
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Code"] != null)
                    {
                        EgitimleriVer(Convert.ToInt32(Request.QueryString["Code"]));
                    }
                    else
                    {
                        EgitimleriVer();
                    }
                }
                else
                {
                    EgitimleriVer();
                }
            }
            else if (e.CommandName == "IptalEt")
            {
                // İptal et
                int Id = Convert.ToInt32(e.CommandArgument);
                int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                EgitimKatilimlari EK =
                    Veriler.EgitimKatilimlari.Where(p => p.EgitimId == Id && p.KullaniciId == KullaniciId).
                        FirstOrDefault();
                EK.IptalTarihi = DateTime.Now;
                EK.IptalEdilmisMi = true;
                Veriler.SaveChanges();
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Code"] != null)
                    {
                        EgitimleriVer(Convert.ToInt32(Request.QueryString["Code"]));
                    }
                    else
                    {
                        EgitimleriVer();
                    }
                }
                else
                {
                    EgitimleriVer();
                }
            }
        }

        protected void ListViewVeriler_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                (e.Item.FindControl("LinkButtonOturumAc") as LinkButton).Visible = false;
                int EgitimId = Convert.ToInt32((e.Item.FindControl("HiddenFieldEgitimId") as HiddenField).Value);
                int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                EgitimKatilimlari EK =
                    Veriler.EgitimKatilimlari.Where(p => p.EgitimId == EgitimId && p.KullaniciId == KullaniciId).
                        FirstOrDefault();
                if (EK != null)
                {
                    if (EK.IptalEdilmisMi)
                    {
                        (e.Item.FindControl("LinkButtonKatil") as LinkButton).Visible = true;
                        if (Request.QueryString.Count != 0)
                        {
                            if (Request.QueryString["Code"] == "1")
                            {
                                (e.Item.FindControl("LinkButtonKatil") as LinkButton).Text = "<i>+1</i>";
                                (e.Item.FindControl("LinkButtonKatil") as LinkButton).ToolTip =
                                    "Bu Eğitimi Sende Talep Et";
                            }
                        }
                        (e.Item.FindControl("LinkButtonIptalEt") as LinkButton).Visible = false;
                    }
                    else
                    {
                        (e.Item.FindControl("LinkButtonKatil") as LinkButton).Visible = false;
                        (e.Item.FindControl("LinkButtonIptalEt") as LinkButton).Visible = true;
                    }
                }
                else
                {
                    (e.Item.FindControl("LinkButtonKatil") as LinkButton).Visible = true;
                    if (Request.QueryString.Count != 0)
                    {
                        if (Request.QueryString["Code"] == "1")
                        {
                            (e.Item.FindControl("LinkButtonKatil") as LinkButton).Text = "<i>+1</i>";
                            (e.Item.FindControl("LinkButtonKatil") as LinkButton).ToolTip = "Bu Eğitimi Sende Talep Et";
                        }
                    }
                    (e.Item.FindControl("LinkButtonIptalEt") as LinkButton).Visible = false;
                }
            }
            else
            {
                (e.Item.FindControl("LinkButtonOturumAc") as LinkButton).Visible = true;
                (e.Item.FindControl("LinkButtonKatil") as LinkButton).Visible = false;
                (e.Item.FindControl("LinkButtonIptalEt") as LinkButton).Visible = false;
            }
        }
    }
}