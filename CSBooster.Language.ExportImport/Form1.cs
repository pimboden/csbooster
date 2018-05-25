using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSBooster.Language.ExportImport
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = radioButton2.Checked;
            if (radioButton2.Checked)
                btnGo.Tag = "export";
            else if (radioButton1.Checked)
                btnGo.Tag = "import";
            else if (radioButton3.Checked)
                btnGo.Tag = "merge"; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;

            btnGo.Enabled = !string.IsNullOrEmpty(textBox1.Text);   
        }

        private void main_Load(object sender, EventArgs e)
        {
            lbxLanguage.Items.Clear();
            lbxLanguage.Items.Add("de");
            lbxLanguage.Items.Add("de-CH");
            lbxLanguage.Items.Add("de-DE");
            lbxLanguage.Items.Add("en");
            lbxLanguage.Items.Add("en-US");
            lbxLanguage.Items.Add("en-GB");
            lbxLanguage.Items.Add("fr");
            lbxLanguage.Items.Add("fr-CH");
            lbxLanguage.Items.Add("fr-FR");
            lbxLanguage.Items.Add("it");
            lbxLanguage.Items.Add("it-CH");
            lbxLanguage.Items.Add("it-IT");
            lbxLanguage.Items.Add("es");
            lbxLanguage.Items.Add("es-ES");

            radioButton2.Checked = true; 
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnGo.Tag == "export")
                {
                    List<string> languages = new List<string>();
                    foreach (object lang in lbxLanguage.SelectedItems)
                    {
                        languages.Add(lang.ToString());
                    }

                    Export export = new Export();
                    export.DoExport(textBox1.Text, languages, cbxMultiFile.Checked, cbxMultiLang.Checked, cbxDefaultText.Checked);
                }
                else if (btnGo.Tag == "import")
                {
                    Import import = new Import();
                    import.DoMerge = false; 
                    import.DoCopy = true;
                    import.DoImport(textBox1.Text);
                }
                else if (btnGo.Tag == "merge")
                {
                    Import import = new Import();
                    import.DoMerge = true;
                    import.DoCopy = false;
                    import.DoImport(textBox1.Text);
                }

                MessageBox.Show(this, "Fertig");  
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.ToString());  
            }

        }
    }
}
