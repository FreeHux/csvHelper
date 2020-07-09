using System.Collections.Generic;
using System.IO;
using System.Text;
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

        }

        public void openCsv() //opens a file dialog, and reads it to string content
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

        public void splitToLines() //splits the content to lines and stores it into List<string> lstLines
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
                    if (line != string.Empty)
                    {
                        lstLines.Add(line.ToLower());
                    }
                }
            }
            lstLines.RemoveAll(string.IsNullOrWhiteSpace);
        }

        public void splitToWords() //splits the List<string>lstLines into Words and stores in List<string> lstWords
        {
            char[] trimChars = { ',', '.', '!', '?', '"', '“', '”', ';', ':', '_' }; //all punctuation marks are removed
            foreach (var line in lstLines)
            {
                string[] words = line.Split(' ');
                foreach (var word in words)
                {
                    if (word != " " && word != "")
                    {
                        lstWords.Add(word.Trim(trimChars).ToLower());
                    }
                }
            }
        }

        public void cleanUp()
        {
            content.Replace("  ", " ");
            content.Replace("\n\r", "\n");
        }

        public string getContent() //getter for Content
        {
            return this.content;
        }

        public List<string> getLstLines() //getter for lstLines
        {
            return this.lstLines;
        }

        public List<string> getLstWords() //getter for words
        {
            return this.lstWords;
        }

        public List<string> findWord(List<string> input, string search) //function to find words in any List of strings
        {
            List<string> foundWords = new List<string>();

            foreach (var line in input)
            {
                if (line.Contains(search))
                {
                    foundWords.Add(line);
                }
            }
            return foundWords;
        }

        public void saveCSV(List<string> source, string path) //saves List of Strings in a File
        {
            StringBuilder csv = new StringBuilder();

            foreach (var line in source)
            {
                csv.Append(line + "\n");
            }

            File.AppendAllText(path, csv.ToString());
        }
    }
}
