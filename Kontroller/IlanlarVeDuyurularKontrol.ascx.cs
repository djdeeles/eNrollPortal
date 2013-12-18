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
    public partial class IlanlarVeDuyurularKontrol : UserControl
    {
        private readonly int DilId = EnrollContext.Current.WorkingLanguage.languageId;
        private readonly int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["IlanCode"] != null)
                    {
                        if (Request.QueryString["IlanCode"] == "IlanEkle")
                        {
                            MultiView1.ActiveViewIndex = 0;
                            Temizle();
                            HiddenFieldBolum.Value = "Ilan";
                            DataPager1.Visible = false;
                            IlanKategorileriVer(CheckBoxListKategoriler);
                            LiteralSiteMap.Text =
                                "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=IlanEkle\">Yeni İlan Ekle</a>";
                        }
                        else
                        {
                            MultiView1.ActiveViewIndex = -1;
                            IlanlariVer();
                        }
                    }
                    else if (Request.QueryString["DuyuruCode"] != null)
                    {
                        if (Request.QueryString["DuyuruCode"] == "DuyuruEkle")
                        {
                            MultiView1.ActiveViewIndex = 0;
                            Temizle();
                            HiddenFieldBolum.Value = "Duyuru";
                            DataPager1.Visible = false;
                            DuyuruKategorileriVer(CheckBoxListKategoriler);
                            LiteralSiteMap.Text =
                                "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=DuyuruEkle\">Yeni Duyuru Ekle</a>";
                        }
                        else
                        {
                            MultiView1.ActiveViewIndex = -1;
                            DuyurulariVer();
                        }
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = -1;
                }
                MenuIlanKategoriler.Items.Clear();
                MenuIlanKategorilerVer(MenuIlanKategoriler.Items);
                MenuDuyuruKategoriler.Items.Clear();
                MenuDuyuruKategorilerVer(MenuDuyuruKategoriler.Items);
            }
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void MenuIlanKategorilerVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IKList =
                Veriler.IlanKategorileri.Where(p => p.Durum && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();

            MenuItem M1 = new MenuItem();
            M1.NavigateUrl = "../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=IlanEkle";
            M1.Text = "Yeni İlan Ekle";
            Items.Add(M1);

            MenuItem M2 = new MenuItem();
            M2.NavigateUrl = "../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=0";
            M2.Text = "İlanlarım";
            Items.Add(M2);

            MenuItem M3 = new MenuItem();
            M3.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?IlanCode=0";
            M3.Text = "Tüm İlanlar";
            Items.Add(M3);

            foreach (IlanKategorileri IK in IKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?IlanCode=" + IK.Id.ToString();
                MI.Text = IK.KategoriAdi;
                Items.Add(MI);
            }
        }

        private void MenuDuyuruKategorilerVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var PDKList =
                Veriler.PersonelDuyuruKategorileri.Where(p => p.Durum && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList
                    ();

            MenuItem M1 = new MenuItem();
            M1.NavigateUrl = "../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=DuyuruEkle";
            M1.Text = "Yeni Duyuru Ekle";
            Items.Add(M1);

            MenuItem M2 = new MenuItem();
            M2.NavigateUrl = "../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=0";
            M2.Text = "Duyurularım";
            Items.Add(M2);

            MenuItem M3 = new MenuItem();
            M3.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?DuyuruCode=0";
            M3.Text = "Tüm Duyurular";
            Items.Add(M3);

            foreach (PersonelDuyuruKategorileri PDK in PDKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?DuyuruCode=" + PDK.Id.ToString();
                MI.Text = PDK.KategoriAdi;
                Items.Add(MI);
            }
        }

        private void Temizle()
        {
            TextBoxBaslik.Text = string.Empty;
            TextBoxOzet.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
            RadDateTimePickerBaslangicTarihi.SelectedDate = DateTime.Now;
            RadDateTimePickerBitisTarihi.SelectedDate = null;
            ImageGorsel1.Visible = false;
            ImageGorsel2.Visible = false;
            ImageGorsel3.Visible = false;
            ImageGorsel4.Visible = false;
            CheckBoxDurum.Checked = false;
            TextBoxAnahtarKelimeler.Text = string.Empty;
            HiddenFieldId.Value = string.Empty;
            HiddenFieldBolum.Value = string.Empty;
        }

        private void IlanKategorileriVer(DropDownList DropDownListKategoriler)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IK = Veriler.IlanKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = IK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void IlanKategorileriVer(CheckBoxList CheckBoxListKategoriler)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IK = Veriler.IlanKategorileri.Where(p => p.DilId == DilId && p.Durum).OrderBy(p => p.SiraNo).ToList();
            CheckBoxListKategoriler.DataTextField = "KategoriAdi";
            CheckBoxListKategoriler.DataValueField = "Id";
            CheckBoxListKategoriler.DataSource = IK;
            CheckBoxListKategoriler.DataBind();
        }

        private void DuyuruKategorileriVer(DropDownList DropDownListKategoriler)
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

        private void DuyuruKategorileriVer(CheckBoxList CheckBoxListKategoriler)
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

        private void IlanlariVer()
        {
            // İlanları ver
            if (Request.QueryString["IlanCode"] == "0")
            {
                var IList = (from IT in Veriler.IlanlarTablosu
                             join I in Veriler.Ilanlar
                                 on IT.IlanId equals I.Id
                             join IK in Veriler.IlanKategorileri
                                 on IT.KategoriId equals IK.Id
                             where IK.DilId == DilId
                                   && I.KaydedenKullaniciId == KullaniciId
                             select new
                                        {
                                            I.Id,
                                            I.Baslik,
                                            I.Ozet,
                                            Tarih = I.BaslangicTarihi,
                                            Resim = I.GorselThumbnail1 != null
                                                        ? I.GorselThumbnail1
                                                        : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).ToList();
                ListViewVeriler.DataSource = IList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text = "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx\">İlanlarım</a>";
            }
            else
            {
                int KategoriId = Convert.ToInt32(Request.QueryString["IlanCode"]);
                var IList = (from IT in Veriler.IlanlarTablosu
                             join I in Veriler.Ilanlar
                                 on IT.IlanId equals I.Id
                             join IK in Veriler.IlanKategorileri
                                 on IT.KategoriId equals IK.Id
                             where IK.DilId == DilId
                                   && I.KaydedenKullaniciId == KullaniciId
                                   && IK.Id == KategoriId
                             select new
                                        {
                                            I.Id,
                                            I.Baslik,
                                            I.Ozet,
                                            Tarih = I.BaslangicTarihi,
                                            Resim = I.GorselThumbnail1 != null
                                                        ? I.GorselThumbnail1
                                                        : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).ToList();
                ListViewVeriler.DataSource = IList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text = "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx\">Tüm İlanlar</a> / ";
                LiteralSiteMap.Text += "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=" + KategoriId + "\">" +
                                       Veriler.IlanKategorileri.Where(p => p.Id == KategoriId).FirstOrDefault().
                                           KategoriAdi + "</a>";
            }
        }

        private void DuyurulariVer()
        {
            // Duyruları ver
            if (Request.QueryString["DuyuruCode"] == "0")
            {
                var DList = (from DT in Veriler.PersonelDuyurularTablosu
                             join D in Veriler.PersonelDuyurular
                                 on DT.DuyuruId equals D.Id
                             join DK in Veriler.PersonelDuyuruKategorileri
                                 on DT.KategoriId equals DK.Id
                             where DK.DilId == DilId
                                   && D.KaydedenKullaniciId == KullaniciId
                             select new
                                        {
                                            D.Id,
                                            D.Baslik,
                                            D.Ozet,
                                            Tarih = D.BaslangicTarihi,
                                            Resim = D.GorselThumbnail1 != null
                                                        ? D.GorselThumbnail1
                                                        : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).ToList();
                ListViewVeriler.DataSource = DList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text = "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=0\">Duyurularım</a>";
            }
            else
            {
                int KategoriId = Convert.ToInt32(Request.QueryString["DuyuruCode"]);
                var DList = (from DT in Veriler.PersonelDuyurularTablosu
                             join D in Veriler.PersonelDuyurular
                                 on DT.DuyuruId equals D.Id
                             join DK in Veriler.PersonelDuyuruKategorileri
                                 on DT.KategoriId equals DK.Id
                             where DK.DilId == DilId
                                   && D.KaydedenKullaniciId == KullaniciId
                                   && DK.Id == KategoriId
                             select new
                                        {
                                            D.Id,
                                            D.Baslik,
                                            D.Ozet,
                                            Tarih = D.BaslangicTarihi,
                                            Resim = D.GorselThumbnail1 != null
                                                        ? D.GorselThumbnail1
                                                        : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).ToList();
                ListViewVeriler.DataSource = DList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=0\">Tüm Duyurular</a> / ";
                LiteralSiteMap.Text += "<a href=\"../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=" + KategoriId + "\">" +
                                       Veriler.PersonelDuyuruKategorileri.Where(p => p.Id == KategoriId).FirstOrDefault()
                                           .KategoriAdi + "</a>";
            }
        }

        protected void LinkButtonKaydet_Click(object sender, EventArgs e)
        {
            if (HiddenFieldBolum.Value == "Ilan")
            {
                IlanEkleDuzenle();
            }
            else if (HiddenFieldBolum.Value == "Duyuru")
            {
                DuyuruEkleDuzenle();
            }
        }

        private void IlanEkleDuzenle()
        {
            try
            {
                Ilanlar I;

                # region Gorsel

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

                #endregion

                string KayitYeri = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                string KayitYeriThumbnail = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle

                    # region Guncelle

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
                    if (FileUploadGorsel1.HasFile)
                    {
                        GorselSil(I.Gorsel1);
                        GorselSil(I.GorselThumbnail1);
                        I.Gorsel1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeri, 700);
                        I.GorselThumbnail1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeriThumbnail, 175);
                    }
                    if (FileUploadGorsel2.HasFile)
                    {
                        GorselSil(I.Gorsel2);
                        GorselSil(I.GorselThumbnail2);
                        I.Gorsel2 = GorselBoyutlandir(FileUploadGorsel2, KayitYeri, 700);
                    }
                    if (FileUploadGorsel3.HasFile)
                    {
                        GorselSil(I.Gorsel3);
                        GorselSil(I.GorselThumbnail3);
                        I.Gorsel3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeri, 700);
                        I.GorselThumbnail3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeriThumbnail, 175);
                    }
                    if (FileUploadGorsel4.HasFile)
                    {
                        GorselSil(I.Gorsel4);
                        GorselSil(I.GorselThumbnail4);
                        I.Gorsel4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeri, 700);
                        I.GorselThumbnail4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeriThumbnail, 175);
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
                    Response.Redirect("../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=0");

                    # endregion
                }
                else
                {
                    // Yeni kayıt

                    # region Ekle

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
                    if (FileUploadGorsel1.HasFile)
                    {
                        GorselSil(I.Gorsel1);
                        GorselSil(I.GorselThumbnail1);
                        I.Gorsel1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeri, 700);
                        I.GorselThumbnail1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        I.Gorsel1 = null;
                        I.GorselThumbnail1 = null;
                    }
                    if (FileUploadGorsel2.HasFile)
                    {
                        GorselSil(I.Gorsel2);
                        GorselSil(I.GorselThumbnail2);
                        I.Gorsel2 = GorselBoyutlandir(FileUploadGorsel2, KayitYeri, 700);
                    }
                    else
                    {
                        I.Gorsel2 = null;
                        I.GorselThumbnail2 = null;
                    }
                    if (FileUploadGorsel3.HasFile)
                    {
                        GorselSil(I.Gorsel3);
                        GorselSil(I.GorselThumbnail3);
                        I.Gorsel3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeri, 700);
                        I.GorselThumbnail3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        I.Gorsel3 = null;
                        I.GorselThumbnail3 = null;
                    }
                    if (FileUploadGorsel4.HasFile)
                    {
                        GorselSil(I.Gorsel4);
                        GorselSil(I.GorselThumbnail4);
                        I.Gorsel4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeri, 700);
                        I.GorselThumbnail4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeriThumbnail, 175);
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
                    Response.Redirect("../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=0");

                    # endregion
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        private void DuyuruEkleDuzenle()
        {
            try
            {
                PersonelDuyurular PD;

                # region Gorsel

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

                #endregion

                string KayitYeri = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                string KayitYeriThumbnail = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle

                    # region Guncelle

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
                    if (FileUploadGorsel1.HasFile)
                    {
                        GorselSil(PD.Gorsel1);
                        GorselSil(PD.GorselThumbnail1);
                        PD.Gorsel1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeri, 700);
                        PD.GorselThumbnail1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeriThumbnail, 175);
                    }
                    if (FileUploadGorsel2.HasFile)
                    {
                        GorselSil(PD.Gorsel2);
                        GorselSil(PD.GorselThumbnail2);
                        PD.Gorsel2 = GorselBoyutlandir(FileUploadGorsel2, KayitYeri, 700);
                        PD.GorselThumbnail2 = GorselBoyutlandir(FileUploadGorsel2, KayitYeriThumbnail, 175);
                    }
                    if (FileUploadGorsel3.HasFile)
                    {
                        GorselSil(PD.Gorsel3);
                        GorselSil(PD.GorselThumbnail3);
                        PD.Gorsel3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeri, 700);
                        PD.GorselThumbnail3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeriThumbnail, 175);
                    }
                    if (FileUploadGorsel4.HasFile)
                    {
                        GorselSil(PD.Gorsel4);
                        GorselSil(PD.GorselThumbnail4);
                        PD.Gorsel4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeri, 700);
                        PD.GorselThumbnail4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeriThumbnail, 175);
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
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                    MultiView1.ActiveViewIndex = -1;
                    DuyurulariVer();

                    #endregion
                }
                else
                {
                    // Yeni kayıt

                    # region Ekle

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
                    if (FileUploadGorsel1.HasFile)
                    {
                        PD.Gorsel1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeri, 700);
                        PD.GorselThumbnail1 = GorselBoyutlandir(FileUploadGorsel1, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        PD.Gorsel1 = null;
                        PD.GorselThumbnail1 = null;
                    }

                    if (FileUploadGorsel2.HasFile)
                    {
                        PD.Gorsel2 = GorselBoyutlandir(FileUploadGorsel2, KayitYeri, 700);
                        PD.GorselThumbnail2 = GorselBoyutlandir(FileUploadGorsel2, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        PD.Gorsel2 = null;
                        PD.GorselThumbnail2 = null;
                    }
                    if (FileUploadGorsel3.HasFile)
                    {
                        PD.Gorsel3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeri, 700);
                        PD.GorselThumbnail3 = GorselBoyutlandir(FileUploadGorsel3, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        PD.Gorsel3 = null;
                        PD.GorselThumbnail3 = null;
                    }

                    if (FileUploadGorsel4.HasFile)
                    {
                        PD.Gorsel4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeri, 700);
                        PD.GorselThumbnail4 = GorselBoyutlandir(FileUploadGorsel4, KayitYeriThumbnail, 175);
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
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                    MultiView1.ActiveViewIndex = -1;
                    DuyurulariVer();

                    # endregion
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        private void KategorileriSil(int Id)
        {
            // Kategorileri sil
            if (HiddenFieldBolum.Value == "Ilan")
            {
                var ITList = Veriler.IlanlarTablosu.Where(p => p.IlanId == Id).ToList();
                foreach (var Item in ITList)
                {
                    IlanlarTablosu IT = Veriler.IlanlarTablosu.Where(p => p.IlanId == Item.IlanId).First();
                    Veriler.IlanlarTablosu.DeleteObject(IT);
                    Veriler.SaveChanges();
                }
            }
            else if (HiddenFieldBolum.Value == "Duyuru")
            {
                var PDTList = Veriler.PersonelDuyurularTablosu.Where(p => p.DuyuruId == Id).ToList();
                foreach (var Item in PDTList)
                {
                    PersonelDuyurularTablosu PDT =
                        Veriler.PersonelDuyurularTablosu.Where(p => p.DuyuruId == Item.DuyuruId).First();
                    Veriler.PersonelDuyurularTablosu.DeleteObject(PDT);
                    Veriler.SaveChanges();
                }
            }
        }

        private void KategoriKaydet(int KategoriId, int Id)
        {
            // Kategori kaydet
            if (HiddenFieldBolum.Value == "Ilan")
            {
                IlanlarTablosu IT = new IlanlarTablosu();
                IT.IlanId = Id;
                IT.KategoriId = KategoriId;
                Veriler.IlanlarTablosu.AddObject(IT);
                Veriler.SaveChanges();
            }
            else if (HiddenFieldBolum.Value == "Duyuru")
            {
                PersonelDuyurularTablosu PDT = new PersonelDuyurularTablosu();
                PDT.DuyuruId = Id;
                PDT.KategoriId = KategoriId;
                Veriler.PersonelDuyurularTablosu.AddObject(PDT);
                Veriler.SaveChanges();
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
            if (HiddenFieldBolum.Value == "Ilan")
            {
                Response.Redirect("../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=0");
            }
            else if (HiddenFieldBolum.Value == "Duyuru")
            {
                Response.Redirect("../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=0");
            }
        }

        protected void ListViewVeriler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["IlanCode"] != null)
                    {
                        MultiView1.ActiveViewIndex = 0;
                        Ilanlar I = Veriler.Ilanlar.Where(p => p.Id == Id).First();
                        IlanGuncelle(I);
                    }
                    else if (Request.QueryString["DuyuruCode"] != null)
                    {
                        MultiView1.ActiveViewIndex = 0;
                        PersonelDuyurular PD = Veriler.PersonelDuyurular.Where(p => p.Id == Id).First();
                        DuyuruGuncelle(PD);
                    }
                }
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["IlanCode"] != null)
                    {
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
                        IlanlariVer();
                        MesajKontrol2.Mesaj(true, "Kayıt silindi.");
                    }
                    else if (Request.QueryString["DuyuruCode"] != null)
                    {
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
                        //MultiView1.ActiveViewIndex = -1;
                        DuyurulariVer();
                        MesajKontrol2.Mesaj(true, "Kayıt silindi.");
                    }
                }
            }
        }

        private void IlanGuncelle(Ilanlar I)
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
            }
            else
            {
                ImageGorsel1.Visible = false;
            }
            if (I.GorselThumbnail2 != null)
            {
                ImageGorsel2.Visible = true;
                ImageGorsel2.ImageUrl = I.GorselThumbnail2;
            }
            else
            {
                ImageGorsel2.Visible = false;
            }
            if (I.GorselThumbnail3 != null)
            {
                ImageGorsel3.Visible = true;
                ImageGorsel3.ImageUrl = I.GorselThumbnail3;
            }
            else
            {
                ImageGorsel3.Visible = false;
            }
            if (I.GorselThumbnail4 != null)
            {
                ImageGorsel4.Visible = true;
                ImageGorsel4.ImageUrl = I.GorselThumbnail4;
            }
            else
            {
                ImageGorsel4.Visible = false;
            }
            CheckBoxDurum.Checked = I.Durum;
            TextBoxAnahtarKelimeler.Text = I.AnahtarKelimeler;
            HiddenFieldId.Value = I.Id.ToString();
            IlanKategorileriVer(CheckBoxListKategoriler);
            for (int i = 0; i <= CheckBoxListKategoriler.Items.Count - 1; i++)
            {
                int KategoriId = Convert.ToInt32(CheckBoxListKategoriler.Items[i].Value);
                var ITList = Veriler.IlanlarTablosu.Where(p => p.IlanId == I.Id && p.KategoriId == KategoriId).ToList();
                foreach (var Item in ITList)
                {
                    CheckBoxListKategoriler.Items[i].Selected = true;
                }
            }
        }

        private void DuyuruGuncelle(PersonelDuyurular PD)
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
            }
            if (PD.GorselThumbnail2 != null)
            {
                ImageGorsel2.Visible = true;
                ImageGorsel2.ImageUrl = PD.GorselThumbnail2;
            }
            if (PD.GorselThumbnail3 != null)
            {
                ImageGorsel3.Visible = true;
                ImageGorsel3.ImageUrl = PD.GorselThumbnail3;
            }
            if (PD.GorselThumbnail4 != null)
            {
                ImageGorsel4.Visible = true;
                ImageGorsel4.ImageUrl = PD.GorselThumbnail4;
            }
            CheckBoxDurum.Checked = PD.Durum;
            TextBoxAnahtarKelimeler.Text = PD.AnahtarKelimeler;
            HiddenFieldId.Value = PD.Id.ToString();
            DuyuruKategorileriVer(CheckBoxListKategoriler);
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
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }
    }
}