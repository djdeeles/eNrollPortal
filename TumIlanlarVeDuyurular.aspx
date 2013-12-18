<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="TumIlanlarVeDuyurular.aspx.cs" Inherits="EnrollPortal.TumIlanlarVeDuyurular" %>

<%@ Register Src="Kontroller/TumIlanlarVeDuyurularKontrol.ascx" TagName="TumIlanlarVeDuyurularKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:TumIlanlarVeDuyurularKontrol ID="TumIlanlarVeDuyurularKontrol1" runat="server" />
</asp:Content>