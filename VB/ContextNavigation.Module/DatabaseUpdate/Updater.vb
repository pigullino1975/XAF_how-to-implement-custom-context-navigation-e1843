Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating

Namespace ContextNavigation.Module
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()

			Dim sampleDocumentOne As HelpDocument = ObjectSpace.FindObject(Of HelpDocument)(CriteriaOperator.Parse("Title == 'Sample topic one'"))
			If sampleDocumentOne Is Nothing Then
				sampleDocumentOne = ObjectSpace.CreateObject(Of HelpDocument)()
				sampleDocumentOne.Title = "Sample topic one"
				sampleDocumentOne.ObjectType = GetType(Contact)
				sampleDocumentOne.Text = "Sample text"
				sampleDocumentOne.Save()
			End If

			Dim sampleDocumentTwo As HelpDocument = ObjectSpace.FindObject(Of HelpDocument)(CriteriaOperator.Parse("Title == 'Sample topic two'"))
			If sampleDocumentTwo Is Nothing Then
				sampleDocumentTwo = ObjectSpace.CreateObject(Of HelpDocument)()
				sampleDocumentTwo.Title = "Sample topic two"
				sampleDocumentTwo.ObjectType = GetType(Task)
				sampleDocumentTwo.Text = "Sample text"
				sampleDocumentTwo.Save()
			End If

		End Sub
	End Class
End Namespace
