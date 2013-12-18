<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="ForumKonuEkle.aspx.cs" Inherits="EnrollPortal.Portal.ForumKonuEkle" %>

<%@ Register Src="../Kontroller/ForumKonulariKontrol.ascx" TagName="ForumKonulariKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:ForumKonulariKontrol ID="ForumKonulariKontrol1" runat="server" />
</asp:Content>