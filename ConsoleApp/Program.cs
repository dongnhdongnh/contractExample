using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            Uri baseUri = new Uri("http://www.contoso.com");
            Uri myUri = new Uri(baseUri, "catalog/shownew.htm");
            Console.WriteLine(myUri);
            Console.WriteLine();
        }
    }
}
