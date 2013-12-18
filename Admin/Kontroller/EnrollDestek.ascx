<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnrollDestek.ascx.cs"
            Inherits="EnrollPortal.Admin.Kontroller.EnrollDestek" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<style type="text/css">
    .AnaTablo {
        width: 700px;
        font-size: 11px;
    }

    .Sol {
        width: 140px;
        padding: 2px;
        vertical-align: top;
        text-align: left;
    }

    .Orta {
        width: 10px;
        padding: 2px;
        text-align: center;
        vertical-align: top;
    }

    .Sag {
        width: 550px;
        vertical-align: top;
        text-align: left;
    }

    .Baslik {
        font-size: 14px;
        font-weight: bold;
        height: 35px;
        text-align: left;
    }
</style>
<table class="AnaTablo">
    <tr>
        <td colspan="3" class="Baslik">
            eNroll Destek Talep Formu
        </td>
    </tr>
    <tr>
        <td class="Sol">
            Önem Durumu
        </td>
        <td class="Orta">
            :
        </td>
        <td class="Sag">
            <asp:DropDownList ID="DropDownListOnemDurumlari" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="Sol">
            Konu
        </td>
        <td class="Orta">
            :
        </td>
        <td class="Sag">
            <asp:TextBox ID="TextBoxKonu" runat="server" Width="545px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="Sol">
            Mesaj
        </td>
        <td class="Orta">
            :
        </td>
        <td class="Sag">
            <telerik:RadEditor ID="RadEditorMesaj" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                               Language="tr-Tr" Width="550px">
                <CssFiles>
                    <telerik:EditorCssFile Value="" />
                </CssFiles>
                <DocumentManager DeletePaths="~/FileManager/" MaxUploadFileSize="2048000" UploadPaths="~/FileManager/"
                                 ViewPaths="~/FileManager/" />
                <FlashManager DeletePaths="~/FileManager/" MaxUploadFileSize="2048000" UploadPaths="~/FileManager/"
                              ViewPaths="~/FileManager/" />
                <ImageManager DeletePaths="~/FileManager/" MaxUploadFileSize="2048000" UploadPaths="~/FileManager/"
                              ViewPaths="~/FileManager/" />
                <MediaManager DeletePaths="~/FileManager/" MaxUploadFileSize="2048000" UploadPaths="~/FileManager/"
                              ViewPaths="~/FileManager/" />
                <SilverlightManager DeletePaths="~/FileManager/" MaxUploadFileSize="2048000" UploadPaths="~/FileManager/"
                                    ViewPaths="~/FileManager/" />
                <TemplateManager DeletePaths="~/FileManager/" MaxUploadFileSize="2048000" UploadPaths="~/FileManager/"
                                 ViewPaths="~/FileManager/" />
            </telerik:RadEditor>
        </td>
    </tr>
    <tr>
        <td class="Sol">
        </td>
        <td class="Orta">
        </td>
        <td class="Sag">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownListOnemDurumlari"
                                        Display="None" ErrorMessage="Önem durumunu belirtiniz." ForeColor="Red" InitialValue="0"
                                        ValidationGroup="g"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxKonu"
                                        Display="None" ErrorMessage="Konuyu belirtiniz." ForeColor="Red" ValidationGroup="g"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadEditorMesaj"
                                        Display="None" ErrorMessage="Mesajınızı giriniz." ForeColor="Red" ValidationGroup="g"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="g" />
        </td>
    </tr>
    <tr>
        <td class="Sol">
        </td>
        <td class="Orta">
        </td>
        <td class="Sag">
            <asp:ImageButton ID="ImageButtonGonder" runat="server" ValidationGroup="g" ImageUrl="../Theme/Images/submit.png"
                             OnClick="ImageButtonGonder_Click" />
        </td>
    </tr>
    <tr>
        <td class="Sol">
        </td>
        <td class="Orta">
        </td>
        <td class="Sag">
            <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
        </td>
    </tr>
</table>