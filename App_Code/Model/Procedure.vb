Imports Microsoft.VisualBasic
Namespace ModelLayer
    Public Class Procedure

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _schema As String
        Public Property Schema() As String
            Get
                Return _schema
            End Get
            Set(ByVal value As String)
                _schema = value
            End Set
        End Property

        Private _parameterCount As Integer
        Public Property ParameterCount() As Integer
            Get
                Return _parameterCount
            End Get
            Set(ByVal value As Integer)
                _parameterCount = value
            End Set
        End Property

    End Class
End Namespace
