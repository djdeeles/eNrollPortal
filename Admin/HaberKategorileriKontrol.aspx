<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="HaberKategorileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.HaberKategorileriKontrol" %>

<%@ Register Src="Kontroller/HaberKategorileriKontrol.ascx" TagName="HaberKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:HaberKategorileriKontrol ID="HaberKategorileriKontrol1" runat="server" />
</asp:Content>