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
    public partial class DuyurularKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Duyurular Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 6))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    KategorileriVer(DropDownListKategorilerGridView);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum==True order by BaslangicTarihi desc";
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
            RadDateTimePickerBaslangicTarihi.SelectedDate = DateTime.Now;
            RadDateTimePickerBitisTarihi.SelectedDate = null;
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
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DK = Veriler.DuyuruKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = DK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void KategorileriVer(CheckBoxList CheckBoxListKategoriler)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DK = Veriler.DuyuruKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList();
            CheckBoxListKategoriler.DataTextField = "KategoriAdi";
            CheckBoxListKategoriler.DataValueField = "Id";
            CheckBoxListKategoriler.DataSource = DK;
            CheckBoxListKategoriler.DataBind();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Duyuru Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Duyurular D;
                string AnaDizin = "../Gorseller";
                string Kategori = "Duyurular";
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
                    D = Veriler.Duyurular.Where(p => p.Id == Id).First();
                    D.Baslik = TextBoxBaslik.Text;
                    D.Ozet = TextBoxOzet.Text;
                    D.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        D.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        D.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        D.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        D.BitisTarihi = null;
                    }
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != D.Gorsel1)
                        {
                            GorselSil(D.Gorsel1);
                            GorselSil(D.GorselThumbnail1);
                            D.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            D.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(D.Gorsel1);
                        GorselSil(D.GorselThumbnail1);
                        D.Gorsel1 = null;
                        D.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != D.Gorsel2)
                        {
                            GorselSil(D.Gorsel2);
                            GorselSil(D.GorselThumbnail2);
                            D.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            D.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(D.Gorsel2);
                        GorselSil(D.GorselThumbnail2);
                        D.Gorsel2 = null;
                        D.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != D.Gorsel3)
                        {
                            GorselSil(D.Gorsel3);
                            GorselSil(D.GorselThumbnail3);
                            D.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            D.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(D.Gorsel3);
                        GorselSil(D.GorselThumbnail3);
                        D.Gorsel3 = null;
                        D.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != D.Gorsel4)
                        {
                            GorselSil(D.Gorsel4);
                            GorselSil(D.GorselThumbnail4);
                            D.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            D.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(D.Gorsel4);
                        GorselSil(D.GorselThumbnail4);
                        D.Gorsel4 = null;
                        D.GorselThumbnail4 = null;
                    }
                    D.Durum = CheckBoxDurum.Checked;
                    D.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    D.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    D.DuzenlemeTarihi = DateTime.Now;
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
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum==True order by BaslangicTarihi desc";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    D = new Duyurular();
                    D.Baslik = TextBoxBaslik.Text;
                    D.Ozet = TextBoxOzet.Text;
                    D.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        D.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        D.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        D.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        D.BitisTarihi = null;
                    }
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != D.Gorsel1)
                        {
                            D.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            D.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        D.Gorsel1 = null;
                        D.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != D.Gorsel2)
                        {
                            D.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            D.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        D.Gorsel2 = null;
                        D.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != D.Gorsel3)
                        {
                            D.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            D.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        D.Gorsel3 = null;
                        D.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != D.Gorsel4)
                        {
                            D.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            D.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        D.Gorsel4 = null;
                        D.GorselThumbnail4 = null;
                    }
                    D.Durum = CheckBoxDurum.Checked;
                    D.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    D.OkunmaSayisi = 0;
                    D.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    D.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToDuyurular(D);
                    Veriler.SaveChanges();
                    Duyurular DD =
                        Veriler.Duyurular.Where(
                            p =>
                            p.Baslik == TextBoxBaslik.Text && p.Ozet == TextBoxOzet.Text &&
                            p.Icerik == RadEditorIcerik.Content).First();
                    for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
                    {
                        if (CheckBoxListKategoriler.Items[i].Selected)
                        {
                            KategoriKaydet(Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value), DD.Id);
                        }
                    }
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum==True order by BaslangicTarihi desc";
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

        private void KategorileriSil(int DuyuruId)
        {
            // Kategorileri sil
            var DTList = Veriler.DuyurularTablosu.Where(p => p.DuyuruId == DuyuruId).ToList();
            foreach (var Item in DTList)
            {
                DuyurularTablosu DT = Veriler.DuyurularTablosu.Where(p => p.DuyuruId == Item.DuyuruId).First();
                Veriler.DuyurularTablosu.DeleteObject(DT);
                Veriler.SaveChanges();
            }
        }

        private void KategoriKaydet(int KategoriId, int DuyuruId)
        {
            // Kategori kaydet
            DuyurularTablosu DT = new DuyurularTablosu();
            DT.DuyuruId = DuyuruId;
            DT.KategoriId = KategoriId;
            Veriler.DuyurularTablosu.AddObject(DT);
            Veriler.SaveChanges();
        }

        public string GorselBoyutlandir(TextBox ResimYolu, string KayitYeri, int Genislik)
        {
            // Görsel buytlandır
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
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                Duyurular D = Veriler.Duyurular.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(D);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                Duyurular D = Veriler.Duyurular.Where(p => p.Id == Id).First();
                Veriler.Duyurular.DeleteObject(D);
                Veriler.SaveChanges();
                GorselSil(D.Gorsel1);
                GorselSil(D.GorselThumbnail1);
                GorselSil(D.Gorsel2);
                GorselSil(D.GorselThumbnail2);
                GorselSil(D.Gorsel3);
                GorselSil(D.GorselThumbnail3);
                GorselSil(D.Gorsel4);
                GorselSil(D.GorselThumbnail4);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                    " and p2.Durum==True order by BaslangicTarihi desc";
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
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                        " and p2.Durum==True order by BaslangicTarihi desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " and p2.Durum==True and p2.Id== " +
                        KategoriId + " order by BaslangicTarihi desc";
                }
            }
        }

        private void Guncelle(Duyurular D)
        {
            // Güncelle
            TextBoxBaslik.Text = D.Baslik;
            TextBoxOzet.Text = D.Ozet;
            RadEditorIcerik.Content = D.Icerik;
            if (D.BaslangicTarihi != null)
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = D.BaslangicTarihi.Value;
            }
            else
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = null;
            }
            if (D.BitisTarihi != null)
            {
                RadDateTimePickerBitisTarihi.SelectedDate = D.BitisTarihi.Value;
            }
            else
            {
                RadDateTimePickerBitisTarihi.SelectedDate = null;
            }
            if (D.GorselThumbnail1 != null)
            {
                ImageGorsel1.Visible = true;
                ImageGorsel1.ImageUrl = D.GorselThumbnail1;
                TextBoxGorsel1.Text = D.Gorsel1;
            }
            if (D.GorselThumbnail2 != null)
            {
                ImageGorsel2.Visible = true;
                ImageGorsel2.ImageUrl = D.GorselThumbnail2;
                TextBoxGorsel2.Text = D.Gorsel2;
            }
            if (D.GorselThumbnail3 != null)
            {
                ImageGorsel3.Visible = true;
                ImageGorsel3.ImageUrl = D.GorselThumbnail3;
                TextBoxGorsel3.Text = D.Gorsel3;
            }
            if (D.GorselThumbnail4 != null)
            {
                ImageGorsel4.Visible = true;
                ImageGorsel4.ImageUrl = D.GorselThumbnail4;
                TextBoxGorsel4.Text = D.Gorsel4;
            }
            CheckBoxDurum.Checked = D.Durum;
            TextBoxAnahtarKelimeler.Text = D.AnahtarKelimeler;
            HiddenFieldId.Value = D.Id.ToString();
            KategorileriVer(CheckBoxListKategoriler);

            for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
            {
                int KategoriId = Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value);
                var DTList =
                    Veriler.DuyurularTablosu.Where(p => p.DuyuruId == D.Id && p.KategoriId == KategoriId).ToList();
                foreach (var Item in DTList)
                {
                    CheckBoxListKategoriler.Items[i].Selected = true;
                }
            }
            LabelBaslik.Text = "Duyuru Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() +
                    " and p2.Durum==True order by BaslangicTarihi desc";
            }
            else
            {
                int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Duyurular as p join DuyurularTablosu as p1 on p.Id == p1.DuyuruId join DuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + " and p2.Durum==True and p2.Id== " +
                    KategoriId + " order by BaslangicTarihi desc";
            }
            GridViewVeriler.DataBind();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }
    }
}