Imports Microsoft.VisualBasic
Imports ModelLayer
Public MustInherit Class GenericResultsTemplateBase
    Inherits System.Web.UI.UserControl
   

    Public MustOverride Sub SetValue(ByVal value As Data.DataSet)
  

End Class
