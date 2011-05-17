
Partial Class _Default
    Inherits System.Web.UI.Page
    Private Const QuerystringProcedure As String = "Procedure"
    Private Const QuerystringSchema As String = "Schema"

    Public Enum ConnectionString
        Not_selected = 0
        Development = 1
        Production = 2
    End Enum
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
    
    'Private _procedures As IList(Of ModelLayer.Procedure)
    'Public Property Procedures() As IList(Of ModelLayer.Procedure)
    '    Get
    '        Return _procedures
    '    End Get
    '    Set(ByVal value As IList(Of ModelLayer.Procedure))
    '        _procedures = value
    '    End Set
    'End Property
    Private _selectedSchema As String
    Public Property SelectedSchema() As String
        Get
            Return _selectedSchema
        End Get
        Set(ByVal value As String)
            _selectedSchema = value
        End Set
    End Property
    Private _selectedProcedure As String
    Public Property SelectedProcedure() As String
        Get
            Return _selectedProcedure
        End Get
        Set(ByVal value As String)
            _selectedProcedure = value
        End Set
    End Property
    Public ReadOnly Property SelectedFullyQualifiedProcedureName() As String
        Get
            Return String.Format("{0}.{1}", SelectedSchema, SelectedProcedure)
        End Get
    End Property
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim FullyQualifiedProc As String = Request.QueryString(QuerystringProcedure)
        SelectedSchema = Request.QueryString(QuerystringSchema)
        If Not String.IsNullOrEmpty(FullyQualifiedProc) Then
            Dim lstOfStrings As String() = FullyQualifiedProc.Split(".")
            If lstOfStrings.Length > 1 Then
                Me.StoredProcedureForm1.SchemaName = lstOfStrings(0)
                Me.StoredProcedureForm1.ProcedureName = lstOfStrings(1)
                SelectedSchema = Me.StoredProcedureForm1.SchemaName
                SelectedProcedure = Me.StoredProcedureForm1.ProcedureName
            End If
        End If
        If Not Page.IsPostBack Then
            PopulateDatabases()
            If SetDefaultDatabase() Then
                PopulateSchemas()
                SetDefaultSchema()
                PopulateProcedures(SelectedSchema)
                SetDefaultProcedure()
            Else
                ClearSchemas()
                ClearProcedures()
            End If
        End If

    End Sub
    Private Sub ClearSchemas()
        lstDatabase.Items.Clear()
    End Sub
    Private Sub ClearProcedures()
        lstDatabase.Items.Clear()
    End Sub
    Private Sub PopulateDatabases()
        'Dim databaseConnections As Generic.List(Of String)
        'databaseConnections.Add("Development")
        'databaseConnections.Add("Production")
        lstDatabase.Items.Add(New WebControls.ListItem("Please select", ConnectionString.Not_selected))
        lstDatabase.Items.Add(New WebControls.ListItem("Development", ConnectionString.Development))
        lstDatabase.Items.Add(New WebControls.ListItem("Production", ConnectionString.Production))

    End Sub
    Private Sub PopulateSchemas()
        Dim procedureAccess As New DataLayer.Procedures
        Dim schemas As New Generic.List(Of ModelLayer.Schema)
        schemas = procedureAccess.GetAllSchemas
        lstSchemas.Items.Add(New WebControls.ListItem("Please select", 0))
        BindSchemasToList(schemas)

    End Sub
    Private Sub PopulateProcedures(Optional ByVal Schema As String = "")
        Dim procedureAccess As New DataLayer.Procedures
        Dim Procedures As New Generic.List(Of ModelLayer.Procedure)
        If String.IsNullOrEmpty(Schema) Then
            Procedures = procedureAccess.GetAllProcedures
        Else
            Procedures = procedureAccess.GetAllProcedures(Schema)
        End If
        
        lstProcedures.Items.Add(New WebControls.ListItem("Please select", 0))
        BindProceduresToList(Procedures)

    End Sub
    Private Sub BindSchemasToList(ByVal Schemas As Generic.List(Of ModelLayer.Schema))
        For Each schema As ModelLayer.Schema In Schemas
            lstSchemas.Items.Add(New WebControls.ListItem(schema.Name, schema.Name))
        Next
    End Sub
    Private Sub BindProceduresToList(ByVal Procedures As Generic.List(Of ModelLayer.Procedure))
        For Each procedure As ModelLayer.Procedure In Procedures
            lstProcedures.Items.Add(New WebControls.ListItem(procedure.Name, String.Format("{0}.{1}", procedure.Schema, procedure.Name)))
        Next
    End Sub

    Private Function GetProcedureQueryString() As String
        Dim ProcValue As String = lstProcedures.SelectedValue
        Return String.Format("{0}={1}", QuerystringProcedure, ProcValue)
    End Function

    Protected Sub lstProcedures_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstProcedures.SelectedIndexChanged
        Response.Redirect(String.Concat("?", GetProcedureQueryString, "&", GetSchemaQueryString))
        'PopulateParameters(ProcValue)
    End Sub
   
    Protected Sub lstDatabase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDatabase.SelectedIndexChanged

        Dim database As ConnectionString = lstDatabase.SelectedValue
        SelectDatabseConnection(database)
       
    End Sub
    Private Function GetSchemaQueryString() As String
        If Not lstSchemas.SelectedValue = "0" Then
            SelectedSchema = lstSchemas.SelectedValue
        End If
        Return String.Format("{0}={1}", QuerystringSchema, SelectedSchema)
    End Function

    Protected Sub lstSchemas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSchemas.SelectedIndexChanged
        If Not lstSchemas.SelectedValue = "0" Then
            SelectedSchema = lstSchemas.SelectedValue
        End If
        Response.Redirect(String.Concat("?", GetSchemaQueryString))
        'PopulateProcedures(SelectedSchema)
    End Sub
    Private Function SetDefaultDatabase() As Boolean
        Dim isValid As Boolean = True
        Dim connString As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("ReportConStr").ConnectionString
        If connString = System.Web.Configuration.WebConfigurationManager.AppSettings("ConnectionString.Development") Then
            lstDatabase.SelectedValue = ConnectionString.Development
        ElseIf connString = System.Web.Configuration.WebConfigurationManager.AppSettings("ConnectionString.Production") Then
            lstDatabase.SelectedValue = ConnectionString.Production
        Else
            isValid = False
            lstDatabase.SelectedValue = ConnectionString.Not_selected
        End If
        Return isValid
    End Function
    Private Function SetDefaultSchema() As Boolean
        Dim isValid As Boolean = False
        If Not String.IsNullOrEmpty(SelectedSchema) Then
            lstSchemas.SelectedValue = SelectedSchema
            isValid = True
        End If
        Return isValid
    End Function
    Private Function SetDefaultProcedure() As Boolean
        Dim isValid As Boolean = False
        If Not String.IsNullOrEmpty(SelectedFullyQualifiedProcedureName) Then
            lstProcedures.SelectedValue = SelectedFullyQualifiedProcedureName
            isValid = True
        End If
        Return isValid
    End Function
    Private Sub SelectDatabseConnection(ByVal database As ConnectionString)

        Dim myConfiguration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
       
        Select Case database
            Case ConnectionString.Development
                myConfiguration.ConnectionStrings.ConnectionStrings.Item("ReportConStr").ConnectionString = myConfiguration.AppSettings.Settings.Item("ConnectionString.Development").Value
                PopulateProcedures()
            Case ConnectionString.Production
                myConfiguration.ConnectionStrings.ConnectionStrings.Item("ReportConStr").ConnectionString = myConfiguration.AppSettings.Settings.Item("ConnectionString.Production").Value
                PopulateProcedures()
            Case Else
                myConfiguration.ConnectionStrings.ConnectionStrings.Item("ReportConStr").ConnectionString = ""
        End Select

        myConfiguration.Save()

    End Sub

   
End Class
