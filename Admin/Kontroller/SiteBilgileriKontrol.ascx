<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteBilgileriKontrol.ascx.cs"
            Inherits="EnrollPortal.Admin.Kontroller.SiteBilgileriKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<link href="../Theme/Styles/AdminKontroller.css" rel="stylesheet" type="text/css" />
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
        <table class="AnaTablo">
            <tr>
                <td>
                    <table class="AnaTablo">
                        <tr>
                            <td colspan="3" style="height: 20px; color: White; background: #5D7B9D; padding-left: 5px; width: 700px;">
                                <asp:Label ID="LabelBaslik" runat="server" Text="Site Bilgileri Düzenle"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Page Title
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxPageTitle" runat="server" Width="550px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Keywords
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxKeywords" runat="server" TextMode="MultiLine" Width="550px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Description
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxDescription" runat="server" TextMode="MultiLine" Width="550px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">İletişim E-Posta
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxIletisimEPosta" runat="server" TextMode="MultiLine" Width="550px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Footer
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <telerik:RadEditor ID="RadEditorIcerik" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                                                   Language="tr-Tr" Width="550px" Height="600px">
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
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:ImageButton ID="ImageButtonKaydet" runat="server" ImageUrl="../Theme/Images/kaydet.png"
                                                 OnClick="ImageButtonKaydet_Click" ValidationGroup="g1" />
                                <asp:ImageButton ID="ImageButtonIptal" runat="server" ImageUrl="../Theme/Images/iptal.png"
                                                 OnClick="ImageButtonIptal_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldId" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="View2" runat="server">
        <table class="AnaTablo">
            <tr>
                <td style="font-size: 16px; text-align: center; width: 775px;">
                    <p>
                        Bu bölümü görüntülemek için yetkiniz bulunmamaktadır.<br />
                        Lütfen site yönetici ile iletişime geçiniz.
                    </p>
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>