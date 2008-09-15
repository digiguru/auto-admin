Imports Microsoft.VisualBasic
Namespace ModelLayer
    Public Class Parameter
       

        Private _extendedParameters As NameValueCollection
        Public Property ExtendedParameters() As NameValueCollection
            Get
                Return _extendedParameters
            End Get
            Set(ByVal value As NameValueCollection)
                _extendedParameters = value
            End Set
        End Property


        Private _nullable As Boolean
        Public Property Nullable() As Boolean  'Specific_Catalog, 
            Get
                Return _nullable
            End Get
            Set(ByVal value As Boolean)
                _nullable = value
            End Set
        End Property

        'Private _schema As String
        'Public Property Schema() As String ' 	Specific_Schema, 
        '    Get
        '        Return _schema
        '    End Get
        '    Set(ByVal value As String)
        '        _schema = value
        '    End Set
        'End Property

        'Private _procedureName As String
        'Public Property ProcedureName() As String ' Specific_name, 
        '    Get
        '        Return _procedureName
        '    End Get
        '    Set(ByVal value As String)
        '        _procedureName = value
        '    End Set
        'End Property


        Private _parameterMode As String
        Public Property ParameterMode() As String ' Parameter_Mode, 
            Get
                Return _parameterMode
            End Get
            Set(ByVal value As String)
                _parameterMode = value
            End Set
        End Property

        Private _parameterName As String
        Public Property ParameterName() As String ' Parameter_name
            Get
                Return _parameterName
            End Get
            Set(ByVal value As String)
                _parameterName = value
            End Set
        End Property


        Private _dataType As String
        Public Property DataType() As String 'Data_Type,
            Get
                Return _dataType
            End Get
            Set(ByVal value As String)
                _dataType = value
            End Set
        End Property


        Private _maxLength As String
        Public Property MaxLength() As String 'Character_Maximum_Length
            Get
                Return _maxLength
            End Get
            Set(ByVal value As String)
                _maxLength = value
            End Set
        End Property

    End Class
End Namespace
