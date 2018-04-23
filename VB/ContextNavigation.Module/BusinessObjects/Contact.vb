Imports System
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace ContextNavigation.Module
    <DefaultClassOptions, ImageName("BO_Contact")> _
    Public Class Contact
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property Name() As String
            Get
                Return GetPropertyValue(Of String)("Name")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", value)
            End Set
        End Property
    End Class
End Namespace
