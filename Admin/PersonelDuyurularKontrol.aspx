<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="PersonelDuyurularKontrol.aspx.cs" Inherits="EnrollPortal.Admin.PersonelDuyurularKontrol" %>

<%@ Register Src="Kontroller/PersonelDuyurularKontrol.ascx" TagName="PersonelDuyurularKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:PersonelDuyurularKontrol ID="PersonelDuyurularKontrol1" runat="server" />
</asp:Content>