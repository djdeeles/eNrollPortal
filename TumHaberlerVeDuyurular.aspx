<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="TumHaberlerVeDuyurular.aspx.cs" Inherits="EnrollPortal.TumHaberlerVeDuyurular" %>

<%@ Register Src="Kontroller/TumHaberlerVeDuyurularKontrol.ascx" TagName="TumHaberlerVeDuyurularKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:TumHaberlerVeDuyurularKontrol ID="TumHaberlerVeDuyurularKontrol1" runat="server" />
</asp:Content>