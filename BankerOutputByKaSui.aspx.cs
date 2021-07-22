using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankerWeb
{
    public partial class BankerOutput : System.Web.UI.Page
    {
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

        //获取p的need
        private int getneed(string pname)
        {
            DataTable dt = (DataTable)ViewState["table"];
            DataRow dr = dt.Rows.Find(pname);
            int n = 0;
            int r = int.Parse(resourcesNum.Text);
            for (int i = 1; i <= r; i++)
            {
                n += int.Parse(dr[i + 2 * r].ToString());
            }
            return n;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["pNum"] == null) Response.Redirect("BankerMainByKaSui.aspx");
                processNum.Text = Request.QueryString["pNum"].ToString();
                resourcesNum.Text = Request.QueryString["rNum"].ToString();
                availableNum.Text = Request.QueryString["aNum"].ToString();
                string max = Request.QueryString["max"].ToString();
                string allo = Request.QueryString["allo"].ToString();

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
                    safeList.Text += string.Join(" ", (string[])safelist.ToArray(typeof(string)));//ing
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

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            TableCellCollection tcl = e.Row.Cells;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                tcl.Clear();

                tcl.Add(new TableHeaderCell());
                tcl[0].Text = "进程名";
                tcl[0].CssClass = "bordery";

                tcl.Add(new TableHeaderCell());
                tcl[1].ColumnSpan = int.Parse(resourcesNum.Text);
                tcl[1].Text = "MAX";
                tcl[1].CssClass = "bordery";

                tcl.Add(new TableHeaderCell());
                tcl[2].ColumnSpan = int.Parse(resourcesNum.Text);
                tcl[2].Text = "Allocation";
                tcl[2].CssClass = "bordery";

                tcl.Add(new TableHeaderCell());
                tcl[3].ColumnSpan = int.Parse(resourcesNum.Text);
                tcl[3].Text = "Need";
                tcl[3].CssClass = "bordery";

                tcl.Add(new TableHeaderCell());
                tcl[4].Text = "状态";
            }
            else
            {
                for (int i = 0; i <= int.Parse(resourcesNum.Text) * 3; i = i + int.Parse(resourcesNum.Text))
                {
                    tcl[i].CssClass = "bordery";
                }
            }
        }

        protected void applyBtn_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["table"];
            DataView dv = new DataView(dt);

            //排除need=0的过滤器
            int r = int.Parse(resourcesNum.Text);
            string filter = "c" + (1 + 2 * r).ToString() + ">0";
            for (int i = 2; i <= r; i++)
            {
                filter += " or c" + (i + 2 * r).ToString() + ">0";
            }
            dv.RowFilter = "pstate='运行' and " + filter;

            p1chP.DataSource = dv;
            p1chP.DataTextField = "c0";
            p1chP.DataValueField = "c0";
            p1chP.DataBind();
            tip.Text = "";
            inputAppNum.Text = "";
            inputAppNum.Attributes.Add("onblur", "checkdata(this)");
            app.Visible = true;
        }

        protected void releaseBtn_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["table"];
            DataView dv = new DataView(dt);

            //排除need!=0的过滤器
            int r = int.Parse(resourcesNum.Text);
            string filter = "c" + (1 + 2 * r).ToString() + "=0";
            for (int i = 2; i <= r; i++)
            {
                filter += " and c" + (i + 2 * r).ToString() + "=0";
            }
            dv.RowFilter = filter;

            p2chP.DataSource = dv;
            p2chP.DataTextField = "c0";
            p2chP.DataValueField = "c0";
            p2chP.DataBind();
            tip.Text = "";
            re.Visible = true;
        }

        protected void newPBtn_Click(object sender, EventArgs e)
        {
            inputPName.Text = "p" + (int.Parse(ViewState["psequence"].ToString()) + 1).ToString();
            inputPMax.Text = "";
            inputPMax.Attributes.Add("onblur", "checkdata(this)");
            tip.Text = "";
            newp.Visible = true;
        }

        protected void restartBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("BankerMainByKaSui.aspx");
        }

        protected void p1noBtn_Click(object sender, EventArgs e)
        {
            app.Visible = false;
        }

        protected void p2noBtn2_Click(object sender, EventArgs e)
        {
            re.Visible = false;
        }

        protected void p3noBtn_Click(object sender, EventArgs e)
        {
            newp.Visible = false;
        }

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

        protected void p2RBtn_Click(object sender, EventArgs e)
        {
            //提取需要的数据
            string pname = p2chP.Text;
            string[] avail = availableNum.Text.Trim().Split(' ', ',');
            DataTable dt = (DataTable)ViewState["table"];
            DataRow dr = dt.Rows.Find(pname);
            int r = int.Parse(resourcesNum.Text);

            //回收资源，删除进程，更新安全序列，更新进程数
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
            tip.Text = "进程" + pname + "释放资源，并结束";

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
                    string suspendlist = " " + stopProcess.Text.Trim() + "";
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
                            tip.Text += "<br /〉进程" + drs[k]["pname"].ToString() + "解除阻塞，并分配资源[" + ofer + " ]";
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
                ViewState["psequence"] = int.Parse(ViewState["psequence"].ToString()) + 1;//prb
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
    }
}