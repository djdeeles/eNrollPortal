<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="EnrollDestek.aspx.cs" Inherits="EnrollPortal.Admin.EnrollDestek" %>

<%@ Register Src="Kontroller/EnrollDestek.ascx" TagName="EnrollDestek" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:EnrollDestek ID="EnrollDestek1" runat="server" />
</asp:Content>