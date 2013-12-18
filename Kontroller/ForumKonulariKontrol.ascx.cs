using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class ForumKonulariKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Temizle();
            }
        }

        private void Temizle()
        {
            ForumKategorileriVer(DropDownListForumKategorileri, new ListItem("Seçiniz", "0"));
            TextBoxBaslik.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
            MesajKontrol1.Reset();
            LiteralSiteMap.Text = "<a href=\"../Forumlar.aspx\">Tüm Forumlar</a> / ";
            LiteralSiteMap.Text += "<a href=\"../Portal/ForumKonuEkle.aspx\">Yeni Konu Ekle</a>";
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

        protected void LinkButtonKonuKaydet_Click(object sender, EventArgs e)
        {
            // Kaydet
            try
            {
                ForumKonulari FK = new ForumKonulari();
                FK.ForumKategoriId =
                    Convert.ToInt32(DropDownListForumKategorileri.SelectedValue);
                FK.Baslik = TextBoxBaslik.Text;
                FK.Icerik = RadEditorIcerik.Content;
                FK.OkunmaSayisi = 0;
                FK.Durum = true;
                FK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                FK.KaydetmeTarihi = DateTime.Now;
                Veriler.AddToForumKonulari(FK);
                Veriler.SaveChanges();
                Temizle();
                MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                Response.Redirect("/Forumlar.aspx?KategoriId=" + FK.ForumKategoriId);
            }
            catch
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu!");
            }
        }

        protected void LinkButtonKonuIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Forumlar.aspx");
        }
    }
}