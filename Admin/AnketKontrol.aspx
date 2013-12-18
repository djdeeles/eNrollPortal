<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="AnketKontrol.aspx.cs" Inherits="EnrollPortal.Admin.AnketKontrol" %>

<%@ Register Src="Kontroller/AnketKontrol.ascx" TagName="AnketKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:AnketKontrol ID="AnketKontrol1" runat="server" />
</asp:Content>