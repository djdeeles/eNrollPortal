<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UstMenuKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.UstMenuKontrol" %>
<asp:Menu ID="MenuAnaMenu" runat="server" StaticEnableDefaultPopOutImage="False"
          StaticSubMenuIndent="0px" CssClass="UstMenu" RenderingMode="List" DynamicHorizontalOffset="2"
          Font-Size="13px" Orientation="Horizontal">
    <StaticMenuItemStyle HorizontalPadding="2px" VerticalPadding="2px" />
    <StaticMenuStyle CssClass="menuStatic" />
    <DynamicSelectedStyle />
    <DynamicMenuStyle CssClass="verticalDynamic" BackColor="#f0f0f0" BorderColor="#b4b4b4"
                      BorderWidth="1px" />
    <DynamicMenuItemStyle CssClass="verticalDynamic_item" HorizontalPadding="10px" VerticalPadding="2px" />
    <DynamicHoverStyle CssClass="verticalDynamic_hover" BackColor="#fefefe" />
</asp:Menu>