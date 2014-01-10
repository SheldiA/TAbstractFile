using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAbstractFile
{
    class Program
    {
        static void Main(string[] args)
        {
            TOSFile file = new TOSFile(@"D:\Project c#\TAbstractFile\TAbstractFile\file\1.txt");
            //file.ChangePosition(5);
            TMemoryFile fileM = new TMemoryFile(@"D:\Project c#\TAbstractFile\TAbstractFile\file\2.txt",0);
            /*byte[] b = {4,6,7,8,9 };
            file.Write(b);
            file.ChangePosition(0);*/
            file.CopyTo(fileM,0);
            file.ChangePosition(0);
            fileM.ChangePosition(0);
            byte[] a = file.Read(15);
            byte[] a2 = fileM.Read(15);

        }
    }
}
