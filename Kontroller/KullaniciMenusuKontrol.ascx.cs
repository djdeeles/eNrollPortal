using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class KullaniciMenusuKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AnaMenuleriVer();
            }
        }

        private void AnaMenuleriVer()
        {
            MenuAnaMenu.Items.Clear();
            AnaMenuler(0, MenuAnaMenu.Items);
        }

        public void AnaMenuler(Int32 UstMenuId, MenuItemCollection items)
        {
            // Menüleri ver
            var MList = new List<Menuler>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler =
                Veriler.Menuler.Where(p => p.MenuLokasyonId == 2 && p.MenudeGoster && p.Durum && p.DilId == DilId).
                    OrderBy(p => p.SiraNo).ToList();
            foreach (Menuler Menu in Menuler)
            {
                if (Menu.UstMenuId == UstMenuId)
                {
                    Menuler oMenu = new Menuler();
                    oMenu.UstMenuId = Menu.UstMenuId;
                    oMenu.MenuTipId = Menu.MenuTipId;
                    if (Menu.MenuTipId != 1)
                    {
                        oMenu.Url = Menu.Url;
                    }
                    else
                    {
                        oMenu.Url = oMenu.MenuAdi;
                    }
                    oMenu.MenuAdi = Menu.MenuAdi;
                    oMenu.Id = Menu.Id;
                    oMenu.MenuGorsel = Menu.MenuGorsel;
                    oMenu.MenuGorselHover = Menu.MenuGorselHover;
                    MList.Add(oMenu);
                }
            }
            foreach (Menuler Menu in MList)
            {
                MenuItem MItem = new MenuItem();
                if (!string.IsNullOrEmpty(Menu.MenuGorsel))
                {
                    string MenuGorselHover = Menu.MenuGorselHover.Replace("~/", "/");
                    string MenuGorsel = Menu.MenuGorsel.Replace("~/", "/");
                    string src = MenuGorsel;
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htmWriter = new HtmlTextWriter(sw);
                    HtmlImage image = new HtmlImage();
                    image.Src = src;
                    image.Style.Add("border-style", "none");
                    if (MenuGorselHover != "" && MenuGorselHover != null)
                    {
                        image.Attributes["onMouseOver"] = "this.src='" + MenuGorselHover + "';";
                        image.Attributes["onMouseDown"] = "this.src='" + MenuGorselHover + "';";
                    }
                    image.Attributes["onMouseOut"] = "this.src='" + MenuGorsel + "';";
                    image.RenderControl(htmWriter);
                    MItem.Text = Server.HtmlDecode(sw.ToString());
                }
                else
                {
                    if (Menu.MenuAdi == "Mesajlarım")
                    {
                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                            var KMList =
                                Veriler.KullaniciMesajlari.Where(
                                    p => p.AliciId == KullaniciId && p.OkunduMu == false && p.AliciSildiMi == false).
                                    ToList();
                            if (KMList.Count() != 0)
                            {
                                MItem.Text = "<b>" + Menu.MenuAdi + " (" + KMList.Count + ")</b>";
                            }
                            else
                            {
                                MItem.Text = Menu.MenuAdi;
                            }
                        }
                        else
                        {
                            MItem.Text = Menu.MenuAdi;
                        }
                    }
                    else
                    {
                        MItem.Text = Menu.MenuAdi;
                    }
                }
                switch (Menu.MenuTipId)
                {
                    case 1:
                        MItem.Selectable = false;
                        break;
                    case 2:
                        if (Menu.Url.Contains("http"))
                        {
                            MItem.NavigateUrl = Menu.Url;
                            MItem.Target = "_blank";
                        }
                        else
                        {
                            MItem.NavigateUrl = "/" + Menu.Url;
                        }
                        break;
                    case 3:
                        MItem.NavigateUrl = "/Sayfa.aspx?code=" + Menu.Id;
                        break;
                }
                items.Add(MItem);
                AnaMenuler(Convert.ToInt32(Menu.Id), MItem.ChildItems);
            }
        }
    }
}