﻿using System;
using System.Globalization;
using System.Windows.Forms;

namespace Planetside2LCDStats
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.CurrentCulture = CultureInfo.InvariantCulture;
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}