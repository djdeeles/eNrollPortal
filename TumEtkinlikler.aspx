<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="TumEtkinlikler.aspx.cs" Inherits="EnrollPortal.TumEtkinlikler" %>

<%@ Register Src="Kontroller/TumEtkinliklerKontrol.ascx" TagName="TumEtkinliklerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:TumEtkinliklerKontrol ID="TumEtkinliklerKontrol1" runat="server" />
</asp:Content>