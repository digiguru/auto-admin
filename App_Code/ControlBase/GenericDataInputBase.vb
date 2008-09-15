Imports Microsoft.VisualBasic
Imports ModelLayer
Public MustInherit Class GenericDataInputBase
    Inherits System.Web.UI.UserControl

    Public MustOverride Sub SetValue()

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetValue()
    End Sub

    Private _parentTemplate As GenericDataTemplateBase
    Public Property ParentTemplate() As GenericDataTemplateBase
        Get
            Return _parentTemplate
        End Get
        Set(ByVal value As GenericDataTemplateBase)
            _parentTemplate = value
        End Set
    End Property
    Private _formBase As IFormBase
    Public Property FormBase() As IFormBase
        Get
            Return _formBase
        End Get
        Set(ByVal value As IFormBase)
            _formBase = value
        End Set
    End Property

End Class
