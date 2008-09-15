Imports Microsoft.VisualBasic
Namespace ModelLayer
    Public Class BoundColumnParameters

        Private _boundObject As String
        Public Property BoundObject() As String
            Get
                Return _boundObject
            End Get
            Set(ByVal value As String)
                _boundObject = value
            End Set
        End Property

        Private _boundIDColumn As String
        Public Property BoundIDColumn() As String
            Get
                Return _boundIDColumn
            End Get
            Set(ByVal value As String)
                _boundIDColumn = value
            End Set
        End Property


        Private _boundNameColumn As String
        Public Property BouldNameColumn() As String
            Get
                Return _boundNameColumn
            End Get
            Set(ByVal value As String)
                _boundNameColumn = value
            End Set
        End Property


    End Class
End Namespace
