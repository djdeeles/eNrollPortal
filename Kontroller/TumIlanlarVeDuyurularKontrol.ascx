<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TumIlanlarVeDuyurularKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.TumIlanlarVeDuyurularKontrol" %>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            PERSONEL'DEN</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div>
            <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1">
                <ItemTemplate>
                    <table class="list">
                        <tr>
                            <td style="vertical-align: top; width: 135px;">
                                <div class="listimage">
                                    <a class="habermanset" href='<%#Eval
                                                                                                   ("Url") %>'>
                                        <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("Resim") %>' Width="125" />
                                    </a>
                                </div>
                            </td>
                            <td style="vertical-align: top;">
                                <div class="listdetay">
                                    <div class="listbaslik">
                                        <a class="habermanset" href='<%#Eval("Url") %>'>
                                            <%#Eval("Baslik") %>
                                        </a>
                                    </div>
                                    <div class="listtarih">
                                        <%#Eval("Tarih") %>
                                        <b>Ekleyen:</b><%#Eval("Ekleyen") %>
                                    </div>
                                    <br />
                                    <div class="listozet">
                                        <%#Eval("Ozet") %></div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <table class="listalternate">
                        <tr>
                            <td style="vertical-align: top; width: 135px;">
                                <div class="listimage">
                                    <a class="habermanset" href='<%#Eval("Url") %>'>
                                        <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("Resim") %>' Width="125" />
                                    </a>
                                </div>
                            </td>
                            <td style="vertical-align: top;">
                                <div class="listdetay">
                                    <div class="listbaslik">
                                        <a class="habermanset" href='<%#Eval("Url") %>'>
                                            <%#Eval("Baslik") %>
                                        </a>
                                    </div>
                                    <div class="listtarih">
                                        <%#Eval("Tarih") %>
                                        <b>Ekleyen:</b><%#Eval("Ekleyen") %></div>
                                    <br />
                                    <div class="listozet">
                                        <%#Eval("Ozet") %></div>
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