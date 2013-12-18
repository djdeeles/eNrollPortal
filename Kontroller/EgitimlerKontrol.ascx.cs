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
    public partial class EgitimlerKontrol : UserControl
    {
        private readonly int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Code"] != null)
                    {
                        if (Convert.ToInt32(Request.QueryString["Code"]) == 0)
                        {
                            Temizle();
                            MultiView1.ActiveViewIndex = 0;
                            LiteralSiteMap.Text =
                                "<a href=\"../Portal/EgitimListeleri.aspx?Code=0\">Yeni Eğitim Talebi</a>";
                        }
                        else
                        {
                            MultiView1.ActiveViewIndex = 1;
                            EgitimleriVer(Convert.ToInt32(Request.QueryString["Code"]));
                        }
                    }
                    else
                    {
                        MultiView1.ActiveViewIndex = 1;
                        EgitimleriVer();
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = 1;
                    EgitimleriVer();
                }
                MenuEgitimlerKategoriler.Items.Clear();
                EgitimKategorileriniVer(MenuEgitimlerKategoriler.Items);
                MenuEgitimlerimKategoriler.Items.Clear();
                EgitimlerimKategorileriniVer(MenuEgitimlerimKategoriler.Items);
            }
        }

        private void Temizle()
        {
            TextBoxBaslik.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
        }

        private void EgitimleriVer()
        {
            // Eğitimleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EList = new List<EgitimlerList>();
            var EL = (from E in Veriler.Egitimler
                      join EK in Veriler.EgitimKatilimlari
                          on E.Id equals EK.EgitimId
                      where E.Durum
                            && E.DilId == DilId
                            && E.PersonelTalebiMi == false
                            && EK.KullaniciId == KullaniciId
                            && EK.IptalEdilmisMi == false
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
                int KullaniciId = Convert.ToInt32(Item.KullaniciAdiSoyadi);
                Kullanicilar K = Veriler.Kullanicilar.Where(p => p.Id == KullaniciId).FirstOrDefault();
                if (K != null)
                {
                    Text += "<b>Ekleyen Üye :</b>" + K.Ad + " " + K.Soyad;
                    E.KayitBilgileri = Text;
                }
                EList.Add(E);
            }
            ListViewVeriler.DataSource = EList;
            ListViewVeriler.DataBind();
            LiteralSiteMap.Text = "<a href=\"../Portal/EgitimListeleri.aspx\">Tüm Eğitimler</a>";
        }

        private void EgitimleriVer(int PersonelTalebiMi)
        {
            // Eğitimleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EList = new List<EgitimlerList>();
            if (PersonelTalebiMi == 1)
            {
                var EL = (from E in Veriler.Egitimler
                          join EK in Veriler.EgitimKatilimlari
                              on E.Id equals EK.EgitimId
                          where E.Durum
                                && E.DilId == DilId
                                && E.PersonelTalebiMi == false
                                && EK.KullaniciId == KullaniciId
                                && EK.IptalEdilmisMi == false
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
                LiteralSiteMap.Text =
                    "<a href=\"../Portal/EgitimListeleri.aspx?Code=1\">Katıldığım Kurum Eğitimleri</a>";
            }
            else if (PersonelTalebiMi == 2)
            {
                var EL = (from E in Veriler.Egitimler
                          join EK in Veriler.EgitimKatilimlari
                              on E.Id equals EK.EgitimId
                          join K in Veriler.Kullanicilar
                              on E.KaydedenKullaniciId equals K.Id
                          where E.Durum
                                && E.DilId == DilId
                                && E.PersonelTalebiMi
                                && EK.KullaniciId == KullaniciId
                                && EK.IptalEdilmisMi == false
                          orderby E.BaslangicTarihi descending
                          select new
                                     {
                                         E.Id,
                                         E.Baslik,
                                         E.Icerik,
                                         Ekleyen = K.Ad + " " + K.Soyad,
                                         KayitTarihi = E.KaydetmeTarihi,
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
                    Text += "<b>Ekleyen :</b>" + Item.Ekleyen + " ";
                    Text += "<b>Talep Tarihi:</b> " + Item.KayitTarihi + " ";
                    E.KayitBilgileri = Text;
                    EList.Add(E);
                }
                ListViewVeriler.DataSource = EList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../Portal/EgitimListeleri.aspx?Code=2\">Katıldığım Personel Talepleri</a>";
            }
            else if (PersonelTalebiMi == 3)
            {
                var EL = (from E in Veriler.Egitimler
                          where E.Durum
                                && E.DilId == DilId
                                && E.PersonelTalebiMi
                                && E.KaydedenKullaniciId == KullaniciId
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
                    int KullaniciId = Convert.ToInt32(Item.KullaniciAdiSoyadi);
                    Kullanicilar K = Veriler.Kullanicilar.Where(p => p.Id == KullaniciId).FirstOrDefault();
                    if (K != null)
                    {
                        Text += "<b>Ekleyen :</b>" + K.Ad + " " + K.Soyad;
                        E.KayitBilgileri = string.Empty;
                    }
                    EList.Add(E);
                }
                ListViewVeriler.DataSource = EList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text = "<a href=\"../Portal/EgitimListeleri.aspx?Code=4\">Eğitim Taleplerim</a>";
            }
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

        private void EgitimKategorileriniVer(MenuItemCollection Items)
        {
            MenuItem MI = new MenuItem();
            MI.NavigateUrl = "../TumEgitimler.aspx?Code=0";
            MI.Text = "Kurum'dan Eğitimler";
            Items.Add(MI);

            MenuItem MI1 = new MenuItem();
            MI1.NavigateUrl = "../TumEgitimler.aspx?Code=1";
            MI1.Text = "Personel'den Talepler";
            Items.Add(MI1);
        }

        protected void ListViewVeriler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Katil")
            {
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
                int Id = Convert.ToInt32(e.CommandArgument);
                int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                EgitimKatilimlari EK =
                    Veriler.EgitimKatilimlari.Where(p => p.EgitimId == Id & p.KullaniciId == KullaniciId).FirstOrDefault
                        ();
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
                            if (Request.QueryString["Code"] == "3")
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
                        if (Request.QueryString["Code"] == "3")
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

        protected void LinkButtonKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                Egitimler E = new Egitimler();
                E.DilId = DilId;
                E.Baslik = TextBoxBaslik.Text;
                E.Icerik = RadEditorIcerik.Content;
                E.BaslangicTarihi = null;
                E.BitisTarihi = null;
                E.KatilimciSayisi = null;
                E.Durum = true;
                E.PersonelTalebiMi = true;
                E.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                E.KaydetmeTarihi = DateTime.Now;
                Veriler.AddToEgitimler(E);
                Veriler.SaveChanges();
                Temizle();
                Response.Redirect("EgitimListeleri.aspx?Code=3");
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        protected void LinkButtonIptalEt_Click(object sender, EventArgs e)
        {
            Response.Redirect("EgitimListeleri.aspx?Code=1");
        }
    }
}