using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private async Task ConvertH264ToMp4Async()
        {
            string directoryPath = textBox1.Text;
            string sp = Application.StartupPath.Substring(0, Application.StartupPath.Length - 5);
            string ffmpegPath = sp + "ffmpeg-2024-03-20-git-e04c638f5f-essentials_build\\bin\\ffmpeg.exe";

            using (var process = new Process())
            {

                foreach (string filePath in Directory.GetFiles(directoryPath, "*.h264"))
                {
                    string filename = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(textBox2.Text, filename + ".mp4");

                    if (File.Exists(outputPath))
                        File.Delete(outputPath);

                    process.StartInfo.FileName = ffmpegPath;
                    
                    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -c:v copy \"{outputPath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.Start();

                    process.WaitForExit();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ConvertH264ToMp4Async();
        }

        private static string ParseProgress(string line)
        {
            var regex = new Regex(@"time=(\d{2}:\d{2}:\d{2}.\d{2})");
            var match = regex.Match(line);

            if (match.Success)
            {
                return $"Current progress: {match.Groups[1].Value}";
            }

            return null;
        }
    }
}
