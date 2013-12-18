<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FotoAlbumlerKontrol.ascx.cs"
            Inherits="EnrollPortal.Admin.Kontroller.FotoAlbumlerKontrol" %>
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
                                             OnClick="ImageButtonYeniEkleClick" />
                        </td>
                    </tr>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <tr>
                        <td>
                            <table class="AnaTablo">
                                <tr>
                                    <td colspan="4" style="height: 20px; color: White; background: #5D7B9D; padding-left: 5px; width: 700px;">
                                        <asp:Label ID="LabelBaslik" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        Kategori Adı
                                    </td>
                                    <td style="width: 10px;">
                                        :
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:DropDownList ID="DropDownListFotoAlbumKategorileri" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 390px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownListFotoAlbumKategorileri"
                                                                    Display="Dynamic" ErrorMessage="Gerekli." ForeColor="Red" InitialValue="0" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        Albüm Adı
                                    </td>
                                    <td style="width: 10px;">
                                        :
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:TextBox ID="TextBoxFotoAlbumAdi" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 390px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxFotoAlbumAdi"
                                                                    Display="Dynamic" ErrorMessage="Gerekli." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        Görsel
                                    </td>
                                    <td style="width: 10px;">
                                        :
                                    </td>
                                    <td style="width: 200px;" colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Image ID="ImageGorsel" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBoxGorsel" runat="server" Width="400px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonGorsel" runat="server" ImageUrl="../Theme/Images/icon/imaj_yukle_label.png" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; vertical-align: top;">
                                        Açıklama
                                    </td>
                                    <td style="width: 10px; vertical-align: top;">
                                        :
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:TextBox ID="TextBoxAciklama" runat="server" Width="200px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td style="width: 390px; vertical-align: bottom;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxAciklama"
                                                                    ErrorMessage="Gerekli." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        Durum
                                    </td>
                                    <td style="width: 10px;">
                                        :
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:CheckBox ID="CheckBoxDurum" runat="server" Text="Aktif" />
                                    </td>
                                    <td style="width: 390px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:ImageButton ID="ImageButtonKaydet" runat="server" ImageUrl="../Theme/Images/kaydet.png"
                                                         OnClick="ImageButtonKaydetClick" ValidationGroup="g1" />
                                        <asp:ImageButton ID="ImageButtonIptal" runat="server" ImageUrl="../Theme/Images/iptal.png"
                                                         OnClick="ImageButtonIptalClick" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
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
                                <asp:DropDownList ID="DropDownListFotoAlbumKategorileriGridView" runat="server" AutoPostBack="True"
                                                  OnSelectedIndexChanged="DropDownListKategorilerGridViewSelectedIndexChanged"
                                                  Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewVeriler" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                              CellPadding="4" ForeColor="#333333" GridLines="None" DataSourceID="EntityDataSource1"
                                              Width="700px" OnRowCommand="GridViewVerilerRowCommand">
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
                                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Görsel" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="ImageGorsel" runat="server" ImageUrl='<%#Eval("GorselThumbnail") %>'
                                                                       Width="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FotoAlbumAdi" HeaderText="Foto Albüm Adı" SortExpression="FotoAlbumAdi">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Aciklama" HeaderText="Açıklama" SortExpression="Aciklama">
                                            <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        </asp:BoundField>
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