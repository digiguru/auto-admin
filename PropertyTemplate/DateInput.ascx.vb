
Partial Class PropertyTemplate_DateInput
    Inherits GenericDataInputBase

    Public ReadOnly Property ParameterDefault() As String
        Get
            Return Date.Now
        End Get
    End Property
    Public ReadOnly Property DateValue() As Date
        Get
            Return Me.datSelector.SelectedDate
        End Get
    End Property
    Public Overrides Sub SetValue()
        ParentTemplate.Value = DateValue
    End Sub
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Page.IsPostBack Then
            Me.datSelector.SelectedDate = ParameterDefault
            Me.datSelector.DataBind()
        End If
    End Sub
  
    
End Class
