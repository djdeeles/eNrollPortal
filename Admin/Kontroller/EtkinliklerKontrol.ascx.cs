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
    public partial class EtkinliklerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Etkinlikler Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 9))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    KategorileriVer(DropDownListKategorilerGridView, new ListItem("Tümü", "0"));
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
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
            KategorileriVer(DropDownListKategoriler, new ListItem("Seçiniz", "0"));
            TextBoxAd.Text = string.Empty;
            TextBoxOzet.Text = string.Empty;
            RadEditorDetay.Content = string.Empty;
            RadDateTimePickerBaslangicTarihi.SelectedDate = DateTime.Now;
            RadDateTimePickerBitisTarihi.SelectedDate = null;
            ImageResim1.Visible = false;
            TextBoxResim1.Text = string.Empty;
            ImageButtonResim1.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim1.ClientID + "','','width=640,height=480');";
            ImageResim2.Visible = false;
            TextBoxResim2.Text = string.Empty;
            ImageButtonResim2.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim2.ClientID + "','','width=640,height=480');";
            ImageResim3.Visible = false;
            TextBoxResim3.Text = string.Empty;
            ImageButtonResim3.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim3.ClientID + "','','width=640,height=480');";
            ImageResim4.Visible = false;
            TextBoxResim4.Text = string.Empty;
            ImageButtonResim4.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim4.ClientID + "','','width=640,height=480');";
            CheckBoxDurum.Checked = false;
            TextBoxAnahtarKelimeler.Text = string.Empty;
            HiddenFieldId.Value = string.Empty;
        }

        private void KategorileriVer(DropDownList DropDownListKategoriler, ListItem ListItem)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EK =
                Veriler.EtkinlikKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = EK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, ListItem);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Etkinlik Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Etkinlikler E;
                string AnaDizin = "../Gorseller";
                string Kategori = "Etkinlikler";
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
                    E = Veriler.Etkinlikler.Where(p => p.Id == Id).First();
                    E.EtkinlikKategoriId = Convert.ToInt32(DropDownListKategoriler.SelectedValue);
                    E.Ad = TextBoxAd.Text;
                    E.Ozet = TextBoxOzet.Text;
                    E.Icerik = RadEditorDetay.Content;
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
                    if (TextBoxResim1.Text != string.Empty)
                    {
                        if (TextBoxResim1.Text != E.Gorsel1)
                        {
                            GorselSil(E.Gorsel1);
                            GorselSil(E.GorselThumbnail1);
                            E.Gorsel1 = GorselBoyutlandir(TextBoxResim1, KayitYeri, 700);
                            E.GorselThumbnail1 = GorselBoyutlandir(TextBoxResim1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(E.Gorsel1);
                        GorselSil(E.GorselThumbnail1);
                        E.Gorsel1 = null;
                        E.GorselThumbnail1 = null;
                    }

                    if (TextBoxResim2.Text != string.Empty)
                    {
                        if (TextBoxResim2.Text != E.Gorsel2)
                        {
                            GorselSil(E.Gorsel2);
                            GorselSil(E.GorselThumbnail2);
                            E.Gorsel2 = GorselBoyutlandir(TextBoxResim2, KayitYeri, 700);
                            E.GorselThumbnail2 = GorselBoyutlandir(TextBoxResim2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(E.Gorsel2);
                        GorselSil(E.GorselThumbnail2);
                        E.Gorsel2 = null;
                        E.GorselThumbnail2 = null;
                    }

                    if (TextBoxResim3.Text != string.Empty)
                    {
                        if (TextBoxResim3.Text != E.Gorsel3)
                        {
                            GorselSil(E.Gorsel3);
                            GorselSil(E.GorselThumbnail3);
                            E.Gorsel3 = GorselBoyutlandir(TextBoxResim3, KayitYeri, 700);
                            E.GorselThumbnail3 = GorselBoyutlandir(TextBoxResim3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(E.Gorsel3);
                        GorselSil(E.GorselThumbnail3);
                        E.Gorsel3 = null;
                        E.GorselThumbnail3 = null;
                    }

                    if (TextBoxResim4.Text != string.Empty)
                    {
                        if (TextBoxResim4.Text != E.Gorsel4)
                        {
                            GorselSil(E.Gorsel4);
                            GorselSil(E.GorselThumbnail4);
                            E.Gorsel4 = GorselBoyutlandir(TextBoxResim4, KayitYeri, 700);
                            E.GorselThumbnail4 = GorselBoyutlandir(TextBoxResim4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(E.Gorsel4);
                        GorselSil(E.GorselThumbnail4);
                        E.Gorsel4 = null;
                        E.GorselThumbnail4 = null;
                    }
                    E.Durum = CheckBoxDurum.Checked;
                    E.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    E.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    E.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListKategorilerGridView.SelectedValue = E.EtkinlikKategoriId.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    E = new Etkinlikler();
                    E.EtkinlikKategoriId = Convert.ToInt32(DropDownListKategoriler.SelectedValue);
                    E.Ad = TextBoxAd.Text;
                    E.Ozet = TextBoxOzet.Text;
                    E.Icerik = RadEditorDetay.Content;
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
                    E.Durum = CheckBoxDurum.Checked;
                    if (TextBoxResim1.Text != string.Empty)
                    {
                        E.Gorsel1 = GorselBoyutlandir(TextBoxResim1, KayitYeri, 700);
                        E.GorselThumbnail1 = GorselBoyutlandir(TextBoxResim1, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        E.Gorsel1 = null;
                        E.GorselThumbnail1 = null;
                    }

                    if (TextBoxResim2.Text != string.Empty)
                    {
                        E.Gorsel2 = GorselBoyutlandir(TextBoxResim2, KayitYeri, 700);
                        E.GorselThumbnail2 = GorselBoyutlandir(TextBoxResim2, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        E.Gorsel2 = null;
                        E.GorselThumbnail2 = null;
                    }

                    if (TextBoxResim3.Text != string.Empty)
                    {
                        E.Gorsel3 = GorselBoyutlandir(TextBoxResim3, KayitYeri, 700);
                        E.GorselThumbnail3 = GorselBoyutlandir(TextBoxResim3, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        E.Gorsel3 = null;
                        E.GorselThumbnail3 = null;
                    }

                    if (TextBoxResim4.Text != string.Empty)
                    {
                        E.Gorsel4 = GorselBoyutlandir(TextBoxResim4, KayitYeri, 700);
                        E.GorselThumbnail4 = GorselBoyutlandir(TextBoxResim4, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        E.Gorsel4 = null;
                        E.GorselThumbnail4 = null;
                    }
                    E.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    E.OkunmaSayisi = 0;
                    E.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    E.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToEtkinlikler(E);
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
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
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                Etkinlikler E = Veriler.Etkinlikler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(E);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                Etkinlikler E = Veriler.Etkinlikler.Where(p => p.Id == Id).First();
                Veriler.Etkinlikler.DeleteObject(E);
                Veriler.SaveChanges();
                GorselSil(E.Gorsel1);
                GorselSil(E.GorselThumbnail1);
                GorselSil(E.Gorsel2);
                GorselSil(E.GorselThumbnail2);
                GorselSil(E.Gorsel3);
                GorselSil(E.GorselThumbnail3);
                GorselSil(E.Gorsel4);
                GorselSil(E.GorselThumbnail4);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
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
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.Id==" +
                        KategoriId.ToString() + " and p1.Durum==True order by BaslangicTarihi desc";
                }
            }
        }

        private void Guncelle(Etkinlikler E)
        {
            // Güncelle
            KategorileriVer(DropDownListKategoriler, new ListItem("Seçiniz", "0"));
            DropDownListKategoriler.SelectedValue = E.EtkinlikKategoriId.ToString();
            TextBoxAd.Text = E.Ad;
            TextBoxOzet.Text = E.Ozet;
            RadEditorDetay.Content = E.Icerik;
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
            if (E.GorselThumbnail1 != null)
            {
                ImageResim1.Visible = true;
                ImageResim1.ImageUrl = E.GorselThumbnail1;
                TextBoxResim1.Text = E.Gorsel1;
            }
            if (E.GorselThumbnail2 != null)
            {
                ImageResim2.Visible = true;
                ImageResim2.ImageUrl = E.GorselThumbnail2;
                TextBoxResim2.Text = E.Gorsel2;
            }
            if (E.GorselThumbnail3 != null)
            {
                ImageResim3.Visible = true;
                ImageResim3.ImageUrl = E.GorselThumbnail3;
                TextBoxResim3.Text = E.Gorsel3;
            }
            if (E.GorselThumbnail4 != null)
            {
                ImageResim4.Visible = true;
                ImageResim4.ImageUrl = E.GorselThumbnail4;
                TextBoxResim4.Text = E.Gorsel4;
            }
            CheckBoxDurum.Checked = E.Durum;
            TextBoxAnahtarKelimeler.Text = E.AnahtarKelimeler;
            HiddenFieldId.Value = E.Id.ToString();
            LabelBaslik.Text = "Etkinlik Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
            }
            else
            {
                int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Etkinlikler as p join EtkinlikKategorileri as p1 on p.EtkinlikKategoriId==p1.Id where p1.[Durum]==true and p1.Id==" +
                    KategoriId.ToString() + " and p1.Durum==True order by BaslangicTarihi desc";
            }
            GridViewVeriler.DataBind();
        }
    }
}