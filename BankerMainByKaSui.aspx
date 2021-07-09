<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankerMainByKaSui.aspx.cs" Inherits="BankerWeb.BankerMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>银行家算法初始界面</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="title" runat="server" Height="20px" Text="银行家算法"></asp:Label>
        <br />
        <br />
        <asp:Label ID="processNum" runat="server" Height="20px" Text="请输入进程数：" Width="200px"></asp:Label>
        <asp:TextBox ID="inputProcessNum" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="pnIsNull" runat="server" ControlToValidate="inputProcessNum" ErrorMessage="必填" Height="20px"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="pnIntBigTo0" runat="server" ControlToValidate="inputProcessNum" ErrorMessage="应为大于0的整数" Height="20px" ValidationExpression="^\+?[1-9][0-9]*$"></asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="resourcesNum" runat="server" Height="20px" Text="请输入资源种类数：" Width="200px"></asp:Label>
        <asp:TextBox ID="inputResourcesNum" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rnIsNull" runat="server" ControlToValidate="inputResourcesNum" ErrorMessage="必填" Height="20px"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="rnIntBigTo0" runat="server" ControlToValidate="inputResourcesNum" ErrorMessage="应为大于0的整数" Height="20px" ValidationExpression="^\+?[1-9][0-9]*$"></asp:RegularExpressionValidator>
        <br />
        <br />
        <asp:Button ID="next" runat="server" Text="下一步" OnClick="next_Click" />
    </form>
</body>
</html>
