Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp.Win

Namespace ContextNavigation.Win
	Partial Public Class ContextNavigationWindowsFormsApplication
		Inherits WinApplication
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
