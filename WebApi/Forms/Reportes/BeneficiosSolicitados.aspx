<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BeneficiosSolicitados.aspx.vb" Inherits="WebApi.BeneficiosSolicitados" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1500px" Width="100%">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
