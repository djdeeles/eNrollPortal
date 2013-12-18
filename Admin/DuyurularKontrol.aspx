<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="DuyurularKontrol.aspx.cs" Inherits="EnrollPortal.Admin.DuyurularKontrol" %>

<%@ Register Src="Kontroller/DuyurularKontrol.ascx" TagName="DuyurularKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:DuyurularKontrol ID="DuyurularKontrol1" runat="server" />
</asp:Content>