<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenulerKontrol.ascx.cs"
            Inherits="EnrollPortal.Admin.Kontroller.MenulerKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<link href="../Theme/Styles/AdminKontroller.css" rel="stylesheet" type="text/css" />
<asp:MultiView ID="MultiView2" runat="server">
    <asp:View ID="View3" runat="server">
        <table class="AnaTablo">
            <tr>
                <td colspan="3">
                    <asp:ImageButton ID="ImageButtonYeniEkle" runat="server" ImageUrl="../Theme/Images/icon/yeni_ekle_label.png"
                                     OnClick="ImageButtonYeniEkle_Click" />
                    <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="Sol" style="vertical-align: top;">
                    <asp:Panel ID="PanelMenuler" runat="server" ScrollBars="Auto" Width="140">
                        <telerik:RadTreeView runat="server" ID="RadTreeViewMenuler" EnableDragAndDrop="true"
                                             EnableDragAndDropBetweenNodes="true" MultipleSelect="true" Height="600px" Width="100%"
                                             IsExpanded="True" Skin="Default" OnNodeClick="RadTreeViewMenuler_NodeClick" OnNodeDrop="RadTreeViewMenuler_NodeDrop"
                                             ToolTip="Sıralamayı değiştirmek için sürükleyin ve bırakın.">
                            <DataBindings>
                                <telerik:RadTreeNodeBinding TextField="MenuAdi" Expanded="True" Checkable="true" />
                                <telerik:RadTreeNodeBinding Checkable="false" TextField="MenuAdi" Expanded="true"
                                                            CssClass="rootNode" />
                            </DataBindings>
                        </telerik:RadTreeView>
                    </asp:Panel>
                    <asp:Panel ID="PanelMenulerEdit" runat="server" ScrollBars="Auto" Width="140">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Menü kategorisi seçiniz."
                                                    ControlToValidate="RadTreeViewMenulerEdit" Display="None" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                        <telerik:RadTreeView runat="server" ID="RadTreeViewMenulerEdit" Height="600px" Width="100%"
                                             IsExpanded="True" Skin="Default">
                            <DataBindings>
                                <telerik:RadTreeNodeBinding TextField="MenuAdi" Expanded="True" Checkable="true" />
                                <telerik:RadTreeNodeBinding Checkable="false" TextField="MenuAdi" Expanded="true"
                                                            CssClass="rootNode" />
                            </DataBindings>
                        </telerik:RadTreeView>
                    </asp:Panel>
                </td>
                <td class="Orta" style="vertical-align: top; background-color: #ECECEC;">
                </td>
                <td class="Sag" style="vertical-align: top;">
                    <asp:Panel ID="PanelEdit" runat="server">
                        <table class="Sag">
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="LabelLokasyon" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Menü Adı
                                </td>
                                <td style="width: 10px">
                                    :
                                </td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="TextBoxMenuAdi" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 240px">
                                    <asp:RequiredFieldValidator ID="rfMenuName" runat="server" ControlToValidate="TextBoxMenuAdi"
                                                                ErrorMessage="Menü adı giriniz." ValidationGroup="g1" Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tip
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListTipler" runat="server" Width="200px" AutoPostBack="True"
                                                      OnSelectedIndexChanged="DropDownListTipler_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfMenuName4" runat="server" ControlToValidate="DropDownListTipler"
                                                                ErrorMessage="Menü tipi seçiniz." InitialValue="Seçiniz" ValidationGroup="g1"
                                                                Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Menü Görseli
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxMenuGorsel" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td valign="middle">
                                    <asp:ImageButton ID="ImageButtonMenuGorsel" runat="server" ImageUrl="../Theme/Images/icon/imaj_yukle_label.png" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Menü Hover Görseli
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxMenuGorselHover" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButtonMenuGorselHover" runat="server" ImageUrl="../Theme/Images/icon/imaj_yukle_label.png" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Durum
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxDurum" runat="server" Text="Aktif" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Menüde Göster
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxMenudeGoster" runat="server" Text="Evet" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Başlığı Göster
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxBasligiGoster" runat="server" Text="Evet" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:MultiView ID="MultiView1" runat="server">
                                        <asp:View ID="View1" runat="server">
                                            <table style="width: 550px;">
                                                <tr>
                                                    <td style="width: 100px">
                                                        Url
                                                    </td>
                                                    <td style="width: 10px">
                                                        :
                                                    </td>
                                                    <td style="width: 200px">
                                                        <asp:TextBox ID="TextBoxUrl" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfMenuName0" runat="server" ControlToValidate="TextBoxUrl"
                                                                                    ErrorMessage="Url giriniz." ValidationGroup="g1" Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="View2" runat="server">
                                            <table style="width: 550px;">
                                                <tr>
                                                    <td style="vertical-align: top; width: 100px;">
                                                        Özet
                                                    </td>
                                                    <td style="width: 10px; vertical-align: top;">
                                                        :
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBoxOzet" runat="server" Height="75px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfMenuName3" runat="server" ControlToValidate="TextBoxOzet"
                                                                                    ErrorMessage="Özet giriniz." ValidationGroup="g1" Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">
                                                        İçerik
                                                    </td>
                                                    <td style="width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="width: 200px;">
                                                    </td>
                                                    <td style="width: 240px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <telerik:RadEditor ID="RadEditorIcerik" runat="server" Width="550" Height="600" Language="tr-Tr"
                                                                           CssClass="RadEditorIcerik" ContentAreaMode="Iframe">
                                                            <CssFiles>
                                                                <telerik:EditorCssFile Value="" />
                                                            </CssFiles>
                                                            <DocumentManager ViewPaths="~/FileManager/" UploadPaths="~/FileManager/" DeletePaths="~/FileManager/"
                                                                             MaxUploadFileSize="2048000" />
                                                            <FlashManager ViewPaths="~/FileManager/" UploadPaths="~/FileManager/" DeletePaths="~/FileManager/"
                                                                          MaxUploadFileSize="2048000" />
                                                            <ImageManager ViewPaths="~/FileManager/" UploadPaths="~/FileManager/" DeletePaths="~/FileManager/"
                                                                          MaxUploadFileSize="2048000" />
                                                            <MediaManager ViewPaths="~/FileManager/" UploadPaths="~/FileManager/" DeletePaths="~/FileManager/"
                                                                          MaxUploadFileSize="2048000" />
                                                            <SilverlightManager ViewPaths="~/FileManager/" UploadPaths="~/FileManager/" DeletePaths="~/FileManager/"
                                                                                MaxUploadFileSize="2048000" />
                                                            <TemplateManager ViewPaths="~/FileManager/" UploadPaths="~/FileManager/" DeletePaths="~/FileManager/"
                                                                             MaxUploadFileSize="2048000" />
                                                        </telerik:RadEditor>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Açıklama
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxAciklama" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Anahtar Kelimeler
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxAnahtatrKelimeler" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="g1"
                                                           ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="width: 550px;">
                                        <tr>
                                            <td style="width: 100px">
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButtonKaydet" runat="server" ImageUrl="../Theme/Images/kaydet.png"
                                                                 ValidationGroup="g1" OnClick="ImageButtonKaydet_Click" />
                                                <asp:ImageButton ID="ImageButtonSil" runat="server" ImageUrl="../Theme/Images/Sil.png"
                                                                 OnClick="ImageButtonSil_Click" OnClientClick="return confirm('Bu menü ve bu menüye bağlı bütün alt menüler silinecektir.');" />
                                                <asp:ImageButton ID="ImageButtonIptal" runat="server" ImageUrl="../Theme/Images/iptal.png"
                                                                 OnClick="ImageButtonIptal_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenFieldEditMode" runat="server" />
        <asp:HiddenField ID="HiddenFieldKategoriId" runat="server" />
    </asp:View>
    <asp:View ID="View4" runat="server">
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