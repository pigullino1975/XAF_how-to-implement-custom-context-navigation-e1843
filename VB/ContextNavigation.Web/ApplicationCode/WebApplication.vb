Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web

Namespace ContextNavigation.Web
    Partial Public Class ContextNavigationAspNetApplication
        Inherits WebApplication
        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
        Private module3 As ContextNavigation.Module.ContextNavigationModule
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
        Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
        Private htmlPropertyEditorAspNetModule1 As DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule
        Private module5 As DevExpress.ExpressApp.Validation.ValidationModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ContextNavigationAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
            e.Updater.Update()
            e.Handled = True
        End Sub

        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
            Me.module3 = New ContextNavigation.Module.ContextNavigationModule()
            Me.module5 = New DevExpress.ExpressApp.Validation.ValidationModule()
            Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.htmlPropertyEditorAspNetModule1 = New DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' module1
            ' 
            Me.module1.AdditionalBusinessClasses.Add(GetType(DevExpress.Xpo.XPObjectType))
            ' 
            ' module5
            ' 
            Me.module5.AllowValidationDetailsAccess = True
            ' 
            ' htmlPropertyEditorAspNetModule1
            ' 
            Me.htmlPropertyEditorAspNetModule1.Description = "Contains the Property Editor which allows end-users to format string properties' " & "values using Hyper Text Markup Language (HTML)."
            Me.htmlPropertyEditorAspNetModule1.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.SystemModule.SystemModule))
            ' 
            ' ContextNavigationAspNetApplication
            ' 
            Me.ApplicationName = "ContextNavigation"
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module6)
            Me.Modules.Add(Me.module3)
            Me.Modules.Add(Me.module5)
            Me.Modules.Add(Me.securityModule1)
            Me.Modules.Add(Me.htmlPropertyEditorAspNetModule1)
'            Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.ContextNavigationAspNetApplication_DatabaseVersionMismatch);
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
    End Class
End Namespace
