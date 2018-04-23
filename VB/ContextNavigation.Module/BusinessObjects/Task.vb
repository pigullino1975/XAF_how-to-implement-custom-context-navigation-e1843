Imports System
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace ContextNavigation.Module
    <DefaultClassOptions, ImageName("BO_Task")> _
    Public Class Task
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property Description() As String
            Get
                Return GetPropertyValue(Of String)("Description")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Description", value)
            End Set
        End Property
    End Class
End Namespace
