Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl


Partial Class ResultsTemplate_GenericExcelResults
    Inherits GenericResultsTemplateBase







    Public Overrides Sub SetValue(ByVal ds As DataSet)
        'Response.ContentType = "application/ms-excel"
        'Response.Output.Write(ds.GetXml)
        'Response.End()

        Dim attachment As String = "attachment; filename=Report.csv"
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.AddHeader("content-disposition", attachment)
        HttpContext.Current.Response.ContentType = "text/csv"
        HttpContext.Current.Response.AddHeader("Pragma", "public")

        Dim str As New StringBuilder
        For Each Table As DataTable In ds.Tables
            For Each dr As DataColumn In Table.Columns
                str.Append(Replace(dr.ColumnName, ",", " ") & ",")
            Next
            str.Append(vbNewLine)
            For Each dr As DataRow In Table.Rows
                For Each field As Object In dr.ItemArray
                    str.Append(Replace(field.ToString, ",", " ") & ",")
                Next
                str.Replace(",", vbNewLine, str.Length - 1, 1)
            Next
            str.Append(vbNewLine)
        Next
        'Dim sOrderID As String = ds.Tables(0).Rows(0).Item(0)
        ''Set up the response for Excel.

        Context.Response.Write(str.ToString)
        ''Transform the DataSet XML using transform.xslt 
        ''and return the results to the client in Response.Outputstream.
        'Dim tw As XmlTextWriter
        'Dim xmlDoc As XmlDataDocument = New XmlDataDocument(ds)
        'Dim xslTran As XslTransform = New XslTransform()
        'xslTran.Load(Context.Server.MapPath("ExportDataSetToExcel.xslt"))
        'xslTran.Transform(xmlDoc, Nothing, Context.Response.OutputStream)
        Context.Response.End()


    End Sub


End Class
