using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp.Security;
using WinWebSolution.Module;

namespace ContextNavigation.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            ContextNavigationWindowsFormsApplication winApplication = new ContextNavigationWindowsFormsApplication();
            winApplication.ConnectionString = CCProvider.ConnectionString;
            try {
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e) {
                winApplication.HandleException(e);
            }
        }
    }
}
