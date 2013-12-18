<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EgitimlerKontrol.ascx.cs"
            Inherits="EnrollPortal.Admin.Kontroller.EgitimlerKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<link href="../Theme/Styles/AdminKontroller.css" rel="stylesheet" type="text/css" />
<asp:MultiView ID="MultiView2" runat="server">
    <asp:View ID="View3" runat="server">
        <table class="AnaTablo">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButtonYeniEkle" runat="server" ImageUrl="../Theme/Images/icon/yeni_ekle_label.png"
                                             OnClick="ImageButtonYeniEkle_Click" />
                        </td>
                    </tr>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <tr>
                        <td>
                            <table class="AnaTablo">
                                <tr>
                                    <td colspan="3" style="height: 20px; color: White; background: #5D7B9D; padding-left: 5px; width: 700px;">
                                        <asp:Label ID="LabelBaslik" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Başlık
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxBaslik" runat="server" Width="550px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        İçerik
                                    </td>
                                    <td style="vertical-align: top;">
                                        :
                                    </td>
                                    <td style="vertical-align: top;">
                                        <telerik:RadEditor ID="RadEditorIcerik" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
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
                                    <td>
                                        Başlangıç Tarihi
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="RadDateTimePickerBaslangicTarihi" runat="server" Culture="tr-TR">
                                            <TimeView CellSpacing="-1" Culture="tr-TR" Interval="01:00:00">
                                            </TimeView>
                                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd.MM.yyyy" DisplayDateFormat="dd.MM.yyyy" EnableSingleInputRendering="True"
                                                       LabelWidth="64px">
                                            </DateInput>
                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bitiş Tarihi
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="RadDateTimePickerBitisTarihi" runat="server" Culture="tr-TR">
                                            <TimeView CellSpacing="-1" Culture="tr-TR" Interval="01:00:00">
                                            </TimeView>
                                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd.MM.yyyy" DisplayDateFormat="dd.MM.yyyy" EnableSingleInputRendering="True"
                                                       LabelWidth="64px">
                                            </DateInput>
                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Katılımcı Sayısı
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxKatilimciSayisi" runat="server"></asp:TextBox>
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
                                </tr>
                                <tr>
                                    <td>
                                        Personel Talebi Mi?
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBoxPersonelTalebiMi" runat="server" Text="Evet" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxBaslik"
                                                                    Display="None" ErrorMessage="Başlık giriniz." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="g1"
                                                               BorderStyle="None" DisplayMode="List" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButtonKaydet" runat="server" ImageUrl="../Theme/Images/kaydet.png"
                                                         OnClick="ImageButtonKaydet_Click" ValidationGroup="g1" />
                                        <asp:ImageButton ID="ImageButtonIptal" runat="server" ImageUrl="../Theme/Images/iptal.png"
                                                         OnClick="ImageButtonIptal_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:HiddenField ID="HiddenFieldId" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:View>
            </asp:MultiView>
            <tr>
                <td>
                    <uc1:MesajKontrol ID="MesajKontrol2" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="AnaTablo">
                        <tr>
                            <td style="text-align: right; width: 700px;">
                                Kategori :
                                <asp:DropDownList ID="DropDownListEgitimKategorileriGridView" runat="server" AutoPostBack="True"
                                                  OnSelectedIndexChanged="DropDownListEgitimKategorileriGridView_SelectedIndexChanged"
                                                  Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewVeriler" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                              CellPadding="4" ForeColor="#333333" GridLines="None" DataSourceID="EntityDataSource1"
                                              Width="700px" OnRowCommand="GridViewVeriler_RowCommand" OnRowDataBound="GridViewVeriler_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="75" HeaderText="İşlemler" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="../Theme/Images/icon/edit.png"
                                                                             CommandArgument='<%#                                        Bind("Id") %>' CommandName="Guncelle" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="../Theme/Images/icon/cop.png"
                                                                             CommandArgument='<%#Bind("Id") %>' CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="75px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Baslik" HeaderText="Başlık" SortExpression="Baslik">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BaslangicTarihi" HeaderText="Başlangıç Tarihi" SortExpression="BaslangicTarihi">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BitisTarihi" HeaderText="Bitiş Tarihi" SortExpression="BitisTarihi">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="75" HeaderText="Başvuru Sayısı" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LabelBasvuruSayisi" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="75px" />
                                        </asp:TemplateField>
                                        <asp:CheckBoxField DataField="Durum" HeaderText="Durum" SortExpression="Durum">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:CheckBoxField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=EnrollPortalEntities"
                                                      DefaultContainerName="EnrollPortalEntities" EnableFlattening="False">
                                </asp:EntityDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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