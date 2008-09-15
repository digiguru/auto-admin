Namespace ModelLayer
    Public Interface IParameterView
        ReadOnly Property ParameterName() As String
        ReadOnly Property ParameterType() As String
        Property Value() As Object
    End Interface
End Namespace