using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;
using Telerik.Web.UI;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class MenulerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            int LokasyonId = Convert.ToInt32(Request.QueryString["LokasyonId"]);
            MenuLokasyonlari MT = Veriler.MenuLokasyonlari.Where(p => p.Id == LokasyonId).First();
            Session["currentPath"] = MT.LokasyonAdi + " Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 3))
                {
                    MultiView2.ActiveViewIndex = 0;
                    ResetMode();
                    PanelEdit.Visible = false;
                    PanelMenulerEdit.Visible = false;
                    MesajKontrol1.Reset();
                    ImageButtonMenuGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                                          + TextBoxMenuGorsel.ClientID + "','','width=640,height=480');";
                    ImageButtonMenuGorselHover.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                                               + TextBoxMenuGorselHover.ClientID +
                                                               "','','width=640,height=480');";
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            InsertMode();
            PanelMenuler.Visible = false;
            PanelMenulerEdit.Visible = true;
            MenuleriVer(RadTreeViewMenulerEdit);
        }

        private void MenuleriVer(RadTreeView RadTreeView)
        {
            // Menuleri ver
            int LokasyonId = Convert.ToInt32(Request.QueryString["LokasyonId"]);
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler = (from p in Veriler.Menuler
                           where p.MenuLokasyonId == LokasyonId && p.DilId == DilId
                           orderby p.SiraNo ascending
                           select new
                                      {
                                          p.MenuAdi,
                                          p.Id,
                                          UstMenuId = p.UstMenuId == 0 ? null : p.UstMenuId
                                      }).ToList();
            RadTreeView.DataTextField = "MenuAdi";
            RadTreeView.DataFieldParentID = "UstMenuId";
            RadTreeView.DataFieldID = "Id";
            RadTreeView.DataValueField = "Id";
            RadTreeView.DataSource = Menuler;
            RadTreeView.DataBind();
            RadTreeView.Nodes.Insert(0, new RadTreeNode("Ana Dizin", "0"));
        }

        protected void RadTreeViewMenuler_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            PanelEdit.Visible = true;
            if (RadTreeViewMenuler.SelectedNode.Value != "0")
            {
                UpdateMode(Convert.ToInt32(RadTreeViewMenuler.SelectedNode.Value));
                LabelLokasyon.Text = e.Node.FullPath;
                RadTreeNode node = RadTreeViewMenuler.FindNodeByValue(e.Node.Value);
                if (node != null)
                {
                    node.Expanded = true;
                    node.Selected = true;
                    HiddenFieldKategoriId.Value = node.Value;
                }
            }
            else
            {
                ResetMode();
                foreach (RadTreeNode Node in RadTreeViewMenuler.GetAllNodes())
                {
                    if (Node.Value == "0")
                    {
                        Node.Expanded = true;
                        Node.Selected = true;
                        HiddenFieldKategoriId.Value = Node.Value;
                    }
                }
            }
        }

        private void MenuTipleriVer()
        {
            // Menü tiplerini ver
            var MenuTipleri = Veriler.MenuTipleri.ToList();
            DropDownListTipler.DataTextField = "TipAdi";
            DropDownListTipler.DataValueField = "Id";
            DropDownListTipler.DataSource = MenuTipleri;
            DropDownListTipler.DataBind();
            DropDownListTipler.Items.Insert(0, new ListItem("Seçiniz", "Seçiniz"));
        }

        private void ResetMode()
        {
            // Reset mode
            LabelLokasyon.Text = string.Empty;
            TextBoxMenuAdi.Text = string.Empty;
            MenuTipleriVer();
            TextBoxMenuGorsel.Text = string.Empty;
            TextBoxMenuGorselHover.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            CheckBoxMenudeGoster.Checked = false;
            CheckBoxBasligiGoster.Checked = false;
            TextBoxUrl.Text = string.Empty;
            TextBoxOzet.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
            TextBoxAciklama.Text = string.Empty;
            TextBoxAnahtatrKelimeler.Text = string.Empty;
            PanelEdit.Visible = false;
            MultiView1.ActiveViewIndex = -1;
            MesajKontrol1.Reset();
            PanelMenuler.Visible = true;
            MenuleriVer(RadTreeViewMenuler);
            PanelMenulerEdit.Visible = false;
            HiddenFieldKategoriId.Value = string.Empty;
        }

        private void InsertMode()
        {
            // Insert mode
            ResetMode();
            HiddenFieldEditMode.Value = "Insert";
            PanelEdit.Visible = true;
            CheckBoxDurum.Checked = true;
            CheckBoxMenudeGoster.Checked = true;
            MesajKontrol1.Reset();
            PanelMenulerEdit.Visible = true;
            MenuleriVer(RadTreeViewMenulerEdit);
        }

        private void UpdateMode(int Id)
        {
            // Update mode
            ResetMode();
            HiddenFieldEditMode.Value = "Update";
            PanelEdit.Visible = true;
            Menuler Menu = Veriler.Menuler.Where(p => p.Id == Id).FirstOrDefault();
            TextBoxMenuAdi.Text = Menu.MenuAdi;
            switch (Menu.MenuTipId)
            {
                case 1:
                    DropDownListTipler.SelectedValue = "1";
                    break;
                case 2:
                    DropDownListTipler.SelectedValue = "2";
                    TextBoxUrl.Text = Menu.Url;
                    MultiView1.ActiveViewIndex = 0;
                    break;
                case 3:
                    DropDownListTipler.SelectedValue = "3";
                    MultiView1.ActiveViewIndex = 1;
                    TextBoxOzet.Text = Menu.Ozet;
                    RadEditorIcerik.Content = Menu.Icerik;
                    TextBoxAciklama.Text = Menu.Aciklama;
                    TextBoxAnahtatrKelimeler.Text = Menu.AnahtarKelimeler;
                    break;
            }
            TextBoxMenuGorsel.Text = Menu.MenuGorsel;
            TextBoxMenuGorselHover.Text = Menu.MenuGorselHover;
            CheckBoxDurum.Checked = Menu.Durum;
            CheckBoxMenudeGoster.Checked = Menu.MenudeGoster;
            CheckBoxBasligiGoster.Checked = Menu.BasligiGöster;
        }

        protected void RadTreeViewMenuler_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            // Menülerin sıralamasını değiştirmek için sürükle bırak metodu
            try
            {
                foreach (RadTreeNode Node in e.DraggedNodes)
                {
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    int LokasyonId = Convert.ToInt32(Request.QueryString["LokasyonId"]);
                    int Id = Convert.ToInt32(Node.Value);
                    int MenuHedefId = Convert.ToInt32(e.DestDragNode.Value);
                    int MenuHedefUstMenuId = 0;
                    int MenuHedefSiraNo = 1;
                    Menuler MenuHedef;
                    if (MenuHedefId != 0)
                    {
                        MenuHedef = Veriler.Menuler.Where(p => p.Id == MenuHedefId).First();
                        MenuHedefUstMenuId = MenuHedef.UstMenuId.Value;
                        MenuHedefSiraNo = MenuHedef.SiraNo;
                    }
                    Menuler MenuKaynak = Veriler.Menuler.Where(p => p.Id == Id).First();
                    List<Menuler> MenuListHedef;
                    List<Menuler> MenuListKaynak;

                    # region Over

                    if (e.DropPosition.ToString() == "Over")
                    {
                        // Taşma farklı dizinde
                        if (MenuHedefId != MenuKaynak.UstMenuId)
                        {
                            // Hedef menünün sıralarını bir artır.

                            # region Hedef

                            MenuListHedef =
                                Veriler.Menuler.Where(
                                    p =>
                                    p.UstMenuId == MenuHedefId && p.DilId == DilId && p.MenuLokasyonId == LokasyonId).
                                    ToList();
                            foreach (Menuler M in MenuListHedef)
                            {
                                Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                MM.SiraNo = MM.SiraNo + 1;
                                Veriler.SaveChanges();
                            }

                            # endregion

                            // Kaynak menünün siralarını kaynak menüden sonra bir azalt.

                            # region Kaynak

                            MenuListKaynak =
                                Veriler.Menuler.Where(
                                    p =>
                                    p.UstMenuId == MenuKaynak.UstMenuId && p.SiraNo >= MenuKaynak.SiraNo &&
                                    p.DilId == DilId && p.MenuLokasyonId == LokasyonId).ToList();
                            foreach (Menuler M in MenuListKaynak)
                            {
                                Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                MM.SiraNo = MM.SiraNo - 1;
                                Veriler.SaveChanges();
                            }

                            # endregion

                            // Kaynak menünün üst menü id'sini ve sırasını güncelle.

                            # region KaynakGuncelle

                            MenuKaynak.UstMenuId = MenuHedefId;
                            MenuKaynak.SiraNo = 1;
                            Veriler.SaveChanges();

                            # endregion

                            ResetMode();
                            MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                        }
                            // Taşıma aynı dizinde 
                        else
                        {
                            List<Menuler> MenuList;
                            // Kaynak Sıra No büyükse Sira nolarını 1 artır.

                            # region KaynakSiraNoBuyukse

                            if (MenuHedefSiraNo < MenuKaynak.SiraNo)
                            {
                                MenuList =
                                    Veriler.Menuler.Where(
                                        p =>
                                        p.UstMenuId == MenuKaynak.UstMenuId && p.SiraNo <= MenuKaynak.SiraNo &&
                                        p.SiraNo >= MenuHedefSiraNo && p.DilId == DilId &&
                                        p.MenuLokasyonId == LokasyonId).ToList();
                                foreach (Menuler M in MenuList)
                                {
                                    Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                    MM.SiraNo = MM.SiraNo + 1;
                                    Veriler.SaveChanges();
                                }
                            }
                                #endregion
                                // Hedef Sıra No büyükse Sira nolarını 1 azalt.
                                # region HedefSiraNoBuyukse

                            else if (MenuHedefSiraNo > MenuKaynak.SiraNo)
                            {
                                MenuList =
                                    Veriler.Menuler.Where(
                                        p =>
                                        p.UstMenuId == MenuKaynak.UstMenuId && p.SiraNo >= MenuKaynak.SiraNo &&
                                        p.SiraNo <= MenuHedefSiraNo && p.DilId == DilId &&
                                        p.MenuLokasyonId == LokasyonId).ToList();
                                foreach (Menuler M in MenuList)
                                {
                                    Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                    MM.SiraNo = MM.SiraNo - 1;
                                    Veriler.SaveChanges();
                                }
                            }

                            #endregion

                            // Kaynak menünün sırasını güncelle.

                            # region KaynakGuncelle

                            MenuKaynak.SiraNo = MenuHedefSiraNo;
                            Veriler.SaveChanges();

                            #endregion

                            ResetMode();
                            MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                        }
                    }
                        # endregion
                        # region AboveBelow

                    else
                    {
                        // Taşma farklı dizinde

                        # region TasimaFarkliDizinde

                        if (MenuHedefId != MenuKaynak.UstMenuId)
                        {
                            // Hedef menünün siralarını kaynak menüden sonra bir artır.                        

                            # region Hedef

                            MenuListHedef =
                                Veriler.Menuler.Where(
                                    p =>
                                    p.UstMenuId == MenuHedefUstMenuId && p.SiraNo >= MenuHedefSiraNo
                                    /* && p.SiraNo <= MenuKaynak.SiraNo*/&& p.DilId == DilId &&
                                    p.MenuLokasyonId == LokasyonId).ToList();
                            foreach (Menuler M in MenuListHedef)
                            {
                                Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                MM.SiraNo = MM.SiraNo + 1;
                                Veriler.SaveChanges();
                            }
                            MenuListKaynak =
                                Veriler.Menuler.Where(
                                    p =>
                                    p.UstMenuId == MenuKaynak.UstMenuId && p.SiraNo >= MenuKaynak.SiraNo &&
                                    p.DilId == DilId && p.MenuLokasyonId == LokasyonId).ToList();
                            foreach (Menuler M in MenuListKaynak)
                            {
                                Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                MM.SiraNo = MM.SiraNo - 1;
                                Veriler.SaveChanges();
                            }
                            MenuKaynak.SiraNo = MenuHedefSiraNo;
                            MenuKaynak.UstMenuId = MenuHedefUstMenuId;
                            Veriler.SaveChanges();
                            ResetMode();
                            MesajKontrol1.Mesaj(true, "Kayıt edildi.");

                            # endregion
                        }
                            # endregion
                            // Taşıma aynı dizinde
                            # region TasimaAyniDizinde

                        else
                        {
                            # region Hedef

                            if (MenuKaynak.SiraNo > MenuHedefSiraNo)
                            {
                                MenuListHedef =
                                    Veriler.Menuler.Where(
                                        p =>
                                        p.UstMenuId == MenuHedefUstMenuId && p.SiraNo >= MenuHedefSiraNo &&
                                        p.SiraNo <= MenuKaynak.SiraNo && p.DilId == DilId &&
                                        p.MenuLokasyonId == LokasyonId).ToList();
                                foreach (Menuler M in MenuListHedef)
                                {
                                    Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                    MM.SiraNo = MM.SiraNo + 1;
                                    Veriler.SaveChanges();
                                }
                                MenuKaynak.SiraNo = MenuHedefSiraNo;
                                Veriler.SaveChanges();
                                ResetMode();
                                MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                            }
                            else if (MenuKaynak.SiraNo < MenuHedefSiraNo)
                            {
                                MenuListHedef =
                                    Veriler.Menuler.Where(
                                        p =>
                                        p.UstMenuId == MenuHedefUstMenuId && p.SiraNo <= MenuHedefSiraNo &&
                                        p.SiraNo >= MenuKaynak.SiraNo && p.DilId == DilId &&
                                        p.MenuLokasyonId == LokasyonId).ToList();
                                foreach (Menuler M in MenuListHedef)
                                {
                                    Menuler MM = Veriler.Menuler.Where(p => p.Id == M.Id).First();
                                    MM.SiraNo = MM.SiraNo - 1;
                                    Veriler.SaveChanges();
                                }
                                MenuKaynak.SiraNo = MenuHedefSiraNo;
                                Veriler.SaveChanges();
                                ResetMode();
                                MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                            }

                            # endregion
                        }

                        # endregion
                    }

                    # endregion
                }
                MenuleriVer(RadTreeViewMenuler);
            }
            catch
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        protected void DropDownListTipler_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DropDownListTipler.SelectedValue)
            {
                case "1":
                    MultiView1.ActiveViewIndex = -1;
                    ImageButtonKaydet.Enabled = true;
                    break;
                case "2":
                    MultiView1.SetActiveView(View1);
                    ImageButtonKaydet.Enabled = true;
                    break;
                case "3":
                    MultiView1.SetActiveView(View2);
                    ImageButtonKaydet.Enabled = true;
                    break;
                case "Seçiniz":
                    MultiView1.ActiveViewIndex = -1;
                    break;
            }
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Menuler Menu;
                if (HiddenFieldEditMode.Value == "Insert")
                {
                    // Yeni kayıt
                    Menu = new Menuler();
                    Menu.UstMenuId = Convert.ToInt32(RadTreeViewMenulerEdit.SelectedNode.Value);
                    Menu.SiraNo = SiraNoVer(Menu.UstMenuId.Value);
                    Menu.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    Menu.MenuLokasyonId = Convert.ToInt32(Request.QueryString["LokasyonId"]);
                    Menu.MenuAdi = TextBoxMenuAdi.Text;
                    switch (DropDownListTipler.SelectedValue)
                    {
                        case "1":
                            Menu.MenuTipId = 1;
                            break;
                        case "2":
                            Menu.MenuTipId = 2;
                            Menu.Url = TextBoxUrl.Text;
                            break;
                        case "3":
                            Menu.MenuTipId = 3;
                            Menu.Ozet = TextBoxOzet.Text;
                            Menu.Icerik = RadEditorIcerik.Content;
                            Menu.Aciklama = TextBoxAciklama.Text;
                            Menu.AnahtarKelimeler = TextBoxAnahtatrKelimeler.Text;
                            break;
                    }
                    Menu.MenuGorsel = TextBoxMenuGorsel.Text;
                    Menu.MenuGorselHover = TextBoxMenuGorselHover.Text;
                    Menu.Durum = CheckBoxDurum.Checked;
                    Menu.MenudeGoster = CheckBoxMenudeGoster.Checked;
                    Menu.BasligiGöster = CheckBoxBasligiGoster.Checked;
                    Menu.OkunmaSayisi = 0;
                    Menu.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Menu.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToMenuler(Menu);
                }
                else
                {
                    // Güncelle
                    int Id = Convert.ToInt32(RadTreeViewMenuler.SelectedNode.Value);
                    Menu = Veriler.Menuler.Where(p => p.Id == Id).FirstOrDefault();
                    Menu.MenuAdi = TextBoxMenuAdi.Text;
                    switch (DropDownListTipler.SelectedValue)
                    {
                        case "1":
                            Menu.MenuTipId = 1;
                            break;
                        case "2":
                            Menu.MenuTipId = 2;
                            Menu.Url = TextBoxUrl.Text;
                            break;
                        case "3":
                            Menu.MenuTipId = 3;
                            Menu.Ozet = TextBoxOzet.Text;
                            Menu.Icerik = RadEditorIcerik.Content;
                            Menu.Aciklama = TextBoxAciklama.Text;
                            Menu.AnahtarKelimeler = TextBoxAnahtatrKelimeler.Text;
                            break;
                    }
                    Menu.MenuGorsel = TextBoxMenuGorsel.Text;
                    Menu.MenuGorselHover = TextBoxMenuGorselHover.Text;
                    Menu.Durum = CheckBoxDurum.Checked;
                    Menu.MenudeGoster = CheckBoxMenudeGoster.Checked;
                    Menu.BasligiGöster = CheckBoxBasligiGoster.Checked;
                    Menu.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Menu.DuzenlemeTarihi = DateTime.Now;
                }
                Veriler.SaveChanges();
                MenuleriVer(RadTreeViewMenulerEdit);
                ResetMode();
                MesajKontrol1.Mesaj(true, "Kayıt edildi.");
            }
            catch (Exception Hata)
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }

        private int SiraNoVer(int UstMenuId)
        {
            // Sıra no ver
            int LokasyonId = Convert.ToInt32(Request.QueryString["LokasyonId"]);
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;

            Menuler Menu =
                Veriler.Menuler.Where(
                    p => p.UstMenuId == UstMenuId && p.MenuLokasyonId == LokasyonId && p.DilId == DilId).
                    OrderByDescending(p => p.SiraNo).FirstOrDefault();
            if (Menu != null)
            {
                return Convert.ToInt32(Menu.SiraNo + 1);
            }
            else
            {
                return 1;
            }
        }

        protected void ImageButtonSil_Click(object sender, ImageClickEventArgs e)
        {
            // Sil
            try
            {
                if (RadTreeViewMenuler.SelectedNode.Value != "0")
                {
                    int Id = Convert.ToInt32(RadTreeViewMenuler.SelectedNode.Value);
                    Menuler Menu = Veriler.Menuler.Where(p => p.Id == Id).First();
                    var AltMenuler = Veriler.Menuler.Where(p => p.UstMenuId == Id).ToList();
                    if (AltMenuler != null)
                    {
                        // Önce silinen menüye ait tüm alt menüler silinir.
                        foreach (var AltMenu in AltMenuler)
                        {
                            Veriler.DeleteObject(AltMenu);
                        }
                    }
                    Veriler.Menuler.DeleteObject(Menu);
                    Veriler.SaveChanges();
                    ResetMode();
                    MesajKontrol1.Mesaj(true, "Kayıt silindi.");
                }
                else
                {
                    MesajKontrol1.Mesaj(true, "Ana dizin silinemez!");
                }
            }
            catch (Exception Hata)
            {
                MesajKontrol1.Mesaj(false, "Hata ouştu.");
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            ResetMode();
        }
    }
}