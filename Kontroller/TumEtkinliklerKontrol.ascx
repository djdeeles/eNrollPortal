<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TumEtkinliklerKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.TumEtkinliklerKontrol" %>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            ETKİNLİKLER</p>
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
                                        <%#Eval("Tarih") %></div>
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
                                        <%#Eval("Tarih") %></div>
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