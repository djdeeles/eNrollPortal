<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="EgitimlerKontrol.aspx.cs" Inherits="EnrollPortal.Admin.EgitimlerKontrol" %>

<%@ Register Src="Kontroller/EgitimlerKontrol.ascx" TagName="EgitimlerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:EgitimlerKontrol ID="EgitimlerKontrol1" runat="server" />
</asp:Content>