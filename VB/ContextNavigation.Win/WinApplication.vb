Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp.Win

Namespace ContextNavigation.Win
	Partial Public Class ContextNavigationWindowsFormsApplication
		Inherits WinApplication
        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection)
        End Sub
		Public Sub New()
			InitializeComponent()
			DelayedViewItemsInitialization = True
		End Sub
		Private Sub ContextNavigationWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
			e.Updater.Update()
			e.Handled = True
		End Sub
	End Class
End Namespace
