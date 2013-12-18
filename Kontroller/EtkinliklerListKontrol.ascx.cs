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
    public partial class EtkinliklerListKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EtkinlikleriVer();
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

        private void EtkinlikleriVer()
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
                                                  BaslangicTarihiTarih = E.BaslangicTarihi,
                                                  E.BitisTarihi,
                                                  Resim =
                                       E.GorselThumbnail1 != null
                                           ? E.GorselThumbnail1.Replace("~/", "../")
                                           : "../App_Themes/MainTheme/Images/noimage.png",
                                              }).Take(1).ToList();
            foreach (var Item in EtkinliklerList)
            {
                if (!string.IsNullOrEmpty(Item.BitisTarihi.ToString()))
                {
                    if (Item.BitisTarihi.Value >= DateTime.Now)
                    {
                        EtkinliklerList E = new EtkinliklerList();
                        E.Id = Item.Id;
                        E.Baslik = Item.Baslik;
                        E.Ozet = Item.Ozet;
                        E.Tarih = Item.BaslangicTarihiTarih.Value;
                        E.Resim = Item.Resim;
                        E.Url = "EtkinlikDetay.aspx?Code=" + Item.Id;
                        EList.Add(E);
                    }
                }
                else
                {
                    if (Item.BaslangicTarihiTarih.Value >= DateTime.Now)
                    {
                        EtkinliklerList E = new EtkinliklerList();
                        E.Id = Item.Id;
                        E.Baslik = Item.Baslik;
                        E.Ozet = Item.Ozet;
                        E.Tarih = Item.BaslangicTarihiTarih.Value;
                        E.Resim = Item.Resim;
                        E.Url = "EtkinlikDetay.aspx?Code=" + Item.Id;
                        EList.Add(E);
                    }
                }
            }
            if (EList != null)
            {
                ListViewEtkinlikler.DataSource = EList;
                ListViewEtkinlikler.DataBind();
            }
            else
            {
                ListViewEtkinlikler.DataSource = null;
                ListViewEtkinlikler.DataBind();
            }
        }
    }
}