<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="EgitimListeleri.aspx.cs" Inherits="EnrollPortal.Portal.EgitimListeleri" %>

<%@ Register Src="../Kontroller/EgitimlerKontrol.ascx" TagName="EgitimlerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:EgitimlerKontrol ID="EgitimlerKontrol1" runat="server" />
</asp:Content>