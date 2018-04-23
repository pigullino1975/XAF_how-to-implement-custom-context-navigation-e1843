Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl

Namespace ContextNavigation.Module
    Public Class Updater
        Inherits ModuleUpdater
        Public Sub New(ByVal session As Session, ByVal currentDBVersion As Version)
            MyBase.New(session, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()

            Dim sampleDocumentOne As HelpDocument = Session.FindObject(Of HelpDocument)(CriteriaOperator.Parse("Title == 'Sample topic one'"))
            If sampleDocumentOne Is Nothing Then
                sampleDocumentOne = New HelpDocument(Session)
                sampleDocumentOne.Title = "Sample topic one"
                sampleDocumentOne.ObjectType = GetType(Contact)
                sampleDocumentOne.Text = "Sample text"
                sampleDocumentOne.Save()
            End If

            Dim sampleDocumentTwo As HelpDocument = Session.FindObject(Of HelpDocument)(CriteriaOperator.Parse("Title == 'Sample topic two'"))
            If sampleDocumentTwo Is Nothing Then
                sampleDocumentTwo = New HelpDocument(Session)
                sampleDocumentTwo.Title = "Sample topic two"
                sampleDocumentTwo.ObjectType = GetType(Task)
                sampleDocumentTwo.Text = "Sample text"
                sampleDocumentTwo.Save()
            End If

        End Sub
    End Class
End Namespace
