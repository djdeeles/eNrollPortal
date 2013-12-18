<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="IlanKategorileriKontrol.aspx.cs" Inherits="EnrollPortal.Admin.IlanKategorileriKontrol" %>

<%@ Register Src="Kontroller/IlanKategorileriKontrol.ascx" TagName="IlanKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:IlanKategorileriKontrol ID="IlanKategorileriKontrol1" runat="server" />
</asp:Content>