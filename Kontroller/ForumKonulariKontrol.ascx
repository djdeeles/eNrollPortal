<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumKonulariKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.ForumKonulariKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
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
            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                <tr>
                    <td style="padding: 10px;">
                        Kategori
                    </td>
                    <td style="padding: 10px;" colspan="2">
                        <asp:DropDownList ID="DropDownListForumKategorileri" runat="server" Width="60%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px;">
                        Başlık
                    </td>
                    <td style="padding: 10px;" colspan="2">
                        <asp:TextBox ID="TextBoxBaslik" runat="server" Width="60%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px;">
                        İçerik
                    </td>
                    <td style="padding: 10px; vertical-align: top; width: 60%;">
                        <telerik:RadEditor ID="RadEditorIcerik" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                                           Language="tr-Tr" Width="100%" ToolsFile="~/Kutuphaneler/RadEditorTolls.xml" EditModes="Design"
                                           Height="200px">
                            <CssFiles>
                                <telerik:EditorCssFile Value="" />
                            </CssFiles>
                            <Content>                            
                            
                            </Content>
                        </telerik:RadEditor>
                    </td>
                    <td style="vertical-align: top; padding-left: 20px; padding-top: 10px; text-align: left; width: 40%;">
                        <asp:LinkButton ID="LinkButtonKonuKaydet" runat="server" CssClass="button" ValidationGroup="g2"
                                        OnClick="LinkButtonKonuKaydet_Click">Kaydet</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonKonuIptal" runat="server" CssClass="button" OnClick="LinkButtonKonuIptal_Click">İptal</asp:LinkButton>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Başlık giriniz."
                                                    ControlToValidate="DropDownListForumKategorileri" Display="None" ValidationGroup="g2"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Başlık giriniz."
                                                    ControlToValidate="TextBoxBaslik" Display="None" ValidationGroup="g2"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="İçerik giriniz."
                                                    ControlToValidate="RadEditorIcerik" Display="None" ValidationGroup="g2"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="g2" />
                        <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>