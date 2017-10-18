using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORepo
{
    public static class RetryOperation
    {
        public static void Retry(int times, Action operation)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    operation();
                    break;
                }
                catch (ConnectionException)
                {
                    if (attempts == times)
                    {
                        throw;
                    }
                }
            } while (true);
        }
    }

}
