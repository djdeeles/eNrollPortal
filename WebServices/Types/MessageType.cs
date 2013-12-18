using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollPortal.WebServices
{
    public class MessageType
    {
        public string Id;
        public string Title;
        public string Content;
        public int ToId;
        public string ToMail;
        public string ToAd;
        public string ToSoyad;
        public int FromId;
        public string FromMail;
        public string FromAd;
        public string FromSoyad;
        public string Date;
        public bool OkunduMu;
        public bool AliciSildiMi;
        public bool GonderenSildiMi;
        public int InboxUnreadCount;
        public int InboxCount;
        public int OutboxCount;
        public int Success;


    }
}