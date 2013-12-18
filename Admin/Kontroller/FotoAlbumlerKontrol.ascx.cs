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
    public partial class FotoAlbumlerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Foto Albüm Yönetimi";
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
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
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
            FotoAlbumKategorileriVer(DropDownListFotoAlbumKategorileri, new ListItem("Seçiniz", "0"));
            TumFotoAlbumKategorileriniVer(DropDownListFotoAlbumKategorileriGridView, new ListItem("Tümü", "0"));
            TextBoxFotoAlbumAdi.Text = string.Empty;
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

        private void FotoAlbumKategorileriVer(DropDownList dropDownList, ListItem item)
        {
            // Kategorileri ver
            int dilId = EnrollContext.Current.WorkingLanguage.languageId;
            var fak = Veriler.FotoAlbumKategorileri.Where(p => p.DilId == dilId && p.Durum).ToList();
            dropDownList.DataTextField = "FotoAlbumKategoriAdi";
            dropDownList.DataValueField = "Id";
            dropDownList.DataSource = fak;
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, item);
        }

        private void TumFotoAlbumKategorileriniVer(DropDownList dropDownList, ListItem item)
        {
            // Tüm kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAK = Veriler.FotoAlbumKategorileri.Where(p => p.DilId == DilId && p.Durum).ToList();
            dropDownList.DataTextField = "FotoAlbumKategoriAdi";
            dropDownList.DataValueField = "Id";
            dropDownList.DataSource = FAK;
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, item);
        }

        protected void ImageButtonYeniEkleClick(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Foto Album Ekle";
        }

        protected void ImageButtonKaydetClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                const string anaDizin = "../Gorseller";
                const string kategori = "FotoAlbumler";
                const string thumbnails = "Thumbnails";
                var yil = DateTime.Now.Year.ToString();

                FotoAlbumler fa;
                string ay;
                var a = DateTime.Now.Month;
                if (a < 9)
                {
                    ay = "0" + a.ToString();
                }
                else
                {
                    ay = a.ToString();
                }

                if (!Directory.Exists(Server.MapPath(anaDizin)))
                {
                    Directory.CreateDirectory(Server.MapPath(anaDizin));
                }
                if (!Directory.Exists(Server.MapPath(anaDizin + "/" + kategori)))
                {
                    Directory.CreateDirectory(Server.MapPath(anaDizin + "/" + kategori));
                }
                if (!Directory.Exists(Server.MapPath(anaDizin + "/" + kategori + "/" + yil)))
                {
                    Directory.CreateDirectory(Server.MapPath(anaDizin + "/" + kategori + "/" + yil));
                }
                if (!Directory.Exists(Server.MapPath(anaDizin + "/" + kategori + "/" + yil + "/" + ay)))
                {
                    Directory.CreateDirectory(Server.MapPath(anaDizin + "/" + kategori + "/" + yil + "/" + ay));
                }
                if (
                    !Directory.Exists(Server.MapPath(anaDizin + "/" + kategori + "/" + yil + "/" + ay + "/" + thumbnails)))
                {
                    Directory.CreateDirectory(
                        Server.MapPath(anaDizin + "/" + kategori + "/" + yil + "/" + ay + "/" + thumbnails));
                }
                var kayitYeri = anaDizin + "/" + kategori + "/" + yil + "/" + ay + "/";
                var kayitYeriThumbnail = anaDizin + "/" + kategori + "/" + yil + "/" + ay + "/" + thumbnails + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int id = Convert.ToInt32(HiddenFieldId.Value);
                    fa = Veriler.FotoAlbumler.First(p => p.Id == id);
                    fa.FotoAlbumKategoriId = Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue);
                    fa.FotoAlbumAdi = TextBoxFotoAlbumAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != fa.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(fa.Gorsel))
                            {
                                GorselSil(fa.Gorsel);
                            }
                            if (!string.IsNullOrEmpty(fa.GorselThumbnail))
                            {
                                GorselSil(fa.GorselThumbnail);
                            }
                            fa.Gorsel = GorselBoyutlandir(TextBoxGorsel, kayitYeri, 700);
                            fa.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, kayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(fa.Gorsel))
                        {
                            GorselSil(fa.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(fa.GorselThumbnail))
                        {
                            GorselSil(fa.GorselThumbnail);
                        }
                        fa.Gorsel = null;
                        fa.GorselThumbnail = null;
                    }
                    fa.Aciklama = TextBoxAciklama.Text;
                    fa.Durum = CheckBoxDurum.Checked;
                    fa.PersonelAlbumuMu = false;
                    fa.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    fa.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListFotoAlbumKategorileriGridView.SelectedValue = fa.FotoAlbumKategoriId.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        fa.FotoAlbumKategoriId + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    fa = new FotoAlbumler();
                    fa.FotoAlbumKategoriId = Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue);
                    fa.FotoAlbumAdi = TextBoxFotoAlbumAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        fa.Gorsel = GorselBoyutlandir(TextBoxGorsel, kayitYeri, 700);
                        fa.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, kayitYeriThumbnail, 175);
                    }
                    else
                    {
                        fa.Gorsel = null;
                        fa.GorselThumbnail = null;
                    }
                    fa.Aciklama = TextBoxAciklama.Text;
                    fa.Durum = CheckBoxDurum.Checked;
                    fa.PersonelAlbumuMu = false;
                    fa.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    fa.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToFotoAlbumler(fa);
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListFotoAlbumKategorileriGridView.SelectedValue = fa.FotoAlbumKategoriId.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        fa.FotoAlbumKategoriId + "";
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

        public string GorselBoyutlandir(TextBox resimYolu, string kayitYeri, int genislik)
        {
            // Görsel boyutlandır
            var orjinalResim = new Bitmap(Server.MapPath(resimYolu.Text.Replace("~", "..")));
            var format = orjinalResim.RawFormat;
            ImageCodecInfo ici;
            if (format.Equals(ImageFormat.Gif) || format.Equals(ImageFormat.Png))
            {
                ici = GetEncoder(ImageFormat.Png);
            }
            else
            {
                ici = GetEncoder(ImageFormat.Jpeg);
            }
            var e = Encoder.Quality;
            var ep = new EncoderParameters(1);
            var eParam = new EncoderParameter(e, 70L);
            ep.Param[0] = eParam;
            Bitmap yeniResim = null;
            Graphics graphic = null;
            if (orjinalResim.Width >= orjinalResim.Height)
            {
                if (orjinalResim.Width > genislik)
                {
                    var orw = orjinalResim.Width;
                    var orh = orjinalResim.Height;
                    var oro = orw/orh;
                    var nw = genislik;
                    var nht = nw/oro;
                    var nh = Convert.ToInt16(nht);
                    yeniResim = new Bitmap(orjinalResim, nw, nh);
                    graphic = Graphics.FromImage(yeniResim);
                    graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(orjinalResim, 0, 0, nw, nh);
                }
                else
                {
                    yeniResim = new Bitmap(orjinalResim, orjinalResim.Width, orjinalResim.Height);
                    graphic = Graphics.FromImage(yeniResim);
                    graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(orjinalResim, 0, 0, orjinalResim.Width, orjinalResim.Height);
                }
            }
            else
            {
                if (orjinalResim.Height > genislik)
                {
                    var oro = orjinalResim.Height/orjinalResim.Width;
                    var nw = Convert.ToInt16(genislik/oro);
                    yeniResim = new Bitmap(orjinalResim, nw, genislik);
                    graphic = Graphics.FromImage(yeniResim);
                    graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(orjinalResim, 0, 0, nw, genislik);
                }
                else
                {
                    yeniResim = new Bitmap(orjinalResim, orjinalResim.Width, orjinalResim.Height);

                    graphic = Graphics.FromImage(yeniResim);
                    graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(orjinalResim, 0, 0, orjinalResim.Width, orjinalResim.Height);
                }
            }
            var resimAdi = Guid.NewGuid() + ".jpg";
            yeniResim.Save(Server.MapPath(kayitYeri + resimAdi), ici, ep);
            return kayitYeri.Replace("../", "~/") + resimAdi;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            // Codec'i ayarla
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        private void GorselSil(string resim)
        {
            // Gösel sil
            if (File.Exists(Server.MapPath(resim)))
            {
                File.Delete(Server.MapPath(resim));
            }
        }

        protected void ImageButtonIptalClick(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewVerilerRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                var id = Convert.ToInt32(e.CommandArgument);
                var dilId = EnrollContext.Current.WorkingLanguage.languageId;
                var fa = Veriler.FotoAlbumler.First(p => p.Id == id);
                MultiView1.ActiveViewIndex = 1;
                Guncelle(fa);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                var id = Convert.ToInt32(e.CommandArgument);
                var dilId = EnrollContext.Current.WorkingLanguage.languageId;
                var fa = Veriler.FotoAlbumler.First(p => p.Id == id);
                var fagList = Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == fa.Id).ToList();
                foreach (var fag in fagList)
                {
                    var fagSil = Veriler.FotoAlbumGorselleri.Where(p => p.Id == fag.Id).First();
                    if (!string.IsNullOrEmpty(fagSil.Gorsel))
                    {
                        GorselSil(fagSil.Gorsel);
                    }
                    if (!string.IsNullOrEmpty(fagSil.GorselThumbnail))
                    {
                        GorselSil(fagSil.GorselThumbnail);
                    }
                    Veriler.FotoAlbumGorselleri.DeleteObject(fagSil);
                    Veriler.SaveChanges();
                }
                if (!string.IsNullOrEmpty(fa.Gorsel))
                {
                    GorselSil(fa.Gorsel);
                }
                if (!string.IsNullOrEmpty(fa.GorselThumbnail))
                {
                    GorselSil(fa.GorselThumbnail);
                }
                Veriler.FotoAlbumler.DeleteObject(fa);
                Veriler.SaveChanges();
                if (DropDownListFotoAlbumKategorileriGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    var fotoAlbumKategoriId =
                        Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        fotoAlbumKategoriId + "";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                // Sırala
                if (DropDownListFotoAlbumKategorileriGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    var fotoAlbumKategoriId =
                        Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        fotoAlbumKategoriId + "";
                }
            }
        }

        private void Guncelle(FotoAlbumler fa)
        {
            // Güncelle
            FotoAlbumKategorileriVer(DropDownListFotoAlbumKategorileri, new ListItem("Seçiniz", "0"));
            DropDownListFotoAlbumKategorileri.SelectedValue = fa.FotoAlbumKategoriId.ToString();
            TextBoxFotoAlbumAdi.Text = fa.FotoAlbumAdi;
            TextBoxGorsel.Text = fa.Gorsel;
            TextBoxAciklama.Text = fa.Aciklama;
            CheckBoxDurum.Checked = fa.Durum;
            HiddenFieldId.Value = fa.Id.ToString();
            LabelBaslik.Text = "Foto Albüm Düzenle";
        }

        protected void DropDownListKategorilerGridViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListFotoAlbumKategorileriGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
            }
            else
            {
                var fotoAlbumKategoriId =
                    Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/MainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                    fotoAlbumKategoriId + "";
            }
            GridViewVeriler.DataBind();
        }
    }
}