<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="IlanlarKontrol.aspx.cs" Inherits="EnrollPortal.Admin.IlanlarKontrol" %>

<%@ Register Src="Kontroller/IlanlarKontrol.ascx" TagName="IlanlarKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:IlanlarKontrol ID="IlanlarKontrol1" runat="server" />
</asp:Content>