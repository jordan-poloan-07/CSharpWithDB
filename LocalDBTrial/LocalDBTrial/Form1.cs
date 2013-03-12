using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace LocalDBTrial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=c:\users\poloan\documents\visual studio 2010\Projects\LocalDBTrial\LocalDBTrial\data.sdf");         
            
            conn.Open();

            Boolean queryBool = true;

            try
            {
                SqlCeCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO dataTbl (name,email) VALUES ( @name , @email )";
                command.Parameters.AddWithValue("@name", nameTextbox.Text);
                command.Parameters.AddWithValue("@email", emailTextbox.Text);
                command.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
                queryBool = false;
                MessageBox.Show("There was an error on your query..\n" + ex.Message.ToString());
            }
            finally
            {
                if (queryBool)
                {
                    MessageBox.Show("Insert success");
                }
                else
                {
                    MessageBox.Show("Insert failed");
                }
            }

            conn.Close();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=c:\users\poloan\documents\visual studio 2010\Projects\LocalDBTrial\LocalDBTrial\data.sdf");

            conn.Open();

            Boolean queryBool = true;

            try
            {
                SqlCeCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE dataTbl SET name = @name , email = @email WHERE id = @id ";
                command.Parameters.AddWithValue("@id", idTextbox.Text);
                command.Parameters.AddWithValue("@name", nameTextbox.Text);
                command.Parameters.AddWithValue("@email", emailTextbox.Text);
                command.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
                queryBool = false;
                MessageBox.Show("There was an error on your query..\n" + ex.Message.ToString());
            }
            finally
            {
                if (queryBool)
                {
                    MessageBox.Show("Update success");
                }
                else
                {
                    MessageBox.Show("Update failed");
                }
            }

            conn.Close();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=c:\users\poloan\documents\visual studio 2010\Projects\LocalDBTrial\LocalDBTrial\data.sdf");

            conn.Open();

            Boolean queryBool = true;

            try
            {
                SqlCeCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM dataTbl WHERE name = @name OR id = @id ";
                command.Parameters.AddWithValue("@id", idTextbox.Text);
                command.Parameters.AddWithValue("@name", nameTextbox.Text);
                command.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
                queryBool = false;
                MessageBox.Show("There was an error on your query..\n" + ex.Message.ToString());
            }
            finally
            {
                if (queryBool)
                {
                    MessageBox.Show("Delete success");
                }
                else
                {
                    MessageBox.Show("Delete failed");
                }
            }

            conn.Close();
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=c:\users\poloan\documents\visual studio 2010\Projects\LocalDBTrial\LocalDBTrial\data.sdf");

            conn.Open();

            Boolean queryBool = true;
            DataTable cdt = null;

            try
            {
                // the below code is weird
                
                /**
                 * can be:
                 * DataTable cdt = new DataTable();
                 * SqlCeDataAdapter cda= new SqlCeDataAdapter(stringQuery,connection);
                 * cda.Fill(datatable);
                 * */

                SqlCeCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM dataTbl";

                SqlCeDataAdapter cda = new SqlCeDataAdapter();
                cda.SelectCommand = command;

                cdt = new DataTable();
                cda.Fill(cdt);
            }
            catch (SqlCeException ex)
            {
                queryBool = false;
                MessageBox.Show("There was an error on your query..\n" + ex.Message.ToString());
            }
            catch (FormatException ex)
            {
                queryBool = false;
                MessageBox.Show("Error:\n" + ex.Message.ToString());
            }
            finally
            {
                if (queryBool)
                {
                    Form2 form = Form2.getInstance(cdt);
                    if (form == null)
                    {
                        MessageBox.Show("Instance already exists", "Error", MessageBoxButtons.OK);
                    }
                    else
                    {
                        form.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Selection failed");
                }
            }

            conn.Close();
        }
    }

    
}
