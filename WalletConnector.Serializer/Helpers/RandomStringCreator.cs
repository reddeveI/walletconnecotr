using System;
using System.Collections.Generic;
using System.Text;

namespace WalletConnector.Serializer.Helpers
{
    public class RandomStringCreator
    {
        private static Random random = new Random();
        
        public static string RandomString(int length) => Guid.NewGuid().ToString("N").Substring(0, length);
    }
}
