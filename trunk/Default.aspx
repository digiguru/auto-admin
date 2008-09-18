<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register src="StoredProcedureForm.ascx" tagname="StoredProcedureForm" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

<style>
body 
{
	font-family: Arial; font-size: 0.8em;
}
.parameter 
{
	display: block; float: none; clear: both; width: 700px;
}
.parameter label
{
width: 12em;
float: left;
text-align: right;
margin-right: 0.5em;
display: block
}
.parameter div.input
{display: inline;}
.parameter div.nullable 
{
width: 12px;
display: inline;
}
.parameter div.not-nullable 
{
display: inline;
}
.parameter div.input>table
{
margin-left: 184px;
}
</style>
</head>
<body>


    <form id="form1" runat="server">
    <div>
    <asp:DropDownList ID="lstDatabase" runat="server" AutoPostBack="true">
    </asp:DropDownList>
    <asp:DropDownList ID="lstSchemas" runat="server" AutoPostBack="true">
    </asp:DropDownList>
    <asp:DropDownList ID="lstProcedures" runat="server" AutoPostBack="true">
    </asp:DropDownList>
    </div>
    <uc1:StoredProcedureForm QueryString="<%=QuerystringProcedure %>" ID="StoredProcedureForm1" runat="server" SchemaName="AdminTool" />
    </form>
</body>
</html>
