Imports Microsoft.VisualBasic
Namespace DataLayer
    Public Class Procedures
        Public Function GetAllSchemas() As IList(Of ModelLayer.Schema)
            Dim AllProcedures As New Generic.List(Of ModelLayer.Schema)
            Using dal As New DAL
                Using reader As New SafeDataReader(dal.getSpReader("AdminTool.GetAllSchemas"))
                    While reader.Read
                        AllProcedures.Add(BuildSchema(reader))
                    End While
                End Using

            End Using

            Return AllProcedures
        End Function
        Public Function GetAllProcedures() As IList(Of ModelLayer.Procedure)
            Dim AllProcedures As New Generic.List(Of ModelLayer.Procedure)
            Using dal As New DAL
                Using reader As New SafeDataReader(dal.getSpReader("AdminTool.GetAllProcedures"))
                    While reader.Read
                        AllProcedures.Add(BuildProcedure(reader))
                    End While
                End Using

            End Using

            Return AllProcedures
        End Function
        Public Function GetAllProcedures(ByVal Schema As String) As IList(Of ModelLayer.Procedure)
            Dim AllProcedures As New Generic.List(Of ModelLayer.Procedure)
            Using dal As New DAL
                Dim params As New Generic.List(Of System.Data.Common.DbParameter)
                params.Add(dal.createParameter("@Schema", Schema))
                Using reader As New SafeDataReader(dal.getSpReader("AdminTool.GetAllProcedures", params.ToArray))
                    While reader.Read
                        AllProcedures.Add(BuildProcedure(reader))
                    End While
                End Using

            End Using

            Return AllProcedures
        End Function
        Private Function BuildProcedure(ByRef reader As SafeDataReader) As ModelLayer.Procedure
            Dim procedure As New ModelLayer.Procedure
            procedure.Name = reader.Item("Name")
            procedure.Schema = reader.Item("Schema")
            procedure.ParameterCount = reader.Item("ParameterCount")
            Return procedure
        End Function
        Private Function BuildSchema(ByRef reader As SafeDataReader) As ModelLayer.Schema
            Dim Schema As New ModelLayer.Schema
            Schema.Name = reader.Item("Specific_Schema")
            Schema.Count = reader.Item("Count")
            Return Schema
        End Function
    End Class
End Namespace
