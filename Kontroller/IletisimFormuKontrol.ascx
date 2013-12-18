<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IletisimFormuKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.IletisimFormuKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .AnaTablo { width: 100%; }

    .Sol {
        min-width: 70px;
        padding: 2px;
        vertical-align: top;
        text-align: left;
    }

    .Orta {
        min-width: 10px;
        padding: 2px;
        text-align: center;
        vertical-align: top;
    }

    .Sag {
        width: 100%;
        vertical-align: top;
        text-align: left;
    }
</style>
<div class="modulforum">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            ÖNERİ / İSTEK / ŞİKAYET</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div class="moduldetayicerik2">
            <table class="AnaTablo">
                <tr>
                    <td class="Sol">
                        Konu Tipi
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
                                           Language="tr-Tr" Width="100%">
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
                                                    Display="None" ErrorMessage="Konu tipini belirtiniz." ForeColor="Red" InitialValue="0"
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
                        <asp:LinkButton ID="LinkButtonGonder" runat="server" CssClass="button" 
                                        ValidationGroup="g" onclick="LinkButtonGonderClick">Gönder</asp:LinkButton>
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
        </div>
    </div>
</div>