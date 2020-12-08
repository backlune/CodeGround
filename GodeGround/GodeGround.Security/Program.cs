using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GodeGround.Security
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "C:\\Users\\emilb\\Downloads\\Faktura 33 Emil Accrual AB.pdf";
            var unsecure_fileName = "C:\\Users\\emilb\\Downloads\\unsecure_Faktura 33 Emil Accrual AB.pdf";

            using (var fs = File.Open(fileName, FileMode.Open))
            using (var output = File.Open(unsecure_fileName, FileMode.OpenOrCreate))
            using (var ms = PdfDocumentScripting.AddAutoPrint(fs))
            {
                ms.Position = 0;
                ms.CopyTo(output);
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
