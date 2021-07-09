using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankerWeb
{
    public partial class BankerMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void next_Click(object sender, EventArgs e)
        {
            Response.Redirect("BankerInputByKaSui.aspx?processNum=" + inputProcessNum.Text + "&resourcesNum=" + inputResourcesNum.Text);
        }
    }
}