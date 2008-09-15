
Partial Class _Default
    Inherits System.Web.UI.Page
    Private Const QuerystringProcedure As String = "Procedure"
    'Private _schemaName As String = "[AdminTool]"
    'Public Property SchemaName() As String
    '    Get
    '        Return _schemaName
    '    End Get
    '    Set(ByVal value As String)
    '        _schemaName = value
    '    End Set
    'End Property
    'Private _procedureName As String
    'Public Property ProcedureName() As String
    '    Get
    '        Return _procedureName
    '    End Get
    '    Set(ByVal value As String)
    '        _procedureName = value
    '    End Set
    'End Property

    Private _procedures As IList(Of ModelLayer.Procedure)
    Public Property Procedures() As IList(Of ModelLayer.Procedure)
        Get
            Return _procedures
        End Get
        Set(ByVal value As IList(Of ModelLayer.Procedure))
            _procedures = value
        End Set
    End Property
   
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim queryString As String = Request.QueryString(QuerystringProcedure)
        If Not String.IsNullOrEmpty(queryString) Then
            Dim lstOfStrings As String() = queryString.Split(".")
            If lstOfStrings.Length > 1 Then
                Me.StoredProcedureForm1.SchemaName = lstOfStrings(0)
                Me.StoredProcedureForm1.ProcedureName = lstOfStrings(1)

            End If
        End If
        PopulateProcedures()
    End Sub
    Private Sub PopulateProcedures()
        Dim procedureAccess As New DataLayer.Procedures
        Procedures = procedureAccess.GetAllProcedures
        lstProcedures.Items.Add(New WebControls.ListItem("Please select", 0))
        BindProceduresToList()
    End Sub
    Private Sub BindProceduresToList()
        For Each procedure As ModelLayer.Procedure In Procedures
            lstProcedures.Items.Add(New WebControls.ListItem(procedure.Name, String.Format("{0}.{1}", procedure.Schema, procedure.Name)))
        Next
    End Sub
    Protected Sub lstProcedures_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstProcedures.SelectedIndexChanged
        Dim ProcValue As String = sender.SelectedValue
        Response.Redirect(String.Format("?{0}={1}", QuerystringProcedure, ProcValue))

        'PopulateParameters(ProcValue)
    End Sub
    
End Class
