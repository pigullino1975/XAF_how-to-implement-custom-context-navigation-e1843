using DevExpress.ExpressApp.Win;

namespace ContextNavigation.Win {
    public partial class ContextNavigationWindowsFormsApplication : WinApplication {
        public ContextNavigationWindowsFormsApplication() {
            InitializeComponent();
            DelayedViewItemsInitialization = true;
        }
        private void ContextNavigationWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();
            e.Handled = true;
        }
    }
}
