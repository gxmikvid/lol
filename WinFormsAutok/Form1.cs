using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsAutok
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=autok;port=3306;password=");
        #region db_stuff
        public void DBHandler(MySqlConnection connection, string command, Action<MySqlCommand> Method)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(command, connection);
                Method(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        public void UpdateList(MySqlCommand cmd)
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            allAutok.Items.Clear();
            while (dr.Read())
            {
                Auto auto = new Auto(dr.GetInt32("id"), dr.GetString("rendszam"), dr.GetInt32("uzembe_helyezve"), dr.GetString("szin"));
                allAutok.Items.Add(auto);
            }
        }

        public void SQLUpload(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@rendszam", rendszamCont.Text);
            cmd.Parameters.AddWithValue("@uzembh", evCont.Text);
            cmd.Parameters.AddWithValue("@szin", szinCont.Text);
            cmd.ExecuteNonQuery();
        }

        public void SQLDelete(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@id", idCont.Text);
            cmd.ExecuteNonQuery();
        }

        public void Fill(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@rendszam", allAutok.Text);
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            idCont.Value = dr.GetInt32("id");
            rendszamCont.Text = dr.GetString("rendszam");
            evCont.Value = dr.GetInt32("uzembe_helyezve");
            szinCont.Text = dr.GetString("szin");
        }

        public void SQLModify(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@rendszam", rendszamCont.Text);
            cmd.Parameters.AddWithValue("@uzembh", evCont.Text);
            cmd.Parameters.AddWithValue("@szin", szinCont.Text);
            cmd.Parameters.AddWithValue("@id", idCont.Value);
            cmd.ExecuteNonQuery();
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBHandler(conn, "SELECT * FROM autok", UpdateList);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            DBHandler(conn, "INSERT INTO autok (id, rendszam, uzembe_helyezve, szin) VALUES (NULL, @rendszam, @uzembh, @szin)", SQLUpload);
            DBHandler(conn, "SELECT * FROM autok", UpdateList);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DBHandler(conn, "DELETE FROM autok WHERE id = @id", SQLDelete);
            DBHandler(conn, "SELECT * FROM autok", UpdateList);
        }

        private void allAutok_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBHandler(conn, "SELECT * FROM autok WHERE rendszam = @rendszam", Fill);
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            DBHandler(conn, "UPDATE autok SET rendszam = @rendszam, uzembe_helyezve = @uzembh, szin = @szin WHERE autok.id = @id", SQLModify);
            DBHandler(conn, "SELECT * FROM autok", UpdateList);
        }
    }
}
