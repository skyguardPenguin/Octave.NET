using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace Octave.NET.Tools
{
   public class Function
    {

        public static OctaveSettings Settings = new OctaveSettings();
        public static string Path;
        
        public OctaveContext octave;
        public string StrFunc { get; set; }
        string FuncDerivative;

        bool packageLoaded;

        Thread LoadPackageProcess;
        public Function(string strFunc)
        {
            StrFunc = strFunc;
            Settings.OctaveCliPath = Path;
            OctaveContext.OctaveSettings = Settings;
            octave = new OctaveContext();
            LoadPackageProcess = new Thread(LoadPackage);
            LoadPackageProcess.Start();

        }

        private void LoadPackage()
        {
            octave.Execute("pkg load symbolic;");
            octave.Execute("syms x");
        }
        public bool PackageLoaded()
        {
            if (packageLoaded == true)
                return true;
            packageLoaded = !LoadPackageProcess.IsAlive; 
            return packageLoaded;
        }

        public Function()
        {
            Settings.OctaveCliPath = Path;
            OctaveContext.OctaveSettings = Settings;
            octave = new OctaveContext();
            LoadPackageProcess = new Thread(LoadPackage);
            LoadPackageProcess.Start();

        }
        public double Derivative(string value)
        {
            octave.Execute("equ=inline('" + StrFunc + "','x')");
            octave.Execute("equ=diff(equ(x))");
            FuncDerivative = octave.Execute("disp(equ)");
            return Evaluate(FuncDerivative, value);
        }

        private double Evaluate(string func,string value)
        {
            octave.Execute("equ=inline('" + func + "','x')");
            return double.Parse(octave.Execute("disp(equ(" + value + "))"));
        }
        public double Evaluate(string value)
        {
            octave.Execute("equ=inline('" + StrFunc + "','x')");
            return double.Parse(octave.Execute("disp(equ(" + value + "))"));
        }





    }
}
