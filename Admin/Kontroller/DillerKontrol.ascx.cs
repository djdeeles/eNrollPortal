using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class DillerKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Dilleri ver
            var DList = Veriler.Diller.Where(p => p.Durum).OrderBy(p => p.Id).ToList();
            foreach (Diller D in DList)
            {
                ImageButton Ib = new ImageButton();
                switch (D.Id)
                {
                    case 1:
                        Ib.ImageUrl = "../Theme/Images/tr.png";
                        break;
                    case 2:
                        Ib.ImageUrl = "../Theme/Images/en.png";
                        break;
                }
                Ib.CommandArgument = D.Id.ToString();
                Ib.Click += Ib_Click;
                Ib.CssClass = "Ib";
                if (EnrollContext.Current.WorkingLanguage.languageId == D.Id)
                {
                    Ib.BorderColor = Color.Purple;
                    Ib.BorderStyle = BorderStyle.Double;
                    Ib.BorderWidth = Unit.Pixel(3);
                }
                else
                {
                    Ib.BorderColor = Color.Purple;
                    Ib.BorderStyle = BorderStyle.Solid;
                    Ib.BorderWidth = Unit.Pixel(1);
                }
                Panel1.Controls.Add(Ib);
            }
        }

        private void Ib_Click(object sender, ImageClickEventArgs e)
        {
            // Dil değiştir
            try
            {
                ImageButton Ib = (ImageButton) sender;
                int DilId = Convert.ToInt32(Ib.CommandArgument);
                EnrollContext.Current.WorkingLanguage.languageId = DilId;
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }
    }
}