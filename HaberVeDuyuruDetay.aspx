<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="HaberVeDuyuruDetay.aspx.cs" Inherits="EnrollPortal.HaberVeDuyuruDetay" %>

<%@ Register Src="Kontroller/HaberVeDuyuruDetayKontrol.ascx" TagName="HaberVeDuyuruDetayKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:HaberVeDuyuruDetayKontrol ID="HaberVeDuyuruDetayKontrol1" runat="server" />
</asp:Content>