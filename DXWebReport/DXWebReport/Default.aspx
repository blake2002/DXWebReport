<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DXWebReport.Default" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web.ClientControls" tagprefix="cc" %>

<%@ Register Src="~/FileDialogControl.ascx" TagPrefix="uc" TagName="FileDialogControl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/ClientFileDialogControl.js"></script>
    <script type="text/javascript" src="Scripts/Script.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource runat="server" ID="DSGenres"
            ConnectionString="<%$ ConnectionStrings:ChinookConnection %>"
            SelectCommand="SELECT * FROM [Genre]"></asp:SqlDataSource>
        
        <dx:ASPxReportDesigner ID="ASPxReportDesigner1" runat="server" ClientInstanceName="reportDesigner">        
             <MenuItems>
                <cc:ClientControlsMenuItem JSClickAction="function() {alert('check me');}" Text="Super duper action"  />
            </MenuItems>   
            <ClientSideEvents 
                CustomizeMenuActions="reportDesigner_CustomizeMenuActions" /> 
        </dx:ASPxReportDesigner>
        <uc:FileDialogControl runat="server" ID="FileDialogControl" ClientInstanceName="fileDialog" />        
    </form>
</body>
</html>
