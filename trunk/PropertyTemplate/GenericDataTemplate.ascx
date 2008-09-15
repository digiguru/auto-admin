<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GenericDataTemplate.ascx.vb" Inherits="PropertyTemplate_GenericDataTemplate" %>
<p>
    <label><%=ParameterName%></label>
    <asp:Panel ID="divNullable" runat="server">
        <asp:CheckBox ID="chkIsNull" runat="server" AutoPostBack="true" />
    </asp:Panel>
    <asp:Panel ID="divInput" runat="server">
        <asp:PlaceHolder ID="divValue" runat="server"></asp:PlaceHolder>
    </asp:Panel>
</p>

