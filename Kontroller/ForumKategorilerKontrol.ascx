<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumKategorilerKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.ForumKategorilerKontrol" %>
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
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1">
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                <tr class="forumheader">
                                    <td style="width: 45px; min-width: 45px;">
                                    </td>
                                    <td style="width: 60%; padding-left: 10px;">
                                        Kategoriler
                                    </td>
                                    <td style="width: 10%;">
                                        Konular
                                    </td>
                                    <td style="width: 10%;">
                                        Mesajlar
                                    </td>
                                    <td style="width: 20%;">
                                        Son Mesaj
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="margin-bottom: 15px;">
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table class="list" cellpadding="0" cellspacing="0">
                                <tr style="min-height: 20px; text-align: left;">
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                        <img src="../App_Themes/MainTheme/Images/mesaj.png" style="vertical-align: middle;" />
                                    </td>
                                    <td style="width: 60%;">
                                        <a href='Forumlar.aspx?KategoriId=<%#Eval("Id") %>' style="font-weight: bold;">
                                            <%#Eval("KategoriAdi") %>
                                        </a>
                                        <br />
                                        <%#Eval("Aciklama") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("KonuSayisi") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("MesajSayisi") %>
                                    </td>
                                    <td style="width: 20%;">
                                        <%#Eval("SonMesaj") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table class="listalternate" cellpadding="0" cellspacing="0">
                                <tr style="min-height: 20px; text-align: left;">
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                        <img src="../App_Themes/MainTheme/Images/mesaj.png" style="vertical-align: middle;" />
                                    </td>
                                    <td style="width: 60%;">
                                        <a href='Forumlar.aspx?KategoriId=<%#Eval("Id") %>' style="font-weight: bold;">
                                            <%#Eval("KategoriAdi") %>
                                        </a>
                                        <br />
                                        <%#Eval("Aciklama") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("KonuSayisi") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("MesajSayisi") %>
                                    </td>
                                    <td style="width: 20%;">
                                        <%#Eval("SonMesaj") %>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <div class="modulicerik2">
                                Veri bulunamadı.
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
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:ListView ID="ListViewKonular" runat="server" ItemPlaceholderID="PlaceHolder2">
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                <tr class="forumheader">
                                    <td style="width: 45px; min-width: 45px;">
                                    </td>
                                    <td style="width: 50%; padding-left: 10px;">
                                        Konular
                                    </td>
                                    <td style="width: 10%;">
                                        Başlatan
                                    </td>
                                    <td style="width: 10%;">
                                        Cevaplar
                                    </td>
                                    <td style="width: 10%;">
                                        Okunma
                                    </td>
                                    <td style="width: 20%;">
                                        Son Mesaj
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="margin-bottom: 15px;">
                                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table class="list" cellpadding="0" cellspacing="0">
                                <tr style="min-height: 20px; text-align: left;">
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                        <img src="../App_Themes/MainTheme/Images/mesaj.png" style="vertical-align: middle;" />
                                    </td>
                                    <td style="width: 50%;">
                                        <a href='/Portal/ForumDetay.aspx?KonuId=<%#Eval("Id") %>'>
                                            <%#Eval("Baslik") %>
                                        </a>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("Baslatan") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("CevapSayisi") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("OkunmaSayisi") %>
                                    </td>
                                    <td style="width: 20%;">
                                        <%#Eval("SonMesaj") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table class="listalternate" cellpadding="0" cellspacing="0">
                                <tr style="min-height: 20px; text-align: left;">
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                        <img src="../App_Themes/MainTheme/Images/mesaj.png" style="vertical-align: middle;" />
                                    </td>
                                    <td style="width: 50%;">
                                        <a href='/Portal/ForumDetay.aspx?KonuId=<%#Eval("Id") %>'>
                                            <%#Eval("Baslik") %>
                                        </a>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("Baslatan") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("CevapSayisi") %>
                                    </td>
                                    <td style="width: 10%;">
                                        <%#Eval("OkunmaSayisi") %>
                                    </td>
                                    <td style="width: 20%;">
                                        <%#Eval("SonMesaj") %>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <div class="modulicerik2">
                                Veri bulunamadı.
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <% if (DataPager2.TotalRowCount > 6)
                       {%>
                        <div class="moduldetayicerik2">
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListViewKonular" PageSize="6"
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
                </asp:View>
            </asp:MultiView>
        </div>
        <table style="width: 100%; text-align: right;">
            <tr>
                <td class="modulicerik2">
                    <a href="../Portal/ForumKonuEkle.aspx" class="button">Yeni Konu Ekle</a>
                </td>
            </tr>
        </table>
    </div>
</div>