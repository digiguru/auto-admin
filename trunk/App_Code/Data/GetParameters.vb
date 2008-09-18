Imports Microsoft.VisualBasic
Namespace DataLayer
    Public Class Parameters
        Public Function GetAllParameters(ByVal ProcedureName As String) As IList(Of ModelLayer.Parameter)
            Dim AllParameters As New Generic.List(Of ModelLayer.Parameter)
            Using dal As New DAL
                Dim params As New Generic.List(Of System.Data.Common.DbParameter)
                params.Add(dal.createParameter("@ProcedureName", ProcedureName))
                Using reader As New SafeDataReader(dal.getSpReader("AdminTool.GetAllParametersForProcedure", params.ToArray))
                    While reader.Read
                        AllParameters.Add(BuildParameter(reader))
                    End While
                End Using
            End Using

            Return AllParameters
        End Function
        Public Function GetExtendedPropertiesForProcedureParameter(ByVal procedureName As String, ByVal paramaterName As String) As NameValueCollection
            Dim ExtendedProperties As New NameValueCollection
            Using dal As New DAL
                Dim params As New Generic.List(Of System.Data.Common.DbParameter)
                params.Add(dal.createParameter("@ProcedureName", procedureName))
                params.Add(dal.createParameter("@ParameterName", paramaterName))
                Using reader As New SafeDataReader(dal.getSpReader("AdminTool.GetExtendedPropertiesForProcedureParameter", params.ToArray))
                    While reader.Read()
                        ExtendedProperties.Add(reader("name"), reader("value"))
                    End While
                End Using
            End Using

            Return ExtendedProperties
        End Function
        Private Function BuildParameter(ByRef reader As SafeDataReader) As ModelLayer.Parameter
            Dim parameter As New ModelLayer.Parameter
            parameter.DataType = reader.Item("Data_Type")
            parameter.MaxLength = reader.Item("Character_Maximum_Length")
            parameter.ParameterMode = reader.Item("Parameter_Mode")
            parameter.ParameterName = reader.Item("Parameter_name")
            parameter.Nullable = reader.Item("Nullable")
            Return parameter
        End Function
    End Class
End Namespace
