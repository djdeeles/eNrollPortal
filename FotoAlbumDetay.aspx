<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="FotoAlbumDetay.aspx.cs" Inherits="EnrollPortal.FotoAlbumDetay" %>

<%@ Register Src="Kontroller/FotoAlbumDetayKontrol.ascx" TagName="FotoAlbumDetayKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:FotoAlbumDetayKontrol ID="FotoAlbumDetayKontrol1" runat="server" />
</asp:Content>