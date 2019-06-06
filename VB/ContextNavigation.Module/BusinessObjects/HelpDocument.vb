Imports System
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Utils

Namespace ContextNavigation.Module
	<DefaultClassOptions, ImageName("BO_Report")>
	Public Class HelpDocument
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<DevExpress.Xpo.ValueConverter(GetType(TypeToStringConverter))>
		Public Property ObjectType() As Type
			Get
				Return GetPropertyValue(Of Type)("ObjectType")
			End Get
			Set(ByVal value As Type)
				SetPropertyValue(Of Type)("ObjectType", value)
			End Set
		End Property
		Public Property Title() As String
			Get
				Return GetPropertyValue(Of String)("Title")
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("Title", value)
			End Set
		End Property
		<Size(-1)>
		Public Property Text() As String
			Get
				Return GetPropertyValue(Of String)("Text")
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("Text", value)
			End Set
		End Property
	End Class
End Namespace