
Partial Class PropertyTemplate_BooleanInput
    Inherits GenericDataInputBase

    Public ReadOnly Property ParameterDefault() As String
        Get
            Return 0
        End Get
    End Property
    Public ReadOnly Property BooleanValue() As Boolean
        Get
            Return Me.chkValue.Checked
        End Get
    End Property
    Public Overrides Sub SetValue()
        ParentTemplate.SetValue(BooleanValue)
    End Sub


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Page.IsPostBack Then
            Me.chkValue.Checked = ParameterDefault
            Me.chkValue.DataBind()
        End If
    End Sub
End Class
