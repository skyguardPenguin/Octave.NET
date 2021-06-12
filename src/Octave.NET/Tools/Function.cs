using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace Octave.NET.Tools
{
   public class Function
    {
        private string _strFunc="";

        public static OctaveSettings Settings = new OctaveSettings();
        public static string Path;
        
        public OctaveContext octave;
        public string StrFunc { get { return _strFunc; } set { _strFunc = value;} }
        public string GeneratedFuncDerivative;
        public string UserFuncDerivative;

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
            LoadPackage();
            if (UserFuncDerivative == "")
                return Evaluate(GeneratedFuncDerivative, value);
            else
                return Evaluate(UserFuncDerivative,value);
        }

        public string Derivative()
        {
            LoadPackage();
            octave.Execute("equ=inline('" + _strFunc + "','x')");
            octave.Execute("equ=diff(equ(x))");
            GeneratedFuncDerivative = octave.Execute("disp(equ)");
            return GeneratedFuncDerivative;
           
        }
        private double Evaluate(string func,string value)
        {
            LoadPackage();
            octave.Execute("equ=inline('" + func + "','x')");
            return double.Parse(octave.Execute("disp(equ(" + value + "))"));
        }
        public double Evaluate(string value)
        {
            LoadPackage();
            octave.Execute("equ=inline('" + StrFunc + "','x')");
            return double.Parse(octave.Execute("disp(equ(" + value + "))"));
        }
        public void Clear()
        {
            octave.Execute("clear");
        }




    }
}
