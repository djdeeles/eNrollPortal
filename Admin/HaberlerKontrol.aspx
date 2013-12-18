<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="HaberlerKontrol.aspx.cs" Inherits="EnrollPortal.Admin.HaberlerKontrol" %>

<%@ Register Src="Kontroller/HaberlerKontrol.ascx" TagName="HaberlerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:HaberlerKontrol ID="HaberlerKontrol1" runat="server" />
</asp:Content>