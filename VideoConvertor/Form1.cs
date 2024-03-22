using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//C:\Git\VideoConvertor\VideoConvertor\bin\ffmpeg-2024-03-20-git-e04c638f5f-essentials_build\bin
namespace VideoConvertor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            this.Focus();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.MyComputer;
            fd.Description = "Selecet folder";

            if(fd.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = fd.SelectedPath;
                textBox1.Text = selectedFilePath;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.MyComputer;
            fd.Description = "Selecet folder";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = fd.SelectedPath;
                textBox2.Text = selectedFilePath;
            }
        }

        private void DoEvent()
        {
            string directoryPath = textBox1.Text;
            string sp = Application.StartupPath.Substring(0, Application.StartupPath.Length - 5);
            string ffmpegPath = sp + "ffmpeg-2024-03-20-git-e04c638f5f-essentials_build\\bin\\ffmpeg.exe";

            foreach(string filePath in Directory.GetFiles(directoryPath, "*.h264"))
            {
                string outputPath = Path.ChangeExtension(textBox2.Text, ".mp4");

                Process process = new Process();
                process.StartInfo.FileName = ffmpegPath;
                process.StartInfo.Arguments = $"-i \"{filePath}\" -c:v copy \"{outputPath}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoEvent();
        }
    }
}
