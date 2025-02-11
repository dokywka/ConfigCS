using System.IO;
using System.IO.Compression;
using System.Net;
using static System.Windows.Forms.LinkLabel;

namespace ConfigCS
{
    public partial class Form1 : Form
    {
        public string filename;
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open";
            openFile.Filter = "Text Document(*.txt)|*.txt|CFG(*.cfg)|*.cfg";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filename = openFile.FileName;
                textBox1.Text = File.ReadAllText(filename);
                this.Text = filename;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Vertical;
            string text = textBox1.Text;
            string[] lines = text.Split('\n');

            // создаем новый список для хранения отформатированных строк
            List<string> formattedLines = new List<string>();

            foreach (string line in lines)
            {
                // убираем лишние пробелы и добавляем строку в список
                string trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    formattedLines.Add(trimmedLine);
                }
            }

            // объединяем строки с символом новой строки и устанавливаем в TextBox
            textBox1.Text = string.Join(Environment.NewLine, formattedLines);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filename))
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "Save";
                saveFile.Filter = "Text Document(*.txt)|*.txt|CFG(*.cfg)|*.cfg";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    filename = saveFile.FileName;
                }
                else
                {
                    return;
                }
            }
            File.WriteAllText(filename, textBox1.Text);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveAsFile = new SaveFileDialog();
            saveAsFile.Title = "Save As";
            saveAsFile.Filter = "Text Document(*.txt)|*.txt|CFG(*.cfg)|*.cfg";
            if (saveAsFile.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveAsFile.FileName, textBox1.Text);
                this.Text = saveAsFile.FileName;
            }
            filename = saveAsFile.FileName;
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void s1mpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://cs-config.ru/files/0-0-0-43-20";
            string downloadPath = @"C:\Users\egorp\Downloads\43_s1mple.zip";
            string extractPath = @"C:\Users\egorp\Downloads\s1mple"; // папка для разархивирования
            string targetFileName = "config.cfg"; // имя файла, который нужно извлечь
            string configFilePath = Path.Combine(extractPath, targetFileName);//соединяет две строки
            string extractedFilePath = configFilePath;

            using (WebClient wc = new WebClient())
            {
                // Скачиваем ZIP-файл
                wc.DownloadFile(url, downloadPath);

                // создаем папку для разархивирования, если она не существует
                if (!Directory.Exists(extractPath))
                {
                    Directory.CreateDirectory(extractPath);
                }

                // открываем ZIP-архив
                using (ZipArchive archive = ZipFile.OpenRead(downloadPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        // проверяем соответствует ли имя файла нужному
                        if (entry.FullName.Equals(targetFileName))
                        {

                            entry.ExtractToFile(extractedFilePath, overwrite: true);//bool overwrite для того, что заменить уже существующий файл, если он был создан в архиве
                        }
                    }
                }

                if (File.Exists(configFilePath))
                {
                    int skipLines = 5;
                    string[] lines = File.ReadAllLines(configFilePath);

                    textBox1.Text = string.Join(Environment.NewLine, lines.Skip(skipLines).Take(lines.Length-12));
                    
                }
                else
                {
                    MessageBox.Show("Файл config.cfg не найден.");
                }
            }
        }
    }
}
