using BORepo.Models;
using System;
using System.Runtime.InteropServices;

namespace BORepo
{
    

    /*
     * Navule Pavan Kumar Rao
     * You have IBOService that performs CRUD operations for some business object. 
     * Task is to create the Repository for this business object and single method that calls the Save method of IBOService and 
     * retries the Save 3 times in case of ConnectionException.
     * 
     */
    public class BORepository
    {
        const int Retry_count= 3;//Can also be read from config
        private readonly IBOService _ser;

        public BORepository(IBOService ser)
        {
            _ser = ser;
        }

        //add IBOService as dependency
        //TDD
        // Various Scenrios 
        //Retry mechanism to be covered in test scenario
        public void Save(BO bo)
        {
            try
            {
                RetryOperation.Retry(Retry_count, () => {
                    _ser.Save(bo);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
