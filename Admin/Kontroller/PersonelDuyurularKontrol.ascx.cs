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
    public partial class PersonelDuyurularKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Personel Duyurular Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 7))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    KategorileriVer(DropDownListKategorilerGridView);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True order by BaslangicTarihi desc";
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
            var PDK =
                Veriler.PersonelDuyuruKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList
                    ();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = PDK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void KategorileriVer(CheckBoxList CheckBoxListKategoriler)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var PDK =
                Veriler.PersonelDuyuruKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList
                    ();
            CheckBoxListKategoriler.DataTextField = "KategoriAdi";
            CheckBoxListKategoriler.DataValueField = "Id";
            CheckBoxListKategoriler.DataSource = PDK;
            CheckBoxListKategoriler.DataBind();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Personel Duyuru Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                PersonelDuyurular PD;
                string AnaDizin = "../Gorseller";
                string Kategori = "PersonelDuyurular";
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
                    PD = Veriler.PersonelDuyurular.Where(p => p.Id == Id).First();
                    PD.Baslik = TextBoxBaslik.Text;
                    PD.Ozet = TextBoxOzet.Text;
                    PD.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        PD.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        PD.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        PD.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        PD.BitisTarihi = null;
                    }
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != PD.Gorsel1)
                        {
                            GorselSil(PD.Gorsel1);
                            GorselSil(PD.GorselThumbnail1);
                            PD.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            PD.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(PD.Gorsel1);
                        GorselSil(PD.GorselThumbnail1);
                        PD.Gorsel1 = null;
                        PD.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != PD.Gorsel2)
                        {
                            GorselSil(PD.Gorsel2);
                            GorselSil(PD.GorselThumbnail2);
                            PD.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            PD.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(PD.Gorsel2);
                        GorselSil(PD.GorselThumbnail2);
                        PD.Gorsel2 = null;
                        PD.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != PD.Gorsel3)
                        {
                            GorselSil(PD.Gorsel3);
                            GorselSil(PD.GorselThumbnail3);
                            PD.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            PD.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(PD.Gorsel3);
                        GorselSil(PD.GorselThumbnail3);
                        PD.Gorsel3 = null;
                        PD.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != PD.Gorsel4)
                        {
                            GorselSil(PD.Gorsel4);
                            GorselSil(PD.GorselThumbnail4);
                            PD.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            PD.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(PD.Gorsel4);
                        GorselSil(PD.GorselThumbnail4);
                        PD.Gorsel4 = null;
                        PD.GorselThumbnail4 = null;
                    }
                    PD.Durum = CheckBoxDurum.Checked;
                    PD.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    PD.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    PD.DuzenlemeTarihi = DateTime.Now;
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
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True order by BaslangicTarihi desc";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    PD = new PersonelDuyurular();
                    PD.Baslik = TextBoxBaslik.Text;
                    PD.Ozet = TextBoxOzet.Text;
                    PD.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        PD.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        PD.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        PD.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        PD.BitisTarihi = null;
                    }
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != PD.Gorsel1)
                        {
                            PD.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            PD.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        PD.Gorsel1 = null;
                        PD.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != PD.Gorsel2)
                        {
                            PD.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            PD.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        PD.Gorsel2 = null;
                        PD.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != PD.Gorsel3)
                        {
                            PD.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            PD.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        PD.Gorsel3 = null;
                        PD.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != PD.Gorsel4)
                        {
                            PD.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            PD.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        PD.Gorsel4 = null;
                        PD.GorselThumbnail4 = null;
                    }
                    PD.Durum = CheckBoxDurum.Checked;
                    PD.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    PD.OkunmaSayisi = 0;
                    PD.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    PD.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToPersonelDuyurular(PD);
                    Veriler.SaveChanges();
                    PersonelDuyurular DD =
                        Veriler.PersonelDuyurular.Where(
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
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True order by BaslangicTarihi desc";
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
            // Kategori sil
            var PDTList = Veriler.PersonelDuyurularTablosu.Where(p => p.DuyuruId == DuyuruId).ToList();
            foreach (var Item in PDTList)
            {
                PersonelDuyurularTablosu PDT =
                    Veriler.PersonelDuyurularTablosu.Where(p => p.DuyuruId == Item.DuyuruId).First();
                Veriler.PersonelDuyurularTablosu.DeleteObject(PDT);
                Veriler.SaveChanges();
            }
        }

        private void KategoriKaydet(int KategoriId, int DuyuruId)
        {
            // Kategori kaydet
            PersonelDuyurularTablosu PDT = new PersonelDuyurularTablosu();
            PDT.DuyuruId = DuyuruId;
            PDT.KategoriId = KategoriId;
            Veriler.PersonelDuyurularTablosu.AddObject(PDT);
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
                PersonelDuyurular PD = Veriler.PersonelDuyurular.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(PD);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                PersonelDuyurular PD = Veriler.PersonelDuyurular.Where(p => p.Id == Id).First();
                Veriler.PersonelDuyurular.DeleteObject(PD);
                Veriler.SaveChanges();
                GorselSil(PD.Gorsel1);
                GorselSil(PD.GorselThumbnail1);
                GorselSil(PD.Gorsel2);
                GorselSil(PD.GorselThumbnail2);
                GorselSil(PD.Gorsel3);
                GorselSil(PD.GorselThumbnail3);
                GorselSil(PD.Gorsel4);
                GorselSil(PD.GorselThumbnail4);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " and p2.Durum==True order by BaslangicTarihi desc";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True order by BaslangicTarihi desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True and p2.Id== "
                        + KategoriId
                        + " order by BaslangicTarihi desc";
                }
            }
        }

        private void Guncelle(PersonelDuyurular PD)
        {
            // Güncelle
            TextBoxBaslik.Text = PD.Baslik;
            TextBoxOzet.Text = PD.Ozet;
            RadEditorIcerik.Content = PD.Icerik;
            if (PD.BaslangicTarihi != null)
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = PD.BaslangicTarihi.Value;
            }
            else
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = null;
            }
            if (PD.BitisTarihi != null)
            {
                RadDateTimePickerBitisTarihi.SelectedDate = PD.BitisTarihi.Value;
            }
            else
            {
                RadDateTimePickerBitisTarihi.SelectedDate = null;
            }
            if (PD.GorselThumbnail1 != null)
            {
                ImageGorsel1.Visible = true;
                ImageGorsel1.ImageUrl = PD.GorselThumbnail1;
                TextBoxGorsel1.Text = PD.Gorsel1;
            }
            if (PD.GorselThumbnail2 != null)
            {
                ImageGorsel2.Visible = true;
                ImageGorsel2.ImageUrl = PD.GorselThumbnail2;
                TextBoxGorsel2.Text = PD.Gorsel2;
            }
            if (PD.GorselThumbnail3 != null)
            {
                ImageGorsel3.Visible = true;
                ImageGorsel3.ImageUrl = PD.GorselThumbnail3;
                TextBoxGorsel3.Text = PD.Gorsel3;
            }
            if (PD.GorselThumbnail4 != null)
            {
                ImageGorsel4.Visible = true;
                ImageGorsel4.ImageUrl = PD.GorselThumbnail4;
                TextBoxGorsel4.Text = PD.Gorsel4;
            }
            CheckBoxDurum.Checked = PD.Durum;
            TextBoxAnahtarKelimeler.Text = PD.AnahtarKelimeler;
            HiddenFieldId.Value = PD.Id.ToString();
            KategorileriVer(CheckBoxListKategoriler);
            for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
            {
                int KategoriId = Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value);
                var PDTList =
                    Veriler.PersonelDuyurularTablosu.Where(p => p.DuyuruId == PD.Id && p.KategoriId == KategoriId).
                        ToList();
                foreach (var Item in PDTList)
                {
                    CheckBoxListKategoriler.Items[i].Selected = true;
                }
            }
            LabelBaslik.Text = "Personel Duyuru Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " and p2.Durum==True order by BaslangicTarihi desc";
            }
            else
            {
                int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from PersonelDuyurular as p join PersonelDuyurularTablosu as p1 on p.Id == p1.DuyuruId join PersonelDuyuruKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " and p2.Durum==True and p2.Id== "
                    + KategoriId
                    + " order by BaslangicTarihi desc";
            }
            GridViewVeriler.DataBind();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }
    }
}