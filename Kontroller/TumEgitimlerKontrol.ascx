<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TumEgitimlerKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.TumEgitimlerKontrol" %>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            EĞİTİMLER</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div>
            <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1"
                          OnItemCommand="ListViewVeriler_ItemCommand" OnItemDataBound="ListViewVeriler_ItemDataBound">
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
                <asp:Menu ID="MenuEgitimKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
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