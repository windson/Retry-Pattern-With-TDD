using BORepo.Models;
using System;
using System.Runtime.InteropServices;

namespace BORepo
{
    public interface IBOService : IDisposable
    {
        void Save(BO bo);
    }

    public class BOService : IBOService
    {
        private SafeHandle resource;
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (resource != null)
                    {
                        resource.Dispose();
                    }
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }


        public void Save(BO bo)
        {
            try
            {
                //Save operation
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
