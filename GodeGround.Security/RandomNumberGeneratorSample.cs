using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GodeGround.Security
{
   class RandomNumberGeneratorSample
   {

      public static string GeneratorNumberToString()
      {
         using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
         {
            byte[] tokenData = new byte[32];
            rng.GetBytes(tokenData);

            string token = Convert.ToBase64String(tokenData);
            //Console.WriteLine(token);
            //Console.WriteLine(token.Length);
            return token;
         }
      }
   }
}
