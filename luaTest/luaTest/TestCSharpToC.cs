using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


class TestCSharpToC
{
    [DllImport("testCharpToC.dll", EntryPoint = "Add", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Add(int a, int b);

    [DllImport("testCharpToC.dll", EntryPoint = "Length", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Length(string str);

    public static void Test()
    {
        int x = Add(1, 2);
        Console.WriteLine(x.ToString());

        int length = Length("test");
        Console.WriteLine(length.ToString());
    }
}

