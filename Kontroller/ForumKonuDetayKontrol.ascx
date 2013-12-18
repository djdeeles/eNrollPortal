<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumKonuDetayKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.ForumKonuDetayKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="modulforum">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            FORUMLAR</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div>
            <asp:Panel ID="PanelKonu" runat="server">
                <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                    <tr class="forumheader">
                        <td style="padding-left: 10px;">
                            <asp:Label ID="LabelBaslik" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="listalternate">
                    <tr>
                        <td style="vertical-align: top; width: 125px; padding-left: 10px;">
                            <div class="forumyazar">
                                <asp:Label ID="LabelKonuyuEkleyen" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="forumyazarimg">
                                <asp:Image ID="ImageResim" runat="server" Width="125" />
                            </div>
                        </td>
                        <td style="vertical-align: top; padding-left: 10px; padding-right: 10px;">
                            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                <tr>
                                    <td style="height: 15px;">
                                        <asp:Label ID="LabelTarih" runat="server" CssClass="listtarih"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Panel ID="PanelKonuGuncelle" runat="server">
                                            <asp:LinkButton ID="LinkButtonDuzenle" runat="server" CssClass="button" OnClick="LinkButtonDuzenle_Click">Düzenle</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonSil" runat="server" CssClass="button" OnClick="LinkButtonSil_Click"
                                                            OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');">Sil</asp:LinkButton>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-right: 10px;">
                                        <asp:Label ID="LabelIcerik" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-right: 10px;" class="duzenlemetarihi">
                                        <asp:Label ID="LabelDuzenleme" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelKonuDuzenle" runat="server">
                <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                    <tr>
                        <td style="padding: 10px;" colspan="2">
                            <asp:TextBox ID="TextBoxBaslik" runat="server" Width="60%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; vertical-align: top; width: 60%;">
                            <telerik:RadEditor ID="RadEditorKonu" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                                               Language="tr-Tr" Width="100%" ToolsFile="~/Kutuphaneler/RadEditorTolls.xml" EditModes="Design"
                                               Height="200px">
                                <CssFiles>
                                    <telerik:EditorCssFile Value="" />
                                </CssFiles>
                                <Content>                            
                                </Content>
                            </telerik:RadEditor>
                        </td>
                        <td style="vertical-align: top; padding-left: 20px; padding-top: 10px; text-align: left; width: 40%;">
                            <asp:LinkButton ID="LinkButtonKonuKaydet" runat="server" CssClass="button" ValidationGroup="g2"
                                            OnClick="LinkButtonKonuKaydet_Click">Konu Güncelle</asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonKonuIptal" runat="server" CssClass="button" OnClick="LinkButtonKonuIptal_Click">İptal</asp:LinkButton>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Başlık giriniz."
                                                        ControlToValidate="TextBoxBaslik" Display="None" ValidationGroup="g2"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="İçerik giriniz."
                                                        ControlToValidate="RadEditorKonu" Display="None" ValidationGroup="g2"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="g2" />
                            <uc1:MesajKontrol ID="MesajKontrol2" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1"
                          DataKeyNames="Id" OnItemDataBound="ListViewVeriler_ItemDataBound" OnItemEditing="ListViewVeriler_ItemEditing"
                          OnItemCanceling="ListViewVeriler_ItemCanceling" OnItemUpdating="ListViewVeriler_ItemUpdating"
                          OnItemDeleting="ListViewVeriler_ItemDeleting">
                <LayoutTemplate>
                    <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                        <tr>
                            <td colspan="5" style="margin-bottom: 15px;">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <table class="list">
                        <tr>
                            <td style="vertical-align: top; width: 125px; padding-left: 10px;">
                                <div class="forumyazar">
                                    <%#Eval
                                                                                                   ("Ekleyen") %>
                                </div>
                                <div class="forumyazarimg">
                                    <asp:Image ID="ImageResim" runat="server" Width="125" ImageUrl='<%#Eval("Resim") %>' />
                                </div>
                            </td>
                            <td style="vertical-align: top; padding-left: 10px; padding-right: 10px;">
                                <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                    <tr>
                                        <td style="height: 15px;" class="listtarih">
                                            <b>Mesaj Tarihi:</b>
                                            <%#Eval("Tarih") %>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CssClass="button" CommandName="Edit">Düzenle</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="button" CommandName="Delete"
                                                            OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');">Sil</asp:LinkButton>
                                            <asp:HiddenField ID="HiddenFieldKaydedenKullaniciId" runat="server" Value='<%#                Eval("KaydedenKullaniciId") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <%#Eval("Mesaj") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="duzenlemetarihi">
                                            <%#Eval("Duzenleme") %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <table class="listalternate">
                        <tr>
                            <td style="vertical-align: top; width: 125px; padding-left: 10px;">
                                <div class="forumyazar">
                                    <%#Eval("Ekleyen") %>
                                </div>
                                <div class="forumyazarimg">
                                    <asp:Image ID="ImageResim" runat="server" Width="125" ImageUrl='<%#Eval("Resim") %>' />
                                </div>
                            </td>
                            <td style="vertical-align: top; padding-left: 10px; padding-right: 10px;">
                                <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                    <tr>
                                        <td style="height: 15px;" class="listtarih">
                                            <b>Mesaj Tarihi:</b>
                                            <%#Eval("Tarih") %>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CssClass="button" CommandName="Edit">Düzenle</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="button" CommandName="Delete"
                                                            OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');">Sil</asp:LinkButton>
                                            <asp:HiddenField ID="HiddenFieldKaydedenKullaniciId" runat="server" Value='<%#                Eval("KaydedenKullaniciId") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <%#Eval("Mesaj") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="duzenlemetarihi">
                                            <%#Eval("Duzenleme") %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%; padding: 10px;">
                        <tr>
                            <td style="vertical-align: top; padding-left: 10px; width: 60%;">
                                <telerik:RadEditor ID="RadEditorMesaj" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                                                   Language="tr-Tr" Width="100%" ToolsFile="~/Kutuphaneler/RadEditorTolls.xml" EditModes="Design"
                                                   Height="200px" Content='<%#Bind("Mesaj") %>'>
                                    <CssFiles>
                                        <telerik:EditorCssFile Value="" />
                                    </CssFiles>
                                </telerik:RadEditor>
                            </td>
                            <td style="vertical-align: top; padding-left: 10px; text-align: left; width: 40%;">
                                <asp:LinkButton ID="LinkButtonCevapGuncelle" runat="server" CssClass="button" ValidationGroup="g3"
                                                CommandName="Update">Cevap Güncelle</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonCevapIptal" runat="server" CssClass="button" CommandName="Cancel">İptal</asp:LinkButton>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cevap yazınız!"
                                                            ControlToValidate="RadEditorMesaj" Display="Dynamic" ValidationGroup="g3"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <div class="modulicerik2">
                        Bu konuya henüz cevap yazılmadı.
                    </div>
                </EmptyDataTemplate>
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
            <% } %>
            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%; padding: 10px;">
                <tr>
                    <td style="vertical-align: top; width: 60%;">
                        <telerik:RadEditor ID="RadEditorMesaj" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                                           Language="tr-Tr" Width="100%" ToolsFile="~/Kutuphaneler/RadEditorTolls.xml" EditModes="Design"
                                           Height="200px">
                            <CssFiles>
                                <telerik:EditorCssFile Value="" />
                            </CssFiles>
                            <Content>
                            </Content>
                        </telerik:RadEditor>
                    </td>
                    <td style="vertical-align: top; padding-left: 10px; text-align: left; width: 20%;">
                        <asp:LinkButton ID="LinkButtonCevapYaz" runat="server" CssClass="button" OnClick="LinkButtonCevapYaz_Click"
                                        ValidationGroup="g1">Cevap Yaz</asp:LinkButton>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cevap yazınız!"
                                                    ControlToValidate="RadEditorMesaj" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: right; width: 20%; vertical-align: top;">
                        <a href="../Portal/ForumKonuEkle.aspx" class="button">Yeni Konu Ekle</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>