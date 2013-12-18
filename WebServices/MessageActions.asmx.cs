using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.WebServices
{
    /// <summary>
    ///   Summary description for WebServicePersonelAra
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class MessageActions : WebService
    {
        [WebMethod]
        public List<MessageType> GetMessages(int itemCount, int pageCount, String type, String userId)
        {
            var veriler = new EnrollPortalEntities();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(type)) return null;

            var returnData = new List<MessageType>();
            var userid = Convert.ToInt32(userId);
            switch (type)
            {
                #region Gelen Mesajlar

                case "0":
                    /*var mesajList = (from m in veriler.KullaniciMesajlari
                                     where m.AliciId == userid && !m.AliciSildiMi
                                     orderby m.Tarih descending
                                     select new
                                     {
                                         id = m.Id,
                                         title = m.Baslik,
                                         mesaj = m.Mesaj,
                                         tarih = m.Tarih,
                                         aliciId = m.AliciId,
                                         gonderenId = m.GonderenId,
                                         m.OkunduMu,
                                         m.AliciSildiMi,
                                         m.GonderenSildiMi
                                     }).ToList();
                    foreach (var item in mesajList)
                    {
                        var newData = new MessageType();
                        newData.Id = item.id.ToString();
                        newData.Title = item.title;
                        newData.Content = item.mesaj;
                        var alici = veriler.Kullanicilar.Single(p => p.Id == item.aliciId);
                        var gonderen = veriler.Kullanicilar.Single(p => p.Id == item.gonderenId);
                        newData.ToId = alici.Id;
                        newData.ToMail = alici.EPosta;
                        newData.ToAd = alici.Ad;
                        newData.ToSoyad = alici.Soyad;
                        newData.FromId = gonderen.Id;
                        newData.FromMail = gonderen.EPosta;
                        newData.FromAd = gonderen.Ad;
                        newData.FromSoyad = gonderen.Soyad;
                        newData.OkunduMu = item.OkunduMu;
                        newData.AliciSildiMi = item.AliciSildiMi;
                        newData.GonderenSildiMi = item.GonderenSildiMi;
                        newData.Date = item.tarih.ToShortDateString() + " " + item.tarih.ToShortTimeString();
                        returnData.Add(newData);
                    }
                    */
                    var mesajList = (from m in veriler.KullaniciMesajlari
                                       where m.AliciId == userid && !m.AliciSildiMi
                                       orderby m.Tarih descending
                                       select new
                                       {
                                           id = m.Id,
                                           title = m.Baslik,
                                           mesaj = m.Mesaj,
                                           tarih = m.Tarih,
                                           aliciId = m.AliciId,
                                           gonderenId = m.GonderenId,
                                           m.OkunduMu,
                                           m.AliciSildiMi,
                                           m.GonderenSildiMi
                                       }).ToList();
                    foreach (var item in mesajList)
                    {
                        var newData = new MessageType();
                        newData.Id = item.id.ToString();
                        newData.Title = item.title;
                        newData.Content = item.mesaj;
                        var alici = veriler.Kullanicilar.Single(p => p.Id == item.aliciId);
                        var gonderen = veriler.Kullanicilar.Single(p => p.Id == item.gonderenId);
                        newData.ToId = alici.Id;
                        newData.ToMail = alici.EPosta;
                        newData.ToAd = alici.Ad;
                        newData.ToSoyad = alici.Soyad;
                        newData.FromId = gonderen.Id;
                        newData.FromMail = gonderen.EPosta;
                        newData.FromAd = gonderen.Ad;
                        newData.FromSoyad = gonderen.Soyad;
                        newData.Date = item.tarih.ToShortDateString() + " " + item.tarih.ToShortTimeString();
                        newData.OkunduMu = item.OkunduMu;
                        newData.AliciSildiMi = item.AliciSildiMi;
                        newData.GonderenSildiMi = item.GonderenSildiMi;
                        returnData.Add(newData);
                    }
                    break;

                #endregion

                #region Giden Mesajlar

                case "1":
                    var toMesajList = (from m in veriler.KullaniciMesajlari
                                       where m.GonderenId == userid && !m.GonderenSildiMi
                                       orderby m.Tarih descending
                                       select new
                                       {
                                           id = m.Id,
                                           title = m.Baslik,
                                           mesaj = m.Mesaj,
                                           tarih = m.Tarih,
                                           aliciId = m.AliciId,
                                           gonderenId = m.GonderenId,
                                           m.OkunduMu,
                                           m.AliciSildiMi,
                                           m.GonderenSildiMi
                                       }).ToList();
                    foreach (var item in toMesajList)
                    {
                        var newData = new MessageType();
                        newData.Id = item.id.ToString();
                        newData.Title = item.title;
                        newData.Content = item.mesaj;
                        var alici = veriler.Kullanicilar.Single(p => p.Id == item.aliciId);
                        var gonderen = veriler.Kullanicilar.Single(p => p.Id == item.gonderenId);
                        newData.ToId = alici.Id;
                        newData.ToMail = alici.EPosta;
                        newData.ToAd = alici.Ad;
                        newData.ToSoyad = alici.Soyad;
                        newData.FromId = gonderen.Id;
                        newData.FromMail = gonderen.EPosta;
                        newData.FromAd = gonderen.Ad;
                        newData.FromSoyad = gonderen.Soyad;
                        newData.Date = item.tarih.ToShortDateString() + " " + item.tarih.ToShortTimeString();
                        newData.OkunduMu = item.OkunduMu;
                        newData.AliciSildiMi = item.AliciSildiMi;
                        newData.GonderenSildiMi = item.GonderenSildiMi;
                        returnData.Add(newData);
                    }
                    break;

                #endregion
            }

            return returnData;
        }

        [WebMethod]
        public List<MessageType> GetMessagesCount(String userId)
        {
            var veriler = new EnrollPortalEntities();

            if (string.IsNullOrEmpty(userId)) return null;
            var userid = Convert.ToInt32(userId);
            var returnData = new List<MessageType>();
            returnData.Add(new MessageType());

            returnData[0].InboxUnreadCount = veriler.KullaniciMesajlari.Count(p => p.AliciId == userid && !p.OkunduMu && !p.AliciSildiMi);
            returnData[0].InboxCount = veriler.KullaniciMesajlari.Count(p => p.AliciId == userid && !p.AliciSildiMi);
            returnData[0].OutboxCount = veriler.KullaniciMesajlari.Count(p => p.GonderenId == userid && !p.GonderenSildiMi);

            return returnData;
        }

        [WebMethod]
        public List<MessageType> DeleteSelectedMessages(string strMesajIds)
        {
            var a = new List<MessageType>();
            var messageType = new MessageType();
            var veriler = new EnrollPortalEntities();

            try
            {
                var data = strMesajIds.Split(',');
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] != null && data[i] != "")
                    {
                        var mesajId = Convert.ToInt32(data[i]);
                        var mesaj = veriler.KullaniciMesajlari.FirstOrDefault(p => p.Id == mesajId);
                        if (mesaj != null)
                        {
                            veriler.DeleteObject(mesaj);
                        }
                        veriler.SaveChanges();
                    }
                }
                messageType.Success = 1;
            }
            catch (Exception)
            {
                messageType.Success = 0;
            }
            
            a.Add(messageType);
            return a;
        }


        [WebMethod]
        public List<MessageType> SendMessage(int fromId, String toMail, String title, String message)
        {
            var a = new List<MessageType>();
            var messageType = new MessageType();

            var veriler = new EnrollPortalEntities();

            try
            {
                var toUser = veriler.Kullanicilar.FirstOrDefault(p => p.EPosta == toMail);
                if (toUser != null)
                {
                    var yeniMesaj = new KullaniciMesajlari();
                    yeniMesaj.AliciId = Convert.ToInt32(toUser.Id);
                    yeniMesaj.GonderenId = fromId;
                    yeniMesaj.Mesaj = message;
                    yeniMesaj.Baslik = title;
                    yeniMesaj.Tarih = DateTime.Now;
                    yeniMesaj.GonderenSildiMi = false;
                    yeniMesaj.AliciSildiMi = false;
                    yeniMesaj.OkunduMu = false;

                    veriler.AddToKullaniciMesajlari(yeniMesaj);
                    veriler.SaveChanges();

                    messageType.Id = yeniMesaj.Id.ToString();
                    messageType.Success = 1;
                }
                else
                {
                    messageType.Success = -1;
                }
            }
            catch (Exception)
            {
                messageType.Success = 0;
            }
            
            a.Add(messageType);
            return a;
        }

        [WebMethod]
        public List<Kullanicilar> GetUsers(String userId)
        {
            var veriler = new EnrollPortalEntities();

            if (string.IsNullOrEmpty(userId)) return null;
            var userid = Convert.ToInt32(userId);

            return veriler.Kullanicilar.Where(p => p.Id != userid).ToList();
        }
        [WebMethod]
        public List<MessageType> MarkAsReadMessage(String userId, int mesajId)
        {
            var veriler = new EnrollPortalEntities();

            if (string.IsNullOrEmpty(userId)) return null;
            var userid = Convert.ToInt32(userId);
            var returnData = new List<MessageType>();
            returnData.Add(new MessageType());
            var mesaj = veriler.KullaniciMesajlari.FirstOrDefault(p => p.AliciId == userid && p.Id == mesajId);
            if (mesaj != null)
            {
                mesaj.OkunduMu = true;
                returnData[0].OkunduMu = true;
            }
            else
            {
                returnData[0].OkunduMu = true;
            }
            veriler.SaveChanges();
            return returnData;
        }

        //[WebMethod]
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] PersonelList(string prefixText)
        {
            ArrayList KullanicilarList = new ArrayList();
            if (HttpContext.Current.Session["ServiceToken"] != null)
            {
                EnrollPortalEntities Veriler = new EnrollPortalEntities();
                int Id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                var KList = (from K in Veriler.Kullanicilar
                             where K.SilindiMi == false
                                   && K.Id != Id
                                   && K.Ad.StartsWith(prefixText)
                             orderby K.Ad ascending
                             select new
                             {
                                 K.Ad,
                                 K.Soyad,
                                 K.EPosta,
                             }).Take(10).ToList();
                foreach (var Item in KList)
                {
                    if (Item.Ad.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase))
                    {
                        KullanicilarList.Add(Item.Ad + " " + Item.Soyad + "<\"" + Item.EPosta + "\">");
                    }
                }
            }
            return (string[])(KullanicilarList.ToArray(typeof(string)));
        }

        public class TestClass
        {
            public string a;
            public string b;
            public TestClass()
            {
            }
        }

        [WebMethod]
        public string Hello(int param1, int param2)
        {
            try
            {
                return (param1 + param2).ToString();
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        [WebMethod]
        public List<Kullanicilar> SearchUsers()
        {
            try
            {
                var _entities = new EnrollPortalEntities();
                return _entities.Kullanicilar.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [WebMethod]
        public List<Kullanicilar> LoginUser(string username, string password)
        {
            var entities = new EnrollPortalEntities();
            try
            {
                var user =
                    entities.Kullanicilar.Where(
                        p => (p.TCNo == username || p.EPosta == username) && p.Parola == password).ToList();
                if (user.Count > 0) return user;
            }
            catch (Exception)
            {
            }
            return null;
        }

        [WebMethod]
        public List<Duyurular> GetNotices(int itemCount, int pageCount)
        {
            var entities = new EnrollPortalEntities();
            try
            {
                //var duyurular = entities.Duyurular.OrderByDescending(p => p.KaydetmeTarihi).Skip(itemCount * pageCount).Take(itemCount).ToList();
                var duyurular = entities.Duyurular.OrderBy(p => p.Baslik).Skip(itemCount * pageCount).Take(itemCount).ToList();
                if (duyurular.Count > 0)
                    return duyurular;
                else
                    return null;
            }
            catch (Exception)
            {
            }
            return null;
        }

        [WebMethod]
        public List<Haberler> GetNews(int itemCount, int pageCount)
        {
            var entities = new EnrollPortalEntities();
            try
            {
                //var haberler = entities.Haberler.OrderByDescending(p => p.KaydetmeTarihi).Skip(itemCount * pageCount).Take(itemCount).ToList();
                var haberler = entities.Haberler.OrderBy(p => p.Baslik).Skip(itemCount * pageCount).Take(itemCount).ToList();
                if (haberler.Count > 0)
                    return haberler;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
