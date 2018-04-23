Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.NodeWrappers
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model

Namespace ContextNavigation.Module
	Public Class TaskBasedHelpController
		Inherits WindowController
		Public Const ViewId As String = "HelpDocument_ListView_FewColumns"
		Public Const CriteriaKeyName As String = "Criteria"
		Private parentViewClassName As String
		Private showNavigationItemController As ShowNavigationItemController

		Public Sub New()
			Me.TargetWindowType = WindowType.Main
		End Sub

		Protected Overrides Sub OnFrameAssigned()
			UnsubscribeFromEvents()
			MyBase.OnFrameAssigned()
			showNavigationItemController = Frame.GetController(Of ShowNavigationItemController)()
			If showNavigationItemController IsNot Nothing Then
				AddHandler showNavigationItemController.NavigationItemCreated, AddressOf showNavigationItemController_NavigationItemCreated
				AddHandler showNavigationItemController.CustomShowNavigationItem, AddressOf showNavigationItemController_CustomShowNavigationItem
			End If
		End Sub

		Private Sub showNavigationItemController_NavigationItemCreated(ByVal sender As Object, ByVal e As NavigationItemCreatedEventArgs)
			Dim navigationItem As ChoiceActionItem = e.NavigationItem
			Dim viewNode As IModelObjectView = TryCast((CType(e.NavigationItem.Model, IModelNavigationItem)).View, IModelObjectView)
			If viewNode IsNot Nothing Then
				Dim objectTypeInfo As ITypeInfo = XafTypesInfo.Instance.FindTypeInfo(viewNode.ModelClass.Name)
				If objectTypeInfo IsNot Nothing Then
					Dim myObjectSpace As IObjectSpace = Application.CreateObjectSpace()
					Dim helpDocumentsCount As Integer = myObjectSpace.GetObjectsCount(GetType(HelpDocument), CriteriaOperator.Parse("ObjectType == '" & objectTypeInfo.Type.ToString() & "'"))
					If helpDocumentsCount <> 0 Then
						Dim shortcut As New ViewShortcut(GetType(HelpDocument), Nothing, ViewId)
						shortcut(CriteriaKeyName) = viewNode.ModelClass.Name
						Dim DocumentsNavigationGroup As New ChoiceActionItem("CustomDocuments", "Task-Based Help", shortcut)
						navigationItem.Items.Add(DocumentsNavigationGroup)
					End If
				End If
			End If
		End Sub

		Private Sub showNavigationItemController_CustomShowNavigationItem(ByVal sender As Object, ByVal e As CustomShowNavigationItemEventArgs)
			If (e.ActionArguments.SelectedChoiceActionItem IsNot Nothing) AndAlso e.ActionArguments.SelectedChoiceActionItem.Id = "CustomDocuments" Then
				Dim parent As ChoiceActionItem = e.ActionArguments.SelectedChoiceActionItem.ParentItem
				Dim objectSpace As IObjectSpace = Application.CreateObjectSpace()
				Dim collectionSource As New CollectionSource(objectSpace, GetType(HelpDocument))
				Dim parentViewNode As IModelObjectView = TryCast((CType(parent.Model, IModelNavigationItem)).View, IModelObjectView)
				collectionSource.Criteria.Add("CustomDocumentsController", CriteriaOperator.Parse("ObjectType == '" & parentViewNode.ModelClass.Name & "'"))
				Dim view As View = Application.CreateListView("HelpDocument_ListView_FewColumns", collectionSource, True)
				e.ActionArguments.ShowViewParameters.CreatedView = view
				parentViewClassName = parentViewNode.ModelClass.Name
				AddHandler view.CustomizeViewShortcut, AddressOf view_CustomizeViewShortcut
				e.Handled = True
			End If
		End Sub

		Private Sub view_CustomizeViewShortcut(ByVal sender As Object, ByVal e As CustomizeViewShortcutArgs)
			If (Not String.IsNullOrEmpty(parentViewClassName)) Then
				e.ViewShortcut(CriteriaKeyName) = parentViewClassName
			End If
		End Sub

		Private Sub UnsubscribeFromEvents()
			If showNavigationItemController IsNot Nothing Then
				RemoveHandler showNavigationItemController.CustomShowNavigationItem, AddressOf showNavigationItemController_CustomShowNavigationItem
				RemoveHandler showNavigationItemController.NavigationItemCreated, AddressOf showNavigationItemController_NavigationItemCreated
				showNavigationItemController = Nothing
			End If
		End Sub

		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			UnsubscribeFromEvents()
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace
