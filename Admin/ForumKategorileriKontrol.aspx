<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="ForumKategorileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.ForumKategorileriKontrol" %>

<%@ Register Src="Kontroller/ForumKategorileriKontrol.ascx" TagName="ForumKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:ForumKategorileriKontrol ID="ForumKategorileriKontrol1" runat="server" />
</asp:Content>