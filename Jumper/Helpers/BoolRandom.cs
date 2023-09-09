using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Jumper.Helpers
{
    public static class BoolRandom
    {
        private static Random random = new Random();

        public static bool GetRandomBool()
        {
            if (random.Next(2) == 1)
                return true;
            return false;
        }
    }
}
