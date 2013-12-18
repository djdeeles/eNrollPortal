<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EgitimlerKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.EgitimlerKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            EĞİTİMLER</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div class="moduldetayicerik2">
                    <table style="width: 100%;">
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
                                <asp:LinkButton ID="LinkButtonKaydet" runat="server" CssClass="button" ValidationGroup="g1"
                                                OnClick="LinkButtonKaydet_Click">Kaydet</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonIptalEt" runat="server" CssClass="button" OnClick="LinkButtonIptalEt_Click">İptal</asp:LinkButton>
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
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div>
                    <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1"
                                  OnItemCommand="ListViewVeriler_ItemCommand" 
                                  OnItemDataBound="ListViewVeriler_ItemDataBound" DataKeyNames="Id">
                        <ItemTemplate>
                            <table class="list">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <div class="listdetay">
                                            <div class="listbaslik">
                                                <%#Eval
                                                                                                   ("Baslik") %>
                                            </div>
                                            <div class="listtarih">
                                                <%#Eval("KayitBilgileri") %>
                                            </div>
                                            <br />
                                            <div class="listozet">
                                                <%#Eval("Icerik") %></div>
                                        </div>
                                    </td>
                                    <td style="vertical-align: top; text-align: center; width: 80px;">
                                        <asp:LinkButton ID="LinkButtonKatil" runat="server" CssClass="button" CommandName="Katil"
                                                        CommandArgument='<%#Eval("Id") %>' Text="Katıl"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonIptalEt" runat="server" CssClass="button" CommandName="IptalEt"
                                                        CommandArgument='<%#Eval("Id") %>' Text="İptal Et"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonOturumAc" runat="server" CssClass="button" Text="Oturum Aç"
                                                        PostBackUrl="../Giris/Giris.aspx?ReturnUrl=../TumEgitimler.aspx"></asp:LinkButton>
                                        <asp:HiddenField ID="HiddenFieldEgitimId" runat="server" Value='<%#Eval("Id") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table class="listalternate">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <div class="listdetay">
                                            <div class="listbaslik">
                                                <%#Eval("Baslik") %>
                                            </div>
                                            <div class="listtarih">
                                                <%#Eval("KayitBilgileri") %>
                                            </div>
                                            <br />
                                            <div class="listozet">
                                                <%#Eval("Icerik") %></div>
                                        </div>
                                    </td>
                                    <td style="vertical-align: top; text-align: center; width: 80px;">
                                        <asp:LinkButton ID="LinkButtonKatil" runat="server" CssClass="button" CommandName="Katil"
                                                        CommandArgument='<%#Eval("Id") %>' Text="Katıl"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonIptalEt" runat="server" CssClass="button" CommandName="IptalEt"
                                                        CommandArgument='<%#Eval("Id") %>' Text="İptal Et"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonOturumAc" runat="server" CssClass="button" Text="Oturum Aç"
                                                        PostBackUrl="../Giris/Giris.aspx?ReturnUrl=../TumEgitimler.aspx"></asp:LinkButton>
                                        <asp:HiddenField ID="HiddenFieldEgitimId" runat="server" Value='<%#Eval("Id") %>' />
                                    </div>
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
                    <% } %>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</div>
<div class="kategori">
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                EĞİTİM LİSTELERİ</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuEgitimlerKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                          MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                EĞİTİMLERİM</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuEgitimlerimKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                          MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
</div>