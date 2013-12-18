<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KullaniciMesajlariKontrol.ascx.cs"
    Inherits="EnrollPortal.Kontroller.KullaniciMesajlariKontrol" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script type="text/javascript">
    var selectedMessages = new Array();
    function SelectAll() {
        selectedMessages = new Array();
        var gelenMesajListesi = document.getElementsByClassName("gelenMesajIslemler");
        for (var i = 0; i < gelenMesajListesi.length; i++) {
            gelenMesajListesi[i].checked = true;
        }
    }
    function DeselectAll() {
        selectedMessages = new Array();
        var gelenMesajListesi = document.getElementsByClassName("gelenMesajIslemler");
        for (var i = 0; i < gelenMesajListesi.length; i++) {
            gelenMesajListesi[i].checked = false;
        }
    }
    
    function ShowActions(data, control, type) { 
        var x = document.getElementById(control); 
        var gelenMesajIslemler = document.getElementById("gelenMesajlarIslemler");
        if (x.checked) {
            if (type == 1) {
                SelectAll();
            } else {
                selectedMessages.push(x.value);
            }
            gelenMesajIslemler.style.display = "block";
            
        } else {
            if (type == 1) DeselectAll();
            gelenMesajIslemler.style.display = "none";
        }
        for (var i = 0; i < selectedMessages.length; i++) {
            //alert("dizi["+i+"]:" + selectedMessages[i]);
        }
        
    } 
    function test() {
        var serilizedData = selectedMessages.toString(); 
        $.ajax({
            type: "POST",
            url: "/Kutuphaneler/WebServicePersonelAra.asmx/Test",
            data: "{selectedMessageIds:'" + serilizedData + "'}",
            //data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            }
        });
    }
    function OnSuccess(response) {
        //alert(response.d);
    }
</script>
<div class="moduldetay">
    <div class="modulbaslik">
        <p class="modulbasliktext">
            MESAJLARIM
        </p>
        <p class="modulbasliktextright">
            <asp:Literal ID="LiteralSiteMap" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="moduldetayicerik">
        <div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="vYeniMesaj" runat="server">
                    <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%; padding: 10px;">
                        <tr>
                            <td colspan="4" style="font-weight: bold; padding-bottom: 10px;">Mesajı Yaz
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 50px; min-width: 50px;">Kime
                            </td>
                            <td style="vertical-align: top; width: 10px; min-width: 10px; text-align: center;">:
                            </td>
                            <td style="vertical-align: top; width: 60%;">
                                <asp:TextBox ID="TextBoxKime" runat="server" Width="60%"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" Enabled="true"
                                    ServicePath="~/Kutuphaneler/WebServicePersonelAra.asmx" TargetControlID="TextBoxKime"
                                    ServiceMethod="PersonelList" MinimumPrefixLength="2" CompletionInterval="10"
                                    ShowOnlyCurrentWordInCompletionListItem="False" FirstRowSelected="True" CompletionSetCount="5"
                                    UseContextKey="True">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td style="vertical-align: top; padding-left: 10px; text-align: left; width: 40%;"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 50px; min-width: 50px;">Başlık
                            </td>
                            <td style="vertical-align: top; width: 10px; min-width: 10px; text-align: center;">:
                            </td>
                            <td style="vertical-align: top; width: 60%;">
                                <asp:TextBox ID="TextBoxBaslik" runat="server" Width="60%"></asp:TextBox>
                            </td>
                            <td style="vertical-align: top; padding-left: 10px; text-align: left; width: 40%;"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Mesaj
                            </td>
                            <td style="vertical-align: top; width: 10px; min-width: 10px; text-align: center;">:
                            </td>
                            <td style="vertical-align: top; width: 60%;">
                                <telerik:RadEditor ID="RadEditorMesaj" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                                    Language="tr-Tr" Width="100%" ToolsFile="~/Kutuphaneler/RadEditorTolls.xml" EditModes="Design"
                                    Height="200px">
                                    <CssFiles>
                                        <telerik:EditorCssFile Value="" />
                                    </CssFiles>
                                    <Content></Content>
                                </telerik:RadEditor>
                            </td>
                            <td style="vertical-align: top; padding-left: 10px; text-align: left; width: 40%;">
                                <asp:LinkButton ID="LinkButtonMesajYaz" runat="server" CssClass="button" ValidationGroup="g1"
                                    OnClick="LinkButtonMesajYazClick">Mesaj Yaz</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonIptal" runat="server" CssClass="button" OnClick="LinkButtonIptalClick">İptal</asp:LinkButton>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Kişi seçiniz!"
                                    ControlToValidate="TextBoxKime" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Başlık giriniz!"
                                    ControlToValidate="TextBoxBaslik" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Mesajınızı yazınız!"
                                    ControlToValidate="RadEditorMesaj" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                <br />
                                <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vGelenMesaj" runat="server">
                    <div style="min-height: 15px;" runat="server" id="dvGelenMesajlarIslemler">
                        <input style="float: left;" type="checkbox"  value="0"  id="cbSelectAll" onchange="ShowActions(0, 'cbSelectAll',1)" title="Tümünü Seç" />
                        <div id="gelenMesajlarIslemler" style="float: left; display: none; width: 90%;">
                            <input type="button" class="button" title="Okundu olarak işaretle" onclick="test()" value="Okundu olarak işaretle">
                            <input type="button" class="button" title="Okunmadı olarak işaretle" onclick="test()" value="Okunmadı olarak işaretle">
                            <input type="button" class="button" title="Sil"  onclick="test()" value="Sil">
                        </div>
                    </div>
                    <br />

                    <asp:ListView ID="ListViewGelenMesajlar" runat="server" ItemPlaceholderID="PlaceHolder1"
                        DataKeyNames="Id"
                        OnItemDeleting="ListViewGelenMesajlarItemDeleting"
                        OnItemEditing="ListViewGelenMesajlarItemEditing"
                        OnItemUpdating="ListViewGelenMesajlarItemUpdating"
                        OnItemCanceling="ListViewGelenMesajlarItemCanceling"
                        OnItemDataBound="ListViewGelenMesajlarItemDataBound">
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                <tr class="forumheader">
                                    <td>&nbsp;</td>
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px; min-width: 40px;"></td>
                                    <td style="width: 60%; min-width: 60%;">Gönderen
                                    </td>
                                    <td style="width: 30%; min-width: 30%;">Başlık
                                    </td>
                                    <td style="width: 10%; min-width: 10%;">Tarih
                                    </td>
                                    <td style="width: 100px; min-width: 100px;">İşlemler
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="margin-bottom: 15px;">
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table class="list" cellpadding="0" cellspacing="0">
                                <tr style="min-height: 20px; text-align: left;" >
                                    <td>
                                        <input type="checkbox"  value="<%#Eval("Id") %>" id="cbSelectOne_<%#Eval("Id") %>" onchange="ShowActions( <%#Eval("Id") %>, 'cbSelectOne_<%#Eval("Id") %>',0)" class="gelenMesajIslemler" /></td>
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                        <asp:Image ID="ImageOkunduMu" runat="server" Style="vertical-align: middle;" />
                                        <asp:HiddenField ID="HiddenFieldOkunduMu" runat="server" Value='<%#Eval("OkunduMu") %>' />
                                    </td>
                                    <td style="width: 60%; min-width: 60%;">
                                        <%#Eval("Alici") %>
                                    </td>
                                    <td style="width: 30%; min-width: 30%;">
                                        <%#Eval("Baslik") %>
                                    </td>
                                    <td style="width: 10%; min-width: 10%;">
                                        <%#Eval("Tarih") %>
                                    </td>
                                    <td style="width: 100px; min-width: 100px; text-align: left;">
                                        <asp:LinkButton ID="LinkButtonCevapla" runat="server" CssClass="button" CommandName="Edit"
                                            Text="Oku"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonSil" runat="server" CssClass="button" CommandName="Delete"
                                            Text="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table class="listalternate" cellpadding="0" cellspacing="0">
                                <tr style="min-height: 20px; text-align: left;">
                                    <td>
                                        <input type="checkbox" value="<%#Eval("Id") %>" id="cbSelectOne_<%#Eval("Id") %>" onchange="ShowActions(<%#Eval("Id") %>, 'cbSelectOne_<%#Eval("Id") %>',0)"  class="gelenMesajIslemler" /></td>
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px;">
                                        <asp:Image ID="ImageOkunduMu" runat="server" Style="vertical-align: middle;" />
                                        <asp:HiddenField ID="HiddenFieldOkunduMu" runat="server" Value='<%#Eval("OkunduMu") %>' />
                                    </td>
                                    <td style="width: 60%; min-width: 60%;">
                                        <%#Eval("Alici") %>
                                    </td>
                                    <td style="width: 30%; min-width: 30%;">
                                        <%#Eval("Baslik") %>
                                    </td>
                                    <td style="width: 10%; min-width: 10%;">
                                        <%#Eval("Tarih") %>
                                    </td>
                                    <td style="width: 100px; min-width: 100px; text-align: left;">
                                        <asp:LinkButton ID="LinkButtonCevapla" runat="server" CssClass="button" CommandName="Edit"
                                            Text="Oku"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonSil" runat="server" CssClass="button" CommandName="Delete"
                                            Text="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%; padding: 10px;">

                                <tr>
                                    <td style="font-weight: bold;">Yanıtla:&nbsp;&nbsp;<asp:TextBox ID="TextBoxBaslik" runat="server" Width="60%" Text='<%#Eval("Baslik") %>'></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top; float: right; padding-left: 10px; text-align: left; width: 40%;">
                                        <div style="float: right;">
                                            <asp:LinkButton ID="LinkButtonCevapla" runat="server" CssClass="button" ValidationGroup="g3"
                                                CommandName="Update">Cevapla</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonIptal" runat="server" CssClass="button" CommandName="Cancel">İptal</asp:LinkButton>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cevap yazınız!"
                                                ControlToValidate="RadEditorMesaj" Display="Dynamic" ValidationGroup="g3"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp; </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="vertical-align: top; width: 100%;">
                                        <telerik:RadEditor ID="RadEditorMesaj" runat="server" ContentAreaMode="Iframe" CssClass="RadEditorIcerik"
                                            Language="tr-Tr" Width="100%" ToolsFile="~/Kutuphaneler/RadEditorTolls.xml" EditModes="Design"
                                            Height="400px" Content='<%#Eval("Mesaj") %>'>
                                            <CssFiles>
                                                <telerik:EditorCssFile Value="" />
                                            </CssFiles>
                                        </telerik:RadEditor>
                                        <asp:HiddenField ID="HiddenFieldAliciId" runat="server" Value='<%#Eval("AliciId") %>' />
                                        <asp:HiddenField ID="HiddenFieldMesajId" runat="server" Value='<%#Eval("Id") %>' />
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <div class="modulicerik2">
                                Mesaj bulunamadı.
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <% if (DataPager1.TotalRowCount > 6)
                       {%>
                    <div class="moduldetayicerik2">
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewGelenMesajlar"
                            PageSize="6" QueryStringField="Sayfa">
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
                <asp:View ID="vGidenMesaj" runat="server">
                    <asp:ListView ID="ListViewGidenMesajlar" runat="server" DataKeyNames="Id" ItemPlaceholderID="PlaceHolder2"
                        OnItemEditing="ListViewGidenMesajlarItemEditing"
                        OnItemDeleting="ListViewGidenMesajlarItemDeleting">
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;">
                                <tr class="forumheader">
                                    <td style="width: 40px; padding-left: 10px; padding-right: 5px; min-width: 40px;"></td>
                                    <td style="width: 60%; min-width: 60%;">Alıcı
                                    </td>
                                    <td style="width: 30%; min-width: 30%;">Başlık
                                    </td>
                                    <td style="width: 10%; min-width: 10%;">Tarih
                                    </td>
                                    <td style="width: 100px; min-width: 100px;">İşlemler
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="margin-bottom: 15px;">
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
                                    <td style="width: 60%; min-width: 60%;">
                                        <%#Eval("Alici") %>
                                    </td>
                                    <td style="width: 30%; min-width: 30%;">
                                        <%#Eval("Baslik") %>
                                    </td>
                                    <td style="width: 10%; min-width: 10%;">
                                        <%#Eval("Tarih") %>
                                    </td>
                                    <td style="width: 100px; min-width: 100px;">
                                        <asp:LinkButton ID="LinkButtonGidenMesajOku" runat="server" CssClass="button" CommandName="Edit"
                                            Text="Oku"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonSil" runat="server" CssClass="button" CommandName="Delete"
                                            Text="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
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
                                    <td style="width: 60%; min-width: 60%;">
                                        <%#Eval("Alici") %>
                                    </td>
                                    <td style="width: 30%; min-width: 30%;">
                                        <%#Eval("Baslik") %>
                                    </td>
                                    <td style="width: 10%; min-width: 10%;">
                                        <%#Eval("Tarih") %>
                                    </td>
                                    <td style="width: 100px; min-width: 100px;">
                                        <asp:LinkButton ID="LinkButtonSil" runat="server" CssClass="button" CommandName="Delete"
                                            Text="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <div class="modulicerik2">
                                Mesaj bulunamadı.
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <% if (DataPager2.TotalRowCount > 6)
                       {%>
                    <div class="moduldetayicerik2">
                        <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListViewGidenMesajlar"
                            PageSize="6" QueryStringField="Sayfa">
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
    </div>
</div>
<div class="kategori">
    <div class="modulkategori">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                MESAJ KUTUSU
            </p>
        </div>
        <div class="moduldetayicerik">
            <div class="moduldetayicerik2">
                <asp:Menu ID="MenuMesajKutusu" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                    MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
                </asp:Menu>
            </div>
        </div>
    </div>
</div>
