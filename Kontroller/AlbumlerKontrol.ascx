<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlbumlerKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.AlbumlerKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            FOTOĞRAF ALBÜMLERİ</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <table cellpadding="0" cellspacing="0" style="width: 100%;">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <tr>
                        <td>
                            <div class="moduldetayicerik2">
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">
                                            Kategori Adı
                                        </td>
                                        <td style="width: 10px;">
                                            :
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:DropDownList ID="DropDownListFotoAlbumKategorileri" runat="server" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
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
                                            <asp:TextBox ID="TextBoxFotoAlbumAdi" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td>
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
                                                        <asp:FileUpload ID="FileUploadResim" runat="server" />
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
                                            <asp:TextBox ID="TextBoxAciklama" runat="server" Width="300px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: bottom;">
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
                                        <td>
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
                                            <asp:LinkButton ID="LinkButtonKaydet" runat="server" ValidationGroup="g1" OnClick="LinkButtonKaydet_Click"
                                                            CssClass="button">Kaydet</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonIptal" runat="server" OnClick="LinkButtonIptal_Click"
                                                            CssClass="button">İptal</asp:LinkButton>
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
                                            <asp:HiddenField ID="HiddenFieldId" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <tr>
                        <td>
                            <div class="moduldetayicerik2">
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">
                                            Görsel Adı
                                        </td>
                                        <td style="width: 10px;">
                                            :
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:TextBox ID="TextBoxGorselAdi" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxGorselAdi"
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
                                                        <asp:Image ID="Image1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="FileUploadGorsel" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="FileUploadGorsel"
                                                                                    Display="Dynamic" ErrorMessage="Gerekli." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="TextBoxGorselAciklama" runat="server" Width="300px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: bottom;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBoxGorselAciklama"
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
                                            <asp:CheckBox ID="CheckBoxGorselDurum" runat="server" Text="Aktif" />
                                        </td>
                                        <td style="">
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
                                            <asp:LinkButton ID="LinkButtonGorselKaydet" runat="server" CssClass="button" ValidationGroup="g2"
                                                            OnClick="LinkButtonGorselKaydet_Click">Kaydet</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonGorselIptal" runat="server" CssClass="button" OnClick="LinkButtonGorselIptal_Click">İptal</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <uc1:MesajKontrol ID="MesajKontrol3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <asp:HiddenField ID="HiddenFieldKategoriId" runat="server" />
                                            <asp:HiddenField ID="HiddenFieldGorselId" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
                    <asp:MultiView ID="MultiView2" runat="server">
                        <asp:View ID="View3" runat="server">
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td>
                                        <div>
                                            <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1"
                                                          OnItemCommand="ListViewVeriler_ItemCommand">
                                                <ItemTemplate>
                                                    <table class="list">
                                                        <tr>
                                                            <td style="vertical-align: top; width: 135px;">
                                                                <div class="listimage">
                                                                    <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("Resim") %>' Width="125" />
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <div class="listdetay">
                                                                    <div class="listbaslik">
                                                                        <%#Eval("AlbumAdi") %>
                                                                    </div>
                                                                    <br />
                                                                    <div class="listozet">
                                                                        <%#Eval("Aciklama") %></div>
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top; text-align: center; width: 300px;">
                                                                <asp:LinkButton ID="LinkButtonAlbumGorselleri" runat="server" CssClass="button" CommandName="AlbumGorselleri"
                                                                                CommandArgument='<%#Eval("Id") %>' Text="Albüm Görselleri"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButtonAlbumDuzenle" runat="server" CssClass="button" CommandName="AlbumDuzenle"
                                                                                CommandArgument='<%#Eval("Id") %>' Text="Albüm Düzenle"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButtonAlbumSil" runat="server" CssClass="button" CommandName="AlbumSil"
                                                                                CommandArgument='<%#Eval("Id") %>' Text="Album Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <table class="listalternate">
                                                        <tr>
                                                            <td style="vertical-align: top; width: 135px;">
                                                                <div class="listimage">
                                                                    <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("Resim") %>' Width="125" />
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <div class="listdetay">
                                                                    <div class="listbaslik">
                                                                        <%#Eval("AlbumAdi") %>
                                                                    </div>
                                                                    <br />
                                                                    <div class="listozet">
                                                                        <%#Eval("Aciklama") %></div>
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top; text-align: center; width: 300px;">
                                                                <asp:LinkButton ID="LinkButtonAlbumGorselleri" runat="server" CssClass="button" CommandName="AlbumGorselleri"
                                                                                CommandArgument='<%#Eval("Id") %>' Text="Albüm Görselleri"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButtonAlbumDuzenle" runat="server" CssClass="button" CommandName="AlbumDuzenle"
                                                                                CommandArgument='<%#Eval("Id") %>' Text="Albüm Düzenle"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButtonAlbumSil" runat="server" CssClass="button" CommandName="AlbumSil"
                                                                                CommandArgument='<%#Eval("Id") %>' Text="Album Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </AlternatingItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div class="modulicerik2">
                                                        Veri bulunamadı.
                                                    </div>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                                        <tr>
                                                            <td style="margin-bottom: 15px;">
                                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                            </asp:ListView>
                                            <% if (DataPager1.TotalRowCount > 5)
                                               {%>
                                            <div class="moduldetayicerik2">
                                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewVeriler" PageSize="6"
                                                               QueryStringField="Sayfa">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                                                    ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                                                                    PreviousPageText="Önceki" ButtonCssClass="button" />
                                                        <asp:NumericPagerField NumericButtonCssClass="button" CurrentPageLabelCssClass="buttonactive" />
                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                                                                    ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                                                                    PreviousPageText="Önceki" ButtonCssClass="button" />
                                                    </Fields>
                                                </asp:DataPager>
                                                <% } %>
                                            </div>
                                                </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <table cellpadding="0" cellspacing="0" class="list">
                                <tr>
                                    <td style="vertical-align: top; text-align: right; padding-right: 20px; padding-bottom: 10px; width: 100%;">
                                        <asp:LinkButton ID="LinkButtonYeniGorselEkle" runat="server" CssClass="button" OnClick="LinkButtonYeniGorselekle_Click">Yeni Görsel Ekle</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:ListView ID="ListViewGorseller" runat="server" ItemPlaceholderID="PlaceHolder2"
                                                          OnItemCommand="ListViewGorseller_ItemCommand">
                                                <ItemTemplate>
                                                    <table class="list">
                                                        <tr>
                                                            <td style="vertical-align: top; width: 135px;">
                                                                <div class="listimage">
                                                                    <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("Gorsel") %>' Width="125" />
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <div class="listdetay">
                                                                    <div class="listbaslik">
                                                                        <%#Eval("GorselAdi") %>
                                                                    </div>
                                                                    <div class="listtarih">
                                                                    </div>
                                                                    <br />
                                                                    <div class="listozet">
                                                                        <%#Eval("Aciklama") %></div>
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top; text-align: center; width: 200px;">
                                                                <asp:LinkButton ID="LinkButtonFotoAlbumGorselDuzenle" runat="server" CssClass="button"
                                                                                CommandName="GorselDuzenle" CommandArgument='<%#Eval("Id") %>' Text="Görsel Düzenle"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButtonFotoAlbumGorselSil" runat="server" CssClass="button"
                                                                                CommandName="GorselSil" CommandArgument='<%#Eval("Id") %>' Text="Görsel Sil"
                                                                                OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <table class="listalternate">
                                                        <tr>
                                                            <td style="vertical-align: top; width: 135px;">
                                                                <div class="listimage">
                                                                    <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("Gorsel") %>' Width="125" />
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <div class="listdetay">
                                                                    <div class="listbaslik">
                                                                        <%#Eval("GorselAdi") %>
                                                                    </div>
                                                                    <div class="listtarih">
                                                                    </div>
                                                                    <br />
                                                                    <div class="listozet">
                                                                        <%#Eval("Aciklama") %></div>
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top; text-align: center; width: 200px;">
                                                                <asp:LinkButton ID="LinkButtonFotoAlbumGorselDuzenle" runat="server" CssClass="button"
                                                                                CommandName="GorselDuzenle" CommandArgument='<%#Eval("Id") %>' Text="Görsel Düzenle"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButtonFotoAlbumGorselSil" runat="server" CssClass="button"
                                                                                CommandName="GorselSil" CommandArgument='<%#Eval("Id") %>' Text="Görsel Sil"
                                                                                OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </AlternatingItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div class="modulicerik2">
                                                        Veri bulunamadı.
                                                    </div>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                                        <tr>
                                                            <td style="margin-bottom: 15px;">
                                                                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                            </asp:ListView>
                                            <% if (DataPager2.TotalRowCount > 5)
                                               {%>
                                            <div class="moduldetayicerik2">
                                                <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListViewGorseller"
                                                               PageSize="6" QueryStringField="Sayfa">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                                                    ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                                                                    PreviousPageText="Önceki" ButtonCssClass="button" />
                                                        <asp:NumericPagerField NumericButtonCssClass="button" CurrentPageLabelCssClass="buttonactive" />
                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                                                                    ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                                                                    PreviousPageText="Önceki" ButtonCssClass="button" />
                                                    </Fields>
                                                </asp:DataPager>
                                                <% } %>
                                            </div>
                                                </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="kategori">
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                KURUM'DAN</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuBelediyeAlbumKategoriler" runat="server" Orientation="Vertical"
                          StaticSubMenuIndent="" MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                PERSONEL'DEN</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuAlbumKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                          MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
</div>