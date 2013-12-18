<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KullaniciMenusuKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.KullaniciMenusuKontrol" %>
<asp:Menu ID="MenuAnaMenu" runat="server" StaticEnableDefaultPopOutImage="False"
          StaticSubMenuIndent="0px" CssClass="KullaniciMenu" RenderingMode="Table" Width="100%"
          DynamicHorizontalOffset="2">
    <StaticMenuItemStyle CssClass="menustaticitem" />
    <StaticMenuStyle CssClass="menustatic" />
    <StaticHoverStyle CssClass="menustatichover" />
    <DynamicMenuStyle CssClass="verticaldynamic" />
    <DynamicMenuItemStyle CssClass="verticaldynamicitem" />
    <DynamicHoverStyle CssClass="verticaldynamichover" />
</asp:Menu>