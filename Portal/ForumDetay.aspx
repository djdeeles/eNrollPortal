<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="ForumDetay.aspx.cs" Inherits="EnrollPortal.Portal.ForumDetay" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../Kontroller/ForumKonuDetayKontrol.ascx" TagName="ForumKonuDetayKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:ForumKonuDetayKontrol ID="ForumKonuDetayKontrol1" runat="server" />
</asp:Content>