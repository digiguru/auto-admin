'CLASSES FOR DATABASE OPERATIONS
'NAMESPACE : DBOPS
'CLASS : clsDBOperations
'DEVELOPED BY : MAYUR PATIL
'CREATED ON : 26-10-2007
'UPDATE ON :

Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Web.HttpServerUtility
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Imports System.Web.Configuration


Namespace DataLayer
    Public Class DAL

        Private cn As Data.Common.DbConnection
        Private cmd As Data.Common.DbCommand
        Private dr As IDataReader
        Private ds As DataSet
        Private dt As DataTable

        Private connectionstr As String

        Sub New()
            'sqlstr = "data source=220.227.236.26;user id=hitched;pwd=aressindia;database=hitched"
            'sqlstr = "data source=220.227.236.29;user id=sa;pwd=aressindia;database=hitched"
            connectionstr = WebConfigurationManager.ConnectionStrings("constr").ConnectionString()
        End Sub

        Function getConnectionString() As String
            Return connectionstr
        End Function
        Function getConnection() As Object
            Try
                cn = New SqlConnection(connectionstr)
            Catch ex As Exception

                Return ex
            End Try
            Return cn
        End Function

        Function spGetData(ByVal spname As String, ByVal param As Common.DbParameter()) As DataTable
            Dim da As SqlDataAdapter

            dt = New DataTable()
            Dim cnobj As Object
            Dim i As Integer
            cnobj = getConnection()

            cn = CType(cnobj, SqlConnection)
            cmd = New SqlCommand(spname, cnobj)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 60
            If Not param Is Nothing Then
                For i = 0 To param.Length - 1
                    cmd.Parameters.Add(param(i))
                Next
            End If

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            Return dt
        End Function
        Function createParameter( _
    ByVal name As String, _
    ByVal value As Object _
    ) As Data.Common.DbParameter
            Dim sqlParameter As New SqlClient.SqlParameter(name, value)
            Return sqlParameter
        End Function

        Function getSpReader( _
        ByVal str As String, _
        Optional ByVal param As System.Data.Common.DbParameter() = Nothing _
        ) As IDataReader
            Dim cnobj As Object

            cnobj = getConnection()
            Try

                cn = CType(cnobj, SqlConnection)
                'added By Pankaj For Connection Lost
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(str, cnobj)
                ' cmd.CommandTimeout = 60
                cmd.CommandType = CommandType.StoredProcedure

                If Not param Is Nothing Then


                    Dim p As New SqlParameter
                    For Each p In param
                        If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                            p.Value = Nothing
                        End If
                        cmd.Parameters.Add(p)
                    Next p
                End If

                dr = New SafeDataReader(cmd.ExecuteReader)
            Catch ex As Exception
                Throw ex

                'Return ex.ToString()
            End Try
            Return dr
        End Function
        'Addedd By: Pankaj H Kshirsagar
        'Function Name : spExecuteNonQuery
        'Date : 31/10/2007
        'Purpose : Execute NonQuery Store Procedure having Parameters List

        Function spExecuteNonQuery(ByVal spname As String, ByVal param As System.Data.Common.DbParameter()) As String
            Dim Result As Integer
            Dim cnobj As Object
            cnobj = getConnection()


                Try
                cn = CType(cnobj, SqlConnection)

                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cnobj)
                cmd.CommandType = CommandType.StoredProcedure
                '                   cmd.CommandTimeout = 60
                Dim p As New SqlParameter

                'cmd.Parameters.Add(param)
                If Not p Is Nothing Then
                    For Each p In param
                        If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                            p.Value = Nothing
                        End If
                        cmd.Parameters.Add(p)
                    Next p
                End If

                Result = cmd.ExecuteNonQuery()
                cn.Close()
                Return Result
            Catch ex As Exception
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                Return ex.ToString()

            End Try
            Return Result
        End Function

        'Addedd By: Pankaj H Kshirsagar
        'Function Name : spExecuteNonQuery
        'Date : 1/11/2007
        'Purpose : Execute Scalar Store Procedure having Parameters List
        Function spExeScalar(ByVal spname As String, ByVal param As System.Data.Common.DbParameter()) As Integer
            Dim Result As Integer
            Dim cnobj As Object
            cnobj = getConnection()
                Try
                cn = CType(cnobj, SqlConnection)
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cnobj)
                cmd.CommandType = CommandType.StoredProcedure
                '                  cmd.CommandTimeout = 60
                Dim p As New SqlParameter
                'cmd.Parameters.Add(param)
                If Not param Is Nothing Then
                    For Each p In param
                        If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                            p.Value = Nothing
                        End If
                        cmd.Parameters.Add(p)
                    Next p
                End If

                Result = cmd.ExecuteScalar()

                cn.Close()
            Catch ex As Exception

            End Try
            Return Result
        End Function
        Function spExeScalarString(ByVal spname As String, ByVal param As System.Data.Common.DbParameter()) As String
            Dim Result As String
            Dim cnobj As Object
            cnobj = getConnection()
                Try
                cn = CType(cnobj, SqlConnection)
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cnobj)
                cmd.CommandType = CommandType.StoredProcedure
                '                 cmd.CommandTimeout = 60
                Dim p As New SqlParameter
                'cmd.Parameters.Add(param)
                For Each p In param
                    If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                        p.Value = Nothing
                    End If
                    cmd.Parameters.Add(p)
                Next p
                Result = cmd.ExecuteScalar()

            Catch ex As Exception
                Throw ex
            Finally
                cn.Close()
            End Try
            Return Result
        End Function
        'Addedd By: Pankaj H Kshirsagar
        'Function Name : spgetData
        'Date : 2/11/2007
        'Purpose : Retrive the data in the form of DataTable
        Function spgetDataTable(ByVal spname As String, ByVal param As System.Data.Common.DbParameter()) As DataTable
            Dim da As SqlDataAdapter
            dt = New DataTable()
            Dim cnobj As Object
            cnobj = getConnection()
            dt = New DataTable()
            cn = CType(cnobj, SqlConnection)


            cn.Open()


            cmd = New SqlCommand(spname, cnobj)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 60
            'Dim p As New SqlParameter
            'cmd.Parameters.Add(param)
            If Not param Is Nothing Then


                For Each p As Common.DbParameter In param
                    cmd.Parameters.Add(p)
                    '                    If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                    ' p.Value = Nothing
                    'End If
                    'cmd.Parameters.Add(p)
                Next p
            End If
            da = New SqlDataAdapter(cmd) '(spname, cn)
            da.Fill(dt)
            cn.Close()
            Return dt
            'cmd.ExecuteReader()
            'Return Nothing
        End Function


        'Addedd By: Pankaj H Kshirsagar
        'Function Name : spCmdExecuteNonQuery
        'Date : 31/10/2007
        'Purpose : Execute NonQuery Store Procedure having Parameters List
        Function spCmdExecuteNonQuery(ByVal spname As String, ByVal cmd As Data.Common.DbCommand) As Integer
            Dim Result As Integer
            Dim cnobj As Object
            cnobj = getConnection()

            Try
                cn = CType(cnobj, SqlConnection)

                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cnobj)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 60
                'Dim p As New SqlParameter
                ''cmd.Parameters.Add(param)
                'For Each p In param
                '    If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                '        p.Value = Nothing
                '    End If
                '    cmd.Parameters.Add(p)
                'Next p
                Result = cmd.ExecuteNonQuery()
                cn.Close()
            Catch ex As Exception
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End Try
            Return Result
        End Function

    End Class
End Namespace
