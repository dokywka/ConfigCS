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

            // Создаем новый список для хранения отформатированных строк
            List<string> formattedLines = new List<string>();

            foreach (string line in lines)
            {
                // Убираем лишние пробелы и добавляем строку в список
                string trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    formattedLines.Add(trimmedLine);
                }
            }

            // Объединяем строки с символом новой строки и устанавливаем в TextBox
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
    }
}
