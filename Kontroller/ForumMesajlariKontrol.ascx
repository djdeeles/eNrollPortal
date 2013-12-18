<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumMesajlariKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.ForumMesajlariKontrol" %>
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
        <table class="list" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 70%;">
                    <a href="<%#                                        Eval("Link") %>" target="_parent">
                        <%#Eval("KonuBaslik") %>
                        ...
                        <img style="vertical-align: middle;" src="../App_Themes/MainTheme/Images/mesajagit.png" />
                    </a>
                </td>
                <td style="width: 15%;">
                    <%#Eval("Tarih") %>
                </td>
                <td style="width: 15%;">
                    <%#Eval("KaydedenKullanici") %>
                </td>
            </tr>
        </table>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <table class="listalternate" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 70%;">
                    <a href="<%#Eval("Link") %>" target="_parent" >
                        <%#Eval("KonuBaslik") %>
                        ...
                        <img style="vertical-align: middle;" src="../App_Themes/MainTheme/Images/mesajagit.png" />
                    </a>
                </td>
                <td style="width: 15%;">
                    <%#Eval("Tarih") %>
                </td>
                <td style="width: 15%;">
                    <%#Eval("KaydedenKullanici") %>
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