using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Blog
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexion = new MySqlConnection("Server=localhost;Database=blogrest;Uid=root;Password="))
            {
                MySqlCommand cmd = new MySqlCommand();

                conexion.Open();
                cmd.Connection = conexion;
                cmd.CommandText = "SELECT * FROM usuarios WHERE user=?a AND pass=?b";
                cmd.Parameters.Add("?a", MySqlDbType.VarChar).Value = TextBox1.Text;
                cmd.Parameters.Add("?b", MySqlDbType.VarChar).Value = TextBox2.Text;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Server.Transfer("Inicio.aspx");
                    }
                    else
                    {
                        Label3.Text = "Error de contraseña";
                    }
                }
            }
            
        }
    }
}