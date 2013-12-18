<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="IlanVeDuyuruListeleri.aspx.cs" Inherits="EnrollPortal.Portal.IlanVeDuyuruListeleri" %>

<%@ Register Src="../Kontroller/IlanlarVeDuyurularKontrol.ascx" TagName="IlanlarVeDuyurularKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:IlanlarVeDuyurularKontrol ID="IlanlarVeDuyurularKontrol1" runat="server" />
</asp:Content>