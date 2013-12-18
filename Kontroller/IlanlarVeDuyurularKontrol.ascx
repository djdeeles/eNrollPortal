<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IlanlarVeDuyurularKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.IlanlarVeDuyurularKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script type="text/javascript">
    function Kategoriler(sender, args) {
        var count = 0;
        var chkListaTipoModificaciones = document.getElementById('<%= CheckBoxListKategoriler.ClientID %>');
        var chkLista = chkListaTipoModificaciones.getElementsByTagName("input");
        for (var i = 0; i < chkLista.length; i++) {
            if (chkLista[i].checked) {
                count = count + 1;
            }
        }
        if (!count > 0) {
            args.IsValid = false;
        } else {
            args.IsValid = true;
        }
    }
</script>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            PERSONEL'DEN</p>
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
                                            Başlık
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBoxBaslik" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            Özet
                                        </td>
                                        <td style="vertical-align: top;">
                                            :
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="TextBoxOzet" runat="server" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
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
                                                               Language="tr-Tr" Width="100%">
                                                <CssFiles>
                                                    <telerik:EditorCssFile Value="" />
                                                </CssFiles>
                                            </telerik:RadEditor>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            Kategoriler
                                        </td>
                                        <td style="vertical-align: top;">
                                            :
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:CheckBoxList ID="CheckBoxListKategoriler" runat="server">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Baslangıç Tarihi
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
                                        <td style="vertical-align: top;">
                                            Görsel 1
                                        </td>
                                        <td style="vertical-align: top;">
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="ImageGorsel1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="FileUploadGorsel1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            Görsel 2
                                        </td>
                                        <td style="vertical-align: top;">
                                            :
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="ImageGorsel2" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="FileUploadGorsel2" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            Görsel 3
                                        </td>
                                        <td style="vertical-align: top;">
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="ImageGorsel3" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="FileUploadGorsel3" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            Görsel 4
                                        </td>
                                        <td style="vertical-align: top;">
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="ImageGorsel4" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="FileUploadGorsel4" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
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
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <i>(Kelimeleri virgül(,) ile ayırınız.)</i>
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
                                            <asp:TextBox ID="TextBoxAnahtarKelimeler" runat="server" Width="500"></asp:TextBox>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxOzet"
                                                                        Display="None" ErrorMessage="Özet metnini giriniz." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadEditorIcerik"
                                                                        Display="None" ErrorMessage="İçerik metnini giriniz." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadDateTimePickerBaslangicTarihi"
                                                                        Display="None" ErrorMessage="Kayıt tarihini giriniz." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="En az bir tane kategori seçmelisiniz."
                                                                 ClientValidationFunction="Kategoriler" ValidationGroup="g1" Display="None" ForeColor="Red"
                                                                 OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
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
                                            <asp:LinkButton ID="LinkButtonKaydet" runat="server" CssClass="button" OnClick="LinkButtonKaydet_Click"
                                                            ValidationGroup="g1">Kaydet</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonIptal" runat="server" CssClass="button" OnClick="LinkButtonIptal_Click">İptal</asp:LinkButton>
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
                                            <asp:HiddenField ID="HiddenFieldBolum" runat="server" />
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
                                                                <%#Eval("Baslik") %>
                                                            </div>
                                                            <div class="listtarih">
                                                                <%#Eval("Tarih") %></div>
                                                            <br />
                                                            <div class="listozet">
                                                                <%#Eval("Ozet") %></div>
                                                        </div>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: center; width: 100px;">
                                                        <asp:LinkButton ID="LinkButtonDuzenle" runat="server" CssClass="button" CommandName="Guncelle"
                                                                        CommandArgument='<%#Eval("Id") %>' Text="Düzenle"></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButtonSil" runat="server" CssClass="button" CommandName="Sil"
                                                                        CommandArgument='<%#Eval("Id") %>' Text="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?'); "></asp:LinkButton>
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
                                                                <%#Eval("Baslik") %>
                                                            </div>
                                                            <div class="listtarih">
                                                                <%#Eval("Tarih") %></div>
                                                            <br />
                                                            <div class="listozet">
                                                                <%#Eval("Ozet") %></div>
                                                        </div>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: center; width: 100px;">
                                                        <asp:LinkButton ID="LinkButtonDuzenle" runat="server" CssClass="button" CommandName="Guncelle"
                                                                        CommandArgument='<%#Eval("Id") %>' Text="Düzenle"></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButtonSil" runat="server" CssClass="button" CommandName="Sil"
                                                                        CommandArgument='<%#Eval("Id") %>' Text="Sil" OnClientClick=" return confirm('Silmek istediğinizden emin misiniz?'); "></asp:LinkButton>
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
                                    <% if (DataPager1.TotalRowCount > 6)
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
                                        </div>
                                    <% }%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="kategori">
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                İLANLAR</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuIlanKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                          MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                DUYURULAR</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuDuyuruKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                          MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
</div>