<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="SonGelismeler.aspx.cs" Inherits="EnrollPortal.SonGelismeler" %>

<%@ Register Src="Kontroller/PortalRssListKontrol.ascx" TagName="PortalRssListKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:PortalRssListKontrol ID="PortalRssListKontrol1" runat="server" />
</asp:Content>