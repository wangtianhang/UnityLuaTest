 #ifndef __NativeCode_H__
    #define __NativeCode_H__

    #if 0
    #define EXPORT_DLL __declspec(dllexport) //����dll����
    #else
    #define EXPORT_DLL 
    #endif

    extern "C" {
        EXPORT_DLL int MyAddFunc(int _a, int _b);
    }

    #endif