<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="TumFotoAlbumler.aspx.cs" Inherits="EnrollPortal.TumFotoAlbumler" %>

<%@ Register Src="Kontroller/FotoAlbumlerListKontrol.ascx" TagName="FotoAlbumlerListKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:FotoAlbumlerListKontrol ID="FotoAlbumlerListKontrol1" runat="server" />
</asp:Content>