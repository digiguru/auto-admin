Imports Microsoft.VisualBasic
Imports ModelLayer
Public MustInherit Class GenericDataTemplateBase
    Inherits System.Web.UI.UserControl
    Implements IParameterView

    Public MustOverride Sub SetValue(ByVal value As Object)

    Public ReadOnly Property Nullable() As Boolean
        Get
            Return Parameter.Nullable
        End Get
    End Property
    Public ReadOnly Property ParameterName() As String Implements IParameterView.ParameterName
        Get
            Return Parameter.ParameterName
        End Get
    End Property
    Public ReadOnly Property ParameterType() As String Implements IParameterView.ParameterType
        Get
            Return Parameter.DataType
        End Get
    End Property

    Private _parameter As ModelLayer.Parameter
    Public Property Parameter() As ModelLayer.Parameter
        Get
            Return _parameter
        End Get
        Set(ByVal value As ModelLayer.Parameter)
            _parameter = value
        End Set
    End Property

    Private _value As Object
    Public Property Value() As Object Implements ModelLayer.IParameterView.Value
        Get
            Return _value
        End Get
        Set(ByVal value As Object)
            _value = value
        End Set
    End Property

    Private _valueControl As GenericDataInputBase
    Public Property ValueControl() As GenericDataInputBase
        Get
            Return _valueControl
        End Get
        Set(ByVal value As GenericDataInputBase)
            _valueControl = value
        End Set
    End Property

End Class
