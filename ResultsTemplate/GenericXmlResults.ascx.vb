Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl


Partial Class ResultsTemplate_GenericXMLResults
    Inherits GenericResultsTemplateBase







    Public Overrides Sub SetValue(ByVal ds As DataSet)
        Response.ContentType = "text/xml"
        Response.Output.Write(ds.GetXml)
        Response.End()
    End Sub


End Class
