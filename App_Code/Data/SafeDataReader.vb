﻿Imports Microsoft.VisualBasic

Imports System.Data

''' <summary>
''' 
''' </summary>
Namespace DataLayer

    ''' <summary>
    ''' This is a DataReader that 'fixes' any null values before
    ''' they are returned to our business code.
    ''' </summary>
    Public Class SafeDataReader

        Implements IDataReader

        Private mDataReader As IDataReader

        ''' <summary>
        ''' Initializes the SafeDataReader object to use data from
        ''' the provided DataReader object.
        ''' </summary>
        ''' <param name="DataReader">The source DataReader object containing the data.</param>
        Public Sub New(ByVal DataReader As IDataReader)
            mDataReader = DataReader
        End Sub

        ''' <summary>
        ''' Gets a string value from the datareader.
        ''' </summary>
        ''' <remarks>
        ''' Returns "" for null.
        ''' </remarks>
        Public Function GetString(ByVal i As Integer) As String Implements IDataReader.GetString
            If mDataReader.IsDBNull(i) Then
                Return ""
            Else
                Return mDataReader.GetString(i)
            End If
        End Function

        ''' <summary>
        ''' Gets a value of type <see cref="System.Object" /> from the datareader.
        ''' </summary>
        ''' <remarks>
        ''' Returns Nothing for null.
        ''' </remarks>
        Public Function GetValue(ByVal i As Integer) As Object Implements IDataReader.GetValue
            If mDataReader.IsDBNull(i) Then
                Return Nothing
            Else
                Return mDataReader.GetValue(i)
            End If
        End Function

        ''' <summary>
        ''' Gets an integer from the datareader.
        ''' </summary>
        ''' <remarks>
        ''' Returns 0 for null.
        ''' </remarks>
        Public Function GetInt32(ByVal i As Integer) As Integer Implements IDataReader.GetInt32
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetInt32(i)
            End If
        End Function

        ''' <summary>
        ''' Gets a double from the datareader.
        ''' </summary>
        ''' <remarks>
        ''' Returns 0 for null.
        ''' </remarks>
        Public Function GetDouble(ByVal i As Integer) As Double Implements IDataReader.GetDouble
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetDouble(i)
            End If
        End Function

        '''' <summary>
        '''' Gets a <see cref="T:CSLA.SmartDate" /> from the datareader.
        '''' </summary>
        '''' <remarks>
        '''' A null is converted into either the min or max possible date
        '''' depending on the MinIsEmpty parameter. See Chapter 5 for more
        '''' details on the SmartDate class.
        '''' </remarks>
        '''' <param name="i">The column number within the datareader.</param>
        '''' <param name="MinIsEmpty">A flag indicating whether the min or max value of a data means an empty date.</param>
        'Public Function GetSmartDate(ByVal i As Integer, Optional ByVal MinIsEmpty As Boolean = True) As SmartDate
        '    If mDataReader.IsDBNull(i) Then
        '        Return New SmartDate(MinIsEmpty)

        '    Else
        '        Return New SmartDate(mDataReader.GetDateTime(i), MinIsEmpty)
        '    End If
        'End Function

        ''' <summary>
        ''' Gets a Guid value from the datareader.
        ''' </summary>
        Public Function GetGuid(ByVal i As Integer) As Guid Implements IDataReader.GetGuid
            If mDataReader.IsDBNull(i) Then
                Return Guid.Empty
            Else
                Return mDataReader.GetGuid(i)
            End If
        End Function

        ''' <summary>
        ''' Reads the next row of data from the datareader.
        ''' </summary>
        Public Function Read() As Boolean Implements IDataReader.Read
            Return mDataReader.Read
        End Function

        ''' <summary>
        ''' Moves to the next result set in the datareader.
        ''' </summary>
        Public Function NextResult() As Boolean Implements IDataReader.NextResult
            Return mDataReader.NextResult()
        End Function

        ''' <summary>
        ''' Closes the datareader.
        ''' </summary>
        Public Sub Close() Implements IDataReader.Close
            mDataReader.Close()
        End Sub

        ''' <summary>
        ''' Returns the depth property value from the datareader.
        ''' </summary>
        Public ReadOnly Property Depth() As Integer Implements System.Data.IDataReader.Depth
            Get
                Return mDataReader.Depth
            End Get
        End Property

        ''' <summary>
        ''' Calls the Dispose method on the underlying datareader.
        ''' </summary>
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Me.Close()
            mDataReader.Dispose()
        End Sub

        ''' <summary>
        ''' Returns the FieldCount property from the datareader.
        ''' </summary>
        Public ReadOnly Property FieldCount() As Integer Implements System.Data.IDataReader.FieldCount
            Get
                Return mDataReader.FieldCount
            End Get
        End Property

        ''' <summary>
        ''' Gets a boolean value from the datareader.
        ''' </summary>
        Public Function GetBoolean(ByVal i As Integer) As Boolean Implements System.Data.IDataReader.GetBoolean
            If mDataReader.IsDBNull(i) Then
                Return False
            Else
                Return mDataReader.GetBoolean(i)
            End If
        End Function

        ''' <summary>
        ''' Gets a byte value from the datareader.
        ''' </summary>
        Public Function GetByte(ByVal i As Integer) As Byte Implements System.Data.IDataReader.GetByte
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetByte(i)
            End If
        End Function

        ''' <summary>
        ''' Invokes the GetBytes method of the underlying datareader.
        ''' </summary>
        Public Function GetBytes(ByVal i As Integer, ByVal fieldOffset As Long, ByVal buffer() As Byte, ByVal bufferoffset As Integer, ByVal length As Integer) As Long Implements System.Data.IDataReader.GetBytes
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length)
            End If
        End Function

        ''' <summary>
        ''' Gets a char value from the datareader.
        ''' </summary>
        Public Function GetChar(ByVal i As Integer) As Char Implements System.Data.IDataReader.GetChar
            If mDataReader.IsDBNull(i) Then
                Return Char.MinValue
            Else
                Return mDataReader.GetChar(i)
            End If
        End Function

        ''' <summary>
        ''' Invokes the GetChars method of the underlying datareader.
        ''' </summary>
        Public Function GetChars(ByVal i As Integer, ByVal fieldoffset As Long, ByVal buffer() As Char, ByVal bufferoffset As Integer, ByVal length As Integer) As Long Implements System.Data.IDataReader.GetChars
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length)
            End If
        End Function

        ''' <summary>
        ''' Invokes the GetData method of the underlying datareader.
        ''' </summary>
        Public Function GetData(ByVal i As Integer) As System.Data.IDataReader Implements System.Data.IDataReader.GetData
            Return mDataReader.GetData(i)
        End Function

        ''' <summary>
        ''' Invokes the GetDataTypeName method of the underlying datareader.
        ''' </summary>
        Public Function GetDataTypeName(ByVal i As Integer) As String Implements System.Data.IDataReader.GetDataTypeName
            Return mDataReader.GetDataTypeName(i)
        End Function

        ''' <summary>
        ''' Gets a date value from the datareader.
        ''' </summary>
        Public Function GetDateTime(ByVal i As Integer) As Date Implements System.Data.IDataReader.GetDateTime
            If mDataReader.IsDBNull(i) Then
                Return Date.MinValue
            Else
                Return mDataReader.GetDateTime(i)
            End If
        End Function

        ''' <summary>
        ''' Gets a decimal value from the datareader.
        ''' </summary>
        Public Function GetDecimal(ByVal i As Integer) As Decimal Implements System.Data.IDataReader.GetDecimal
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetDecimal(i)
            End If
        End Function

        ''' <summary>
        ''' Invokes the GetFieldType method of the underlying datareader.
        ''' </summary>
        Public Function GetFieldType(ByVal i As Integer) As System.Type Implements System.Data.IDataReader.GetFieldType
            Return mDataReader.GetFieldType(i)
        End Function

        ''' <summary>
        ''' Gets a Single value from the datareader.
        ''' </summary>
        Public Function GetFloat(ByVal i As Integer) As Single Implements System.Data.IDataReader.GetFloat
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetFloat(i)
            End If
        End Function

        ''' <summary>
        ''' Gets a Short value from the datareader.
        ''' </summary>
        Public Function GetInt16(ByVal i As Integer) As Short Implements System.Data.IDataReader.GetInt16
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetInt16(i)
            End If
        End Function

        ''' <summary>
        ''' Gets a Long value from the datareader.
        ''' </summary>
        Public Function GetInt64(ByVal i As Integer) As Long Implements System.Data.IDataReader.GetInt64
            If mDataReader.IsDBNull(i) Then
                Return 0
            Else
                Return mDataReader.GetInt64(i)
            End If
        End Function

        ''' <summary>
        ''' Invokes the GetName method of the underlying datareader.
        ''' </summary>
        Public Function GetName(ByVal i As Integer) As String Implements System.Data.IDataReader.GetName
            Return mDataReader.GetName(i)
        End Function

        ''' <summary>
        ''' Gets an ordinal value from the datareader.
        ''' </summary>
        Public Function GetOrdinal(ByVal name As String) As Integer Implements System.Data.IDataReader.GetOrdinal
            Return mDataReader.GetOrdinal(name)
        End Function

        ''' <summary>
        ''' Invokes the GetSchemaTable method of the underlying datareader.
        ''' </summary>
        Public Function GetSchemaTable() As System.Data.DataTable Implements System.Data.IDataReader.GetSchemaTable
            Return mDataReader.GetSchemaTable
        End Function

        ''' <summary>
        ''' Invokes the GetValues method of the underlying datareader.
        ''' </summary>
        Public Function GetValues(ByVal values() As Object) As Integer Implements System.Data.IDataReader.GetValues
            Return mDataReader.GetValues(values)
        End Function

        ''' <summary>
        ''' Returns the IsClosed property value from the datareader.
        ''' </summary>
        Public ReadOnly Property IsClosed() As Boolean Implements System.Data.IDataReader.IsClosed
            Get
                Return mDataReader.IsClosed
            End Get
        End Property

        ''' <summary>
        ''' Invokes the IsDBNull method of the underlying datareader.
        ''' </summary>
        Public Function IsDBNull(ByVal i As Integer) As Boolean Implements System.Data.IDataReader.IsDBNull
            Return mDataReader.IsDBNull(i)
        End Function

        ''' <summary>
        ''' Returns a value from the datareader.
        ''' </summary>
        ''' <remarks>
        ''' Returns Nothing if the value is null.
        ''' </remarks>
        Default Public Overloads ReadOnly Property Item(ByVal name As String) As Object Implements System.Data.IDataReader.Item
            Get
                Dim value As Object = mDataReader.Item(name)
                If DBNull.Value.Equals(value) Then
                    Return Nothing
                Else
                    Return value
                End If
            End Get
        End Property

        ''' <summary>
        ''' Returns a value from the datareader.
        ''' </summary>
        ''' <remarks>
        ''' Returns Nothing if the value is null.
        ''' </remarks>
        Default Public Overloads ReadOnly Property Item(ByVal i As Integer) As Object Implements System.Data.IDataReader.Item
            Get
                If mDataReader.IsDBNull(i) Then
                    Return Nothing
                Else
                    Return mDataReader.Item(i)
                End If
            End Get
        End Property

        ''' <summary>
        ''' Returns the RecordsAffected property value from the underlying datareader.
        ''' </summary>
        Public ReadOnly Property RecordsAffected() As Integer Implements System.Data.IDataReader.RecordsAffected
            Get
                Return mDataReader.RecordsAffected
            End Get
        End Property
    End Class

End Namespace
