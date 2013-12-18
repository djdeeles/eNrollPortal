<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="TumEgitimler.aspx.cs" Inherits="EnrollPortal.TumEgitimler" %>

<%@ Register Src="Kontroller/TumEgitimlerKontrol.ascx" TagName="TumEgitimlerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:TumEgitimlerKontrol ID="TumEgitimlerKontrol1" runat="server" />
</asp:Content>