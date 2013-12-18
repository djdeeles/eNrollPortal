<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
         CodeBehind="Default.aspx.cs" Inherits="EnrollPortal.Default2" %>
<%@ Register Src="Kontroller/EtkinliklerListKontrol.ascx" TagName="EtkinliklerListKontrol"
             TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <div class="modul">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                KURUM'DAN</p>
            <p class="modulbasliktextright">
                <a href="TumHaberlerVeDuyurular.aspx">Tüm Haberler ve Duyurular</a></p>
        </div>
        <div class="modulicerik">
            <iframe src="HaberlerVeDuyurularManset.aspx" frameborder="0" style="border: 0px; width: 100%; height: 350px; overflow: hidden;"></iframe>
        </div>
    </div>
    <div class="modul">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                PERSONEL'DEN</p>
            <p class="modulbasliktextright">
                <a href="TumIlanlarVeDuyurular.aspx">Tüm İlanlar ve Duyurular</a></p>
        </div>
        <div class="modulicerik">
            <iframe src="IlanlarVeDuyurularManset.aspx" frameborder="0" style="border: 0px; width: 100%; height: 350px; overflow: hidden;"></iframe>
        </div>
    </div>
    <div class="modulkucuk">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                ETKİNLİK TAKVİMİ</p>
            <p class="modulbasliktextright">
                <a href="TumEtkinlikler.aspx">Tüm Etkinlikler</a></p>
        </div>
        <div class="modulicerik">
            <uc3:EtkinliklerListKontrol ID="EtkinliklerListKontrol1" runat="server" />
        </div>
    </div>
    <div class="modulkucuk">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                BUGÜN DOĞANLAR</p>
        </div>
        <div class="modulicerik">
            <div class="modulicerik2">
                içerik</div>
        </div>
    </div>
    <div class="modulkucuk">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                ARAMIZA YENİ KATILANLAR</p>
        </div>
        <div class="modulicerik">
            <div class="modulicerik2">
                içerik</div>
        </div>
    </div>
    <div class="modulbuyuk">
        <div class="modulbaslik">
            <p class="modulbasliktext">
                FORUM'DAN SON MESAJLAR</p>
            <p class="modulbasliktextright">
                <a href="Forumlar.aspx">Forumlara Git</a></p>
        </div>
        <div class="modulicerik">
            <iframe src="ForumMesajlariKontrol.aspx" frameborder="0" style="border: 0px; width: 100%;
                height: 350px;"></iframe>
        </div>
    </div>
</asp:Content>