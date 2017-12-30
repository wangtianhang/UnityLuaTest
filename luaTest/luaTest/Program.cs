using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

class Program
{

    static void Main(string[] args)
    {
        LuaState.Test();

        Console.WriteLine("回车键退出");
        Console.ReadLine();
    }
}

