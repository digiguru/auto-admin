
Partial Class PropertyTemplate_GenericDataTemplate
    Inherits GenericDataTemplateBase

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.divValue.Controls.Add(ValueControl)
        Me.divValue.DataBind()
    End Sub
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Page.IsPostBack Then
            'ValueControl.Value = ParameterDefault
            ValueControl.DataBind()
        End If
        '<%=ParameterName%>
        ShowHideNullField()
    End Sub
    Public Overrides Sub SetValue(ByVal value As Object)
        If NullChecked Then
            Me.Value = Nothing
        Else
            Me.Value = value
        End If

    End Sub
    Private Sub ShowHideNullField()
        divNullable.Visible = Nullable
        divNotNullable.Visible = Not Nullable
      End Sub
    Private Sub ShowHideInputField()
        divInput.Visible = Not NullChecked
        divNullable.Visible = True
    End Sub
    Protected Sub chkIsNull_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsNull.CheckedChanged
        ShowHideInputField()
    End Sub
    Public ReadOnly Property NullChecked() As Boolean
        Get
            Return Me.chkIsNull.Checked
        End Get
    End Property


End Class
