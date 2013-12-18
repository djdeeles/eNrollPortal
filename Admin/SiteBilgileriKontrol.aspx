<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="SiteBilgileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.SiteBilgileriKontrol" %>

<%@ Register Src="Kontroller/SiteBilgileriKontrol.ascx" TagName="SiteBilgileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:SiteBilgileriKontrol ID="SiteBilgileriKontrol1" runat="server" />
</asp:Content>