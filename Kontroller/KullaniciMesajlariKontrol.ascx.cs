using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler.DataModels;
using Telerik.Web.UI;

namespace EnrollPortal.Kontroller
{
    public partial class KullaniciMesajlariKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();
        private readonly int _kullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Code"] != null)
                    {
                        switch (Request.QueryString["Code"])
                        {
                            case "YeniMesaj":
                                MultiView1.ActiveViewIndex = 0;
                                LiteralSiteMap.Text =
                                    "<a href=\"/Portal/Mesajlar.aspx?Code=YeniMesaj\">Yeni Mesaj</a>";
                                // Web servis için session token
                                if (Session["ServiceToken"] == null)
                                    Session["ServiceToken"] = Guid.NewGuid().ToString() + ":ServiceToken";

                                break;

                            case "GelenMesajlar":
                                GelenMesajlariVer();
                                break;

                            case "GidenMesajlar":
                                GidenMesajlariVer();
                                break;
                        }
                    }
                    else
                    {
                        GelenMesajlariVer();
                    }
                }
                else
                {
                    GelenMesajlariVer();
                }
                MenuMesajKutusu.Items.Clear();
                MesajKutusunuVer(MenuMesajKutusu.Items);
            }
        }

        private void GelenMesajlariVer()
        {
            // Gelen mesajları ver
            var kmList = (from km in Veriler.KullaniciMesajlari
                          join k in Veriler.Kullanicilar
                              on km.GonderenId equals k.Id
                          where km.AliciSildiMi == false
                                && km.AliciId == _kullaniciId
                          orderby km.Tarih descending
                          select new
                                     {
                                         km.Id,
                                         Alici = k.Ad + " " + k.Soyad,
                                         AliciId = k.Id,
                                         km.Baslik,
                                         km.Mesaj,
                                         km.Tarih,
                                         km.OkunduMu,
                                     }).ToList();
            ListViewGelenMesajlar.DataSource = kmList;
            ListViewGelenMesajlar.DataBind();

            dvGelenMesajlarIslemler.Visible = kmList.Count > 0;

            LiteralSiteMap.Text = "<a href=\"/Portal/Mesajlar.aspx?Code=GelenMesajlar\">Gelen Mesajlar</a>";
            MultiView1.ActiveViewIndex = 1;
        }

        private void GidenMesajlariVer()
        {
            // Giden mesajları ver
            var kmList = (from km in Veriler.KullaniciMesajlari
                          join k in Veriler.Kullanicilar
                              on km.AliciId equals k.Id
                          where km.GonderenSildiMi == false
                                && km.GonderenId == _kullaniciId
                          orderby km.Tarih descending
                          select new
                                     {
                                         km.Id,
                                         Alici = k.Ad + " " + k.Soyad,
                                         km.Baslik,
                                         km.Mesaj,
                                         km.Tarih,
                                     }).ToList();
            ListViewGidenMesajlar.DataSource = kmList;
            ListViewGidenMesajlar.DataBind();
            LiteralSiteMap.Text = "<a href=\"/Portal/Mesajlar.aspx?Code=GidenMesajlar\">Giden Mesajlar</a>";
            MultiView1.ActiveViewIndex = 2;
        }

        private void MesajKutusunuVer(MenuItemCollection items)
        {
            var m1 = new MenuItem();
            m1.NavigateUrl = "/Portal/Mesajlar.aspx?Code=YeniMesaj";
            m1.Text = "Yeni Mesaj";
            items.Add(m1);

            var m2 = new MenuItem();
            m2.NavigateUrl = "/Portal/Mesajlar.aspx?Code=GelenMesajlar";
            m2.Text = OkunmamisMesajSayisi() > 0
                          ? "<b>Gelen Mesajlar(" + OkunmamisMesajSayisi() + "/" + GelenMesajSayisiniVer() + ")</b>"
                          : "Gelen Mesajlar(" + GelenMesajSayisiniVer() + ")";
            items.Add(m2);

            var m3 = new MenuItem();
            m3.NavigateUrl = "/Portal/Mesajlar.aspx?Code=GidenMesajlar";
            m3.Text = "Giden Mesajlar(" + GidenMesajSayisiniVer() + ")";
            items.Add(m3);
        }

        private int GelenMesajSayisiniVer()
        {
            // Gelen mesaj sayısını ver
            return Veriler.KullaniciMesajlari.Count(p => p.AliciId == _kullaniciId && p.AliciSildiMi == false);
        }

        private int OkunmamisMesajSayisi()
        {
            // Okunmamış mesaj sayısı
            return
                Veriler.KullaniciMesajlari.Count(
                    p => p.AliciId == _kullaniciId && p.AliciSildiMi == false && p.OkunduMu == false);
        }

        private int GidenMesajSayisiniVer()
        {
            // Giden mesaj sayısı
            return Veriler.KullaniciMesajlari.Count(p => p.GonderenId == _kullaniciId && p.GonderenSildiMi == false);
        }

        protected void LinkButtonMesajYazClick(object sender, EventArgs e)
        {
            var E = TextBoxKime.Text.Split('"');
            try
            {
                var ePosta = E[1];
                var kullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                var k = Veriler.Kullanicilar.FirstOrDefault(p => p.EPosta == ePosta);
                if (k != null)
                {
                    var km = new KullaniciMesajlari();
                    km.GonderenId = kullaniciId;
                    km.AliciId = k.Id;
                    km.Baslik = TextBoxBaslik.Text;
                    km.Mesaj = RadEditorMesaj.Content;
                    km.Tarih = DateTime.Now;
                    km.GonderenSildiMi = false;
                    km.AliciSildiMi = false;
                    Veriler.KullaniciMesajlari.AddObject(km);
                    Veriler.SaveChanges();
                    TextBoxKime.Text = string.Empty;
                    TextBoxBaslik.Text = string.Empty;
                    RadEditorMesaj.Content = string.Empty;
                    MesajKontrol1.Mesaj(true, "Mesaj gönderildi.");
                    MesajKutusunuVer(MenuMesajKutusu.Items);
                }
                else
                {
                    MesajKontrol1.Mesaj(false, "Kayıtlı bir kullanıcı seçiniz!");
                }
            }
            catch (Exception exception)
            {
                MesajKontrol1.Mesaj(false, exception + "\nHata oluştu!");
            }
        }

        protected void LinkButtonIptalClick(object sender, EventArgs e)
        {
            Response.Redirect("/Portal/Mesajlar.aspx");
        }

        #region ListView GELEN MESAJLAR edit, update, delete, cancel, databound

        protected void ListViewGelenMesajlarItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewGelenMesajlar.EditIndex = e.NewEditIndex;
            GelenMesajlariVer();
            var hiddenFieldAliciId = ListViewGelenMesajlar.EditItem.FindControl("HiddenFieldAliciId") as HiddenField;
            if (hiddenFieldAliciId != null)
            {
                var aliciId = Convert.ToInt32(hiddenFieldAliciId.Value);
            }
            var textBoxBaslik = ListViewGelenMesajlar.EditItem.FindControl("TextBoxBaslik") as TextBox;
            if (textBoxBaslik != null)
            {
                var baslik = textBoxBaslik.Text;
                var radEditorMesaj = ListViewGelenMesajlar.EditItem.FindControl("RadEditorMesaj") as RadEditor;
                if (radEditorMesaj != null)
                {
                    var mesaj = radEditorMesaj.Content;
                    textBoxBaslik.Text = !baslik.StartsWith("Cvp:") ? "Cvp:" + baslik : baslik;
                    radEditorMesaj.Content = "<br/><br/><hr/>" + mesaj;
                }
            }
            var hiddenFieldMesajId = ListViewGelenMesajlar.EditItem.FindControl("HiddenFieldMesajId") as HiddenField;
            if (hiddenFieldMesajId != null)
            {
                var id = Convert.ToInt32(hiddenFieldMesajId.Value);
                var km = Veriler.KullaniciMesajlari.FirstOrDefault(p => p.Id == id);
                if (km != null) km.OkunduMu = true;
            }
            Veriler.SaveChanges();
        }

        protected void ListViewGelenMesajlarItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            Response.Redirect("/Portal/Mesajlar.aspx?Code=GelenMesajlar");
        }

        protected void ListViewGelenMesajlarItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var hiddenFieldAliciId = ListViewGelenMesajlar.EditItem.FindControl("HiddenFieldAliciId") as HiddenField;
            if (hiddenFieldAliciId != null)
            {
                var aliciId = Convert.ToInt32(hiddenFieldAliciId.Value);
                var textBoxBaslik = ListViewGelenMesajlar.EditItem.FindControl("TextBoxBaslik") as TextBox;
                if (textBoxBaslik != null)
                {
                    var baslik = textBoxBaslik.Text;
                    var radEditorMesaj = ListViewGelenMesajlar.EditItem.FindControl("RadEditorMesaj") as RadEditor;
                    if (radEditorMesaj != null)
                    {
                        var mesaj = radEditorMesaj.Content;
                        var kullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                        var km = new KullaniciMesajlari();
                        km.GonderenId = kullaniciId;
                        km.AliciId = aliciId;
                        km.Baslik = baslik;
                        km.Mesaj = "<span style=\"font-size:10px;\">" + DateTime.Now + "</span><br/>" + mesaj;
                        km.Tarih = DateTime.Now;
                        km.GonderenSildiMi = false;
                        km.AliciSildiMi = false;
                        Veriler.KullaniciMesajlari.AddObject(km);
                    }
                }
            }
            Veriler.SaveChanges();
            Response.Redirect("/Portal/Mesajlar.aspx?Code=GelenMesajlar");
        }

        protected void ListViewGelenMesajlarItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var dataKey = ListViewGelenMesajlar.DataKeys[e.ItemIndex];
            if (dataKey != null)
            {
                var mesajId = (int) dataKey.Value;
                var km = Veriler.KullaniciMesajlari.FirstOrDefault(p => p.Id == mesajId);
                if (km != null) km.AliciSildiMi = true;
            }
            Veriler.SaveChanges();
            Response.Redirect("/Portal/Mesajlar.aspx?Code=GelenMesajlar");
        }

        protected void ListViewGelenMesajlarItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                try
                {
                    var hiddenField = e.Item.FindControl("HiddenFieldOkunduMu") as HiddenField;
                    if (hiddenField != null && (!string.IsNullOrWhiteSpace(hiddenField.Value) &&
                                                Convert.ToBoolean(hiddenField.Value)))
                    {
                        var image = e.Item.FindControl("ImageOkunduMu") as Image;
                        if (image != null)
                            image.ImageUrl
                                = "../App_Themes/MainTheme/Images/mesaj.png";
                    }
                    else
                    {
                        var image = e.Item.FindControl("ImageOkunduMu") as Image;
                        if (image != null) image.ImageUrl = "../App_Themes/MainTheme/Images/mesajvar.png";
                    }
                }
                catch
                {
                    //
                }
            }
        }

        #endregion

        #region ListView GİDEN MESAJLAR edit, update, delete, cancel, databound

        protected void ListViewGidenMesajlarItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewGidenMesajlar.EditIndex = e.NewEditIndex;
        }

        protected void ListViewGidenMesajlarItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var dataKey = ListViewGidenMesajlar.DataKeys[e.ItemIndex];
            if (dataKey != null)
            {
                var mesajId = (int) dataKey.Value;
                var km = Veriler.KullaniciMesajlari.FirstOrDefault(p => p.Id == mesajId);
                if (km != null) km.GonderenSildiMi = true;
            }
            Veriler.SaveChanges();
            Response.Redirect("/Portal/Mesajlar.aspx?Code=GidenMesajlar");
        }

        #endregion
    }
}