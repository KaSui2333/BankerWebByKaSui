<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankerOutputByKaSui.aspx.cs" Inherits="BankerWeb.BankerOutput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>银行家算法输出界面</title>
    <style type="text/css">
        .bordery{
            border-right:solid 2px #000000
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="title" runat="server" Text="银行家算法——操作界面"></asp:Label>
            <br />
            <br />
            <asp:Label ID="pNum" runat="server" Text="进程数："></asp:Label>
            <asp:Label ID="processNum" runat="server" Text="[processNum]" Width="150px"></asp:Label>
            <asp:Label ID="rNum" runat="server" Text="资源种类数："></asp:Label>
            <asp:Label ID="resourcesNum" runat="server" Text="[resourcesNum]" Width="150px"></asp:Label>
            <asp:Label ID="allNum" runat="server" Text="系统资源总数："></asp:Label>
            <asp:Label ID="sysAllNum" runat="server" Text="[sysAllNum]" Width="150px"></asp:Label>
            <asp:Label ID="aNum" runat="server" Text="系统剩余资源数："></asp:Label>
            <asp:Label ID="availableNum" runat="server" Text="[availableNum]" Width="150px"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" Width="700px" OnRowCreated="GridView1_RowCreated" style="margin:0 auto">
            </asp:GridView>
            <br />
            <asp:Label ID="state" runat="server" Text="当前系统状态："></asp:Label>
            <asp:Label ID="sysState" runat="server" Text="[sysState]"></asp:Label>
            <asp:Label ID="safeList" runat="server" Text="[safeList]" Width="150px"></asp:Label>
            <asp:Label ID="stopP" runat="server" Text="因资源申请未满足而被阻塞的进程："></asp:Label>
            <asp:Label ID="stopProcess" runat="server" Text="[stopProcess]" Width="150px"></asp:Label>
            <br />
            <br />
            <asp:Label ID="tip" runat="server" Text="[tip]"></asp:Label>
            <br />
            <br />
            <asp:Button ID="applyBtn" runat="server" Text="申请资源" OnClick="applyBtn_Click" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="releaseBtn" runat="server" Text="释放资源" OnClick="releaseBtn_Click" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="newPBtn" runat="server" Text="新建进程" OnClick="newPBtn_Click" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="restartBtn" runat="server" Text="重启" OnClick="restartBtn_Click" />
            <br />
            <br />
            <asp:Panel ID="app" runat="server" Visible="False">
                <asp:Label ID="p1Title" runat="server" Text="申请资源&gt;&gt;"></asp:Label>
                <br />
                <br />
                <asp:Label ID="p1ch" runat="server" Height="20px" Text="选择申请资源的进程："></asp:Label>
                <asp:DropDownList ID="p1chP" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="appNum" runat="server" Height="20px" Text="申请资源数目："></asp:Label>
                <asp:TextBox ID="inputAppNum" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="p1yesBtn" runat="server" Text="确定" OnClick="p1yesBtn_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="p1noBtn" runat="server" Text="取消" OnClick="p1noBtn_Click" />
            </asp:Panel>
            <br />
            <asp:Panel ID="re" runat="server" Visible="False">
                <asp:Label ID="p2Title" runat="server" Text="释放资源&gt;&gt;"></asp:Label>
                <br />
                <br />
                <asp:Label ID="p2ch" runat="server" Height="20px" Text="选择释放资源的进程："></asp:Label>
                <asp:DropDownList ID="p2chP" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="p2RBtn" runat="server" Text="释放进程资源" OnClick="p2RBtn_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="p2RallBtn1" runat="server" Text="满足条件的全部释放" OnClick="p2RallBtn1_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="p2noBtn2" runat="server" Text="取消" OnClick="p2noBtn2_Click" />
            </asp:Panel>
            <br />
            <asp:Panel ID="newp" runat="server" Visible="False">
                <asp:Label ID="p3Title" runat="server" Text="新建进程&gt;&gt;"></asp:Label>
                <br />
                <br />
                <asp:Label ID="pName" runat="server" Height="20px" Text="进程名："></asp:Label>
                <asp:TextBox ID="inputPName" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="pMax" runat="server" Height="20px" Text="资源最大需求数："></asp:Label>
                <asp:TextBox ID="inputPMax" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="p3yesBtn" runat="server" Text="确定" OnClick="p3yesBtn_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="p3noBtn" runat="server" Text="取消" OnClick="p3noBtn_Click" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
