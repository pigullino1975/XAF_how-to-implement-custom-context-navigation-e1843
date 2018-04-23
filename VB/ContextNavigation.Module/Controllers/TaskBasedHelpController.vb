Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.SystemModule

Namespace ContextNavigation.Module
	Public Class TaskBasedHelpController
		Inherits WindowController
		Private navigationController As ShowNavigationItemController
		Public Sub New()
			TargetWindowType = WindowType.Main
		End Sub
		Protected Overrides Sub OnFrameAssigned()
			UnsubscribeFromEvents()
			MyBase.OnFrameAssigned()
			navigationController = Frame.GetController(Of ShowNavigationItemController)()
			If navigationController IsNot Nothing Then
				AddHandler navigationController.NavigationItemCreated, AddressOf navigationItemCreated
			End If
		End Sub
		Private Sub navigationItemCreated(ByVal sender As Object, ByVal e As NavigationItemCreatedEventArgs)
			Dim navigationItem As ChoiceActionItem = e.NavigationItem
			Dim viewNode As IModelObjectView = TryCast((CType(e.NavigationItem.Model, IModelNavigationItem)).View, IModelObjectView)
			If viewNode IsNot Nothing Then
				Dim objectTypeInfo As ITypeInfo = XafTypesInfo.Instance.FindTypeInfo(viewNode.ModelClass.Name)
				If objectTypeInfo IsNot Nothing Then
					Dim docCriteria As CriteriaOperator = CriteriaOperator.Parse("ObjectType == ?", objectTypeInfo.Type)
					Dim myObjectSpace As IObjectSpace = Application.CreateObjectSpace()
					Dim docs As IList(Of HelpDocument) = myObjectSpace.GetObjects(Of HelpDocument)(docCriteria)
					If docs.Count > 0 Then
						Dim docsGroup As New ChoiceActionItem("CustomDocuments", "Task-Based Help", Nothing) With {.ImageName = "BO_Report"}
						navigationItem.Items.Add(docsGroup)
						For Each doc As HelpDocument In docs
							Dim shortcut As New ViewShortcut(GetType(HelpDocument), doc.Oid.ToString(), "HelpDocument_DetailView_FewColumns")
							Dim docItem As New ChoiceActionItem(doc.Oid.ToString(), doc.Title, shortcut) With {.ImageName = "Navigation_Item_Report"}
							docsGroup.Items.Add(docItem)
						Next doc
					End If
				End If
			End If
		End Sub
		Private Sub UnsubscribeFromEvents()
			If navigationController IsNot Nothing Then
				RemoveHandler navigationController.NavigationItemCreated, AddressOf navigationItemCreated
				navigationController = Nothing
			End If
		End Sub
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			UnsubscribeFromEvents()
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace