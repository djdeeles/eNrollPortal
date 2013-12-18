using System;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kutuphaneler
{
    public class EnrollExceptionManager
    {
        public static void ManageException(Exception Ex, string Sayfa)
        {
            try
            {
                EnrollPortalEntities Veriler = new EnrollPortalEntities();
                Hatalar Hata = new Hatalar();
                Hata.HataMesaji = Ex.Message;
                Hata.Kaynak = Ex.StackTrace;
                Hata.Sayfa = Sayfa;
                Hata.Tarih = DateTime.Now;
                Veriler.AddToHatalar(Hata);
                Veriler.SaveChanges();
            }
            catch (Exception)
            {
                //
            }
        }
    }
}