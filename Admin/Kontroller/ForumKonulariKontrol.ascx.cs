using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class ForumKonulariKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Forum Konuları Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 12))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select FA.[Id], FA.[Baslik], FA.[Durum] from ForumKonulari as FA join ForumKategorileri as FK on FA.ForumKategoriId == FK.Id where FK.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void Temizle()
        {
            ForumKategorileriVer(DropDownListForumKategorileri, new ListItem("Seçiniz", "0"));
            ForumKategorileriVer(DropDownListForumKategorileriGridView, new ListItem("Tümü", "0"));
            TextBoxBaslik.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void ForumKategorileriVer(DropDownList DropDownList, ListItem Item)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FKList = Veriler.ForumKategorileri.Where(p => p.DilId == DilId && p.Durum).ToList();
            DropDownList.DataTextField = "KategoriAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = FKList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Forum Konu Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ForumKonulari FK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    FK = Veriler.ForumKonulari.Where(p => p.Id == Id).First();
                    FK.ForumKategoriId =
                        Convert.ToInt32(DropDownListForumKategorileri.SelectedValue);
                    FK.Baslik = TextBoxBaslik.Text;
                    FK.Icerik = RadEditorIcerik.Content;
                    FK.Durum = CheckBoxDurum.Checked;
                    FK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FK.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListForumKategorileriGridView.SelectedValue = FK.ForumKategoriId.ToString();
                    EntityDataSource1.CommandText =
                        "select FA.[Id], FA.[Baslik], FA.[Durum] from ForumKonulari as FA join ForumKategorileri as FK on FA.ForumKategoriId == FK.Id where FK.Id == " +
                        FK.ForumKategoriId + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    FK = new ForumKonulari();
                    FK.ForumKategoriId =
                        Convert.ToInt32(DropDownListForumKategorileri.SelectedValue);
                    FK.Baslik = TextBoxBaslik.Text;
                    FK.Icerik = RadEditorIcerik.Content;
                    FK.OkunmaSayisi = 0;
                    FK.Durum = CheckBoxDurum.Checked;
                    FK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToForumKonulari(FK);
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListForumKategorileriGridView.SelectedValue = FK.ForumKategoriId.ToString();
                    EntityDataSource1.CommandText =
                        "select FA.[Id], FA.[Baslik], FA.[Durum] from ForumKonulari as FA join ForumKategorileri as FK on FA.ForumKategoriId == FK.Id where FK.Id == " +
                        FK.ForumKategoriId + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                ForumKonulari FK = Veriler.ForumKonulari.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(FK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                ForumKonulari FK = Veriler.ForumKonulari.Where(p => p.Id == Id).First();
                Veriler.ForumKonulari.DeleteObject(FK);
                Veriler.SaveChanges();
                if (DropDownListForumKategorileriGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select FA.[Id], FA.[Baslik], FA.[Durum] from ForumKonulari as FA join ForumKategorileri as FK on FA.ForumKategoriId == FK.Id where FK.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    EntityDataSource1.CommandText =
                        "select FA.[Id], FA.[Baslik], FA.[Durum] from ForumKonulari as FA join ForumKategorileri as FK on FA.ForumKategoriId == FK.Id where FK.Id == "
                        + DropDownListForumKategorileriGridView.SelectedValue
                        + "";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(ForumKonulari FK)
        {
            // Güncelle
            DropDownListForumKategorileri.SelectedValue = FK.ForumKategoriId.ToString();
            TextBoxBaslik.Text = FK.Baslik;
            RadEditorIcerik.Content = FK.Icerik;
            CheckBoxDurum.Checked = FK.Durum;
            HiddenFieldId.Value = FK.Id.ToString();
            LabelBaslik.Text = "Forum Konu Düzenle";
        }

        protected void DropDownListForumKategorileriGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListForumKategorileriGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select FA.[Id], FA.[Baslik], FA.[Durum] from ForumKonulari as FA join ForumKategorileri as FK on FA.ForumKategoriId == FK.Id where FK.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
            }
            else
            {
                EntityDataSource1.CommandText =
                    "select FA.[Id], FA.[Baslik], FA.[Durum] from ForumKonulari as FA join ForumKategorileri as FK on FA.ForumKategoriId == FK.Id where FK.Id == "
                    + DropDownListForumKategorileriGridView.SelectedValue
                    + "";
            }
            GridViewVeriler.DataBind();
        }
    }
}