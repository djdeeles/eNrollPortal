using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class EtkinlikDetayKontrol : UserControl
    {
        public static List<Gorseller> GorsellerList;
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EtkinlikDetayVer();
                MenuEtkinlikKategoriler.Items.Clear();
                EtkinlikKategorileriVer(MenuEtkinlikKategoriler.Items);
            }
        }

        private void EtkinlikDetayVer()
        {
            // Etkimlik detay ver
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["Code"] != null)
                {
                    int Id = Convert.ToInt32(Request.QueryString["Code"]);
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    var EList = (from E in Veriler.Etkinlikler
                                 join EK in Veriler.EtkinlikKategorileri
                                     on E.EtkinlikKategoriId equals EK.Id
                                 where
                                     E.Durum
                                     && EK.Durum
                                     && EK.DilId == DilId
                                     && E.Id == Id
                                 select new
                                            {
                                                E.Id,
                                                Baslik = E.Ad,
                                                E.Icerik,
                                                Tarih = E.BaslangicTarihi,
                                                E.AnahtarKelimeler,
                                                KategoriId = EK.Id,
                                                EK.KategoriAdi,
                                            }).First();
                    LabelBaslik.Text = EList.Baslik;
                    LabelTarih.Text = EList.Tarih.Value.ToString("dd/MM/yyyy");
                    LabelIcerik.Text = EList.Icerik;
                    Page.MetaKeywords = EList.AnahtarKelimeler;
                    Page.Title = Page.Header.Title + " :: " + EList.Baslik;
                    LiteralSiteMap.Text =
                        "<a href=\"../TumEtkinlikler.aspx\">Tüm Etkinlikler</a> / "
                        + "<a href=\"../TumEtkinlikler.aspx?Code=" + EList.KategoriId + "\">"
                        + EList.KategoriAdi + "</a>";
                    Etkinlikler EE = Veriler.Etkinlikler.Where(p => p.Id == EList.Id && p.Durum).FirstOrDefault();
                    if (EE != null)
                    {
                        if (EE.OkunmaSayisi == null)
                        {
                            EE.OkunmaSayisi = 1;
                        }
                        else
                        {
                            EE.OkunmaSayisi = EE.OkunmaSayisi + 1;
                        }
                        Veriler.SaveChanges();
                        GorsellerList = new List<Gorseller>();
                        Gorseller G;
                        if (!string.IsNullOrEmpty(EE.GorselThumbnail1))
                        {
                            G = new Gorseller();
                            G.Gorsel = EE.Gorsel1.Replace("~/", "");
                            G.GorselThumbnail = EE.GorselThumbnail1.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(EE.GorselThumbnail2))
                        {
                            G = new Gorseller();
                            G.Gorsel = EE.Gorsel2.Replace("~/", "");
                            G.GorselThumbnail = EE.GorselThumbnail2.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(EE.GorselThumbnail3))
                        {
                            G = new Gorseller();
                            G.Gorsel = EE.Gorsel3.Replace("~/", "");
                            G.GorselThumbnail = EE.GorselThumbnail3.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(EE.GorselThumbnail4))
                        {
                            G = new Gorseller();
                            G.Gorsel = EE.Gorsel4.Replace("~/", "");
                            G.GorselThumbnail = EE.GorselThumbnail4.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (GorsellerList.Count > 1)
                        {
                            DataListResimler.DataSource = GorsellerList;
                            DataListResimler.DataBind();
                        }
                        if (GorsellerList.Count != 0)
                        {
                            ImageHaberGorsel.ImageUrl = "../" + GorsellerList[0].Gorsel;
                        }
                        else
                        {
                            ImageHaberGorsel.ImageUrl = "..//App_Themes/MainTheme/Images/noimage.png";
                        }
                    }
                }
            }
        }

        private void EtkinlikKategorileriVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EKList =
                Veriler.EtkinlikKategorileri.Where(p => p.Durum && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            MenuItem MII = new MenuItem();
            MII.NavigateUrl = "../TumEtkinlikler.aspx?Code=0";
            MII.Text = "Tüm Etkinlikler";
            Items.Add(MII);
            foreach (EtkinlikKategorileri EK in EKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumEtkinlikler.aspx?Code=" + EK.Id.ToString();
                MI.Text = EK.KategoriAdi;
                Items.Add(MI);
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            var EList = Veriler.Etkinlikler.Where(p => p.Durum).ToList();
            foreach (Etkinlikler E in EList)
            {
                if (e.Day.Date.ToShortDateString() == Convert.ToDateTime(E.BaslangicTarihi).ToShortDateString())
                {
                    if (e.Day.IsSelected)
                    {
                        // Response.Redirect("~/EtkinlikDetay.aspx?Code=" + E.Id);
                        // EtkinlikleriVer(e.Day.Date);
                    }
                    e.Day.IsSelectable = true;
                    e.Cell.ToolTip = E.Ad;
                    e.Cell.ForeColor = Color.OrangeRed;
                }
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Session["EtkinlikTarih"] = Calendar1.SelectedDate.Date;
            Response.Redirect("TumEtkinlikler.aspx");
        }

        #region Nested type: Gorseller

        public class Gorseller
        {
            public string Gorsel { get; set; }
            public string GorselThumbnail { get; set; }
        }

        #endregion
    }
}