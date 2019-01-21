using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Data.Helper
{
    public static class DatabaseHelper
    {
        public static int GenerateUniqueId() {
            Random rnd = new Random();
            int UniqueId = rnd.Next(1000, 1000000);
            return UniqueId;
        }
    }
}
