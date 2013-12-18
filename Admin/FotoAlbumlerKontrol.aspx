<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="FotoAlbumlerKontrol.aspx.cs" Inherits="EnrollPortal.Admin.FotoAlbumlerKontrol" %>

<%@ Register Src="Kontroller/FotoAlbumlerKontrol.ascx" TagName="FotoAlbumlerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:FotoAlbumlerKontrol ID="FotoAlbumlerKontrol1" runat="server" />
</asp:Content>