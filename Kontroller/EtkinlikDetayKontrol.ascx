<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikDetayKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.EtkinlikDetayKontrol" %>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            ETKİNLİKLER</p>
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
                ETKİNLİK TAKVİMİ</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Calendar ID="Calendar1" runat="server" BorderColor="#CCCCCC" CellPadding="4"
                              Font-Bold="False" Font-Size="8pt" SelectionMode="None" Width="100%" OnDayRender="Calendar1_DayRender"
                              BackColor="White" DayNameFormat="Shortest" OnSelectionChanged="Calendar1_SelectionChanged">
                    <DayHeaderStyle BorderWidth="0px" BackColor="#F5F5F5" Font-Bold="False" Font-Size="7pt" />
                    <DayStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="#FFFFFF" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#F9F9F9" BorderColor="#CCCCCC" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                    <WeekendDayStyle BackColor="#F0F0F0" />
                </asp:Calendar>
            </div>
        </div>
    </div>
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                ETKİNLİKLER</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuEtkinlikKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                          MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
</div>