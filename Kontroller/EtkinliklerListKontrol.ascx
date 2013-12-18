<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EtkinliklerListKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.EtkinliklerListKontrol" %>
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
    <asp:ListView ID="ListViewEtkinlikler" runat="server">
        <ItemTemplate>
            <div class="mansetetkinlikbasliktext">
                En Yakın Etkinlik
            </div>
            <table class="mansetetkinlik">
                <tr>
                    <td style="vertical-align: top; width: 55px;">
                        <div class="mansetetkinlikimage">
                            <a class="habermanset" href='<%#Eval("Url") %>'>
                                <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("Resim") %>' Style="max-height: 50px; max-width: 50px;" />
                            </a>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <div class="mansetetkinlikdetay">
                            <div class="mansetetkinlikbaslik">
                                <a class="habermanset" href='<%#Eval("Url") %>'>
                                    <%#Eval("Baslik") %>
                                </a>
                            </div>
                            <div class="mansetetkinliktarih">
                                <%#Eval("Tarih") %></div>
                            <br />
                            <div class="mansetetkinlikozet">
                                <%#Eval("Ozet") %></div>
                        </div>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div style="margin-top: 15px;">
                Yakın tarihli bir etkinlik bulunamadı.
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</div>