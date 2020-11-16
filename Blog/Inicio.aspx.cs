using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Blog
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            actualiza_tabla();
        }

        private void actualiza_tabla()
        {
            using (MySqlConnection conexion = new MySqlConnection("Server=localhost;Database=blogrest;Uid=root;Password="))
            {
                //MySqlCommand cmd = new MySqlCommand("SELECT * FROM temas", conexion);
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM temas", conexion);
                DataTable tabla = new DataTable();
                da.Fill(tabla);
                GridView1.DataSource = tabla;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexion = new MySqlConnection("Server=localhost;Database=blogrest;Uid=root;Password="))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO temas(idtema, nombre) Values(?id, ?n)", conexion);
                    cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = TextBox1.Text;
                    cmd.Parameters.Add("?n", MySqlDbType.VarChar).Value = TextBox2.Text;
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Label3.Text = "Se agrego correctamente el tema";
                        actualiza_tabla();
                    }
                    else
                    {
                        Label3.Text = "No se pudo agregar";
                    }
                } catch (Exception ex)
                {
                    Label3.Text = "Error " + ex.Message;
                }
            }
        }

        protected void Borrar_click(object sender, GridViewDeleteEventArgs e)
        {
            using (MySqlConnection conexion = new MySqlConnection("Server=localhost;Database=blogrest;Uid=root;Password="))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM temas WHERE idtema = ?id", conexion);
                    cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = GridView1.Rows[e.RowIndex].Cells[1].Text;

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Label3.Text = "Se elimino correctamente el tema";
                        actualiza_tabla();
                    }
                    else
                    {
                        Label3.Text = "No se pudo eliminar";
                    }
                }
                catch (Exception ex)
                {
                    Label3.Text = "Error " + ex.Message;
                }
            }
        }

        protected void Editar_click(object sender, GridViewEditEventArgs e)
        {
            TextBox1.Text = GridView1.Rows[e.NewEditIndex].Cells[1].Text;
            TextBox2.Text = GridView1.Rows[e.NewEditIndex].Cells[2].Text;
            Button3.Visible = true;
            Button2.Visible = true;
            Button1.Visible = false;
            e.Cancel = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            Button3.Visible = false;
            Button2.Visible = false;
            Button1.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexion = new MySqlConnection("Server=localhost;Database=blogrest;Uid=root;Password="))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE temas SET nombre=?n WHERE idtema=?id", conexion);
                    cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = TextBox1.Text;
                    cmd.Parameters.Add("?n", MySqlDbType.VarChar).Value = TextBox2.Text;
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Label3.Text = "Se actualizo correctamente el tema";
                        actualiza_tabla();
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        Button3.Visible = false;
                        Button2.Visible = false;
                        Button1.Visible = true;
                    }
                    else
                    {
                        Label3.Text = "No se pudo actualizar";
                    }
                }
                catch (Exception ex)
                {
                    Label3.Text = "Error " + ex.Message;
                }
            }

            

        }
    }


}