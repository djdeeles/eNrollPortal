<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="EtkinliklerKontrol.aspx.cs" Inherits="EnrollPortal.Admin.EtkinliklerKontrol" %>

<%@ Register Src="Kontroller/EtkinliklerKontrol.ascx" TagName="EtkinliklerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:EtkinliklerKontrol ID="EtkinliklerKontrol1" runat="server" />
</asp:Content>