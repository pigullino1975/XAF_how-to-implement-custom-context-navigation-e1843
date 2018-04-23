Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel

Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation


Namespace ContextNavigation.Module
    Public Class TypeValueConverter
        Inherits ValueConverter
        Public Overrides Function ConvertToStorageType(ByVal value As Object) As Object
            Return value.ToString()
        End Function
        Public Overrides Function ConvertFromStorageType(ByVal value As Object) As Object
            If value Is Nothing Then
                Return Nothing
            Else
                Return Type.GetType(CStr(value))
            End If
        End Function
        Public Overrides ReadOnly Property StorageType() As Type
            Get
                Return GetType(String)
            End Get
        End Property
    End Class

    <DefaultClassOptions, ImageName("BO_Report")> _
    Public Class HelpDocument
        Inherits BaseObject
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <DevExpress.Xpo.ValueConverter(GetType(TypeValueConverter))> _
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
        <Size(-1)> _
        Public Property Text() As String
            Get
                Return GetPropertyValue(Of String)("Text")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Text", value)
            End Set
        End Property
    End Class

    <DefaultClassOptions> _
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

    <DefaultClassOptions> _
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
