<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FotoAlbumDetayKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.FotoAlbumDetayKontrol" %>
<link href="../App_Themes/MainTheme/Styles/prettyPhoto.css" rel="stylesheet" type="text/css" />
<script src="../App_Themes/MainTheme/Scripts/jquery.min.js" type="text/javascript"></script>
<script src="../App_Themes/MainTheme/Scripts/jquery.prettyPhoto.js" type="text/javascript"></script>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("a[rel^='prettyPhoto']").prettyPhoto({ social_tools: false });
    });
</script>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            FOTOĞRAF ALBÜMLERİ</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div class="moduldetayicerik2">
            <div id="gallery" style="height: 100%; width: 100%;">
                <asp:ListView ID="ListView1" runat="server" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder"
                              ItemPlaceholderID="itemPlaceHolder">
                    <LayoutTemplate>
                        <table>
                            <tr>
                                <td>
                                    <ul>
                                        <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </LayoutTemplate>
                    <GroupTemplate>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </GroupTemplate>
                    <ItemTemplate>
                        <li>
                            <div>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval(
                                                                                                  "Id") %>' Font-Bold="true"
                                               rel="prettyPhoto[pp_gal]" Font-Size="14px" OnDataBinding="HyperLink1_DataBinding">
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Id") %>' OnDataBinding="Image1_DataBinding" />
                                </asp:HyperLink>
                                <p>
                                    <%#Eval("GorselAdi") %>
                                </p>
                            </div>
                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div class="modulicerik2">
                            Veri bulunamadı.
                        </div>
                    </EmptyDataTemplate>
                </asp:ListView>
                <% if (DataPager1.TotalRowCount > 19)
                   { %>
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="20"
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
                <asp:Menu ID="MenuPersonelAlbumKategoriler" runat="server" Orientation="Vertical"
                          StaticSubMenuIndent="" MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
</div>