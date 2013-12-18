<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="MenulerKontrol.aspx.cs" Inherits="EnrollPortal.Admin.MenulerKontrol" %>

<%@ Register Src="Kontroller/MenulerKontrol.ascx" TagName="MenulerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:MenulerKontrol ID="MenulerKontrol1" runat="server" />
</asp:Content>