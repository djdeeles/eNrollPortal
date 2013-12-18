<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IcerikDetayKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.IcerikDetayKontrol" %>
<div class="modulforum">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            İÇERİK</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div class="moduldetayicerik2">
            <%--<asp:Label ID="LabelBaslik" runat="server" Text=""></asp:Label>--%>
            <asp:Label ID="LabelIcerik" runat="server" Text=""></asp:Label>
        </div>
    </div>
</div>