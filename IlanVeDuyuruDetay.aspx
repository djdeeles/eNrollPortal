<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="IlanVeDuyuruDetay.aspx.cs" Inherits="EnrollPortal.IlanVeDuyuruDetay" %>

<%@ Register Src="Kontroller/IlanVeDuyuruDetayKontrol.ascx" TagName="IlanVeDuyuruDetayKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:IlanVeDuyuruDetayKontrol ID="IlanVeDuyuruDetayKontrol1" runat="server" />
</asp:Content>