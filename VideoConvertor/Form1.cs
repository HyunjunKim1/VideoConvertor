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

namespace VideoConvertor
{
    public partial class Form1 : Form
    {
        string NowVideoType;
        Process process;
        bool Flag = false;

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

        private async Task FolderConvertH264ToMp4Async()
        {
            Flag = false;

            string directoryPath = textBox1.Text;
            string sp = Application.StartupPath.Substring(0, Application.StartupPath.Length - 5);
            string ffmpegPath = Path.Combine(sp, "ffmpeg-2024-03-20-git-e04c638f5f-essentials_build", "bin", "ffmpeg.exe");

            foreach (string filePath in Directory.GetFiles(directoryPath, "*.h264"))
            {
                string filename = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(textBox2.Text, filename + ".mp4");

                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }

                process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ffmpegPath,
                        //Arguments = $"-r 30 -i \"{filePath}\" -c:v libx264 -crf 23 \"{outputPath}\"",
                        Arguments = $"-r 30 -i \"{filePath}\" -c:v copy -c:a aac \"{outputPath}\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                process.OutputDataReceived += (sender, args) => {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        Console.WriteLine(args.Data); // 콘솔에 출력
                        this.Invoke(new Action(() =>
                        {
                            tBox_Log.AppendText(args.Data.ToString() + Environment.NewLine);
                        }));
                    }
                };

                process.ErrorDataReceived += (sender, args) => {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        Console.WriteLine(args.Data); // 콘솔에 출력
                        this.Invoke(new Action(() =>
                        {
                            tBox_Log.AppendText(args.Data.ToString() + Environment.NewLine);
                        }));
                    }
                };

                try
                {
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode != 0)
                    {
                        Console.WriteLine($"Error processing file {filePath}. See logs for details.");
                    }
                    else
                    {
                        Console.WriteLine($"Successfully processed file {filePath}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception processing file {filePath}:\n{ex.Message}");
                }
            }

            if(!Flag)
                MessageBox.Show("Work Done", "Video format Convert", MessageBoxButtons.OK);
            else
                MessageBox.Show("Stoped!", "Video format Convert", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
                FolderConvertH264ToMp4Async();
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

        private void button2_Click(object sender, EventArgs e)
        {
            process.Kill();
            Flag = true;
        }
    }
}
