#region License

/**********************************************************************
 * LogiLCD.net - Copyright (C) 2013 by Daniel Drywa (daniel@drywa.me)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ***********************************************************************
 * Logitech Gaming Lcd SDK
 * Copyright (C) 2013 Logitech Inc. All Rights Reserved
 * 
 * Logitech Gaming LCD SDK is either a registered trademark or trademark of Logitech in the United States and/or other countries. 
 * All other trademarks are the property of their respective owners.
 * 
 **********************************************************************/

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace dd.logilcd
{
    internal static class NativeMethods
    {
        // Logitech Main Methods
        [DllImport("LogitechLcd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool LogiLcdInit([MarshalAs(UnmanagedType.LPWStr)] string friendlyName, int lcdType);

        [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool LogiLcdIsConnected(int lcdType);

        [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool LogiLcdIsButtonPressed(int button);

        [DllImport("LogitechLcd.dll")]
        public static extern void LogiLcdUpdate();

        [DllImport("LogitechLcd.dll")]
        public static extern void LogiLcdShutdown();

        // Monochrome LCD functions
        [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdMonoSetBackground(IntPtr monoBitmap);

        [DllImport("LogitechLcd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool LogiLcdMonoSetText(int lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text);

        // Color LCD functions
        [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdColorSetBackground(IntPtr colorBitmap);

        [DllImport("LogitechLcd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool LogiLcdColorSetTitle([MarshalAs(UnmanagedType.LPWStr)] string text, int red, int green,
            int blue);

        [DllImport("LogitechLcd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool LogiLcdColorSetText(int lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text,
            int red, int green, int blue);

        //UDK functions, use this only if working with UDK
        //[ DllImport( "LogitechLcd.dll" ) ]
        //static extern int LogiLcdColorSetBackgroundUDK(BYTE partialBitmap[], int arraySize);

        //[ DllImport( "LogitechLcd.dll" ) ]
        //static extern int LogiLcdColorResetBackgroundUDK();

        //[ DllImport( "LogitechLcd.dll" ) ]
        //static extern int LogiLcdMonoSetBackgroundUDK(BYTE partialBitmap[], int arraySize);

        //[ DllImport( "LogitechLcd.dll" ) ]
        //static extern int LogiLcdMonoResetBackgroundUDK();
    } // NativeMethods class
}