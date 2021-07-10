# BankerWebByKaSui
基于ASP.NET和JS的银行家算法Web版

一、实验目的
做一个银行家算法Web版，更加了解银行家算法。
二、实验要求
1.实现形式：web页面。
2.实现技术：从下面3个选项中任选一个：
·APS.NET+JS
·JAVA+JS
·纯JQuery
3.写明算法的实现过程。
4.考勤签到。
5.撰写实验报告。

三、环境说明
Windows 10 企业版 LTSC Version 1809 x64
Visual Studio Community 2019 16.7.4
Brave 浏览器 1.26.74 Chromium: 91.0.4472.124

四、实验步骤
1.设计银行家算法初始界面
（1）页面截图

图4.1.1-1 银行家算法初始界面

（2）前端关键代码
//RequiredFieldValidator组件，判断textbox是否为空
<asp:RequiredFieldValidator ID="pnIsNull" runat="server" ControlToValidate="inputProcessNum" ErrorMessage="必填" Height="20px"></asp:RequiredFieldValidator>

//RegularExpressionValidator组件，判断textbox非法输入
<asp:RegularExpressionValidator ID="pnIntBigTo0" runat="server" ControlToValidate="inputProcessNum" ErrorMessage="应为大于0的整数" Height="20px" ValidationExpression="^\+?[1-9][0-9]*$"></asp:RegularExpressionValidator>

（3）后端关键代码
//传输数据到下一个页面
Response.Redirect("BankerInputByKaSui.aspx?processNum=" + inputProcessNum.Text + "&resourcesNum=" + inputResourcesNum.Text);

2.设计银行家算法输入界面
（1）页面截图

图4.2.1-1 银行家算法输入界面


图4.2.1-2 Alloction>Max（1）


图4.2.1-3 Alloction>Max（2）


图4.2.1-4 输入数据与资源种类数不匹配（1）



图4.2.1-5 输入数据与资源种类数不匹配（2）


图4.2.1-6 数据未输入


图4.2.1-7 数据输入有误时下一步

（2）前端关键代码
//css设计
<style type="text/css">
    //textbox css
    .textbox{
        height:21px;
        width:200px;
        border:1px solid #c0c4c0;
        margin-bottom:4px;
        background-color:#FFFFFF;
}
    //界面竖向对齐 css
    .column{
        float:left;
        width:33.33%;
        text-align:center;
}
//进程名与textbox对齐 css
    .spanp{
        line-height:29px;
    }
</style>

//JS函数设计
<script>
    //判断输入的格式是否正确，判断输入的数据与资源种类数目是否匹配
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
//判断输入Allocation是否大于Max
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
//判断输入是否为空，判断输入是否全部正确
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
                if (c != "RGB(255, 255, 255)") {
                    flag = 0;
                }
            }
            if (j2.value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                j2.style.backgroundColor = '#FF99FF';
                flag = 0;
            }
            else {
                c = j2.style.backgroundColor;
                c = c.toUpperCase();
                if (c != "RGB(255, 255, 255)") {
                    flag = 0;
                }
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
            if (c != "RGB(255, 255, 255)") {
                flag = 0;
            }
        }
        if (flag == 0) {
            alert("您没有正确输入所有的数据！");
            event.preventDefault();
        }
    }
</script>

（3）后端代码设计
//从上一个页面获取数据
if (Request.QueryString["processNum"] == null)
    {
        Response.Redirect("BankerMainByKaSui.aspx");
    }
processNum.Text = Request.QueryString["processNum"].ToString();
if (Request.QueryString["resourcesNum"] == null)
    {
        Response.Redirect("BankerMainByKaSui.aspx");
}
resourcesNum.Text = Request.QueryString["resourcesNum"].ToString();

//给(textbox)inputAvailableNum添加检查数据的JS函数checkdata(obj)
inputAvailableNum.Attributes.Add("onblur", "checkdata(this)");

//自动生成需要的textbox
TextBox tb;
Label lb;
plist.Text = "<span class=\"spanp\">p1</span>";
tb = new TextBox();
tb.ID = "max1";
tb.CssClass = "textbox";
tb.Attributes.Add("onblur", "cmp(max1,allo1)");
maxPH.Controls.Add(tb);
tb = new TextBox();
tb.ID = "allo1";
tb.CssClass = "textbox";
tb.Attributes.Add("onblur", "cmp(max1,allo1)");
alloPH.Controls.Add(tb);
for(int i = 2; i <= int.Parse(processNum.Text); i++)
{
    plist.Text += "<br /><span class=\"spanp\">p" + i + "</span>";
    lb = new Label();
    lb.Text = @"<br />";
    maxPH.Controls.Add(lb);
    tb = new TextBox();
    tb.ID = "max"+i;
    tb.CssClass = "textbox";
    tb.Attributes.Add("onblur", "cmp(max"+i+",allo"+i+")");
    maxPH.Controls.Add(tb);
    lb = new Label();
    lb.Text = @"<br />";
    alloPH.Controls.Add(lb);
    tb = new TextBox();
    tb.ID = "allo"+i;
    tb.CssClass = "textbox";
    tb.Attributes.Add("onblur", "cmp(max" + i + ",allo" + i + ")");
    alloPH.Controls.Add(tb);
}

//传参的下一个页面
string pNum = processNum.Text;
string rNum = resourcesNum.Text;
string aNum = inputAvailableNum.Text;
string max = Request.Form["max1"].ToString().Trim();
string allo = Request.Form["allo1"].ToString().Trim();
for (int i=2; i<= int.Parse(pNum); i++)
{
    max += " ";
    allo += " ";
    max += Request.Form["max" + i].ToString().Trim();
    allo += Request.Form["allo" + i].ToString().Trim();
}
Response.Redirect("BankerOutputByKaSui.aspx?pNum=" + pNum + "&rNum=" + rNum+ "&aNum="+ aNum+"&max="+max+"&allo="+allo);

3.设计银行家算法操作界面
（1）页面截图

图4.2.3-1 银行家算法操作界面


图4.2.3-2 银行家算法不安全状态


图4.2.3-3 银行家算法申请资源不足阻塞（1）


图4.2.3-4 银行家算法申请资源不足阻塞（2）


图4.2.3-5 申请资源超过Need


图4.2.3-6 银行家算法申请不安全阻塞（1）


图4.2.3-7 银行家算法申请不安全阻塞（2）


图4.2.3-8 银行家算法释放资源解除阻塞（1）


图4.2.3-9 银行家算法释放资源解除阻塞（2）


图4.2.3-10 银行家算法新建进程


图4.2.3-11 银行家算法新建进程无法满足资源需求

（2）前端关键代码
//使用Panel 控件隐藏局部页面
<asp:Panel ID="app" runat="server" Visible="False">
    ……
</asp:Panel>

（3）后端关键代码
//安全算法
private int safe(DataTable dt1, string avail, out ArrayList safelist)
{
    string[] work = avail.Split(' ');
    DataTable dt = dt1.Copy();
    safelist = new ArrayList();
    string exp = "";
    int r = int.Parse(resourcesNum.Text);
    while (dt.Rows.Count > 0)
    {
        exp = "";
        exp = "c" + (1 + r * 2).ToString() + "<=" + work[0];
        for (int i = 2; i <= r; i++)
        {
            exp += " and c" + (i + r * 2).ToString() + "<=" + work[i - 1];
        }
        DataRow[] dr = dt.Select(exp);
        if (dr.Length > 0)
        {
            safelist.Add(dr[0][0].ToString());
            for (int i = 0; i < r; i++)
            {
                work[i] = (int.Parse(dr[0][i + 1 + r].ToString()) + int.Parse(work[i])).ToString();
            }
            dt.Rows.Remove(dr[0]);
                }
        else break;
    }
    if (dt.Rows.Count == 0) return 1;
    else return 0;
}

//Page_Load
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
{
        //处理上一个页面来的参数
        if (Request.QueryString["pNum"] == null) Response.Redirect("BankerMainByKaSui.aspx");
        processNum.Text = Request.QueryString["pNum"].ToString();
        resourcesNum.Text = Request.QueryString["rNum"].ToString();
        availableNum.Text = Request.QueryString["aNum"].ToString();
        string max = Request.QueryString["max"].ToString();
        string allo = Request.QueryString["allo"].ToString();
        //生成表
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("c0", typeof(string)));
        for (int i = 1; i <= int.Parse(resourcesNum.Text) * 3; i++)
        {
            dt.Columns.Add(new DataColumn("c" + i.ToString(), typeof(int)));
        }
        dt.Columns.Add(new DataColumn("pstate", typeof(string)));
        dt.Columns["c0"].Unique = true;
        dt.PrimaryKey = new DataColumn[] { dt.Columns["c0"] };
        string[] maxlist = max.Split(' ', ',');
        string[] allolist = allo.Split(' ', ',');
        DataRow dr;
        int[] all = new int[int.Parse(resourcesNum.Text)];
        for (int i = 1; i <= int.Parse(processNum.Text); i++)
        {
            dr = dt.NewRow();
            dr[0] = "p" + i.ToString();
            dr[dt.Columns.Count - 1] = "运行";
            int rNum = int.Parse(resourcesNum.Text);
            for (int j = 0; j < rNum; j++)
            {
                dr[1 + j] = maxlist[(i - 1) * rNum + j];
                dr[1 + j + rNum] = allolist[(i - 1) * rNum + j];
                dr[1 + j + rNum * 2] = (int.Parse(dr[1 + j].ToString()) - int.Parse(dr[1 + j + rNum].ToString())).ToString();
                all[j] += int.Parse(allolist[(i - 1) * rNum + j]);
            }
            dt.Rows.Add(dr);
        }
        string[] alist = availableNum.Text.Split(' ', ',');
        sysAllNum.Text = "";
        for (int k = 0; k < alist.Length; k++)
        {
            all[k] += int.Parse(alist[k]);
            sysAllNum.Text += all[k].ToString() + " ";
        }
        sysAllNum.Text = sysAllNum.Text.Trim();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ViewState["table"] = dt;
        ViewState["psequence"] = processNum.Text;
        //调用安全算法
        string avail = availableNum.Text.Trim();
        ArrayList safelist = new ArrayList();
        safeList.Text = "";
        int f = safe(dt, avail, out safelist);
        if (f == 1)
        {
            sysState.Text = "安全 可以找到安全序列：";
            safeList.Text += string.Join(" ", (string[])safelist.ToArray(typeof(string)));
        }
        else if (f == 0)
        {
            sysState.Text = "不安全！";
            safeList.Text = "";
            applyBtn.Enabled = false;
            releaseBtn.Enabled = false;
            newPBtn.Enabled = false;
            tip.Text = "系统处于不安全状态，已经或即将发生死锁，禁止一切操作！";
        }
        //被阻塞进程的request数据存放对象定义
        DataTable dt_request = new DataTable();
        dt_request.Columns.Add(new DataColumn("no", typeof(int)));
        dt_request.Columns.Add(new DataColumn("pname", typeof(string)));
        for (int i = 1; i <= int.Parse(resourcesNum.Text); i++)
        {
            dt_request.Columns.Add(new DataColumn("r" + i.ToString(), typeof(int)));
        }
        dt_request.Columns["no"].Unique = true;
        dt_request.PrimaryKey = new DataColumn[] { dt_request.Columns["no"] };
        ViewState["requesttable"] = dt_request;
    }
}

//申请时排除need=0和阻塞的过滤器
int r = int.Parse(resourcesNum.Text);
string filter = "c" + (1 + 2 * r).ToString() + ">0";
for (int i = 2; i <= r; i++)
{
    filter += " or c" + (i + 2 * r).ToString() + ">0";
}
dv.RowFilter = "pstate='运行' and " + filter;

//释放时排除need!=0的过滤器
int r = int.Parse(resourcesNum.Text);
string filter = "c" + (1 + 2 * r).ToString() + "=0";
for (int i = 2; i <= r; i++)
{
    filter += " and c" + (i + 2 * r).ToString() + "=0";
}
dv.RowFilter = filter;

//申请确认
protected void p1yesBtn_Click(object sender, EventArgs e)
{
    //提取需要的数据
    string[] prequest = inputAppNum.Text.Trim().Split(' ', ',');
    string[] avail = availableNum.Text.Trim().Split(' ', ',');
    string pname = p1chP.Text;
    DataTable dt = (DataTable)ViewState["table"];
    DataRow dr = dt.Rows.Find(pname);
    int flag = 1;
    int r = int.Parse(resourcesNum.Text);
    //判断申请是否合理
    for (int i = 0; i < r; i++)
    {
        if (int.Parse(prequest[i]) > int.Parse(dr[i + 1 + 2 * r].ToString()))
        {
            flag = 0;
            break;
        }
    }
    if (flag == 0)
    {
        Response.Write("<script>alert('您申请的资源数目超过进程" + pname + "的need数，不合理，无法进行分配')</script>");
        return;
    }
    //判断系统剩余资源是否够分配
    for (int j = 0; j < r; j++)
    {
        if (int.Parse(prequest[j]) > int.Parse(avail[j]))
        {
            flag = 0;
            break;
        }
    }
    stopProcess.Text = "";
    if (flag == 0)
    {
        Response.Write("<script>alert('剩余资源不足进程" + pname + "被阻塞! ')</script>");
        tip.Text = "剩余资源不足，进程" + pname + "被阻塞";
        stopProcess.Text += " " + pname;
        dr.BeginEdit();
        dr["pstate"] = "阻塞";
        dr.EndEdit();
        ViewState["table"] = dt;
        //保存资源请求数据
        DataTable dtre = (DataTable)ViewState["requesttable"];
        DataRow dr1 = dtre.NewRow();
        dr1["no"] = dtre.Rows.Count.ToString();
        dr1["pname"] = pname;
        for (int i = 0; i < prequest.Length; i++)
        {
            dr1[i + 2] = prequest[i];
        }
        dtre.Rows.Add(dr1);
        ViewState["requesttable"] = dtre;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        app.Visible = false;
        return;
    }
    //尝试分配资源
    dr.BeginEdit();
    for (int i = 0; i < r; i++)
    {
        dr[i + 1 + r] = int.Parse(dr[i + 1 + r].ToString()) + int.Parse(prequest[i]);
        dr[i + 1 + 2 * r] = int.Parse(dr[i + 1 + 2 * r].ToString()) - int.Parse(prequest[i]);
        avail[i] = (int.Parse(avail[i]) - int.Parse(prequest[i])).ToString();
    }
    dr.EndEdit();
    //安全检查
    ArrayList safelist = new ArrayList();
    int f = safe(dt, string.Join(" ", avail), out safelist);
    if (f == 1)
    {
        sysState.Text = "安全 可以找到安全序列：";
        safeList.Text = string.Join(" ", (string[])safelist.ToArray(typeof(string)));
        availableNum.Text = string.Join(" ", avail);
        ViewState["table"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        app.Visible = false;
        tip.Text = "进程" + pname + "资源请求[" + inputAppNum.Text + "]成功";
    }
    else if (f == 0)
    {
        dr.BeginEdit();
        dr["pstate"] = "阻塞";
        for (int i = 0; i < r; i++)
        {
            dr[i + 1 + r] = int.Parse(dr[i + 1 + r].ToString()) - int.Parse(prequest[i]);
            dr[i + 1 + 2 * r] = int.Parse(dr[i + 1 + 2 * r].ToString()) + int.Parse(prequest[i]);
            avail[i] = (int.Parse(avail[i]) - int.Parse(prequest[i])).ToString();
        }
        dr.EndEdit();
        //保存资源请求数据
        DataTable dtre = (DataTable)ViewState["requesttable"];
        DataRow dr1 = dtre.NewRow();
        dr1["no"] = dtre.Rows.Count.ToString();
        dr1["pname"] = pname;
        for (int i = 0; i < prequest.Length; i++)
        {
            dr1[i + 2] = prequest[i];
        }
        dtre.Rows.Add(dr1);
        ViewState["requesttable"] = dtre;
        ViewState["table"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Response.Write("<script>alert('尝试分配结果不安全，进程" + pname + "被阻塞! ')</script>");
        tip.Text = "尝试分配结果不安全，进程" + pname + "被阻塞";
        stopProcess.Text += " " + pname;
        app.Visible = false;
    }
}

//释放全部资源
protected void p2RallBtn1_Click(object sender, EventArgs e)
{
//提取需要的数据
    int r = int.Parse(resourcesNum.Text);
    int p = int.Parse(processNum.Text);
    string[] pname=new string[p];
    string[] avail = availableNum.Text.Trim().Split(' ', ',');
    DataTable dt = (DataTable)ViewState["table"];

    for (int j = 0; j <dt.Rows.Count; j++)
    {
        pname[j]=dt.Rows[j]["c0"].ToString();
    }
    //回收资源，删除进程，更新安全序列，更新进程数
    for (int j = 0; j < p; j++)
    {
        DataRow dr = dt.Rows.Find(pname[j]);
        if (getneed(pname[j]) == 0)
        {
            for (int i = 0; i < r; i++)
            {
                avail[i] = (int.Parse(avail[i]) + int.Parse(dr[1 + i + r].ToString())).ToString();
            }
            dt.Rows.Remove(dr);
            ArrayList safes = new ArrayList();
            safe(dt, string.Join(" ", avail), out safes);
            if (safe(dt, string.Join(" ", avail), out safes) == 1)
            {
                sysState.Text = "安全 可以找到安全序列:";
                safeList.Text = string.Join(" ", (string[])safes.ToArray(typeof(string)));
            }
            else
            {
                sysState.Text = "不安全";
                safeList.Text = "";
            }
            processNum.Text = (int.Parse(processNum.Text) - 1).ToString();
            tip.Text += "<br />进程" + pname[j] + "释放资源，并结束";
            //查找阻塞进程进行资源分配
            DataTable dtrequest = (DataTable)ViewState["requesttable"];
            string exp = "";
            bool stop = false;
            while (!stop)
            {
                stop = true;
                exp = "r1 <=" + avail[0];
                for (int i = 2; i <= r; i++)
                {
                    exp += " and r" + i.ToString() + "<=" + avail[i - 1];
                }
                DataRow[] drs = dtrequest.Select(exp);
                if (drs.Length > 0)
                {
                    DataTable tempdt = dt.Copy();
                    DataRow temprow = null;
                    int f;
                    ArrayList safelist = new ArrayList();
                    string suspendlist = " " + stopProcess.Text.Trim() + " ";
                    for (int k = 0; k < drs.Length; k++)
                    {
                        temprow = tempdt.Rows.Find(drs[k]["pname"]);
                        temprow.BeginEdit();
                        for (int i = 0; i < r; i++)
                        {
                            temprow[i + 1 + r] = int.Parse(temprow[i + 1 + r].ToString()) + int.Parse(drs[k][i + 2].ToString());
                            temprow[i + 1 + 2 * r] = int.Parse(temprow[i + 1 + 2 * r].ToString()) - int.Parse(drs[k][i + 2].ToString());
                            avail[i] = (int.Parse(avail[i]) - int.Parse(drs[k][i + 2].ToString())).ToString();
                        }
                        temprow.EndEdit();
                        //调用安全算法
                        f = safe(dt, string.Join(" ", avail), out safelist);
                        if (f == 1)
                        {
                            dr = dt.Rows.Find(drs[k]["pname"]);
                            dr.BeginEdit();
                            dr["pstate"] = "运行";
                            string ofer = "";
                            for (int i = 0; i < r; i++)
                            {
                                dr[i + 1 + r] = int.Parse(dr[i + 1 + r].ToString()) + int.Parse(drs[k][i + 2].ToString());
                                dr[i + 1 + 2 * r] = int.Parse(dr[i + 1 + 2 * r].ToString()) - int.Parse(drs[k][i + 2].ToString());
                                ofer += " " + drs[k][i + 2].ToString();
                            }
                            dr.EndEdit();
                            tip.Text += "<br />进程" + drs[k]["pname"].ToString() + "解除阻塞，并分配资源[" + ofer + " ]";
                            suspendlist = suspendlist.Replace(" " + drs[k]["pname"].ToString() + "", "");
                            dtrequest.Rows.Remove(drs[k]);
                            stop = false;
                            break;
                        }
                        else if (f == 0)
                        {
                            for (int i = 0; i < r; i++)
                            {
                                avail[i] = (int.Parse(avail[i]) + int.Parse(drs[k][i + 2].ToString())).ToString();
                            }
                        }
                    }
                    //更新阻塞队列的提示
                    stopProcess.Text = string.Join(" ", suspendlist);
                }
            }
            //更新信息
            ViewState["requesttable"] = dtrequest;
            ViewState["table"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            availableNum.Text = string.Join(" ", avail);
            re.Visible = false;
        }
    }
}

//新建进程
protected void p3yesBtn_Click(object sender, EventArgs e)
{
    string[] sysNum = sysAllNum.Text.Trim().Split(' ', ',');
    string[] pMax = inputPMax.Text.Trim().Split(' ', ',');
    bool f = true;
    for (int i = 0; i < sysNum.Length; i++)
    {
        if (int.Parse(pMax[i]) > int.Parse(sysNum[i]))
        {
            f = false;
            break;
        }
    }
    if (f) {
        DataTable dt = (DataTable)ViewState["table"];
        DataRow dr = dt.NewRow();
        dr[0] = inputPName.Text;
        int r = int.Parse(resourcesNum.Text);
        for (int i = 0; i < r; i++)
        {
            dr[i + 1] = pMax[i];
            dr[i + 1 + r] = 0;
            dr[i + 1 + 2 * r] = pMax[i];
        }
        dr[dt.Columns.Count - 1] = "运行";
        dt.Rows.Add(dr);
        processNum.Text = (int.Parse(processNum.Text) + 1).ToString();
        ViewState["psequence"] = int.Parse(ViewState["psequence"].ToString()) + 1;
        ViewState["table"] = dt;
        sysState.Text += " " + inputPName.Text;
        tip.Text = "创建新进程" + inputPName.Text + "成功!";
        GridView1.DataSource = dt;
        GridView1.DataBind();
        newp.Visible = false;
    }
    else if(!f)
    {
        Response.Write("<script>alert('系统无法满足新进程"+inputPName.Text+"的资源露求，创建失败!')</script>");
        tip.Text = "创建新进程失败!";
}
}

五、问题解决
1.初始界面使用了哪些验证控件，设置了什么属性？
使用了RequiredFieldValidator，使其ControlToValidate="inputProcessNum" ErrorMessage="必填"，在输入为空时提示“必填”；
使用了RegularExpressionValidator控件，使其ControlToValidate="inputProcessNum" ErrorMessage="应为大于0的整数" ValidationExpression="^\+?[1-9][0-9]*$"，在有其他非法输入时提示"应为大于0的整数" 。

2.页面跳转有哪些方法？
最常用的页面跳转（原窗口被替代）：Response.Redirect("XXX.aspx");
利用url地址打开本地网页或互联网：Respose.Write("<script language='javascript'>window.open('"+ url+"');</script>");
原窗口保留再新打开另一个页面（浏览器可能阻止，需要解除）：Response.Write("<script>window.open('xxxx.aspx','_blank')</script>");
效果同1中的另一种写法：Response.Write("<script>window.location='xxxx.aspx'</script>");
常用于传递session变量的页面跳转 （原窗口被替代）：Server.Transfer("XXX.aspx");
原窗口保留，以对话框形式打开新窗口：Response.Write("<script>window.showModelessDialog('XXX.aspx')</script>");
对话框形式打开新窗口，原窗口被代替：Response.Write("<script>window.showModelDialog('XXX.aspx')</script>");
打开简洁窗口：Respose.Write("<script language='javascript'>window.open('"+url+"','','resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no, menu=no');</script>");
利用vs端口：System.Diagnostics.Process.Start(http://localhost:3210/xxxx.aspx);

3.输入页面输入不正确时页面上回有什么反应？
具体参照 四、实验步骤/2.设计银行家算法输入界面/（1）页面截图 。

4.如何让测试输入页面JS方法的正确性？
每个非法输入都是一边。也可参照 四、实验步骤/2.设计银行家算法输入界面/（1）页面截图 。

5.submitcheck()方法怎么判断文本框的输入？
textbox是否为空，textbox的背景色是否为白色。

6.操作界面为什么使用了panel控件，有什么好处？
每次使用按钮都会刷新页面，保证数据的更新，也使页面更加简介。

7.创建基本表时的DataTable、DataRow和ViewState是什么？
DataTable是数据表，DataRow是数据行和ViewState是数据值。

8.创建的DataTable表长什么样？
具体参照 四、实验步骤/3.设计银行家算法操作界面/（1）页面截图 。

9.GridView控件的数据源是什么，如何把数据显示在GridView中？
GridView控件的数据源是DataTable。数据显示代码具体参照 四、实验步骤/3.设计银行家算法操作界面/（3）后端关键代码///Page_Load///生成表 。

10.系统资源总数为x时，GridView有几列，max从第几列开始，allocation从第几列开始，need从第几列开始（列标号从0开始）？
共有3*x+1列，max从第1列开始，allocation从第1+x列开始，need从第1+2*x列开始。

11.创建表头时调用ColumnSpan属性的作用是什么
确认这个表头占几列。

12.安全算法如果安全，返回值是多少，安全序列通过什么返回？
如果安全，返回（int）1，安全序列通过out ArrayList safelist返回。

13.如何查询DataTable中符合条件的记录，如何删除DataTable中的特定行？
设置查询条件exp，使用DataTable.Select(exp)查询；查找出要删除的记录dr，使用DataTable.Rows.Remove(dr[0])删除特定行。

14.如何将need=0的进程排除在申请列表外，代码是什么？
具体参照 四、实验步骤/3.设计银行家算法操作界面/（3）后端关键代码///申请时排除need=0和阻塞的过滤器 。 

15.被阻塞进程的request数据存放对象定义应该放在那里？
放在Page_Load里面。

16.关于部分浏览器textbox的背景色返回的RGB值的解决方法。
使用textbox.style.backgroundColor = "RGB(255, 255, 255)"，或者textbox.style.backgroundColor = "RGB(255, 255, 255)" or textbox.style.backgroundColor = "#FFFFFF" 两个都放上。具体参照 四、实验步骤/2.设计银行家算法输入界面/（2）前端关键代码///判断输入是否为空，判断输入是否全部正确 。


六、实验总结
对银行家和死锁有一定理解。UI啊懒得设计，不同浏览器textbox背景色返回值啊还不一样，前端再见。
