
Partial Class PropertyTemplate_DropdownInput
    Inherits GenericDataInputBase
    Dim dal As DataLayer.DAL

    Private _parameterCollection As Generic.List(Of Data.Common.DbParameter)
    Public Property ParameterCollection() As Generic.List(Of Data.Common.DbParameter)
        Get
            Return _parameterCollection
        End Get
        Set(ByVal value As Generic.List(Of Data.Common.DbParameter))
            _parameterCollection = value
        End Set
    End Property

    Public ReadOnly Property ParameterDefault() As String
        Get
            Return 0
        End Get
    End Property
    Public ReadOnly Property SelectedValue() As String
        Get
            Return Me.lstValue.Text
        End Get
    End Property
    Public Overrides Sub SetValue()
        If String.IsNullOrEmpty(SelectedValue) Then
            ParentTemplate.SetValue(Nothing)
        Else
            ParentTemplate.SetValue(SelectedValue)
        End If
    End Sub
    Public boundColumnParams As ModelLayer.BoundColumnParameters
    Public Sub AssignExtendedProperties()
        Dim extendedPropetiesRaw As NameValueCollection = Me.ParentTemplate.Parameter.ExtendedParameters
        boundColumnParams = New ModelLayer.BoundColumnParameters
        boundColumnParams.BoundIDColumn = extendedPropetiesRaw.Item("BoundID")
        boundColumnParams.BouldNameColumn = extendedPropetiesRaw.Item("BoundName")
        boundColumnParams.BoundObject = extendedPropetiesRaw.Item("BoundObject")
    End Sub
    Private Sub AddPleaseSelectItem()
        Dim emptyListItem As New WebControls.ListItem
        emptyListItem.Value = "0"
        emptyListItem.Text = "Please Select"
        Me.lstValue.Items.Add(emptyListItem)

    End Sub
    Private Sub ResetList()
        Me.lstValue.Items.Clear()
    End Sub


    Private Function GetSchemaName(ByVal procedureName As String) As String
        Dim schema As String = String.Empty
        Dim procedureAccess As New DataLayer.Procedures
        Dim Procedures As IList(Of ModelLayer.Procedure) = procedureAccess.GetAllProcedures
        For Each procedure As ModelLayer.Procedure In Procedures
            If procedureName = procedure.Name Then
                schema = procedure.Schema
            End If
        Next
        Return schema



    End Function




    Public Sub RePopulate()

        Dim saveItem As String = Me.lstValue.SelectedValue

        ResetList()
        AddPleaseSelectItem()
        Dim FullyQualifiedProcedureName As String = boundColumnParams.BoundObject
        dal = New DataLayer.DAL

        GetParameters(JustProcedureName(FullyQualifiedProcedureName))


        Dim schemaName As String = GetSchemaName(FullyQualifiedProcedureName)


        Try
            Dim dataTable As Data.DataTable
            dataTable = dal.spGetData(String.Format("{0}.{1}", schemaName, FullyQualifiedProcedureName), ParameterCollection.ToArray)

            For Each dataLine As Data.DataRow In dataTable.Rows
                Dim listItem As New WebControls.ListItem
                listItem.Value = dataLine.Item(boundColumnParams.BoundIDColumn)
                listItem.Text = dataLine.Item(boundColumnParams.BouldNameColumn)

                Me.lstValue.Items.Add(listItem)
            Next

        Catch ex As Exception
            Throw ex
        Finally
            dal.Dispose()

        End Try
        ReselectSavedItem(saveItem)

        Me.lstValue.DataBind()

    End Sub
    Private Sub ReselectSavedItem(ByVal saveItem As String)
        If Not String.IsNullOrEmpty(saveItem) Then
            For Each item As WebControls.ListItem In Me.lstValue.Items
                If item.Value = saveItem Then
                    Me.lstValue.SelectedValue = saveItem
                End If
            Next
        Else
            Me.lstValue.SelectedValue = ParameterDefault
        End If
    End Sub
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim param As ModelLayer.Parameter = Me.ParentTemplate.Parameter

        'param.DataType
        'Request.QueryString

        AssignExtendedProperties()
        RePopulate()
    End Sub
    Public Function JustProcedureName(ByVal FullyQualifiedProcedureName As String) As String
        Dim procName As String
        Dim lstTempName As String() = FullyQualifiedProcedureName.Split(".")
        If lstTempName.Length > 1 Then
            procName = lstTempName(1)
        Else
            procName = FullyQualifiedProcedureName
        End If
        Return procName
    End Function
    Private Sub GetParameters(ByVal procedureName As String)
        Dim params As NameValueCollection = Me.FormBase.RequestParameters(procedureName)
        ParameterCollection = New Generic.List(Of Data.Common.DbParameter)


        For i As Integer = 1 To params.Count
            ParameterCollection.Add(dal.createParameter(params.AllKeys(i - 1), params.Item(i - 1)))
        Next
    End Sub
End Class
