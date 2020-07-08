using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csvklasse
{
    class csvhelper
    {
        private string content;
        private string[] lines;
        private List<string> lstLines = new List<string>();


        public csvhelper()
        {
            string filePath;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "C:\\";
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    var fileStream = ofd.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        this.content = reader.ReadToEnd();
                        this.content = content.Replace("\r\n", "\n").Replace('\r', '\n');
                    }
                }
            }
        }

        public void splitToLines()
        {
            if (content == string.Empty)
            {
                MessageBox.Show("kein content geladen");
            }
            else
            { 
                lines = content.Split('\n');
                foreach (var line in lines)
                {
                    lstLines.Add(line);
                }
            }
        }


        public string getContent()
        {
            return this.content;
        }

        public List<string> getLstLines()
        {
            return this.lstLines;
        }
    }
}
