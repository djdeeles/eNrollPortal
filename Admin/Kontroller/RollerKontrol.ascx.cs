using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class RollerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Roller Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 2))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
        }

        private void Temizle()
        {
            TextBoxRolAdi.Text = string.Empty;
            YetkiAlalariniVer(CheckBoxListYetkiAlanlari);
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void YetkiAlalariniVer(CheckBoxList CheckBoxList)
        {
            // Yetki alanlarını ver
            var YAList = from p in Veriler.YetkiAlanlari
                         orderby p.YetkiAlani
                         select p;
            if (YAList.Count() != 0)
            {
                CheckBoxList.DataTextField = "YetkiAlani";
                CheckBoxList.DataValueField = "Id";
                CheckBoxList.DataSource = YAList;
                CheckBoxList.DataBind();
            }
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            Temizle();
            MultiView1.ActiveViewIndex = 1;
            LabelBaslik.Text = "Rol Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Roller Rol;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    Rol = Veriler.Roller.Where(p => p.Id == Id).First();
                    Rol.RolAdi = TextBoxRolAdi.Text;
                    Rol.Durum = CheckBoxDurum.Checked;
                    Rol.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Rol.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    RolYetkiAlanlariniKaydet(CheckBoxListYetkiAlanlari, Rol);
                    Temizle();
                    GridViewRoller.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    Rol = new Roller();
                    Rol.RolAdi = TextBoxRolAdi.Text;
                    Rol.Durum = CheckBoxDurum.Checked;
                    Rol.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Rol.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToRoller(Rol);
                    Veriler.SaveChanges();
                    RolYetkiAlanlariniKaydet(CheckBoxListYetkiAlanlari, Rol);
                    Temizle();
                    GridViewRoller.DataBind();
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

        private void RolYetkiAlanlariniKaydet(CheckBoxList YetkiAlanlari, Roller Roller)
        {
            // Yetki alanlarını kaydet
            Roller R = Veriler.Roller.Where(p => p.RolAdi == Roller.RolAdi && p.Durum == Roller.Durum).First();
            var RYAList = Veriler.RolYetkiAlanlari.Where(p => p.RolId == R.Id).ToList();
            foreach (RolYetkiAlanlari RYA in RYAList)
            {
                Veriler.RolYetkiAlanlari.DeleteObject(RYA);
                Veriler.SaveChanges();
            }
            foreach (ListItem Li in YetkiAlanlari.Items)
            {
                if (Li.Selected)
                {
                    AktifRolKaydet(Convert.ToInt32(Li.Value), R.Id);
                }
            }
        }

        private void AktifRolKaydet(int YetkiAlaniId, int RolId)
        {
            // Aktif rolü kaydet
            RolYetkiAlanlari RYA = new RolYetkiAlanlari();
            RYA.YetkiAlaniId = YetkiAlaniId;
            RYA.RolId = RolId;
            Veriler.AddToRolYetkiAlanlari(RYA);
            Veriler.SaveChanges();
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewRoller_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                Roller Rol = Veriler.Roller.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                RolGuncelle(Rol);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                Roller Rol = Veriler.Roller.Where(p => p.Id == Id).First();
                var RYAList = Veriler.RolYetkiAlanlari.Where(p => p.RolId == Rol.Id).ToList();
                foreach (RolYetkiAlanlari RYA in RYAList)
                {
                    Veriler.RolYetkiAlanlari.DeleteObject(RYA);
                    Veriler.SaveChanges();
                }
                var KRList = Veriler.KullaniciRolleri.Where(p => p.RolId == Rol.Id).ToList();
                foreach (KullaniciRolleri KR in KRList)
                {
                    Veriler.KullaniciRolleri.DeleteObject(KR);
                    Veriler.SaveChanges();
                }
                Veriler.Roller.DeleteObject(Rol);
                Veriler.SaveChanges();
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
                GridViewRoller.DataBind();
                MultiView1.ActiveViewIndex = 0;
            }
        }

        private void RolGuncelle(Roller Rol)
        {
            // Güncelle
            Temizle();
            TextBoxRolAdi.Text = Rol.RolAdi;
            CheckBoxDurum.Checked = Rol.Durum;
            HiddenFieldId.Value = Rol.Id.ToString();
            LabelBaslik.Text = "Rol Düzenle";
            YetkiAlalariniVer(CheckBoxListYetkiAlanlari);
            var RYAList = Veriler.RolYetkiAlanlari.Where(p => p.RolId == Rol.Id).ToList();
            foreach (RolYetkiAlanlari RYA in RYAList)
            {
                for (int i = 0; i <= CheckBoxListYetkiAlanlari.Items.Count - 1; i++)
                {
                    if (CheckBoxListYetkiAlanlari.Items[i].Value == RYA.YetkiAlaniId.ToString())
                    {
                        CheckBoxListYetkiAlanlari.Items[i].Selected = true;
                    }
                }
            }
        }
    }
}