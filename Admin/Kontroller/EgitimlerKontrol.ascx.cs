using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class EgitimlerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Eğitimler Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 11))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " order by BaslangicTarihi desc";
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
            EgitimKategorileriVer();
            TextBoxBaslik.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
            RadDateTimePickerBaslangicTarihi.SelectedDate = null;
            RadDateTimePickerBaslangicTarihi.SelectedDate = null;
            TextBoxKatilimciSayisi.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            CheckBoxPersonelTalebiMi.Checked = false;
            HiddenFieldId.Value = string.Empty;
        }

        private void EgitimKategorileriVer()
        {
            DropDownListEgitimKategorileriGridView.Items.Insert(0, new ListItem("Tümü", "0"));
            DropDownListEgitimKategorileriGridView.Items.Insert(1, new ListItem("Kurum'dan Eğitimler", "1"));
            DropDownListEgitimKategorileriGridView.Items.Insert(2, new ListItem("Personel Talepleri", "2"));
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Eğitim Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Egitimler E;
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    E = Veriler.Egitimler.Where(p => p.Id == Id).First();
                    E.Baslik = TextBoxBaslik.Text;
                    E.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        E.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        E.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        E.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        E.BitisTarihi = null;
                    }
                    if (!string.IsNullOrEmpty(TextBoxKatilimciSayisi.Text))
                    {
                        E.KatilimciSayisi = Convert.ToInt32(TextBoxKatilimciSayisi.Text);
                    }
                    else
                    {
                        E.KatilimciSayisi = null;
                    }
                    E.Durum = CheckBoxDurum.Checked;
                    E.PersonelTalebiMi = CheckBoxPersonelTalebiMi.Checked;
                    E.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    E.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    if (DropDownListEgitimKategorileriGridView.SelectedValue == "0")
                    {
                        EntityDataSource1.CommandText =
                            "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.DilId=="
                            + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                            + " order by BaslangicTarihi desc";
                    }
                    else if (DropDownListEgitimKategorileriGridView.SelectedValue == "1")
                    {
                        EntityDataSource1.CommandText =
                            "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == False order by BaslangicTarihi desc";
                    }
                    else if (DropDownListEgitimKategorileriGridView.SelectedValue == "2")
                    {
                        EntityDataSource1.CommandText =
                            "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == True order by BaslangicTarihi desc";
                    }
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    E = new Egitimler();
                    E.DilId = DilId;
                    E.Baslik = TextBoxBaslik.Text;
                    E.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        E.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        E.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        E.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        E.BitisTarihi = null;
                    }
                    if (!string.IsNullOrEmpty(TextBoxKatilimciSayisi.Text))
                    {
                        E.KatilimciSayisi = Convert.ToInt32(TextBoxKatilimciSayisi.Text);
                    }
                    else
                    {
                        E.KatilimciSayisi = null;
                    }
                    E.Durum = CheckBoxDurum.Checked;
                    E.PersonelTalebiMi = CheckBoxPersonelTalebiMi.Checked;
                    E.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    E.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToEgitimler(E);
                    Veriler.SaveChanges();
                    Temizle();
                    if (DropDownListEgitimKategorileriGridView.SelectedValue == "0")
                    {
                        EntityDataSource1.CommandText =
                            "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.DilId=="
                            + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                            + " order by BaslangicTarihi desc";
                    }
                    else if (DropDownListEgitimKategorileriGridView.SelectedValue == "1")
                    {
                        EntityDataSource1.CommandText =
                            "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == False order by BaslangicTarihi desc";
                    }
                    else if (DropDownListEgitimKategorileriGridView.SelectedValue == "2")
                    {
                        EntityDataSource1.CommandText =
                            "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == True order by BaslangicTarihi desc";
                    }
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
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                Egitimler E = Veriler.Egitimler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(E);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                Egitimler E = Veriler.Egitimler.Where(p => p.Id == Id).First();
                Veriler.Egitimler.DeleteObject(E);
                Veriler.SaveChanges();
                if (DropDownListEgitimKategorileriGridView.SelectedValue == "0")
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " order by BaslangicTarihi desc";
                }
                else if (DropDownListEgitimKategorileriGridView.SelectedValue == "1")
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == False order by BaslangicTarihi desc";
                }
                else if (DropDownListEgitimKategorileriGridView.SelectedValue == "2")
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == True order by BaslangicTarihi desc";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                // Sırala
                if (DropDownListEgitimKategorileriGridView.SelectedValue == "0")
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " order by BaslangicTarihi desc";
                }
                else if (DropDownListEgitimKategorileriGridView.SelectedValue == "1")
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == False order by BaslangicTarihi desc";
                }
                else if (DropDownListEgitimKategorileriGridView.SelectedValue == "2")
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == True order by BaslangicTarihi desc";
                }
            }
        }

        private void Guncelle(Egitimler E)
        {
            // Güncelle
            TextBoxBaslik.Text = E.Baslik;
            RadEditorIcerik.Content = E.Icerik;
            if (E.BaslangicTarihi != null)
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = E.BaslangicTarihi.Value;
            }
            else
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = null;
            }
            if (E.BitisTarihi != null)
            {
                RadDateTimePickerBitisTarihi.SelectedDate = E.BitisTarihi.Value;
            }
            else
            {
                RadDateTimePickerBitisTarihi.SelectedDate = null;
            }
            if (!string.IsNullOrEmpty(E.KatilimciSayisi.ToString()))
            {
                TextBoxKatilimciSayisi.Text = E.KatilimciSayisi.ToString();
            }
            else
            {
                TextBoxKatilimciSayisi.Text = string.Empty;
            }
            CheckBoxDurum.Checked = E.Durum;
            CheckBoxPersonelTalebiMi.Checked = E.PersonelTalebiMi;
            HiddenFieldId.Value = E.Id.ToString();
            LabelBaslik.Text = "Eğitim Düzenle";
        }

        protected void DropDownListEgitimKategorileriGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListEgitimKategorileriGridView.SelectedValue == "0")
            {
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " order by BaslangicTarihi desc";
            }
            else if (DropDownListEgitimKategorileriGridView.SelectedValue == "1")
            {
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == False order by BaslangicTarihi desc";
            }
            else if (DropDownListEgitimKategorileriGridView.SelectedValue == "2")
            {
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Egitimler as p where p.[PersonelTalebiMi] == True order by BaslangicTarihi desc";
            }
        }

        protected void GridViewVeriler_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelBasvuruSayisi = e.Row.FindControl("LabelBasvuruSayisi") as Label;
                LabelBasvuruSayisi.Text = BasvurusuSayisiVer(Convert.ToInt32(LabelBasvuruSayisi.Text)).ToString();
            }
        }

        private int BasvurusuSayisiVer(int EgitimId)
        {
            return Veriler.EgitimKatilimlari.Where(p => p.EgitimId == EgitimId && p.IptalEdilmisMi == false).Count();
        }
    }
}