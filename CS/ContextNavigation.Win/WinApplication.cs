using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp;

namespace ContextNavigation.Win {
    public partial class ContextNavigationWindowsFormsApplication : WinApplication {
        public ContextNavigationWindowsFormsApplication() {
            InitializeComponent();
        }

        private void ContextNavigationWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();
            e.Handled = true;
        }
    }
}
