using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Xml;
using EnrollPortal.Kutuphaneler;

namespace EnrollPortal.Kontroller
{
    public partial class PortalRssListKontrol : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RssVer();
            }
        }

        private void RssVer()
        {
            try
            {
                XmlTextReader okuyucu = new XmlTextReader("http://enroll.com.tr/feed");
                XmlDocument dokuman = new XmlDocument();
                dokuman.Load(okuyucu);
                XmlNode rss = dokuman.SelectSingleNode("/rss");
                XmlNodeList title = dokuman.SelectNodes("/rss/channel/title");
                XmlNodeList Nodelist = dokuman.SelectNodes("/rss/channel/item");
                var PRList = new List<PortalRss>();
                var PRListSort = new List<PortalRss>();
                foreach (XmlNode Item in Nodelist)
                {
                    PortalRss PR = new PortalRss();
                    if (Item.SelectSingleNode("category") != null)
                    {
                        PR.category = Item.SelectSingleNode("category").InnerText;
                        switch (PR.category)
                        {
                            case "Haber":
                                PR.icon =
                                    "<img src=\"../App_Themes/MainTheme/Images/ikonlar/haber.png\" style=\"vertical-align: middle;\" alt=\"" +
                                    PR.category + "\" />";
                                break;

                            case "Duyuru":
                                PR.icon =
                                    "<img src=\"../App_Themes/MainTheme/Images/ikonlar/duyuru.png\" style=\"vertical-align: middle;\" alt=\"" +
                                    PR.category + "\" />";
                                break;

                            case "Etkinlik":
                                PR.icon =
                                    "<img src=\"../App_Themes/MainTheme/Images/ikonlar/etkinlik.png\" style=\"vertical-align: middle;\"  alt=\"" +
                                    PR.category + "\" />";
                                break;

                            case "İhale":
                                PR.icon =
                                    "<img src=\"../App_Themes/MainTheme/Images/ikonlar/ihale.png\" style=\"vertical-align: middle;\" alt=\"" +
                                    PR.category + "\" />";
                                break;

                            case "Albüm":
                                PR.icon =
                                    "<img src=\"../App_Themes/MainTheme/Images/ikonlar/album.png\" style=\"vertical-align: middle;\" alt=\"" +
                                    PR.category + "\" />";
                                break;

                            case "Doküman":
                                PR.icon =
                                    "<img src=\"../App_Themes/MainTheme/Images/ikonlar/dokuman.png\" style=\"vertical-align: middle;\" alt=\"" +
                                    PR.category + "\" />";
                                break;

                            case "Video":
                                PR.icon =
                                    "<img src=\"../App_Themes/MainTheme/Images/ikonlar/video.png\" style=\"vertical-align: middle;\" alt=\"" +
                                    PR.category + "\" />";
                                break;
                        }
                    }
                    if (Item.SelectSingleNode("title") != null)
                        PR.title = Item.SelectSingleNode("title").InnerText;
                    if (Item.SelectSingleNode("description") != null)
                        PR.description = Item.SelectSingleNode("description").InnerText;
                    if (Item.SelectSingleNode("date") != null)
                        PR.date = Convert.ToDateTime(Item.SelectSingleNode("date").InnerText).ToString("dd.MM.yyyy");
                    if (Item.SelectSingleNode("date") != null)
                        PR.time = Convert.ToDateTime(Item.SelectSingleNode("date").InnerText).ToString("HH:mm");
                    if (Item.SelectSingleNode("link") != null)
                        PR.link = Item.SelectSingleNode("link").InnerText;
                    PRList.Add(PR);
                }
                var PRssList = new List<PortalRssList>();
                var GList = from o in PRList
                            orderby o.date descending
                            group o by o.date
                            into g
                            orderby g.FirstOrDefault().date descending
                            select g.ToList().Distinct();
                foreach (var Item in GList)
                {
                    string Liste = string.Empty;
                    Liste += "<table class=\"listalternate\" cellpadding=\"0\" cellspacing=\"0\"><tr style=\"min-height: 20px; text-align: left;\"><td style=\"width: 100%; font-weight: bold; padding-left: 10px;\">"
                             + Item.FirstOrDefault().date
                             + "</td></tr></table>";
                    PortalRssList P = new PortalRssList();
                    P.Liste = Liste;
                    PRssList.Add(P);
                    foreach (var PR in Item)
                    {
                        string Liste2 = string.Empty;
                        Liste2 = "<table class=\"list\" cellpadding=\"0\" cellspacing=\"0\" style=\" padding:5px 0;\"><tr style=\"text-align: left;\"><td style=\"min-width: 65px; padding-left: 10px; padding-right: 5px; \">"
                                 + PR.time
                                 + "&nbsp;&nbsp; "
                                 + PR.icon
                                 + "</td> <td style=\"width: 100%;\"> <a href=\" "
                                 + PR.link
                                 + "\" target=\"_blank;\">"
                                 + PR.title
                                 + "</a></td></tr></table>";
                        PortalRssList P2 = new PortalRssList();
                        P2.Liste = Liste2;
                        PRssList.Add(P2);
                    }
                }
                ListViewVeriler.DataSource = PRssList;
                ListViewVeriler.DataBind();
            }
            catch
            {
                ListViewVeriler.DataSource = null;
                ListViewVeriler.DataBind();
            }
        }
    }
}