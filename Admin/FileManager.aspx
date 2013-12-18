<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManager.aspx.cs" Inherits="EnrollPortal.Admin.FileManager"
         EnableSessionState="False" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
    </head>
    <body style="margin: 0 auto; padding: 0 auto;">
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <script type="text/javascript">
                    function OnClientFileOpen(sender, args) {
                        var item = args.get_item();
                        if (item.get_type() == Telerik.Web.UI.FileExplorerItemType.File) {
                            args.set_cancel(true);
                            var ReturnField = document.getElementById("<%= HiddenFieldReturnField.ClientID %>").value;
                            ReturnField.value = item.get_path();
                            window.opener.update(ReturnField, item.get_path());
                            window.close();
                        }
                    }
                </script>
                <telerik:RadFileExplorer runat="server" ID="FileExplorer1" Width="530" Height="500"
                                         OnClientFileOpen="OnClientFileOpen">
                    <Configuration ViewPaths="~/FileManager" DeletePaths="~/FileManager" UploadPaths="~/FileManager"
                                   MaxUploadFileSize="1048576000" />
                </telerik:RadFileExplorer>
                <asp:HiddenField ID="HiddenFieldReturnField" runat="server" />
            </div>
        </form>
    </body>
</html>