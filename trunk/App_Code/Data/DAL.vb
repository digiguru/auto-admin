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
        Implements IDisposable


        Private cn As Data.Common.DbConnection
        Private cmd As Data.Common.DbCommand
        Private dr As IDataReader
        Private ds As DataSet
        Private dt As DataTable

        Private connectionstr As String

        Sub New()
            'sqlstr = "data source=220.227.236.26;user id=hitched;pwd=aressindia;database=hitched"
            'sqlstr = "data source=220.227.236.29;user id=sa;pwd=aressindia;database=hitched"
            connectionstr = WebConfigurationManager.ConnectionStrings("ReportConStr").ConnectionString() '
            If String.IsnullOrEmpty(connectionstr) Then
                Throw New Exception("No Connection")
            End If
        End Sub
        Public ReadOnly Property DBTimeoutInSeconds As Integer
            Get
                Return WebConfigurationManager.AppSettings("DBTimeoutInSeconds")
            End Get
        End Property
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
            Try

                Dim da As SqlDataAdapter

                dt = New DataTable()
                Dim i As Integer
                cn = getConnection()

                cmd = New SqlCommand(spname, cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = DBTimeoutInSeconds
                If Not param Is Nothing Then
                    For i = 0 To param.Length - 1
                        cmd.Parameters.Add(param(i))
                    Next
                End If

                da = New SqlDataAdapter(cmd)
                da.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw ex
            Finally
                cn.Close()
            End Try
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
            

            cn = getConnection()
            Try

                'added By Pankaj For Connection Lost
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(str, cn)
                cmd.CommandTimeout = DBTimeoutInSeconds
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
                cn.Close()

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
            

            Try
                cn = getConnection()

                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = DBTimeoutInSeconds
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
                Return Result
            Catch ex As Exception
                Throw ex
            Finally
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End Try
            Return Result
        End Function

        'Addedd By: Pankaj H Kshirsagar
        'Function Name : spExecuteNonQuery
        'Date : 1/11/2007
        'Purpose : Execute Scalar Store Procedure having Parameters List
        Function spExeScalar(ByVal spname As String, ByVal param As System.Data.Common.DbParameter()) As Integer
            Dim Result As Integer
            Try
                cn = getConnection()
                cn = CType(cn, SqlConnection)
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = DBTimeoutInSeconds
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


            Catch ex As Exception
            Finally
                cn.Close()
            End Try
            Return Result
        End Function
        Function spExeScalarString(ByVal spname As String, ByVal param As System.Data.Common.DbParameter()) As String
            Dim Result As String
            Try
                cn = getConnection()
                cn = CType(cn, SqlConnection)
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = DBTimeoutInSeconds
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
            cn = getConnection()
            dt = New DataTable()
            

            cn.Open()


            cmd = New SqlCommand(spname, cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = DBTimeoutInSeconds
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

        Function spgetDataSet(ByVal spname As String, ByVal param As System.Data.Common.DbParameter()) As DataSet
            Try


                Dim da As SqlDataAdapter
                ds = New DataSet()
                cn = getConnection()
                'dt = New DataTable()
                

                cn.Open()


                cmd = New SqlCommand(spname, cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = DBTimeoutInSeconds
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
                da.Fill(ds)
                Return ds
            Catch ex As Exception
                Throw ex
            Finally
                cn.Close()

            End Try
            'cmd.ExecuteReader()
            'Return Nothing
        End Function
        'Addedd By: Pankaj H Kshirsagar
        'Function Name : spCmdExecuteNonQuery
        'Date : 31/10/2007
        'Purpose : Execute NonQuery Store Procedure having Parameters List
        Function spCmdExecuteNonQuery(ByVal spname As String, ByVal cmd As Data.Common.DbCommand) As Integer
            Dim Result As Integer
            cn = getConnection()

            Try
                cn = CType(cn, SqlConnection)

                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()

                cmd = New SqlCommand(spname, cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = DBTimeoutInSeconds
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
                CloseConnection()
            End Try
            Return Result
        End Function

        Private Sub CloseConnection()
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Sub
        Private Sub CloseReader()
            If Not dr Is Nothing Then
                dr.Close()
                dr.Dispose()
            End If
        End Sub
        Private Sub CloseDataSet()
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
        End Sub
        Private Sub CloseDataTable()
            If Not dt Is Nothing Then
                dt.Dispose()
            End If
        End Sub
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                    CloseConnection()
                    CloseReader()
                    CloseDataSet()
                    CloseDataTable()
                End If
                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
