<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GenericDataTemplate.ascx.vb" Inherits="PropertyTemplate_GenericDataTemplate" %>
<div class="parameter">
    <label><%=ParameterName%></label>
    <asp:Panel ID="divNullable" runat="server" class="nullable">
        <asp:CheckBox ID="chkIsNull" runat="server" AutoPostBack="true" />
    </asp:Panel>
    <asp:Panel ID="divNotNullable" runat="server" class="not-nullable">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </asp:Panel>
    <asp:Panel ID="divInput" runat="server" class="input">
        <asp:PlaceHolder ID="divValue" runat="server"></asp:PlaceHolder>
    </asp:Panel>
</div>

