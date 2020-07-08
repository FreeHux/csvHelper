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
        private List<string> lstWords = new List<string>();

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
                    line.Trim();
                    lstLines.Add(line);
                }
            }
            lstLines.RemoveAll(string.IsNullOrWhiteSpace);
        }

        public void splitToWords()
        {
            foreach (var line in lstLines)
            {
                string[] words = line.Split(' ');
                foreach (var word in words)
                {
                    if (word != " ")
                    {
                        word.Trim();
                        lstWords.Add(word);
                    }
                }
            }
        }

        public void cleanUp()
        {
            content.Replace("  ", " ");
            content.Replace("\n\r", "\n");
        }

        public string getContent()
        {
            return this.content;
        }

        public List<string> getLstLines()
        {
            return this.lstLines;
        }

        public List<string> getLstWords()
        {
            return this.lstWords;
        }

        public List<string> findWord(List<string> input, string search)
        {
            List<string> foundWords = new List<string>();
            
            foreach(var line in input)
            {
                if (line.Contains(search))
                {
                    foundWords.Add(line);
                }
            }
            return foundWords;
        }
    }
}