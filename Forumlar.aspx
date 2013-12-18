<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="Forumlar.aspx.cs" Inherits="EnrollPortal.Forumlar" %>

<%@ Register Src="Kontroller/ForumKategorilerKontrol.ascx" TagName="ForumKategorilerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:ForumKategorilerKontrol ID="ForumKategorilerKontrol1" runat="server" />
</asp:Content>