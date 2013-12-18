<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="Iletisim.aspx.cs" Inherits="EnrollPortal.Portal.Iletisim" %>

<%@ Register Src="../Kontroller/IletisimFormuKontrol.ascx" TagName="IletisimFormuKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:IletisimFormuKontrol ID="IletisimFormuKontrol1" runat="server" />
</asp:Content>