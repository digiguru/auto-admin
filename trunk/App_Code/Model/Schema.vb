Imports Microsoft.VisualBasic
Namespace ModelLayer
    Public Class Schema

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _count As Integer
        Public Property Count() As Integer
            Get
                Return _count
            End Get
            Set(ByVal value As Integer)
                _count = value
            End Set
        End Property

    End Class
End Namespace
