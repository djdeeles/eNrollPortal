<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="EtkinlikKategorileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.EtkinlikKategorileriKontrol" %>

<%@ Register Src="Kontroller/EtkinlikKategorileriKontrol.ascx" TagName="EtkinlikKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:EtkinlikKategorileriKontrol ID="EtkinlikKategorileriKontrol1" runat="server" />
</asp:Content>