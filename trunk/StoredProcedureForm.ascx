<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StoredProcedureForm.ascx.vb" Inherits="StoredProcedureForm" CodeFileBaseClass="GenericFormBase" %>
<fieldset>
    <legend><asp:Literal ID="divLegend" runat="server"></asp:Literal></legend>
    <asp:PlaceHolder ID="divForm" runat="server">
    </asp:PlaceHolder>
    <asp:Button ID="btnSubmit" runat="server" Text="submit" />
    <asp:PlaceHolder ID="divResults" runat="server">
    </asp:PlaceHolder>
</fieldset>
