<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankerInputByKaSui.aspx.cs" Inherits="BankerWeb.BankerInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>银行家算法输入界面</title>
    <style type="text/css">
        .textbox{
            height:21px;
            width:200px;
            border:1px solid #c0c4c0;
            margin-bottom:4px;
            background-color:#FFFFFF;
        }
        .column{
            float:left;
            width:33.33%;
            text-align:center;
        }
        .spanp{
            line-height:29px;
        }
    </style>
    <script>
        function checkdata(obj) {
            var s = obj.value;
            if (s == "") return false;
            var ss = s.replace(/(^\s*)|(\s*$)/g, "");
            var patrn = /^\d+([\s|,]{1}\d+)*$/;
            if (!patrn.exec(ss)) {
                alert("您输入的数据格式不正确！");
                obj.style.backgroundColor = '#FFFF00';
                return false;
            }
            else {
                var s1 = ss.replace(/,/g, " ");
                var s2 = s1.split(" ")
                var rNum = document.getElementById("resourcesNum").innerHTML;
                if (s2.length != rNum) {
                    alert("您输入的数据与资源种类数目不匹配！");
                    obj.style.backgroundColor = '#FFFF00';
                    return false;
                }
            }
            obj.style.backgroundColor = '#FFFFFF';
            return true;
        }

        function cmp(obj1, obj2) {
            if (!checkdata(obj1) || !checkdata(obj2)) return false;
            var s1 = obj1.value.replace(/(^\s*)|(\s*$)/g, "");
            var s2 = obj2.value.replace(/(^\s*)|(\s*$)/g, "");
            var s11 = s1.replace(/,/g, " ").split(" ");
            var s22 = s2.replace(/,/g, " ").split(" ");
            for (var i = 0; i < s11.length; i++) {
                if (parseInt(s11[i]) < parseInt(s22[i])) {
                    obj1.style.backgroundColor = '#99FF99';
                    obj2.style.backgroundColor = '#99FF99';
                    alert("已取得的资源数alloction不能超过进程对资源的最大需求数max！");
                    return false;
                }
                obj1.style.backgroundColor = '#FFFFFF';
                obj2.style.backgroundColor = '#FFFFFF';
            }
        }

        function submitcheck() {
            var pNum = document.getElementById("processNum").innerHTML;
            var flag = 1;
            var j1, j2, j3, c;
            for (var i = 1; i <= pNum; i++) {
                j1 = document.getElementById("max" + i);
                j2 = document.getElementById("allo" + i);

                if (j1.value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                    j1.style.backgroundColor = '#FF99FF';
                    flag = 0;
                }
                else {
                    c = j1.style.backgroundColor;
                    c = c.toUpperCase();
                    if (c == "RGB(255, 255, 255)") {
                        //flag = 1;
                    } else {
                        flag = 0;
                    }
                    //flag = 1;
                }

                if (j2.value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                    j2.style.backgroundColor = '#FF99FF';
                    flag = 0;
                }
                else {
                    c = j2.style.backgroundColor;
                    c = c.toUpperCase();
                    if (c == "RGB(255, 255, 255)") {
                        //flag = 1;
                    } else {
                        flag = 0;
                    }
                    //flag = 1;
                }
            }

            j3 = document.getElementById("inputAvailableNum");
            if (j3.value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                j3.style.backgroundColor = '#FF99FF';
                flag = 0;
            }
            else {
                c = j3.style.backgroundColor;
                c = c.toUpperCase();
                if (c == "RGB(255, 255, 255)") {
                    //flag = 1;
                } else {
                    flag = 0;
                }
                //flag = 1;
            }

            if (flag == 0) {
                alert("您没有正确输入所有的数据！");
                event.preventDefault();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Label ID="title" runat="server" Text="银行家算法——输入当前系统状态的数据"></asp:Label>
            <br />
            <br />
        </div>

        <div class="column">
            <p>
                <asp:Label ID="pNum" runat="server" Text="进程数：" Height="20px"></asp:Label>
                <asp:Label ID="processNum" runat="server" Height="20px" Text="[processNum]"></asp:Label><br />
            </p>
            <span>
                <asp:Label ID="processid" runat="server" Height="20px" Text="进程" Width="200px"></asp:Label><br />
            </span>
            <span>
                <asp:Label ID="plist" runat="server" Text="[plist]"></asp:Label>
            </span>
        </div>

        <div class="column">
            <p>
                <asp:Label ID="rNum" runat="server" Height="20px" Text="资源种类数："></asp:Label>
                <asp:Label ID="resourcesNum" runat="server" Height="20px" Text="[resourcesNum]"></asp:Label><br />
            </p>
            <span>
                <asp:Label ID="max" runat="server" Height="20px" Text="MAX" Width="200px"></asp:Label><br />
            </span>
            <span>
                <asp:PlaceHolder ID="maxPH" runat="server"></asp:PlaceHolder>
            </span>
         </div>  
        
        <div class="column">
            <p>
                <asp:Label ID="availableNum" runat="server" Text="Available:" Height="20px"></asp:Label>
                <asp:TextBox ID="inputAvailableNum" runat="server"></asp:TextBox>
                <br />
            </p>
            <span>
                <asp:Label ID="allocation" runat="server" Height="20px" Text="Allocation" Width="200px"></asp:Label><br />
            </span>
            <span>
                <asp:PlaceHolder ID="alloPH" runat="server"></asp:PlaceHolder>
            </span>
        </div>

        <div>
            <asp:Button ID="next" runat="server" OnClick="next_Click" Text="下一步" OnClientClick="submitcheck()" />
        </div>
    </form>
</body>
</html>