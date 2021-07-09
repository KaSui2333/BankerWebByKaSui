using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankerWeb
{
    public partial class BankerInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            inputAvailableNum.Attributes.Add("onblur", "checkdata(this)");

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

        }

        protected void next_Click(object sender, EventArgs e)
        {
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
        }
    }
}