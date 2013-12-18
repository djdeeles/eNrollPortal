using System;
using System.Linq;
using System.Web.UI;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class IcerikDetayKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
                    {
                        IcerikDetayVer(Convert.ToInt32(Request.QueryString["Code"]));
                    }
                }
            }
        }

        private void IcerikDetayVer(int MenuId)
        {
            Menuler M = Veriler.Menuler.Where(p => p.Id == MenuId && p.Durum && p.MenuTipId == 3).FirstOrDefault();
            if (M != null)
            {
                M.OkunmaSayisi = M.OkunmaSayisi + 1;
                Veriler.SaveChanges();
                if (M.BasligiGöster)
                {
                    LabelIcerik.Text = "<span style='font-weight:bold; font-size:16px;'>"
                                       + M.MenuAdi + " </span><br /><br />";
                }
                LabelIcerik.Text = LabelIcerik.Text + M.Icerik;
                Page.Title = Page.Header.Title + " :: " + M.MenuAdi;
                Page.MetaKeywords = M.AnahtarKelimeler;
                Page.MetaDescription = M.Aciklama;
            }
        }
    }
}