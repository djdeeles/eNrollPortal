<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="Sayfa.aspx.cs" Inherits="EnrollPortal.Sayfa" %>

<%@ Register Src="Kontroller/IcerikDetayKontrol.ascx" TagName="IcerikDetayKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:IcerikDetayKontrol ID="IcerikDetayKontrol1" runat="server" />
</asp:Content>