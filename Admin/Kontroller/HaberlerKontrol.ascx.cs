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
    public partial class HaberlerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Haberler Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 5))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    KategorileriVer(DropDownListKategorilerGridView);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id == p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum==True order by KayitTarihi desc";
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
            TextBoxBaslik.Text = string.Empty;
            TextBoxOzet.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
            KategorileriVer(CheckBoxListKategoriler);
            RadDateTimePickerKayitTarihi.SelectedDate = DateTime.Now;
            ImageGorsel1.Visible = false;
            TextBoxGorsel1.Text = string.Empty;
            ImageButtonGorsel1.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                               + TextBoxGorsel1.ClientID + "','','width=640,height=480');";
            ImageGorsel2.Visible = false;
            TextBoxGorsel2.Text = string.Empty;
            ImageButtonGorsel2.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                               + TextBoxGorsel2.ClientID + "','','width=640,height=480');";
            ImageGorsel3.Visible = false;
            TextBoxGorsel3.Text = string.Empty;
            ImageButtonGorsel3.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                               + TextBoxGorsel3.ClientID + "','','width=640,height=480');";
            ImageGorsel4.Visible = false;
            TextBoxGorsel4.Text = string.Empty;
            ImageButtonGorsel4.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                               + TextBoxGorsel4.ClientID + "','','width=640,height=480');";
            CheckBoxDurum.Checked = false;
            TextBoxAnahtarKelimeler.Text = string.Empty;
            KategorileriVer(DropDownListKategorilerGridView);
            HiddenFieldId.Value = string.Empty;
        }

        private void KategorileriVer(DropDownList DropDownListKategoriler)
        {
            // Kategoriler ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HK = Veriler.HaberKategorileri.Where(p => p.DilId == DilId && p.Durum).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = HK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void KategorileriVer(CheckBoxList CheckBoxListKategoriler)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HK = Veriler.HaberKategorileri.Where(p => p.DilId == DilId && p.Durum).ToList();
            CheckBoxListKategoriler.DataTextField = "KategoriAdi";
            CheckBoxListKategoriler.DataValueField = "Id";
            CheckBoxListKategoriler.DataSource = HK;
            CheckBoxListKategoriler.DataBind();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Haber Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Haberler H;
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                string AnaDizin = "../Gorseller";
                string Kategori = "Haberler";
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
                    H = Veriler.Haberler.Where(p => p.Id == Id).First();
                    H.Baslik = TextBoxBaslik.Text;
                    H.Ozet = TextBoxOzet.Text;
                    H.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerKayitTarihi.SelectedDate != null)
                    {
                        H.KayitTarihi = Convert.ToDateTime(RadDateTimePickerKayitTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        H.KayitTarihi = null;
                    }
                    H.DuzenlenmeTarihi = DateTime.Now;
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != H.Gorsel1)
                        {
                            GorselSil(H.Gorsel1);
                            GorselSil(H.GorselThumbnail1);
                            H.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            H.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(H.Gorsel1);
                        GorselSil(H.GorselThumbnail1);
                        H.Gorsel1 = null;
                        H.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != H.Gorsel2)
                        {
                            GorselSil(H.Gorsel2);
                            GorselSil(H.GorselThumbnail2);
                            H.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            H.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(H.Gorsel2);
                        GorselSil(H.GorselThumbnail2);
                        H.Gorsel2 = null;
                        H.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != H.Gorsel3)
                        {
                            GorselSil(H.Gorsel3);
                            GorselSil(H.GorselThumbnail3);
                            H.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            H.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(H.Gorsel3);
                        GorselSil(H.GorselThumbnail3);
                        H.Gorsel3 = null;
                        H.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != H.Gorsel4)
                        {
                            GorselSil(H.Gorsel4);
                            GorselSil(H.GorselThumbnail4);
                            H.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            H.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(H.Gorsel4);
                        GorselSil(H.GorselThumbnail4);
                        H.Gorsel4 = null;
                        H.GorselThumbnail4 = null;
                    }
                    H.Durum = CheckBoxDurum.Checked;
                    H.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    H.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    H.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    KategorileriSil(Id);
                    for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
                    {
                        if (CheckBoxListKategoriler.Items[i].Selected)
                        {
                            KategoriKaydet(Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value), Id);
                        }
                    }
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id == p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum== True order by KayitTarihi desc";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    H = new Haberler();
                    H.Baslik = TextBoxBaslik.Text;
                    H.Ozet = TextBoxOzet.Text;
                    H.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerKayitTarihi.SelectedDate.Value != null)
                    {
                        H.KayitTarihi = Convert.ToDateTime(RadDateTimePickerKayitTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        H.KayitTarihi = null;
                    }
                    H.DuzenlenmeTarihi = DateTime.Now;
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != H.Gorsel1)
                        {
                            H.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            H.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        H.Gorsel1 = null;
                        H.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != H.Gorsel2)
                        {
                            H.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            H.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        H.Gorsel2 = null;
                        H.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != H.Gorsel3)
                        {
                            H.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            H.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        H.Gorsel3 = null;
                        H.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != H.Gorsel4)
                        {
                            H.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            H.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        H.Gorsel4 = null;
                        H.GorselThumbnail4 = null;
                    }
                    H.Durum = CheckBoxDurum.Checked;
                    H.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    H.OkunmaSayisi = 0;
                    H.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    H.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToHaberler(H);
                    Veriler.SaveChanges();
                    Haberler HH =
                        Veriler.Haberler.Where(
                            p =>
                            p.Baslik == TextBoxBaslik.Text && p.Ozet == TextBoxOzet.Text &&
                            p.Icerik == RadEditorIcerik.Content).First();
                    for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
                    {
                        if (CheckBoxListKategoriler.Items[i].Selected)
                        {
                            KategoriKaydet(Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value), HH.Id);
                        }
                    }
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id == p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum== True order by KayitTarihi desc";
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

        private void KategorileriSil(int HaberId)
        {
            // Kategori sil
            var HTList = Veriler.HaberlerTablosu.Where(p => p.HaberId == HaberId).ToList();
            foreach (var Item in HTList)
            {
                HaberlerTablosu HT = Veriler.HaberlerTablosu.Where(p => p.HaberId == Item.HaberId).First();
                Veriler.HaberlerTablosu.DeleteObject(HT);
                Veriler.SaveChanges();
            }
        }

        private void KategoriKaydet(int KategoriId, int HaberId)
        {
            // Kategori sil
            HaberlerTablosu HT = new HaberlerTablosu();
            HT.HaberId = HaberId;
            HT.KategoriId = KategoriId;
            Veriler.HaberlerTablosu.AddObject(HT);
            Veriler.SaveChanges();
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
            // Codec ayarla
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
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                Haberler H = Veriler.Haberler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(H);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                Haberler H = Veriler.Haberler.Where(p => p.Id == Id).First();
                Veriler.Haberler.DeleteObject(H);
                Veriler.SaveChanges();
                GorselSil(H.Gorsel1);
                GorselSil(H.GorselThumbnail1);
                GorselSil(H.Gorsel2);
                GorselSil(H.GorselThumbnail2);
                GorselSil(H.Gorsel3);
                GorselSil(H.GorselThumbnail3);
                GorselSil(H.Gorsel4);
                GorselSil(H.GorselThumbnail4);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id == p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                    " and p2.Durum== True order by KayitTarihi desc";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                // Sırala
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id == p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum== True order by KayitTarihi desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id==p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " and p2.Durum==True and p2.Id== " +
                        KategoriId + " order by KayitTarihi desc";
                }
            }
        }

        private void Guncelle(Haberler H)
        {
            // Güncelle
            TextBoxBaslik.Text = H.Baslik;
            TextBoxOzet.Text = H.Ozet;
            RadEditorIcerik.Content = H.Icerik;
            if (H.KayitTarihi != null)
            {
                RadDateTimePickerKayitTarihi.SelectedDate = H.KayitTarihi.Value;
            }
            else
            {
                RadDateTimePickerKayitTarihi.SelectedDate = null;
            }
            if (H.GorselThumbnail1 != null)
            {
                ImageGorsel1.Visible = true;
                ImageGorsel1.ImageUrl = H.GorselThumbnail1;
                TextBoxGorsel1.Text = H.Gorsel1;
            }
            if (H.GorselThumbnail2 != null)
            {
                ImageGorsel2.Visible = true;
                ImageGorsel2.ImageUrl = H.GorselThumbnail2;
                TextBoxGorsel2.Text = H.Gorsel2;
            }
            if (H.GorselThumbnail3 != null)
            {
                ImageGorsel3.Visible = true;
                ImageGorsel3.ImageUrl = H.GorselThumbnail3;
                TextBoxGorsel3.Text = H.Gorsel3;
            }
            if (H.GorselThumbnail4 != null)
            {
                ImageGorsel4.Visible = true;
                ImageGorsel4.ImageUrl = H.GorselThumbnail4;
                TextBoxGorsel4.Text = H.Gorsel4;
            }
            CheckBoxDurum.Checked = H.Durum;
            TextBoxAnahtarKelimeler.Text = H.AnahtarKelimeler;
            HiddenFieldId.Value = H.Id.ToString();
            KategorileriVer(CheckBoxListKategoriler);

            for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
            {
                int KategoriId = Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value);
                var HTList =
                    Veriler.HaberlerTablosu.Where(p => p.HaberId == H.Id && p.KategoriId == KategoriId).ToList();
                foreach (var Item in HTList)
                {
                    CheckBoxListKategoriler.Items[i].Selected = true;
                }
            }
            LabelBaslik.Text = "Haber Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id == p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                    " and p2.Durum== True order by KayitTarihi desc";
            }
            else
            {
                int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[KayitTarihi], p.[DuzenlenmeTarihi], p.[Durum] from Haberler as p join HaberlerTablosu as p1 on p.Id==p1.HaberId join HaberKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + " and p2.Durum==True and p2.Id== " +
                    KategoriId + " order by KayitTarihi desc";
            }
            GridViewVeriler.DataBind();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }
    }
}