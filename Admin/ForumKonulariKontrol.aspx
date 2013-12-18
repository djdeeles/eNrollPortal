<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="ForumKonulariKontrol.aspx.cs" Inherits="EnrollPortal.Admin.ForumKonulariKontrol" %>

<%@ Register Src="Kontroller/ForumKonulariKontrol.ascx" TagName="ForumKonulariKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:ForumKonulariKontrol ID="ForumKonulariKontrol1" runat="server" />
</asp:Content>