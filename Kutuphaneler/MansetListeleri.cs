using System;

namespace EnrollPortal.Kutuphaneler
{
    // Bazı class'lar aynı olsa da birbiririnden bağımsız olabileceği düşünülmüştür.

    public class HaberlerVeDuyurularList
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; }
        public string Resim { get; set; }
        public string ResimThumbnail { get; set; }
        public string ResimOriginal { get; set; }
        public string Url { get; set; }
    }

    public class IlanlarVeDuyurularList
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; }
        public string Ekleyen { get; set; }
        public string Resim { get; set; }
        public string ResimThumbnail { get; set; }
        public string ResimOriginal { get; set; }
        public string Url { get; set; }
    }

    public class EtkinliklerList
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public DateTime Tarih { get; set; }
        public string Resim { get; set; }
        public string ResimThumbnail { get; set; }
        public string ResimOriginal { get; set; }
        public string Url { get; set; }
    }

    public class EgitimlerList
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int KatilimciSayisi { get; set; }
        public string KayitBilgileri { get; set; }
    }

    public class ForumKategorilerList
    {
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
        public string Aciklama { get; set; }
        public int KonuSayisi { get; set; }
        public int MesajSayisi { get; set; }
        public string SonMesaj { get; set; }
    }

    public class ForumKonularList
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Baslatan { get; set; }
        public int CevapSayisi { get; set; }
        public int OkunmaSayisi { get; set; }
        public string SonMesaj { get; set; }
    }

    public class ForumMesajlarList
    {
        public int Id { get; set; }
        public string Ekleyen { get; set; }
        public string Resim { get; set; }
        public DateTime Tarih { get; set; }
        public string Mesaj { get; set; }
        public int KaydedenKullaniciId { get; set; }
        public string Duzenleme { get; set; }
    }

    public class ForumAnaSayfaMesajlarList
    {
        public int KonuId { get; set; }
        public string KonuBaslik { get; set; }
        public string Link { get; set; }
        public DateTime Tarih { get; set; }
        public string KaydedenKullanici { get; set; }
    }

    public class PortalRss
    {
        public string category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string link { get; set; }
        public string icon { get; set; }
    }

    public class PortalRssList
    {
        public string Liste { get; set; }
    }
}