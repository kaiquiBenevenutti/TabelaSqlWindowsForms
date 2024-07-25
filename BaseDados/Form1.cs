using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BaseDados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }


        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=DESKTOP-KNF4FEE\SQLEXPRESS;Initial Catalog=BaseDeDados;trusted_connection=true;TrustServerCertificate=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                labelResultado.Text = "Connection open !";
                cnn.Close();
            }
            catch (SqlException erro)
            {
                MessageBox.Show("Erro ao se conectar no banco de dados \n" +
                    "Verifique os dados informados" + erro);
                throw;
            }

        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-KNF4FEE\SQLEXPRESS;Initial Catalog=BaseDeDados;trusted_connection=true;TrustServerCertificate=True";
            cnn = new SqlConnection(connetionString);
            
            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;

                cmd.CommandText = "CREATE TABLE pessoas ( id INT NOT NULL PRIMARY KEY, nome NVARCHAR(50) NOT NULL, email NVARCHAR(50) NOT NULL)";
                cmd.ExecuteNonQuery();

                labelResultado.Text = "Tabela Criada Sql Server Menagement Studio";
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                labelResultado.Text = ex.Message;
                throw;
            }
            finally
            {

            cnn.Close(); 
            
            }

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-KNF4FEE\SQLEXPRESS;Initial Catalog=BaseDeDados;trusted_connection=true;TrustServerCertificate=True";
            cnn = new SqlConnection(connetionString);

            try
            {
                //Inicio:

                //labelResultado.Text = "Insira um nome e um email para inserir na lista!";

                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;

                int id = new Random(DateTime.Now.Millisecond).Next(0,1000);
                string nome = txtNome.Text;
                string email = txtEmail.Text;

                if (nome != "" || email != "")
                {
                    cmd.CommandText = "INSERT INTO pessoas VALUES (" + id + ", '" + nome + "', '" + email + "')";

                    cmd.ExecuteNonQuery();

                    labelResultado.Text = "Informações inseridas na tabela Sql Server Menagement Studio";
                    cmd.Dispose();
            }
                //else
                //{
                //    goto Inicio;
                //}


            }
            catch (SqlException ex)
            {
                labelResultado.Text = ex.Message;
                throw;
            }
            finally
            {

                cnn.Close();

            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {

            labelResultado.Text = "";
            lista.Rows.Clear();

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-KNF4FEE\SQLEXPRESS;Initial Catalog=BaseDeDados;trusted_connection=true;TrustServerCertificate=True";
            cnn = new SqlConnection(connetionString);

            try
            {
                string query = "SELECT * FROM pessoas";

                if (txtNome.Text != "")
                {
                    query = "SELECT * FROM pessoas WHERE nome LIKE '" + txtNome.Text + "'";
                }

                DataTable dados = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(query, cnn);

                cnn.Open();

                adp.Fill(dados);

                foreach (DataRow linha in dados.Rows)
                {
                     lista.Rows.Add(linha.ItemArray);
                }
                
            }
            catch (SqlException ex)
            {
                lista.Rows.Clear();
                labelResultado.Text = ex.Message;                
            }
            finally
            {

                cnn.Close();

            }
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-KNF4FEE\SQLEXPRESS;Initial Catalog=BaseDeDados;trusted_connection=true;TrustServerCertificate=True";
            cnn = new SqlConnection(connetionString);

            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;

                int id = (int)lista.SelectedRows[0].Cells[0].Value;
                
                cmd.CommandText = "DELETE FROM pessoas WHERE id = '" + id + "'";

                cmd.ExecuteNonQuery();

                labelResultado.Text = "Informações apagadas da tabela Sql Server Menagement Studio";
                cmd.Dispose();                                


            }
            catch (SqlException ex)
            {
                labelResultado.Text = ex.Message;
                throw;
            }
            finally
            {

                cnn.Close();

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-KNF4FEE\SQLEXPRESS;Initial Catalog=BaseDeDados;trusted_connection=true;TrustServerCertificate=True";
            cnn = new SqlConnection(connetionString);

            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;

                int id = (int)lista.SelectedRows[0].Cells[0].Value;
                string nome = txtNome.Text;
                string email = txtEmail.Text;

                string query = "UPDATE pessoas SET nome = '" + nome + "', email = '" + email + "' WHERE id LIKE '" + id + "'"; 

                cmd.CommandText = query;

                cmd.ExecuteNonQuery();

                labelResultado.Text = "Informações alteradas da tabela Sql Server Menagement Studio";
                cmd.Dispose();


            }
            catch (SqlException ex)
            {
                labelResultado.Text = ex.Message;
                throw;
            }
            finally
            {

                cnn.Close();

            }
        }
    }
}
