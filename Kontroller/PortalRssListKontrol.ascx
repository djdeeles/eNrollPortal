<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortalRssListKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.PortalRssListKontrol" %>
<div class="modulforum">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            WEB SİTESİNDEN SON GELİŞMELER</p>
        <p class="modulbasliktextright">
            <a href="../SonGelismeler.aspx">Web Sitesinden Son Gelişmeler</a>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div>
            <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1">
                <LayoutTemplate>
                    <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                        <tr>
                            <td style="margin-bottom: 15px;">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%#Eval
                                                                                                   ("Liste") %>
                    <%-- <table class="list" cellpadding="0" cellspacing="0">
                        <tr style="min-height: 20px; text-align: left;">
                            <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                <%# Eval("icon") %>                               
                            </td>
                            <td style="width: 100%;">
                                <a href='<%# Eval("link") %>' style="font-weight: bold;">
                                    <%# Eval("title") %>
                                </a>
                            </td>
                        </tr>
                    </table>--%>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <%#Eval("Liste") %>
                    <%--  <table class="listalternate" cellpadding="0" cellspacing="0">
                        <tr style="min-height: 20px; text-align: left;">
                            <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                <%# Eval("icon") %>
                            </td>
                            <td style="width: 100%;">
                                <a href='<%# Eval("link") %>' style="font-weight: bold;">
                                    <%# Eval("title") %>
                                </a>
                            </td>
                        </tr>
                    </table>--%>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    <div class="modulicerik2">
                        Veri bulunamadı.
                    </div>
                </EmptyDataTemplate>
            </asp:ListView>
            <% if (DataPager1.TotalRowCount > 29)
               {%>
                <div class="moduldetayicerik2">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewVeriler" PageSize="30"
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