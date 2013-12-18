<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IlanVeDuyuruDetayKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.IlanVeDuyuruDetayKontrol" %>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            PERSONEL'DEN</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div class="moduldetayicerik2">
            <table style="width: 100%;">
                <tr>
                    <td style="vertical-align: top; width: 280px;">
                        <div class="Images">
                            <script type="text/javascript">
                                function ResimDegistir(Resim) {
                                    document.getElementById('<%= ImageHaberGorsel.ClientID %>').src = "../../" + Resim;
                                }
                            </script>
                            <div id='preview' style="width: 280px; height: 220px; overflow: hidden; padding-bottom: 3px;">
                                <asp:Image ID="ImageHaberGorsel" runat="server" Width="280" />
                            </div>
                            <asp:DataList ID="DataListResimler" runat="server" RepeatColumns="2">
                                <ItemTemplate>
                                    <div onclick="ResimDegistir('<%#                                        Eval("Gorsel") %>')" style="width: 134px; height: 100px; overflow: hidden;">
                                        <img src='../../<%#Eval("GorselThumbnail") %>' width="134" style="padding: 3px;" />
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <div class="Detay">
                            <div class="Baslik">
                                <asp:Label ID="LabelBaslik" runat="server" Text=""></asp:Label></div>
                            <div class="Tarih">
                                <asp:Label ID="LabelTarih" runat="server" Text=""></asp:Label></div>
                            <div class="Icerik">
                                <asp:Label ID="LabelIcerik" runat="server" Text=""></asp:Label></div>
                        </div>
                    </td>
                </tr>
            </table>
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