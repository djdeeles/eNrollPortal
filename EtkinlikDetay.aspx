<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="EtkinlikDetay.aspx.cs" Inherits="EnrollPortal.EtkinlikDetay" %>

<%@ Register Src="Kontroller/EtkinlikDetayKontrol.ascx" TagName="EtkinlikDetayKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:EtkinlikDetayKontrol ID="EtkinlikDetayKontrol1" runat="server" />
</asp:Content>