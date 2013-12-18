using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class FotoAlbumGorselleriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Albüm Görselleri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 10))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumKategorileri as FAK join FotoAlbumler as FA on FAK.Id==FA.FotoAlbumKategoriId join FotoAlbumGorselleri as FAG on FA.Id==FAG.FotoAlbumId where FAK.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and FAK.Durum==true and FA.Durum==true";
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
            FotoAlbumKategorileriVer
                (DropDownListFotoAlbumKategorileri, new ListItem("Seçiniz", "0"));
            FotoAlbumKategorileriVer
                (DropDownListFotoAlbumKategorileriGridView, new ListItem("Seçiniz", "0"));
            FotoAlbumleriVer
                (DropDownListAlbumler, new ListItem("Seçiniz", "0"), 0);
            FotoAlbumleriVer
                (DropDownListAlbumlerGridView, new ListItem("Tümü", "0"), 0);
            TextBoxGorselAdi.Text = string.Empty;
            ImageGorsel.Visible = false;
            TextBoxGorsel.Text = string.Empty;
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
            TextBoxAciklama.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void FotoAlbumKategorileriVer(DropDownList DropDownList, ListItem Item)
        {
            // Kategoriler ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAK = Veriler.FotoAlbumKategorileri.Where(p => p.DilId == DilId && p.Durum).ToList();
            DropDownList.DataTextField = "FotoAlbumKategoriAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = FAK;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        private void FotoAlbumleriVer(DropDownList DropDownList, ListItem Item, int FotoAlbumKategoriId)
        {
            // Albümleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FA = Veriler.FotoAlbumler.Where(p => p.FotoAlbumKategoriId == FotoAlbumKategoriId && p.Durum).ToList();
            DropDownList.DataTextField = "FotoAlbumAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = FA;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Görsel Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                FotoAlbumGorselleri FAG;
                string AnaDizin = "../Gorseller";
                string Kategori = "FotoAlbumGorselleri";
                string Yil = DateTime.Now.Year.ToString();
                string Ay = string.Empty;
                int A = DateTime.Now.Month;
                if (A < 9)
                {
                    Ay = "0" + A.ToString();
                }
                else
                {
                    Ay = A.ToString();
                }
                string Thumbnails = "Thumbnails";
                if (!Directory.Exists(Server.MapPath(AnaDizin)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin));
                }
                if (!Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin + "/" + Kategori));
                }
                if (!Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil));
                }
                if (!Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay));
                }
                if (
                    !Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails)))
                {
                    Directory.CreateDirectory(
                        Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails));
                }
                string KayitYeri = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                string KayitYeriThumbnail = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    FAG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
                    FAG.FotoAlbumId = Convert.ToInt32(DropDownListAlbumler.SelectedValue);
                    FAG.GorselAdi = TextBoxGorselAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != FAG.Gorsel)
                        {
                            GorselSil(FAG.Gorsel);
                            GorselSil(FAG.GorselThumbnail);
                            FAG.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                            FAG.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(FAG.Gorsel);
                        GorselSil(FAG.GorselThumbnail);
                        FAG.Gorsel = null;
                        FAG.GorselThumbnail = null;
                    }
                    FAG.Aciklama = TextBoxAciklama.Text;
                    FAG.Durum = CheckBoxDurum.Checked;
                    FAG.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FAG.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListFotoAlbumKategorileriGridView.SelectedValue
                        =
                        Veriler.FotoAlbumler.Where(p => p.Id == FAG.FotoAlbumId).First().FotoAlbumKategoriId.ToString();
                    FotoAlbumleriVer(DropDownListAlbumlerGridView, new ListItem("Seçiniz", "0"),
                                     Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue));
                    DropDownListAlbumlerGridView.SelectedValue
                        = FAG.FotoAlbumId.ToString();
                    EntityDataSource1.CommandText =
                        "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumGorselleri as FAG where FAG.FotoAlbumId==" +
                        DropDownListAlbumlerGridView.SelectedValue + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    FAG = new FotoAlbumGorselleri();
                    FAG.FotoAlbumId = Convert.ToInt32(DropDownListAlbumler.SelectedValue);
                    FAG.GorselAdi = TextBoxGorselAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        FAG.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                        FAG.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        FAG.Gorsel = null;
                        FAG.GorselThumbnail = null;
                    }
                    FAG.Aciklama = TextBoxAciklama.Text;
                    FAG.Durum = CheckBoxDurum.Checked;
                    FAG.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FAG.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToFotoAlbumGorselleri(FAG);
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListFotoAlbumKategorileriGridView.SelectedValue
                        =
                        Veriler.FotoAlbumler.Where(p => p.Id == FAG.FotoAlbumId).First().FotoAlbumKategoriId.ToString();
                    FotoAlbumleriVer(DropDownListAlbumlerGridView, new ListItem("Seçiniz", "0"),
                                     Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue));
                    DropDownListAlbumlerGridView.SelectedValue
                        = FAG.FotoAlbumId.ToString();
                    EntityDataSource1.CommandText =
                        "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumGorselleri as FAG where FAG.FotoAlbumId=="
                        + DropDownListAlbumlerGridView.SelectedValue + "";
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

        public string GorselBoyutlandir(TextBox ResimYolu, string KayitYeri, int Genislik)
        {
            // Görsel boyutlandır
            Bitmap OrjinalResim = new Bitmap(Server.MapPath(ResimYolu.Text.Replace("~", "..")));
            ImageFormat Format = OrjinalResim.RawFormat;
            ImageCodecInfo ICI;
            if (Format.Equals(ImageFormat.Gif) || Format.Equals(ImageFormat.Png))
            {
                ICI = GetEncoder(ImageFormat.Png);
            }
            else
            {
                ICI = GetEncoder(ImageFormat.Jpeg);
            }
            Encoder E = Encoder.Quality;
            EncoderParameters EP = new EncoderParameters(1);
            EncoderParameter eParam = new EncoderParameter(E, 70L);
            EP.Param[0] = eParam;
            Bitmap YeniResim = null;
            Graphics Graphic = null;
            if (OrjinalResim.Width >= OrjinalResim.Height)
            {
                if (OrjinalResim.Width > Genislik)
                {
                    decimal ORW = OrjinalResim.Width;
                    decimal ORH = OrjinalResim.Height;
                    decimal ORO = ORW/ORH;
                    int NW = Genislik;
                    decimal NHT = NW/ORO;
                    int NH = Convert.ToInt16(NHT);
                    YeniResim = new Bitmap(OrjinalResim, NW, NH);
                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, NW, NH);
                }
                else
                {
                    YeniResim = new Bitmap(OrjinalResim, OrjinalResim.Width, OrjinalResim.Height);
                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, OrjinalResim.Width, OrjinalResim.Height);
                }
            }
            else
            {
                if (OrjinalResim.Height > Genislik)
                {
                    decimal ORO = OrjinalResim.Height/OrjinalResim.Width;
                    int NW = Convert.ToInt16(Genislik/ORO);
                    YeniResim = new Bitmap(OrjinalResim, NW, Genislik);
                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, NW, Genislik);
                }
                else
                {
                    YeniResim = new Bitmap(OrjinalResim, OrjinalResim.Width, OrjinalResim.Height);

                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, OrjinalResim.Width, OrjinalResim.Height);
                }
            }
            string ResimAdi = Guid.NewGuid() + ".jpg";
            YeniResim.Save(Server.MapPath(KayitYeri + ResimAdi), ICI, EP);
            return KayitYeri.Replace("../", "~/") + ResimAdi;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            // Codec'i ayarla
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void GorselSil(string Resim)
        {
            // Görsel sil
            if (File.Exists(Server.MapPath(Resim)))
            {
                File.Delete(Server.MapPath(Resim));
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
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                FotoAlbumGorselleri FAG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(FAG);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                FotoAlbumGorselleri FAG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
                Veriler.FotoAlbumGorselleri.DeleteObject(FAG);
                Veriler.SaveChanges();
                GorselSil(FAG.Gorsel);
                GorselSil(FAG.GorselThumbnail);
                if (DropDownListAlbumlerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumKategorileri as FAK join FotoAlbumler as FA on FAK.Id==FA.FotoAlbumKategoriId join FotoAlbumGorselleri as FAG on FA.Id==FAG.FotoAlbumId where FAK.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and FAK.Durum==true and FA.Durum==true";
                }
                else
                {
                    EntityDataSource1.CommandText =
                        "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumGorselleri as FAG where FAG.FotoAlbumId==" +
                        DropDownListAlbumlerGridView.SelectedValue + "";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                // Sırala
                if (DropDownListAlbumlerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumKategorileri as FAK join FotoAlbumler as FA on FAK.Id==FA.FotoAlbumKategoriId join FotoAlbumGorselleri as FAG on FA.Id==FAG.FotoAlbumId where FAK.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and FAK.Durum==true and FA.Durum==true";
                }
                else
                {
                    EntityDataSource1.CommandText =
                        "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumGorselleri as FAG where FAG.FotoAlbumId==" +
                        DropDownListAlbumlerGridView.SelectedValue + "";
                }
            }
        }

        private void Guncelle(FotoAlbumGorselleri FAG)
        {
            // Güncelle
            DropDownListFotoAlbumKategorileri.SelectedValue =
                Veriler.FotoAlbumler.Where(p => p.Id == FAG.FotoAlbumId).First().FotoAlbumKategoriId.ToString();
            FotoAlbumleriVer(DropDownListAlbumler, new ListItem("Seçiniz", "0"),
                             Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue));
            DropDownListAlbumler.SelectedValue = FAG.FotoAlbumId.ToString();
            TextBoxGorselAdi.Text = FAG.GorselAdi;
            ImageGorsel.Visible = true;
            ImageGorsel.ImageUrl = FAG.GorselThumbnail;
            TextBoxGorsel.Text = FAG.Gorsel;
            TextBoxAciklama.Text = FAG.Aciklama;
            CheckBoxDurum.Checked = FAG.Durum;
            HiddenFieldId.Value = FAG.Id.ToString();
            LabelBaslik.Text = "Görsel Düzenle";
        }

        protected void DropDownListFotoAlbumKategorileri_SelectedIndexChanged(object sender, EventArgs e)
        {
            FotoAlbumleriVer(DropDownListAlbumler, new ListItem("Seçiniz", "0"),
                             Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue));
        }

        protected void DropDownListFotoAlbumKategorileriGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            FotoAlbumleriVer(DropDownListAlbumlerGridView, new ListItem("Tümü", "0"),
                             Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue));
        }

        protected void DropDownListAlbumlerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListAlbumlerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumKategorileri as FAK join FotoAlbumler as FA on FAK.Id==FA.FotoAlbumKategoriId join FotoAlbumGorselleri as FAG on FA.Id==FAG.FotoAlbumId where FAK.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                    " and FAK.Durum==true and FA.Durum==true";
            }
            else
            {
                EntityDataSource1.CommandText =
                    "select FAG.Id, FAG.GorselAdi, FAG.GorselThumbnail, FAG.Aciklama, FAG.Durum from FotoAlbumGorselleri as FAG where FAG.FotoAlbumId==" +
                    DropDownListAlbumlerGridView.SelectedValue + "";
            }
            GridViewVeriler.DataBind();
        }
    }
}