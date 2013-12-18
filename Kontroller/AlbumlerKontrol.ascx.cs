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

namespace EnrollPortal.Kontroller
{
    public partial class AlbumlerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Code"] != null)
                    {
                        Temizle();
                        MultiView1.ActiveViewIndex = 0;
                        MultiView2.ActiveViewIndex = 0;
                        AlbümleriVer();
                        LiteralSiteMap.Text = "<a href=\"../Portal/AlbumListeleri.aspx\">Tüm Albümlerim</a> / ";
                        LiteralSiteMap.Text +=
                            "<a href=\"../Portal/AlbumListeleri.aspx?Code=AlbumEkle\">Yeni Albüm Ekle</a>";
                    }
                    else
                    {
                        MultiView1.ActiveViewIndex = -1;
                        LiteralSiteMap.Text = "<a href=\"../Portal/AlbumListeleri.aspx\">Tüm Albümlerim</a>";
                        AlbümleriVer();
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = -1;
                    LiteralSiteMap.Text = "<a href=\"../Portal/AlbumListeleri.aspx\">Tüm Albümlerim</a>";
                    AlbümleriVer();
                }
                MenuAlbumKategoriler.Items.Clear();
                PersonelAlbumKategorileriVer(MenuAlbumKategoriler.Items);
                MenuBelediyeAlbumKategoriler.Items.Clear();
                BeldiyeAlbumKategorileriVer(MenuBelediyeAlbumKategoriler.Items);
            }
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void Temizle()
        {
            FotoAlbumKategorileriVer(DropDownListFotoAlbumKategorileri, new ListItem("Seçiniz", "0"));
            TextBoxFotoAlbumAdi.Text = string.Empty;
            ImageGorsel.Visible = false;
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
            var FAK =
                Veriler.FotoAlbumKategorileri.Where(p => p.DilId == DilId && p.Durum && p.PersonelKategorisiMi).ToList();
            DropDownList.DataTextField = "FotoAlbumKategoriAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = FAK;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        private void PersonelAlbumKategorileriVer(MenuItemCollection Items)
        {
            // Personel kategorilerini ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAKList =
                Veriler.FotoAlbumKategorileri.Where(p => p.Durum && p.DilId == DilId && p.PersonelKategorisiMi).ToList();

            MenuItem M1 = new MenuItem();
            M1.NavigateUrl = "../Portal/AlbumListeleri.aspx?Code=AlbumEkle";
            M1.Text = "Yeni Albüm Ekle";
            Items.Add(M1);

            MenuItem M2 = new MenuItem();
            M2.NavigateUrl = "../Portal/AlbumListeleri.aspx";
            M2.Text = "Tüm Albümlerim";
            Items.Add(M2);

            MenuItem M3 = new MenuItem();
            M3.NavigateUrl = "../TumFotoAlbumler.aspx?Code=0";
            M3.Text = "Personel'den Tüm Albümler";
            Items.Add(M3);

            foreach (FotoAlbumKategorileri FAK in FAKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumFotoAlbumler.aspx?Code=" + FAK.Id.ToString();
                MI.Text = FAK.FotoAlbumKategoriAdi;
                Items.Add(MI);
            }
        }

        private void BeldiyeAlbumKategorileriVer(MenuItemCollection Items)
        {
            // Belediye kategorilerini ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAKList =
                Veriler.FotoAlbumKategorileri.Where(p => p.Durum && p.DilId == DilId && p.PersonelKategorisiMi == false)
                    .ToList();
            MenuItem MII = new MenuItem();
            MII.NavigateUrl = "../TumFotoAlbumler.aspx";
            MII.Text = "Kurum'dan Tüm Albümler";
            Items.Add(MII);
            foreach (FotoAlbumKategorileri FAK in FAKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumFotoAlbumler.aspx?Code=" + FAK.Id.ToString();
                MI.Text = FAK.FotoAlbumKategoriAdi;
                Items.Add(MI);
            }
        }

        private void AlbümleriVer()
        {
            // Albümleri ver
            MultiView2.ActiveViewIndex = 0;
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            var FAList = (from FA in Veriler.FotoAlbumler
                          join FAK in Veriler.FotoAlbumKategorileri
                              on FA.FotoAlbumKategoriId equals FAK.Id
                          where FAK.DilId == DilId
                                && FAK.PersonelKategorisiMi
                                && FA.PersonelAlbumuMu
                                && FA.KaydedenKullaniciId == KullaniciId
                          select new
                                     {
                                         FA.Id,
                                         AlbumAdi = FA.FotoAlbumAdi,
                                         FA.Aciklama,
                                         Resim = FA.GorselThumbnail != null
                                                     ? FA.GorselThumbnail
                                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                     }).ToList();
            ListViewVeriler.DataSource = FAList;
            ListViewVeriler.DataBind();
        }

        protected void LinkButtonKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                FotoAlbumler FA;

                #region

                string AnaDizin = "../Gorseller";
                string Kategori = "FotoAlbumler";
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

                #endregion

                string KayitYeri = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                string KayitYeriThumbnail = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle

                    # region Güncelle

                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
                    FA.FotoAlbumKategoriId = Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue);
                    FA.FotoAlbumAdi = TextBoxFotoAlbumAdi.Text;
                    if (FileUploadResim.HasFile)
                    {
                        GorselSil(FA.Gorsel);
                        GorselSil(FA.GorselThumbnail);
                        FA.Gorsel = GorselBoyutlandir(FileUploadResim, KayitYeri, 700);
                        FA.GorselThumbnail = GorselBoyutlandir(FileUploadResim, KayitYeriThumbnail, 175);
                    }
                    FA.Aciklama = TextBoxAciklama.Text;
                    FA.Durum = CheckBoxDurum.Checked;
                    FA.PersonelAlbumuMu = true;
                    FA.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FA.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MultiView1.ActiveViewIndex = -1;
                    AlbümleriVer();
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");

                    # endregion
                }
                else
                {
                    // Yeni kayıt

                    # region Ekle

                    FA = new FotoAlbumler();
                    FA.FotoAlbumKategoriId =
                        Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue);
                    FA.FotoAlbumAdi = TextBoxFotoAlbumAdi.Text;
                    if (FileUploadResim.HasFile)
                    {
                        FA.Gorsel = GorselBoyutlandir(FileUploadResim, KayitYeri, 700);
                        FA.GorselThumbnail = GorselBoyutlandir(FileUploadResim, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        FA.Gorsel = null;
                        FA.GorselThumbnail = null;
                    }
                    FA.Aciklama = TextBoxAciklama.Text;
                    FA.Durum = CheckBoxDurum.Checked;
                    FA.PersonelAlbumuMu = true;
                    FA.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FA.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToFotoAlbumler(FA);
                    Veriler.SaveChanges();
                    Temizle();
                    MultiView1.ActiveViewIndex = -1;
                    AlbümleriVer();
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");

                    # endregion
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        public string GorselBoyutlandir(FileUpload Resim, string KayitYeri, int Genislik)
        {
            // Görsel boyutlandır
            Bitmap OrjinalResim = new Bitmap(Resim.PostedFile.InputStream);
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

        protected void LinkButtonIptal_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            Temizle();
        }

        protected void ListViewVeriler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "AlbumGorselleri")
            {
                // Albüm görsellerini ver
                HiddenFieldKategoriId.Value = e.CommandArgument.ToString();
                FotoAlbumGorselleriVer(Convert.ToInt32(e.CommandArgument.ToString()));
                MultiView1.ActiveViewIndex = -1;
            }
            else if (e.CommandName == "AlbumDuzenle")
            {
                // Düzenle
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                FotoAlbumler FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 0;
                Guncelle(FA);
            }
            else if (e.CommandName == "AlbumSil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                FotoAlbumler FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
                var FAGList = Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == FA.Id).ToList();
                foreach (FotoAlbumGorselleri FAG in FAGList)
                {
                    FotoAlbumGorselleri FAGSil = Veriler.FotoAlbumGorselleri.Where(p => p.Id == FAG.Id).First();
                    if (!string.IsNullOrEmpty(FAGSil.Gorsel))
                    {
                        GorselSil(FAGSil.Gorsel);
                    }
                    if (!string.IsNullOrEmpty(FAGSil.GorselThumbnail))
                    {
                        GorselSil(FAGSil.GorselThumbnail);
                    }
                    Veriler.FotoAlbumGorselleri.DeleteObject(FAGSil);
                    Veriler.SaveChanges();
                }
                if (!string.IsNullOrEmpty(FA.Gorsel))
                {
                    GorselSil(FA.Gorsel);
                }
                if (!string.IsNullOrEmpty(FA.GorselThumbnail))
                {
                    GorselSil(FA.GorselThumbnail);
                }
                Veriler.FotoAlbumler.DeleteObject(FA);
                Veriler.SaveChanges();
                AlbümleriVer();
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(FotoAlbumler FA)
        {
            // Güncelle
            FotoAlbumKategorileriVer(DropDownListFotoAlbumKategorileri, new ListItem("Seçiniz", "0"));
            DropDownListFotoAlbumKategorileri.SelectedValue = FA.FotoAlbumKategoriId.ToString();
            TextBoxFotoAlbumAdi.Text = FA.FotoAlbumAdi;
            TextBoxAciklama.Text = FA.Aciklama;
            CheckBoxDurum.Checked = FA.Durum;
            if (!string.IsNullOrEmpty(FA.GorselThumbnail))
            {
                ImageGorsel.Visible = true;
                ImageGorsel.ImageUrl = FA.GorselThumbnail;
            }
            HiddenFieldId.Value = FA.Id.ToString();
        }

        private void FotoAlbumGorselleriVer(int FotoAlbumKategoriId)
        {
            // Görselleri ver
            MultiView2.ActiveViewIndex = 1;
            var FAGList = (from FAG in Veriler.FotoAlbumGorselleri
                           where FAG.FotoAlbumId == FotoAlbumKategoriId
                           select new
                                      {
                                          FAG.Id,
                                          Gorsel = FAG.GorselThumbnail,
                                          FAG.GorselAdi,
                                          FAG.Aciklama
                                      }).ToList();
            ListViewGorseller.DataSource = FAGList;
            ListViewGorseller.DataBind();
        }

        protected void LinkButtonGorselKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                FotoAlbumGorselleri FAG;

                # region Görsel

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

                # endregion

                string KayitYeri = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                string KayitYeriThumbnail = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails + "/";
                if (HiddenFieldGorselId.Value != string.Empty)
                {
                    // Güncelle

                    # region Guncelle

                    int Id = Convert.ToInt32(HiddenFieldGorselId.Value);
                    FAG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
                    FAG.FotoAlbumId = Convert.ToInt32(HiddenFieldKategoriId.Value);
                    FAG.GorselAdi = TextBoxGorselAdi.Text;
                    if (FileUploadGorsel.HasFile)
                    {
                        GorselSil(FAG.Gorsel);
                        GorselSil(FAG.GorselThumbnail);
                        FAG.Gorsel = GorselBoyutlandir(FileUploadGorsel, KayitYeri, 700);
                        FAG.GorselThumbnail = GorselBoyutlandir(FileUploadGorsel, KayitYeriThumbnail, 175);
                    }
                    FAG.Aciklama = TextBoxAciklama.Text;
                    FAG.Durum = CheckBoxGorselDurum.Checked;
                    FAG.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FAG.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    FotoAlbumGorselleriVer(Convert.ToInt32(HiddenFieldKategoriId.Value));
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 1;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");

                    # endregion
                }
                else
                {
                    // Yeni kayıt

                    # region Ekle

                    FAG = new FotoAlbumGorselleri();
                    FAG.FotoAlbumId = Convert.ToInt32(HiddenFieldKategoriId.Value);
                    FAG.GorselAdi = TextBoxGorselAdi.Text;
                    if (FileUploadGorsel.HasFile)
                    {
                        FAG.Gorsel = GorselBoyutlandir(FileUploadGorsel, KayitYeri, 700);
                        FAG.GorselThumbnail = GorselBoyutlandir(FileUploadGorsel, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        FAG.Gorsel = null;
                        FAG.GorselThumbnail = null;
                    }
                    FAG.Aciklama = TextBoxAciklama.Text;
                    FAG.Durum = CheckBoxGorselDurum.Checked;
                    FAG.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FAG.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToFotoAlbumGorselleri(FAG);
                    Veriler.SaveChanges();
                    Temizle();
                    FotoAlbumGorselleriVer(Convert.ToInt32(HiddenFieldKategoriId.Value));
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 1;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");

                    # endregion
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        protected void LinkButtonGorselIptal_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
        }

        private void GorselAlanlariniTemizle()
        {
            TextBoxGorselAdi.Text = string.Empty;
            Image1.Visible = false;
            TextBoxGorselAciklama.Text = String.Empty;
            HiddenFieldGorselId.Value = string.Empty;
        }

        protected void ListViewGorseller_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "GorselDuzenle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument.ToString());
                FotoAlbumGorselleri FAG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).FirstOrDefault();
                if (FAG != null)
                {
                    TextBoxGorselAdi.Text = FAG.GorselAdi;
                    if (!string.IsNullOrEmpty(FAG.GorselThumbnail))
                    {
                        Image1.Visible = true;
                        Image1.ImageUrl = FAG.GorselThumbnail;
                    }
                    TextBoxGorselAciklama.Text = FAG.Aciklama;
                    CheckBoxGorselDurum.Checked = FAG.Durum;
                    HiddenFieldGorselId.Value = FAG.Id.ToString();
                    HiddenFieldKategoriId.Value = FAG.FotoAlbumId.ToString();
                    MultiView1.ActiveViewIndex = 1;
                }
            }
            else if (e.CommandName == "GorselSil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument.ToString());
                FotoAlbumGorselleri FAG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).FirstOrDefault();
                if (!string.IsNullOrEmpty(FAG.GorselThumbnail))
                {
                    GorselSil(FAG.GorselThumbnail);
                }
                Veriler.FotoAlbumGorselleri.DeleteObject(FAG);
                Veriler.SaveChanges();
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 1;
                FotoAlbumGorselleriVer(FAG.FotoAlbumId);
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        protected void LinkButtonYeniGorselekle_Click(object sender, EventArgs e)
        {
            GorselAlanlariniTemizle();
            MultiView1.ActiveViewIndex = 1;
        }
    }
}