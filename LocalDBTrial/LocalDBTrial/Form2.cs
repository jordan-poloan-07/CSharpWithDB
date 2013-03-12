using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace LocalDBTrial
{
    public partial class Form2 : Form
    {

        private static Form2 form = null;

        private Form2(DataTable dt)
        {
            InitializeComponent();
            dataTable.DataSource = dt;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            form = null;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=C:\Users\Poloan\Documents\Visual Studio 2010\Projects\LocalDBTrial\LocalDBTrial\data.sdf");
            
            Boolean isSuccessful = true;
            DataTable dt = null;

            conn.Open();
            
            try
            {
                SqlCeCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM dataTbl WHERE id = @id";
                command.Parameters.AddWithValue("@id", selectId.Text);

                SqlCeDataAdapter cda = new SqlCeDataAdapter();
                cda.SelectCommand = command;

                dt = new DataTable();
                cda.Fill(dt);
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show("Error:\n" + ex.Message.ToString());
                isSuccessful = false;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error:\n" + ex.Message.ToString());
                isSuccessful = false;
            }
            finally
            {
                if (isSuccessful)
                {
                    dataTable.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Selection error");
                }
            }

            conn.Close();
        }

        public static Form2 getInstance(DataTable dt)
        {
            if(form == null)
            {
                form = new Form2(dt);
                return form;
            }
            else
            {
                return null;
            }
        }

        
    }
}
