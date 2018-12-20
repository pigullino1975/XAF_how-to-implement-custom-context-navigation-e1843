Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
'using DevExpress.ExpressApp.Security;

Namespace ContextNavigation.Web
    ' You can override various virtual methods and handle corresponding events to manage various aspects of your XAF application UI and behavior.
    Partial Public Class ContextNavigationAspNetApplication
        Inherits WebApplication ' http://documentation.devexpress.com/#Xaf/DevExpressExpressAppWebWebApplicationMembersTopicAll
        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
        Private module3 As ContextNavigation.Module.ContextNavigationModule
        Private sqlConnection1 As System.Data.SqlClient.SqlConnection

        Public Sub New()
            InitializeComponent()
        End Sub

        ' Override to execute custom code after a logon has been performed, the SecuritySystem object is initialized, logon parameters have been saved and user model differences are loaded.
        Protected Overrides Sub OnLoggedOn(ByVal args As LogonEventArgs) ' http://documentation.devexpress.com/#Xaf/DevExpressExpressAppXafApplication_LoggedOntopic
            MyBase.OnLoggedOn(args)
        End Sub

        ' Override to execute custom code after a user has logged off.
        Protected Overrides Sub OnLoggedOff() ' http://documentation.devexpress.com/#Xaf/DevExpressExpressAppXafApplication_LoggedOfftopic
            MyBase.OnLoggedOff()
        End Sub

        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection)
        End Sub

        Private Sub ContextNavigationAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
                e.Updater.Update()
                e.Handled = True
        End Sub

        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
            Me.module3 = New ContextNavigation.Module.ContextNavigationModule()
            Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
            DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' sqlConnection1
            ' 
            Me.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.\SQLEXPRESS;Initial Catalog=ContextNavigation"
            Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
            ' 
            ' ContextNavigationAspNetApplication
            ' 
            Me.ApplicationName = "ContextNavigation"
            Me.Connection = Me.sqlConnection1
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module3)

            DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
    End Class
End Namespace
