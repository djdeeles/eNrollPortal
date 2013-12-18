<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AramaKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.AramaKontrol" %>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            ARAMA</p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div>
            <asp:ListView ID="ListViewVeriler" runat="server" ItemPlaceholderID="PlaceHolder1">
                <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" class="list">
                        <tr>
                            <td>
                                <%#Eval
                                                                                                   ("Ara") %>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <table cellpadding="0" cellspacing="0" class="listalternate">
                        <tr>
                            <td>
                                <%#Eval("Ara") %>
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
            <% if (DataPager1.TotalRowCount > 14)
               {%>
                <div class="moduldetayicerik2">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewVeriler" PageSize="15"
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
                DETAYLI ARAMA</p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <div style="margin-bottom: 7px;">
                    <asp:TextBox ID="TextBoxAra" runat="server" Width="100%"></asp:TextBox>
                </div>
                <div style="margin-bottom: 7px;">
                    <asp:DropDownList ID="DropDownListKategoriler" runat="server" Width="100%">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButtonAra" runat="server" ValidationGroup="garama" OnClick="LinkButtonAra_Click"
                                    CssClass="button">Ara</asp:LinkButton>
                </div>
                <div style="margin-bottom: 7px; text-align: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ne aramıştınız?"
                                                ValidationGroup="garama" ControlToValidate="TextBoxAra" InitialValue="Ne aramıştınız?"
                                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ne aramıştınız?"
                                                ValidationGroup="garama" ControlToValidate="TextBoxAra" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
    </div>
</div>