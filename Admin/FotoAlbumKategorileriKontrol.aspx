<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="FotoAlbumKategorileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.FotoAlbumKategorileriKontrol" %>

<%@ Register Src="Kontroller/FotoAlbumKategorileriKontrol.ascx" TagName="FotoAlbumKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:FotoAlbumKategorileriKontrol ID="FotoAlbumKategorileriKontrol1" runat="server" />
</asp:Content>