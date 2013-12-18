<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="Mesajlar.aspx.cs" Inherits="EnrollPortal.Portal.Mesajlar" %>

<%@ Register Src="../Kontroller/KullaniciMesajlariKontrol.ascx" TagName="KullaniciMesajlariKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:KullaniciMesajlariKontrol ID="KullaniciMesajlariKontrol1" runat="server" />
</asp:Content>