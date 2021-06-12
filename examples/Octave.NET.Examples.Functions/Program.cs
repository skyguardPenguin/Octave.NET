using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octave.NET;
using Octave.NET.Tools;
namespace Octave.NET.Examples.Functions
{
    class Program
    {
        static void Main(string[] args)
        {
            //Replace with your octave.bat path:            
            Function.Path = @"C:\Users\sinoa\AppData\Local\Programs\GNU Octave\Octave-6.2.0\mingw64\bin\octave.bat";
           
            Function Func = new Function();


            Console.WriteLine("\t===Evaluate Function===");

            Console.Write("Insert a function: ");
            string func = Console.ReadLine();
            Func.StrFunc = func;

            Console.Write("Insert a value: ");
            string value = Console.ReadLine();
            Console.WriteLine("Please wait...");
            while (!Func.PackageLoaded()) { }
            Console.WriteLine("The value is: "+ Func.Evaluate(value));

            Console.WriteLine("\t===Diff===");
           
            
            Console.WriteLine("The derivative is: "+Func.Derivative(value));
            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }
    }
}
