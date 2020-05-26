using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSLGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SSLGrabber by Maximqs");
            Console.WriteLine("Site:");
            Console.WriteLine("\n");
            string input = Console.ReadLine();



            try
            {
                WebClient client = new WebClient();
                string data = client.DownloadString(input);

                Console.WriteLine("Gathering Info..");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(input);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();

                X509Certificate cert = request.ServicePoint.Certificate;

                X509Certificate2 cert2 = new X509Certificate2(cert);
#pragma warning disable CS0618
                string issuer = cert2.GetIssuerName();
#pragma warning restore CS0618 
                Thread.Sleep(1000);

                Console.Clear();

                string issuerdetailed1 = cert2.GetIssuerName().ToString().Replace("C=", "").Replace("CN=", "").Replace("O=", ""); ; 

                Console.WriteLine("\n");
                Console.WriteLine("***************************************************");
                Console.WriteLine("SSLGrabber Environment Print @ " + DateTime.Now);
                Console.WriteLine("***************************************************");
                Console.WriteLine("");
                Console.WriteLine("Website: " + input);
                Console.WriteLine("");
                Console.WriteLine("SSL Country of Issuer : " + issuerdetailed1);
                Console.WriteLine(".NET Check passed?    : " + cert2.Verify().ToString());
                Console.WriteLine("SSL Serial Number     : " + cert2.SerialNumber.ToString());
                Console.WriteLine("SSL Format            : " + cert2.GetFormat().ToString());
                Console.WriteLine("SSL Thumbprint        : " + cert2.Thumbprint.ToString());
                Console.WriteLine("SSL Started @         : " + cert2.GetEffectiveDateString().ToString());
                Console.WriteLine("SSL Expires @         : " + cert2.GetExpirationDateString().ToString());
                Console.WriteLine("SSL Version           : " + cert2.Version.ToString());
                Console.WriteLine("Exists over Prv. Key? : " + cert2.HasPrivateKey.ToString());
                Console.WriteLine("SSL Key Algorythm     : " + cert2.GetKeyAlgorithm().ToString());
                Console.WriteLine("");
                Console.WriteLine("SSL Public Key        : " + cert2.GetPublicKeyString());
                Console.ReadKey();

            }
            catch
            {
                Console.WriteLine("\nInvalid Server (Is it HTTP?, did you type the full domain?)");
                Thread.Sleep(1500);
                Environment.Exit(550);
            }
        }
    }
}
