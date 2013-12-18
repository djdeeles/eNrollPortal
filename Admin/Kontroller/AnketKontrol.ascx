<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnketKontrol.ascx.cs"
            Inherits="EnrollPortal.Admin.Kontroller.AnketKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<link href="../../Theme/Styles/AdminKontroller.css" rel="stylesheet" type="text/css" />
<asp:MultiView ID="MultiView2" runat="server">
    <asp:View ID="View4" runat="server">
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
                                    <td colspan="4" style="height: 20px; color: White; background: #5D7B9D; padding-left: 5px; width: 700px;">
                                        <asp:Label ID="LabelSoruBaslik" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        Soru
                                    </td>
                                    <td style="width: 10px;">
                                        :
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:TextBox ID="TextBoxSoru" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 390px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxSoru"
                                                                    ErrorMessage="Gerekli." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Durum
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBoxSoruDurum" runat="server" Text="Aktif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
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
                                    <td colspan="2">
                                        <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:HiddenField ID="HiddenFieldSoruId" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <tr>
                        <td>
                            <table class="AnaTablo">
                                <tr>
                                    <td colspan="4" style="height: 20px; color: White; background: #5D7B9D; padding-left: 5px;
                                        width: 700px;">
                                        <asp:Label ID="LabelCevapBaslik" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        Cevap
                                    </td>
                                    <td style="width: 10px;">
                                        :
                                    </td>
                                    <td style="width: 00px;">
                                        <asp:TextBox ID="TextBoxCevap" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 390px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxCevap"
                                                                    ErrorMessage="Gerekli." ForeColor="Red" ValidationGroup="g2"></asp:RequiredFieldValidator>
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
                                        <asp:CheckBox ID="CheckBoxCevapDurum" runat="server" Text="Aktif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Oy Sayısı
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxOysayisi" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxOysayisi"
                                                                        ErrorMessage="Sadece tamsayı giriniz." ForeColor="Red" ValidationExpression="\d+"
                                                                        ValidationGroup="g2"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:ImageButton ID="ImageButtonKaydet1" runat="server" ImageUrl="../Theme/Images/kaydet.png"
                                                         OnClick="ImageButtonKaydet1_Click" ValidationGroup="g2" />
                                        <asp:ImageButton ID="ImageButtonIptal1" runat="server" ImageUrl="../Theme/Images/iptal.png"
                                                         OnClick="ImageButtonIptal1_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <uc1:MesajKontrol ID="MesajKontrol2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:HiddenField ID="HiddenFieldSoruId1" runat="server" />
                                        <asp:HiddenField ID="HiddenFieldCevapId" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:View>
            </asp:MultiView>
            <tr>
                <td>
                    <uc1:MesajKontrol ID="MesajKontrol3" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="AnaTablo">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewAnketSorular" runat="server" AutoGenerateColumns="False"
                                              CssClass="GridViewStyle" DataSourceID="EntityDataSourceAnketSorulari" CellPadding="4"
                                              Width="700px" PageSize="5" ForeColor="#333333" GridLines="None" AllowSorting="True"
                                              OnRowCommand="GridViewAnketSorular_RowCommand" OnRowDataBound="GridViewAnketSorular_RowDataBound">
                                    <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="75" HeaderText="İşlemler" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButtonDuzenle" runat="server" ImageUrl="../Theme/Images/icon/edit.png"
                                                                             CommandArgument='<%#                                        Bind("Id") %>' CommandName="SoruDuzenle" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButtonSil" runat="server" ImageUrl="../Theme/Images/icon/cop.png"
                                                                             CommandArgument='<%#Bind("Id") %>' CommandName="SoruSil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz');" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="75px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Soru" HeaderText="Soru" SortExpression="Soru" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="Durum" HeaderText="Durum" SortExpression="Durum" HeaderStyle-HorizontalAlign="Left"
                                                           ItemStyle-HorizontalAlign="Justify" ItemStyle-Width="50">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Justify" Width="50px" />
                                        </asp:CheckBoxField>
                                        <asp:TemplateField HeaderText="Cevaplar" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:ImageButton ID="ImageButtonEkle" runat="server" ImageUrl="../Theme/Images/icon/yeni_ekle_label.png"
                                                                             CommandArgument='<%#Bind("Id") %>' CommandName="CevapEkle" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridViewAnketCevaplar" runat="server" AutoGenerateColumns="False"
                                                                          CellPadding="4" PageSize="8" Width="100%" ForeColor="#333333" GridLines="None"
                                                                          OnRowCommand="GridViewAnketCevaplar_RowCommand" AllowSorting="True">
                                                                <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Width="75" HeaderText="İşlemler" ItemStyle-VerticalAlign="Top">
                                                                        <ItemTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ImageButtonDuzenle" runat="server" CommandArgument='<%#Bind("Id") %>'
                                                                                                         CommandName="CevapDuzenle" ImageUrl="../Theme/Images/icon/edit.png" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ImageButtonSil" runat="server" CommandArgument='<%#Bind("Id") %>'
                                                                                                         CommandName="CevapSil" ImageUrl="../Theme/Images/icon/cop.png" OnClientClick="return confirm('Silmek istediğinizden emin misiniz');" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Cevap" HeaderText="Cevaplar" SortExpression="Cevap">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="OySayisi" HeaderText="Oy Sayısı" SortExpression="OySayisi"
                                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50">
                                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle CssClass="PagerStyle" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#E2DED6" Font-Bold="True"
                                                                                  ForeColor="#333333" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle CssClass="HeaderStyle" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <EditRowStyle CssClass="EditRowStyle" BackColor="#999999" />
                                                                <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="PagerStyle" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#E2DED6" Font-Bold="True"
                                                      ForeColor="#333333" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="HeaderStyle" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle CssClass="EditRowStyle" BackColor="#999999" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:EntityDataSource ID="EntityDataSourceAnketSorulari" runat="server" ConnectionString="name=EnrollPortalEntities"
                                                      DefaultContainerName="EnrollPortalEntities" EntitySetName="AnketSorulari" Where="it.DilId=@DilId">
                                    <WhereParameters>
                                        <asp:CookieParameter CookieName="EnrollWorkingLanguage" Name="DilId" Type="Int32" />
                                    </WhereParameters>
                                </asp:EntityDataSource>
                                <asp:EntityDataSource ID="EntityDataSourceAnketCevaplari" runat="server" ConnectionString="name=EnrollPortalEntities"
                                                      DefaultContainerName="EnrollPortalEntities" EntitySetName="AnketCevaplari">
                                </asp:EntityDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="View5" runat="server">
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