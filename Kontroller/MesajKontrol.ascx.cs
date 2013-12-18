using System;
using System.Web.UI;

namespace EnrollPortal.Kontroller
{
    public partial class MesajKontrol : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Panel1.Visible = false;
            }
        }

        public void Mesaj(bool Durum, string Mesaj)
        {
            Panel1.Visible = true;
            switch (Durum)
            {
                case true:
                    ImageMesaj.ImageUrl = "/App_Themes/MainTheme/Images/true.jpg";
                    break;
                case false:
                    ImageMesaj.ImageUrl = "/App_Themes/MainTheme/Images/false.jpg";
                    break;
            }
            LabelMesaj.Text = Mesaj;
        }

        public void Reset()
        {
            Panel1.Visible = false;
        }
    }
}