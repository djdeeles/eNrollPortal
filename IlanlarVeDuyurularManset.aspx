<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IlanlarVeDuyurularManset.aspx.cs"
         Inherits="EnrollPortal.IlanlarVeDuyurularManset" %>

<%@ Register Src="Kontroller/IlanlarVeDuyurularListKontrol.ascx" TagName="IlanlarVeDuyurularListKontrol"
             TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="App_Themes/MainTheme/Styles/Manset.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <uc1:IlanlarVeDuyurularListKontrol ID="IlanlarVeDuyurularListKontrol1" runat="server" />
            </div>
        </form>
    </body>
</html>