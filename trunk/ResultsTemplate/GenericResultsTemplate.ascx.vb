
Partial Class ResultsTemplate_GenericResultsTemplate
    Inherits GenericResultsTemplateBase

  
   Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        gridResults.DataBind()
    End Sub
    Public Overrides Sub SetValue(ByVal value As Data.DataTable)
        Me.Value = value
        gridResults.DataSource = value

    End Sub
End Class
