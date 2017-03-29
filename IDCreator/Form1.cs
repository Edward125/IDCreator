using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IDCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 參數


        int[] weight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        char[] validdate = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        string id = string.Empty;
        #endregion


        private char  getValidateCode(string id17)
        {

            int sum = 0;
            int mode = 0;           

            for (int i = 0; i < id17.Length; i++)
            {
                sum = sum + Convert.ToInt16(id17.Substring(i, 1)) * weight[i];
            }
            mode = sum % 11;
            return validdate[mode];

        }

        private void txtNew17_TextChanged(object sender, EventArgs e)
        {
            if (txtNew17.Text.Trim().Length == 17)
            {
                txtNew18.Text = txtNew17.Text.Trim() + getValidateCode(txtNew17.Text.Trim());
            }
        }

        private void txtNew17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 & e.KeyChar <= 57 | e.KeyChar == 8)
            {
               // e.KeyChar = Convert.ToChar(8);
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void txtOld15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 & e.KeyChar <= 57 | e.KeyChar == 8)
            {
                // e.KeyChar = Convert.ToChar(8);
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void txtOld15_TextChanged(object sender, EventArgs e)
        {
            if (txtOld15.Text.Length <= 6)
            {
                txtNew17.Text = txtOld15.Text;
            }
            else
            {
                txtNew17.Text = txtOld15.Text.Substring(0, 6) + "19" + txtOld15.Text.Substring(6, txtOld15.Text.Length - 6);
            }            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "身份證號生成驗證器,Ver:" + Application.ProductVersion;
        }





        #region Person



        enum sex
        {
            男,
            女
        }


        struct Person
        {

            string Name;
            sex Sex;
            string Birthday;
            string Address;

        }  


        #endregion


        #region create ranom nunmber

        /// <summary>
        /// create a random number between min ~ max
        /// </summary>
        /// <param name="min">min</param>
        /// <param name="max">max</param>
        /// <returns></returns>
        public int CreateRandNum(int min, int max)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(min, max);
        }

        #endregion
        private void btnCreate_Click(object sender, EventArgs e)
        {
            txtName.Text = GenCnName.getRandomName();
            string ID = GenPinCode.GetGenPinCode();
            txtID.Text = ID.Substring(0, 18);
            txtAddress.Text = ID.Remove(0, 18);
            //MessageBox.Show(txtID.Text.Substring(16, 1));
            int i = Convert.ToInt16(txtID.Text.Substring(16, 1));
            if (i % 2 == 0)
                txtSex.Text = "女";
            else
                txtSex.Text = "男";

            txtDate.Text = txtID.Text.Substring(6, 4) + "年" + txtID.Text.Substring(10, 2) + "月" + txtID.Text.Substring(12, 2) + "日";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtID.Text.Trim()) && id != txtID .Text )
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\ID_INFO_LIST.txt", true);
                string head = "--------------------" + DateTime.Now.ToString("yyyyMMddHHmmss") + "--------------------";
                sw.WriteLine(head);
                sw.WriteLine("Name:" + txtName.Text);
                sw.WriteLine("Sex:" + txtSex.Text);
                sw.WriteLine("Birthday:" + txtDate.Text);
                sw.WriteLine("Address:" + txtAddress.Text);
                sw.WriteLine("ID:" + txtID.Text);
                sw.Close();
                id = txtID.Text;
            }
            
        }
    }
}
