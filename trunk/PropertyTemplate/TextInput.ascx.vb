
Partial Class PropertyTemplate_DateInput
    Inherits GenericDataInputBase

    Public ReadOnly Property ParameterDefault() As String
        Get
            Return 0
        End Get
    End Property
    Public ReadOnly Property TextValue() As String
        Get
            Return Me.txtValue.Text
        End Get
    End Property
    Public Overrides Sub SetValue()
        ParentTemplate.SetValue(TextValue)
    End Sub


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Page.IsPostBack Then
            Me.txtValue.Text = ParameterDefault
            Me.txtValue.DataBind()
        End If
    End Sub
End Class
