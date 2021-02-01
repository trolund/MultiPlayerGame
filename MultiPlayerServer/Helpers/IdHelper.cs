using System;
using HashidsNet;

namespace MultiPlayerServer.Helpers
{
    public class IdHelper
    {
        private static Hashids hashids;

        public IdHelper()
        {
            hashids = new Hashids("MultipalyerGame12345", 3);
        }

        public static string GenerateShortId(int id)
        {
            return hashids.Encode(id);
        }

        public static int GetId(string shortId)
        {
            return hashids.Decode(shortId)[0];
        }
    }
}
