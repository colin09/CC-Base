using System;
using C.B.Spider.sites;

namespace C.B.Spider {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello, Start...");

            var site1 = new cnblogs ();
            site1.Start ();
            Console.WriteLine ("Bye, Done!");
        }
    }
}