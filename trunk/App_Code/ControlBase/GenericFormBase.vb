Imports Microsoft.VisualBasic

Public MustInherit Class GenericFormBase
    Inherits System.Web.UI.UserControl
    Implements IFormBase
    Public MustOverride Function RequestParameters(ByVal procedureName As String) As System.Collections.Specialized.NameValueCollection Implements IFormBase.RequestParameters
End Class
