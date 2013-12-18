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
    public partial class TumEtkinliklerKontrol : UserControl
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
                        EtkinlikleriVer(Convert.ToInt32(Request.QueryString["Code"]));
                    }
                }
                else
                {
                    if (Session.Count != 0)
                    {
                        if (Session["EtkinlikTarih"] != null)
                        {
                            EtkinlikleriVer(Convert.ToDateTime(Session["EtkinlikTarih"]));
                            Session["EtkinlikTarih"] = null;
                        }
                        else
                        {
                            EtkinlikleriVer(0);
                        }
                    }
                    else
                    {
                        EtkinlikleriVer(0);
                    }
                }
                MenuEtkinlikKategoriler.Items.Clear();
                EtkinlikKategorileriVer(MenuEtkinlikKategoriler.Items);
            }
        }

        private void EtkinlikleriVer(int KategoriId)
        {
            // Etkinlikleri ver
            var EList = new List<EtkinliklerList>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (KategoriId != 0)
            {
                var EtkinliklerList = (from E in Veriler.Etkinlikler
                                       join EK in Veriler.EtkinlikKategorileri
                                           on E.EtkinlikKategoriId equals EK.Id
                                       where E.Durum
                                             && EK.Durum
                                             && EK.DilId == DilId
                                             && EK.Id == KategoriId
                                       orderby E.BaslangicTarihi descending
                                       select new
                                                  {
                                                      E.Id,
                                                      Baslik = E.Ad,
                                                      E.Ozet,
                                                      Tarih = E.BaslangicTarihi,
                                                      Resim =
                                           E.GorselThumbnail1 != null
                                               ? E.GorselThumbnail1.Replace("~/", "../")
                                               : "../App_Themes/MainTheme/Images/noimage.png",
                                                  }).Distinct().ToList();
                foreach (var Item in EtkinliklerList)
                {
                    EtkinliklerList E = new EtkinliklerList();
                    E.Id = Item.Id;
                    E.Baslik = Item.Baslik;
                    E.Ozet = Item.Ozet;
                    E.Tarih = Item.Tarih.Value;
                    E.Resim = Item.Resim;
                    E.Url = "EtkinlikDetay.aspx?Code=" + Item.Id;
                    EList.Add(E);
                }
                ListViewVeriler.DataSource = EList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumEtkinlikler.aspx\">Tüm Etkinlikler</a> / "
                    + "<a href=\"../TumEtkinlikler.aspx?Code=" + KategoriId + "\">"
                    + Veriler.EtkinlikKategorileri.Where(p => p.Id == KategoriId).FirstOrDefault().KategoriAdi + "</a>";
            }
            else
            {
                var EtkinliklerList = (from E in Veriler.Etkinlikler
                                       join EK in Veriler.EtkinlikKategorileri
                                           on E.EtkinlikKategoriId equals EK.Id
                                       where E.Durum
                                             && EK.Durum
                                             && EK.DilId == DilId
                                       orderby E.BaslangicTarihi descending
                                       select new
                                                  {
                                                      E.Id,
                                                      Baslik = E.Ad,
                                                      E.Ozet,
                                                      Tarih = E.BaslangicTarihi,
                                                      Resim =
                                           E.GorselThumbnail1 != null
                                               ? E.GorselThumbnail1.Replace("~/", "../")
                                               : "../App_Themes/MainTheme/Images/noimage.png",
                                                  }).Distinct().ToList();
                foreach (var Item in EtkinliklerList)
                {
                    EtkinliklerList E = new EtkinliklerList();
                    E.Id = Item.Id;
                    E.Baslik = Item.Baslik;
                    E.Ozet = Item.Ozet;
                    E.Tarih = Item.Tarih.Value;
                    E.Resim = Item.Resim;
                    E.Url = "EtkinlikDetay.aspx?Code=" + Item.Id;
                    EList.Add(E);
                }
                ListViewVeriler.DataSource = EList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumEtkinlikler.aspx\">Tüm Etkinlikler</a>";
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
                        //EtkinlikleriVer(e.Day.Date);
                    }
                    e.Day.IsSelectable = true;
                    e.Cell.ToolTip = E.Ad;
                    e.Cell.ForeColor = Color.OrangeRed;
                }
            }
        }

        private void EtkinlikleriVer(DateTime BaslangicTarihi)
        {
            // Etkinlikleri ver
            var EList = new List<EtkinliklerList>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EtkinliklerList = (from E in Veriler.Etkinlikler
                                   join EK in Veriler.EtkinlikKategorileri
                                       on E.EtkinlikKategoriId equals EK.Id
                                   where E.Durum
                                         && EK.Durum
                                         && EK.DilId == DilId
                                   orderby E.BaslangicTarihi descending
                                   select new
                                              {
                                                  E.Id,
                                                  Baslik = E.Ad,
                                                  E.Ozet,
                                                  Tarih = E.BaslangicTarihi,
                                                  Resim =
                                       E.GorselThumbnail1 != null
                                           ? E.GorselThumbnail1.Replace("~/", "../")
                                           : "../App_Themes/MainTheme/Images/noimage.png",
                                              }).Distinct().ToList();
            foreach (var Item in EtkinliklerList)
            {
                if (Item.Tarih.Value.ToShortDateString() == Convert.ToDateTime(BaslangicTarihi).ToShortDateString())
                {
                    EtkinliklerList E = new EtkinliklerList();
                    E.Id = Item.Id;
                    E.Baslik = Item.Baslik;
                    E.Ozet = Item.Ozet;
                    E.Tarih = Item.Tarih.Value;
                    E.Resim = Item.Resim;
                    E.Url = "EtkinlikDetay.aspx?Code=" + Item.Id;
                    EList.Add(E);
                }
            }
            ListViewVeriler.DataSource = EList;
            ListViewVeriler.DataBind();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            EtkinlikleriVer(Calendar1.SelectedDate.Date);
        }
    }
}