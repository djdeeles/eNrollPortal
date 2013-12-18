<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="AlbumListeleri.aspx.cs" Inherits="EnrollPortal.Portal.AlbumListeleri" %>

<%@ Register Src="../Kontroller/AlbumlerKontrol.ascx" TagName="AlbumlerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:AlbumlerKontrol ID="AlbumlerKontrol1" runat="server" />
</asp:Content>