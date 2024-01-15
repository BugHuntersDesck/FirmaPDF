using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAFirmasGestion.WebForms
{
    public partial class PInicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnHacerSolicitud_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/PHacerSolicitud.aspx"); 
        }

        protected void btnGestionSolicitudes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/PGestionSolicitudes.aspx"); 
        }

        protected void btnFirmarDocumetos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/PFirmarSolicitud.aspx"); 
        }

        protected void btnGestionarDocumetos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/PFirmaGestion.aspx"); 
        }

    }
}