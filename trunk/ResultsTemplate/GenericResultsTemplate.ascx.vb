
Partial Class ResultsTemplate_GenericResultsTemplate
    Inherits GenericResultsTemplateBase
    'Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    '    gridResults.DataBind()
    'End Sub
    Public Overrides Sub SetValue(ByVal value As Data.DataSet)
        'gridResults.DataSource = value
        For Each Table As System.Data.DataTable In value.Tables
            Dim dataGrid As New Web.UI.WebControls.GridView
            dataGrid.DataSource = Table
            Dim name As New HtmlGenericControl("h4")
            name.InnerText = Table.TableName
            divResults.Controls.Add(name)
            divResults.Controls.Add(dataGrid)

        Next
        divResults.DataBind()
    End Sub
  
  
End Class
