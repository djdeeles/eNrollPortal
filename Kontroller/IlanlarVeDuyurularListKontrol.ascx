<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IlanlarVeDuyurularListKontrol.ascx.cs"
            Inherits="EnrollPortal.Kontroller.IlanlarVeDuyurularListKontrol" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function HandleScrolling(e) {
            var grid = $find("<%= RadGridVeriler.ClientID %>");
            var scrollArea = document.getElementById("<%= RadGridVeriler.ClientID %>" + "_GridData");
            if (IsScrolledToBottom(scrollArea)) {
                var currentlyDisplayedRecords = grid.get_masterTableView().get_pageSize() * (grid.get_masterTableView().get_currentPageIndex() + 1);
                if (currentlyDisplayedRecords < 100) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("LoadMoreRecords");
                }
            }
        }

        function IsScrolledToBottom(scrollArea) {
            var currentPosition = scrollArea.scrollTop + scrollArea.clientHeight;
            return currentPosition == scrollArea.scrollHeight;
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGridVeriler" LoadingPanelID="RadAjaxLoadingPanel2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadGrid ID="RadGridVeriler" runat="server" PageSize="10" CellSpacing="0"
                 CellPadding="0" GridLines="None" AutoGenerateColumns="False" ShowHeader="False"
                 Width="100%" Height="350px" BorderStyle="None" AllowSorting="True" AllowPaging="true">
    <PagerStyle Visible="false" />
    <MasterTableView TableLayout="Fixed">
        <Columns>
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <a class="habermanset" target="_parent" href='<%#                                        Eval("Url") %>'>
                        <div class="habermansetdiv">
                            <img src='<%#Eval("Resim") %>' style="max-width: 50px; max-height: 50px; vertical-align: middle;" />
                            <div class="habermansetbaslik">
                                <%#Eval("Baslik") %></div>
                            <div class="habermansettarih">
                                <%#Eval("Tarih") %>
                                <%#Eval("Ekleyen") %>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings>
        <ClientEvents OnScroll=" HandleScrolling" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="350px"></Scrolling>
    </ClientSettings>
</telerik:RadGrid>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>