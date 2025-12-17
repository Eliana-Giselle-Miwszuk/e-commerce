using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dominio;

namespace e_commerce
{
    public partial class Gracias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario != null)
                {
                    lblNombreUsuario.Text = usuario.Nombre;
                }
            }
        }
    }
}