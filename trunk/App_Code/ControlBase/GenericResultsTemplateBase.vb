Imports Microsoft.VisualBasic
Imports ModelLayer
Public MustInherit Class GenericResultsTemplateBase
    Inherits System.Web.UI.UserControl
   
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _value As Object
    Public Property Value() As Object
        Get
            Return _value
        End Get
        Set(ByVal value As Object)
            _value = value
        End Set
    End Property

    Public MustOverride Sub SetValue(ByVal value As Data.DataTable)
  

End Class
