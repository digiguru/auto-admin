Imports Microsoft.VisualBasic
Namespace DataLayer
    Public Class Procedures
        Public Function GetAllProcedures() As IList(Of ModelLayer.Procedure)
            Dim AllProcedures As New Generic.List(Of ModelLayer.Procedure)
            Dim dal As New DAL
            Dim reader As New SafeDataReader(dal.getSpReader("AdminTool.GetAllProcedures"))
            While reader.Read
                AllProcedures.Add(BuildProcedure(reader))
            End While
            Return AllProcedures
        End Function
        Private Function BuildProcedure(ByRef reader As SafeDataReader) As ModelLayer.Procedure
            Dim procedure As New ModelLayer.Procedure
            procedure.Name = reader.Item("Name")
            procedure.Schema = reader.Item("Schema")
            procedure.ParameterCount = reader.Item("ParameterCount")
            Return procedure
        End Function
    End Class
End Namespace
