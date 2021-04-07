using System;
using System.Collections.Generic;
using System.Text;

namespace WalletConnector.Serializer.Helpers
{
    public class RandomStringCreator
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            string guid = Guid.NewGuid().ToString();
            var result = guid.Replace("-", "").Substring(0, length);
            return result;
        }
    }
}
