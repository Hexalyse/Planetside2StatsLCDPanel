LogiLCD.net
========================================
Version: 0.1
Webpage: http://www.drywa.me
Platforms: Windows Vista SP2 ( x86 and x64 ), Windows 7 SP1 ( x86 and x64 ), Windows 8 ( x86 and x64 )
Environment: C# with .NET 4.5 under Visual Studio 2012 ( Update 2 )
Status: Init, Shutdown and Button checking is working. The rest will follow soon. x64 is not working right now.

Release Notes:
----------------------------------------
LogiLCD.net is a .NET wrapper for the Logitech Gaming LCD SDK V1.01. It is written in C# with .NET 4.5 and supports the x86 and x64 version of the native library. It behaves exaclty the same as the native SDK except that the defines have been packed in simple enumerations and all methods have been packed into one class named LogiLcd. Just reference the LogiLcd Assembly and you are good to go. The main namespace to use is dd.logilcd. For an example to see how to use the Library simply take a look into the sample Application.

Tested Devices:
----------------------------------------
G19 ( TESTED )
G510 ( UNTESTED )
G13 ( UNTESTED )
G15 v1 ( UNTESTED )
G15 v2 ( UNTESTED )

If you have tested the wrapper with one of the untested devices please e-mail me your results.

Clarification:
----------------------------------------
I am in no way affiliated with Logitech. I am just an indie developer that likes the Logitech Gaming Lcd SDK and I wanted to make it useable under C#.

License:
----------------------------------------
The MIT License

LogiLCD.net - Copyright (C) 2013 by Daniel Drywa (daniel@drywa.me)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Other Licenses:
----------------------------------------
Logitech Gaming Lcd SDK
Copyright (C) 2013 Logitech Inc. All Rights Reserved

Logitech Gaming LCD SDK is either a registered trademark or trademark of Logitech in the United States and/or other countries. All other trademarks are the property of their respective owners.