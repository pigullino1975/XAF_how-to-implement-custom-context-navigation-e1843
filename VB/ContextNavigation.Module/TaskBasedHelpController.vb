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
                AddHandler showNavigationItemController.NavigationItemCreated, _
                AddressOf showNavigationItemController_NavigationItemCreated
                AddHandler showNavigationItemController.CustomShowNavigationItem, _
                AddressOf showNavigationItemController_CustomShowNavigationItem
			End If
		End Sub

        Private Sub showNavigationItemController_NavigationItemCreated( _
        ByVal sender As Object, ByVal e As NavigationItemCreatedEventArgs)
            Dim navigationItem As ChoiceActionItem = e.NavigationItem
            Dim viewId As String = e.NavigationItem.Info.GetAttributeValue( _
            showNavigationItemController.ViewIdAttributeName)
            If (Not String.IsNullOrEmpty(viewId)) Then
                Dim viewInfoNode As DictionaryNode = Application.FindViewInfo(viewId)
                If viewInfoNode IsNot Nothing Then
                    Dim viewInfo As BaseViewInfoNodeWrapper = _
                    ViewsNodeWrapper.CreateWrapper(viewInfoNode)
                    Dim objectTypeInfo As ITypeInfo = _
                    XafTypesInfo.Instance.FindTypeInfo(viewInfo.ClassName)
                    If objectTypeInfo IsNot Nothing Then
                        Dim myObjectSpace As ObjectSpace = Application.CreateObjectSpace()
                        Dim helpDocumentsCount As Integer = myObjectSpace.GetObjectsCount( _
                        GetType(HelpDocument), CriteriaOperator.Parse("ObjectType == '" & _
                        objectTypeInfo.Type.ToString() & "'"))
                        If helpDocumentsCount <> 0 Then
                            Dim shortcut As New ViewShortcut(GetType(HelpDocument), _
                            Nothing, TaskBasedHelpController.ViewId)
                            shortcut(CriteriaKeyName) = viewInfo.ClassName
                            Dim DocumentsNavigationGroup As New ChoiceActionItem( _
                            "CustomDocuments", "Task-Based Help", shortcut)
                            DocumentsNavigationGroup.Info.SetAttribute( _
                            showNavigationItemController.GroupImageAttributeName, "BO_Report")
                            navigationItem.Items.Add(DocumentsNavigationGroup)
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub showNavigationItemController_CustomShowNavigationItem( _
        ByVal sender As Object, ByVal e As CustomShowNavigationItemEventArgs)
            If (e.ActionArguments.SelectedChoiceActionItem IsNot Nothing) AndAlso _
            e.ActionArguments.SelectedChoiceActionItem.Id = "CustomDocuments" Then
                Dim parent As ChoiceActionItem = _
                e.ActionArguments.SelectedChoiceActionItem.ParentItem
                Dim objectSpace As ObjectSpace = Application.CreateObjectSpace()
                Dim collectionSource As New CollectionSource(objectSpace, GetType(HelpDocument))
                Dim parentViewInfo As DictionaryNode = Application.GetViewInfo( _
                parent.Info.GetAttribute("ViewID").Value)
                collectionSource.Criteria.Add("CustomDocumentsController", _
                CriteriaOperator.Parse("ObjectType == '" & _
                parentViewInfo.GetAttribute("ClassName").Value & "'"))
                Dim view As View = Application.CreateListView( _
                "HelpDocument_ListView_FewColumns", collectionSource, True)
                e.ActionArguments.ShowViewParameters.CreatedView = view
                parentViewClassName = parentViewInfo.GetAttribute("ClassName").Value
                AddHandler view.CustomizeViewShortcut, AddressOf view_CustomizeViewShortcut
                e.Handled = True
            End If
        End Sub

        Private Sub view_CustomizeViewShortcut(ByVal sender As Object, _
        ByVal e As CustomizeViewShortcutArgs)
            If (Not String.IsNullOrEmpty(parentViewClassName)) Then
                e.ViewShortcut(CriteriaKeyName) = parentViewClassName
            End If
        End Sub

		Private Sub UnsubscribeFromEvents()
			If showNavigationItemController IsNot Nothing Then
                RemoveHandler showNavigationItemController.CustomShowNavigationItem, _
                AddressOf showNavigationItemController_CustomShowNavigationItem
                RemoveHandler showNavigationItemController.NavigationItemCreated, _
                AddressOf showNavigationItemController_NavigationItemCreated
				showNavigationItemController = Nothing
			End If
		End Sub

		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			UnsubscribeFromEvents()
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace
