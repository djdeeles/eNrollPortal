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
    public partial class IlanlarKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "İlanlar Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 8))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    KategorileriVer(DropDownListKategorilerGridView);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
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
            // Kategoriler ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IK = Veriler.IlanKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = IK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void KategorileriVer(CheckBoxList CheckBoxListKategoriler)
        {
            // Kategoriler ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IK = Veriler.IlanKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList();
            CheckBoxListKategoriler.DataTextField = "KategoriAdi";
            CheckBoxListKategoriler.DataValueField = "Id";
            CheckBoxListKategoriler.DataSource = IK;
            CheckBoxListKategoriler.DataBind();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "İlan Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Ilanlar I;
                string AnaDizin = "../Gorseller";
                string Kategori = "Ilanlar";
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
                    I = Veriler.Ilanlar.Where(p => p.Id == Id).First();
                    I.Baslik = TextBoxBaslik.Text;
                    I.Ozet = TextBoxOzet.Text;
                    I.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        I.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        I.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BitisTarihi = null;
                    }
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != I.Gorsel1)
                        {
                            GorselSil(I.Gorsel1);
                            GorselSil(I.GorselThumbnail1);
                            I.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            I.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel1);
                        GorselSil(I.GorselThumbnail1);
                        I.Gorsel1 = null;
                        I.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != I.Gorsel2)
                        {
                            GorselSil(I.Gorsel2);
                            GorselSil(I.GorselThumbnail2);
                            I.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            I.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel2);
                        GorselSil(I.GorselThumbnail2);
                        I.Gorsel2 = null;
                        I.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != I.Gorsel3)
                        {
                            GorselSil(I.Gorsel3);
                            GorselSil(I.GorselThumbnail3);
                            I.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            I.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel3);
                        GorselSil(I.GorselThumbnail3);
                        I.Gorsel3 = null;
                        I.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != I.Gorsel4)
                        {
                            GorselSil(I.Gorsel4);
                            GorselSil(I.GorselThumbnail4);
                            I.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            I.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel4);
                        GorselSil(I.GorselThumbnail4);
                        I.Gorsel4 = null;
                        I.GorselThumbnail4 = null;
                    }
                    I.Durum = CheckBoxDurum.Checked;
                    I.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    I.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.DuzenlemeTarihi = DateTime.Now;
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
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True order by BaslangicTarihi desc";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    I = new Ilanlar();
                    I.Baslik = TextBoxBaslik.Text;
                    I.Ozet = TextBoxOzet.Text;
                    I.Icerik = RadEditorIcerik.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        I.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        I.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BitisTarihi = null;
                    }
                    if (TextBoxGorsel1.Text != string.Empty)
                    {
                        if (TextBoxGorsel1.Text != I.Gorsel1)
                        {
                            I.Gorsel1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeri, 700);
                            I.GorselThumbnail1 = GorselBoyutlandir(TextBoxGorsel1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        I.Gorsel1 = null;
                        I.GorselThumbnail1 = null;
                    }

                    if (TextBoxGorsel2.Text != string.Empty)
                    {
                        if (TextBoxGorsel2.Text != I.Gorsel2)
                        {
                            I.Gorsel2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeri, 700);
                            I.GorselThumbnail2 = GorselBoyutlandir(TextBoxGorsel2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        I.Gorsel2 = null;
                        I.GorselThumbnail2 = null;
                    }

                    if (TextBoxGorsel3.Text != string.Empty)
                    {
                        if (TextBoxGorsel3.Text != I.Gorsel3)
                        {
                            I.Gorsel3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeri, 700);
                            I.GorselThumbnail3 = GorselBoyutlandir(TextBoxGorsel3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        I.Gorsel3 = null;
                        I.GorselThumbnail3 = null;
                    }

                    if (TextBoxGorsel4.Text != string.Empty)
                    {
                        if (TextBoxGorsel4.Text != I.Gorsel4)
                        {
                            I.Gorsel4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeri, 700);
                            I.GorselThumbnail4 = GorselBoyutlandir(TextBoxGorsel4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        I.Gorsel4 = null;
                        I.GorselThumbnail4 = null;
                    }
                    I.Durum = CheckBoxDurum.Checked;
                    I.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    I.OkunmaSayisi = 0;
                    I.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToIlanlar(I);
                    Veriler.SaveChanges();
                    Ilanlar II =
                        Veriler.Ilanlar.Where(
                            p =>
                            p.Baslik == TextBoxBaslik.Text && p.Ozet == TextBoxOzet.Text &&
                            p.Icerik == RadEditorIcerik.Content).First();
                    for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
                    {
                        if (CheckBoxListKategoriler.Items[i].Selected)
                        {
                            KategoriKaydet(Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value), II.Id);
                        }
                    }
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
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

        private void KategorileriSil(int IlanId)
        {
            // Kategorileri sil
            var ITList = Veriler.IlanlarTablosu.Where(p => p.IlanId == IlanId).ToList();
            foreach (var Item in ITList)
            {
                IlanlarTablosu IT = Veriler.IlanlarTablosu.Where(p => p.IlanId == Item.IlanId).First();
                Veriler.IlanlarTablosu.DeleteObject(IT);
                Veriler.SaveChanges();
            }
        }

        private void KategoriKaydet(int KategoriId, int IlanId)
        {
            // Kategori kaydet
            IlanlarTablosu IT = new IlanlarTablosu();
            IT.IlanId = IlanId;
            IT.KategoriId = KategoriId;
            Veriler.IlanlarTablosu.AddObject(IT);
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
                Ilanlar I = Veriler.Ilanlar.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(I);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                Ilanlar I = Veriler.Ilanlar.Where(p => p.Id == Id).First();
                Veriler.Ilanlar.DeleteObject(I);
                Veriler.SaveChanges();
                GorselSil(I.Gorsel1);
                GorselSil(I.GorselThumbnail1);
                GorselSil(I.Gorsel2);
                GorselSil(I.GorselThumbnail2);
                GorselSil(I.Gorsel3);
                GorselSil(I.GorselThumbnail3);
                GorselSil(I.Gorsel4);
                GorselSil(I.GorselThumbnail4);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " and p2.Durum==True order by BaslangicTarihi desc";
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
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True order by BaslangicTarihi desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and p2.Durum==True and p2.Id== "
                        + KategoriId
                        + " order by BaslangicTarihi desc";
                }
            }
        }

        private void Guncelle(Ilanlar I)
        {
            // Güncelle
            TextBoxBaslik.Text = I.Baslik;
            TextBoxOzet.Text = I.Ozet;
            RadEditorIcerik.Content = I.Icerik;
            if (I.BaslangicTarihi != null)
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = I.BaslangicTarihi.Value;
            }
            else
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = null;
            }
            if (I.BitisTarihi != null)
            {
                RadDateTimePickerBitisTarihi.SelectedDate = I.BitisTarihi.Value;
            }
            else
            {
                RadDateTimePickerBitisTarihi.SelectedDate = null;
            }
            if (I.GorselThumbnail1 != null)
            {
                ImageGorsel1.Visible = true;
                ImageGorsel1.ImageUrl = I.GorselThumbnail1;
                TextBoxGorsel1.Text = I.Gorsel1;
            }
            if (I.GorselThumbnail2 != null)
            {
                ImageGorsel2.Visible = true;
                ImageGorsel2.ImageUrl = I.GorselThumbnail2;
                TextBoxGorsel2.Text = I.Gorsel2;
            }
            if (I.GorselThumbnail3 != null)
            {
                ImageGorsel3.Visible = true;
                ImageGorsel3.ImageUrl = I.GorselThumbnail3;
                TextBoxGorsel3.Text = I.Gorsel3;
            }
            if (I.GorselThumbnail4 != null)
            {
                ImageGorsel4.Visible = true;
                ImageGorsel4.ImageUrl = I.GorselThumbnail4;
                TextBoxGorsel4.Text = I.Gorsel4;
            }
            CheckBoxDurum.Checked = I.Durum;
            TextBoxAnahtarKelimeler.Text = I.AnahtarKelimeler;
            HiddenFieldId.Value = I.Id.ToString();
            KategorileriVer(CheckBoxListKategoriler);

            for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
            {
                int KategoriId = Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value);
                var ITList = Veriler.IlanlarTablosu.Where(p => p.IlanId == I.Id && p.KategoriId == KategoriId).ToList();
                foreach (var Item in ITList)
                {
                    CheckBoxListKategoriler.Items[i].Selected = true;
                }
            }
            LabelBaslik.Text = "İlan Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " and p2.Durum==True order by BaslangicTarihi desc";
            }
            else
            {
                int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select distinct p.[Id], p.[Baslik], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ilanlar as p join IlanlarTablosu as p1 on p.Id == p1.IlanId join IlanKategorileri as p2 on p1.KategoriId==p2.Id where p2.DilId=="
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