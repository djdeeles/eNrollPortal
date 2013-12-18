<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="Ara.aspx.cs" Inherits="EnrollPortal.Ara" %>

<%@ Register Src="Kontroller/AramaKontrol.ascx" TagName="AramaKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:AramaKontrol ID="AramaKontrol1" runat="server" />
</asp:Content>