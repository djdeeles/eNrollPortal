<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="DuyuruKategorileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.DuyuruKategorileriKontrol" %>

<%@ Register Src="Kontroller/DuyuruKategorileriKontrol.ascx" TagName="DuyuruKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:DuyuruKategorileriKontrol ID="DuyuruKategorileriKontrol1" runat="server" />
</asp:Content>