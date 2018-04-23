using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Win;

namespace ContextNavigation.Win {
    public partial class ContextNavigationWindowsFormsApplication : WinApplication {
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection);
        }
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
