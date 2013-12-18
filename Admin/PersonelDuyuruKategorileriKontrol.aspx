<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="PersonelDuyuruKategorileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.PersonelDuyuruKategorileriKontrol" %>

<%@ Register Src="Kontroller/PersonelDuyuruKategorileriKontrol.ascx" TagName="PersonelDuyuruKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:PersonelDuyuruKategorileriKontrol ID="PersonelDuyuruKategorileriKontrol1" runat="server" />
</asp:Content>