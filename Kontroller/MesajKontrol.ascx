<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MesajKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.MesajKontrol" %>
<asp:Panel ID="Panel1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Image ID="ImageMesaj" runat="server" />
            </td>
            <td>
                <asp:Label ID="LabelMesaj" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>