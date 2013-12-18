<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="FotoAlbumGorselleriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.FotoAlbumGorselleriKontrol" %>

<%@ Register Src="Kontroller/FotoAlbumGorselleriKontrol.ascx" TagName="FotoAlbumGorselleriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:FotoAlbumGorselleriKontrol ID="FotoAlbumGorselleriKontrol1" runat="server" />
</asp:Content>