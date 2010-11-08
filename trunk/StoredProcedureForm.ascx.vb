
Public Class StoredProcedureForm
    Inherits GenericFormBase


    Private Const QuerystringProcedure As String = "Procedure"

    Private LoggedInUser As User



    Private _schemaName As String
    Public Property SchemaName() As String
        Get
            Return _schemaName
        End Get
        Set(ByVal value As String)
            _schemaName = value
        End Set
    End Property
    Private _procedureName As String
    Public Property ProcedureName() As String
        Get
            Return _procedureName
        End Get
        Set(ByVal value As String)
            _procedureName = value
        End Set
    End Property
    Private _parameters As IList(Of ModelLayer.Parameter)
    Public Property Parameters() As IList(Of ModelLayer.Parameter)
        Get
            Return _parameters
        End Get
        Set(ByVal value As IList(Of ModelLayer.Parameter))
            _parameters = value
        End Set
    End Property

    Private Sub GetLoggedInUser()
        LoggedInUser = New User
        LoggedInUser.ID = 1
        LoggedInUser.Username = "Adam"
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim queryString As String = Request.QueryString(QuerystringProcedure)
        GetLoggedInUser()
        If Not String.IsNullOrEmpty(queryString) Then
            Dim lstOfStrings As String() = queryString.Split(".")
            If lstOfStrings.Length > 1 Then
                SchemaName = lstOfStrings(0)
                ProcedureName = lstOfStrings(1)
                PopulateParameters(ProcedureName)
            End If
        End If
    End Sub

    Private Sub PopulateParameters(ByVal ProcedureName As String)
        divLegend.Text = ProcedureName
        Parameters = GetParameters(ProcedureName)
        BindParametersToPlaceholder()
    End Sub
    Private Function GetParameters(ByVal ProcedureName As String) As Generic.IList(Of ModelLayer.Parameter)
        Dim procedureAccess As New DataLayer.Parameters
        Return procedureAccess.GetAllParameters(ProcedureName)
    End Function
    Private Function LoadExtendedProperties(ByVal ProcedureName As String, ByVal ParameterName As String) As NameValueCollection
        Dim procedureAccess As New DataLayer.Parameters
        Return procedureAccess.GetExtendedPropertiesForProcedureParameter(ProcedureName, ParameterName)
    End Function
    Private Sub BindParametersToPlaceholder()


        For Each parameter As ModelLayer.Parameter In Parameters
            Dim dynamicControl As GenericDataTemplateBase
            dynamicControl = LoadControl("PropertyTemplate/GenericDataTemplate.ascx")
            Select Case parameter.DataType
                Case "datetime", "date"
                    dynamicControl.ValueControl = LoadControl("PropertyTemplate/DateInput.ascx")
                Case "bit", "boolean"
                    dynamicControl.ValueControl = LoadControl("PropertyTemplate/BooleanInput.ascx")
                Case "dropdown"
                    dynamicControl.ValueControl = LoadControl("PropertyTemplate/DropdownInput.ascx")
                    parameter.ExtendedParameters = LoadExtendedProperties(ProcedureName, parameter.ParameterName)
                Case Else
                    dynamicControl.ValueControl = LoadControl("PropertyTemplate/TextInput.ascx")
            End Select
            dynamicControl.ValueControl.ParentTemplate = dynamicControl
            dynamicControl.ValueControl.FormBase = Me
            dynamicControl.Parameter = parameter

            'dynamicControl = controlFactory.CreateSimpleControl()
            Me.divForm.Controls.Add(dynamicControl)
        Next
    End Sub
    Private Sub Audit(ByVal ParamsValue As String)
        Using dal As New DataLayer.DAL

            Dim params As New Generic.List(Of Data.Common.DbParameter)
            params.Add(dal.createParameter("Schema", SchemaName)) 'param.StringValue()
            params.Add(dal.createParameter("ProcedureName", ProcedureName))
            params.Add(dal.createParameter("Params", ParamsValue))
            params.Add(dal.createParameter("UserID", LoggedInUser.ID))
            '@Schema varchar(50) = null, 

            '@ProcedureName varchar(MAx), 
            '@Params varchar(MAx), 
            '@UserID int
            dal.spExecuteNonQuery("AdminTool.InsertAudit", params.ToArray)

        End Using
    End Sub
    Private Function WriteParams(ByVal params As Generic.List(Of Data.Common.DbParameter)) As String
        Dim s As New StringBuilder
        For Each param In params
            s.Append(param.ParameterName).Append(":").Append(param.Value)
        Next
        Return s.ToString
    End Function
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Using dal As New DataLayer.DAL
            Dim params As New Generic.List(Of Data.Common.DbParameter)
            For Each param As ModelLayer.IParameterView In Me.divForm.Controls
                params.Add(dal.createParameter(param.ParameterName, param.Value)) 'param.StringValue()
            Next

            Dim a As System.Data.DataSet = dal.spgetDataSet(String.Format("{0}.{1}", SchemaName, ProcedureName), params.ToArray)
            Audit(WriteParams(params))
            For Each Table As System.Data.DataTable In a.Tables
                Dim dynamicResultsArea As GenericResultsTemplateBase
                dynamicResultsArea = LoadControl("ResultsTemplate/GenericResultsTemplate.ascx")

                dynamicResultsArea.SetValue(Table)
                Me.divResults.Controls.Add(dynamicResultsArea)
            Next
            Me.divResults.DataBind()
        End Using

    End Sub

    Public Function GetParameterValueByName(ByVal parameterName As String) As Object
        Dim tempValue As Object = Nothing
        For Each dynamicControl As GenericDataTemplateBase In Me.divForm.Controls
            If dynamicControl.ParameterName = parameterName Then
                tempValue = dynamicControl.Value
                Exit For
            End If
        Next
        Return tempValue
    End Function
    Public Overrides Function RequestParameters(ByVal procedureName As String) As System.Collections.Specialized.NameValueCollection

        Dim params As Generic.IList(Of ModelLayer.Parameter) = GetParameters(procedureName)


        Dim items As New NameValueCollection
        For Each param As ModelLayer.Parameter In params
            Dim value As Object = GetParameterValueByName(param.ParameterName)
            If Not value Is Nothing Then
                items.Add(param.ParameterName, value)
            End If
        Next
        Return items
    End Function
End Class
