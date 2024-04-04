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
        string NowVideoType;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NowVideoType = string.Empty;
            this.ActiveControl = null;
            this.Focus();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            //if(cBox_Type.SelectedIndex == 0)
            //{
            //    FolderBrowserDialog fd = new FolderBrowserDialog();
            //    fd.RootFolder = Environment.SpecialFolder.MyComputer;
            //    fd.Description = "Selecet folder";
            //
            //    if (fd.ShowDialog() == DialogResult.OK)
            //    {
            //        string selectedFilePath = fd.SelectedPath;
            //        textBox1.Text = selectedFilePath;
            //    }
            //}
            //else
            //{
            //    OpenFileDialog ofd = new OpenFileDialog();
            //    ofd.InitialDirectory = "c:\\";
            //    ofd.Filter = "h264 files (*.h264)|*.h264|All files (*.*)|*.*";
            //    ofd.RestoreDirectory = true;
            //
            //    if(ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        string selectedFile = ofd.FileName;
            //        textBox1.Text = selectedFile;
            //    }
            //}
        }   //

        private void textBox2_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog fd = new FolderBrowserDialog();
            //fd.RootFolder = Environment.SpecialFolder.MyComputer;
            //fd.Description = "Selecet folder";
            //
            //if (fd.ShowDialog() == D3ialogResult.OK)
            //{
            //    string selectedFilePath = fd.SelectedPath;
            //    textBox2.Text = selectedFilePath;
            //}
        }

        private void FolderConvertH264ToMp4Async()
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

                    /*
                    //if (NowVideoType == ".wmv")
                    //{
                    //    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -threads 4 -c:v wmv2 -c:a wmav2 \"{outputPath}\"";
                    //    process.StartInfo.UseShellExecute = false;
                    //    process.StartInfo.RedirectStandardOutput = true;
                    //    process.StartInfo.RedirectStandardError = true;
                    //}
                    //else if (NowVideoType == ".flv")
                    //{
                    //    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -c:v libx264 -c:a aac \"{outputPath}\"";
                    //    process.StartInfo.UseShellExecute = false;
                    //    process.StartInfo.RedirectStandardOutput = true;
                    //    process.StartInfo.RedirectStandardError = true;
                    //}
                    //else if (NowVideoType == ".mov")
                    //{
                    //    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -c:v libx264 -c:a aac \"{outputPath}\"";
                    //    process.StartInfo.UseShellExecute = false;
                    //    process.StartInfo.RedirectStandardOutput = true;
                    //    process.StartInfo.RedirectStandardError = true;
                    //}
                    //else
                    //{
                    */
                        process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -c:v copy \"{outputPath}\"";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                    //}

                    try
                    {
                        process.Start();

                        if (!process.WaitForExit(10000))
                            process.Kill();
                        else if (process.ExitCode != 0)
                            Console.WriteLine("");
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }

            MessageBox.Show("Work Done", "Video format Convert", MessageBoxButtons.OK);
        }


        private void FileConvertH264ToMp4Async()
        {
            string directoryPath = textBox1.Text;
            string sp = Application.StartupPath.Substring(0, Application.StartupPath.Length - 5);
            string ffmpegPath = sp + "ffmpeg-2024-03-20-git-e04c638f5f-essentials_build\\bin\\ffmpeg.exe";

            using (var process = new Process())
            {
                string filePath = textBox1.Text;
                string filename = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(textBox2.Text, filename + NowVideoType);

                if (File.Exists(outputPath))
                    File.Delete(outputPath);

                process.StartInfo.FileName = ffmpegPath;

                if (NowVideoType == ".wmv")
                {
                    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -threads 4 -c:v wmv2 -c:a wmav2 \"{outputPath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                }
                else if (NowVideoType == ".flv")
                {
                    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -c:v libx264 -c:a aac \"{outputPath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                }
                else if (NowVideoType == ".mov")
                {
                    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -c:v libx264 -c:a aac \"{outputPath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                }
                else
                {
                    process.StartInfo.Arguments = $"-r 30 -i \"{filePath}\" -c:v copy \"{outputPath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                }
                process.Start();

                process.WaitForExit();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //if(cBox_Type.SelectedIndex == 0)
                FolderConvertH264ToMp4Async();
            //else
                //FileConvertH264ToMp4Async();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch(comboBox1.SelectedIndex)
            //{
            //    case 0:
            //        NowVideoType = ".mkv";
            //        break;
            //    case 1:
            //        NowVideoType = ".wmv";
            //        break;
            //    case 2:
            //        NowVideoType = ".mp4";
            //        break;
            //    case 3:
            //        NowVideoType = ".mpeg";
            //        break;
            //    case 4:
            //        NowVideoType = ".mov";
            //        break;
            //    case 5:
            //        NowVideoType = ".flv";
            //        break;
            //}
        }

        private void cBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.MyComputer;
            fd.Description = "Selecet folder";

            string lastPath = Properties.Settings.Default.LastFolderPath;
            if (!string.IsNullOrEmpty(lastPath))
            {
                fd.SelectedPath = lastPath;
            }
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = fd.SelectedPath;

                Properties.Settings.Default.LastFolderPath = selectedFilePath;
                Properties.Settings.Default.Save();

                textBox1.Text = selectedFilePath;
            }
        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.MyComputer;
            fd.Description = "Selecet folder";

            string lastPath = Properties.Settings.Default.LastFolderPath2; 
            if (!string.IsNullOrEmpty(lastPath))
            {
                fd.SelectedPath = lastPath;
            }
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = fd.SelectedPath;

                Properties.Settings.Default.LastFolderPath2 = selectedFilePath;
                Properties.Settings.Default.Save();

                textBox2.Text = selectedFilePath;
            }
        }
    }
}
