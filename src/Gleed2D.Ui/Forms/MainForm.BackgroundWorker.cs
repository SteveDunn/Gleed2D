using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Diagnostics ;
using System.IO ;
using System.Drawing ;
using System.Windows.Forms ;
using GLEED2D.Properties ;
using System.Threading ;

namespace GLEED2D.Forms
{
    public partial class MainForm : Form
    {
    	readonly Stopwatch _stopwatch = new Stopwatch();

    	class PassedObject //for passing to background worker
        {
            public Bitmap bmp;
            public FileInfo fileinfo;
        }

        public void loadfolder_background(string path)
        {
            if (backgroundWorker1.IsBusy) backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy)
            {
                Application.DoEvents();
                Thread.Sleep(50);
            }
            imageList48.Images.Clear();
            imageList64.Images.Clear();
            imageList96.Images.Clear();
            imageList128.Images.Clear();
            imageList256.Images.Clear();
            uiTexturesListView.Clear();

            DirectoryInfo di = new DirectoryInfo(path);
            textBox1.Text = di.FullName;

            DirectoryInfo[] folders = di.GetDirectories();
            foreach (DirectoryInfo folder in folders)
            {
                Image img = Resources.FolderImage;

                imageList48.Images.Add(folder.Name, img);
                imageList64.Images.Add(folder.Name, img);
                imageList96.Images.Add(folder.Name, img);
                imageList128.Images.Add(folder.Name, img);
                imageList256.Images.Add(folder.Name, img);

                ListViewItem lvi = new ListViewItem();
                lvi.Text = folder.Name;
                lvi.ToolTipText = folder.Name;
                lvi.ImageIndex = imageList128.Images.IndexOfKey(folder.Name);
                lvi.Tag = "folder";
                lvi.Name = folder.FullName;
                uiTexturesListView.Items.Add(lvi);
            }

            string filters = "*.jpg;*.png;*.gif;*.bmp;*.tga";
            List<FileInfo> fileList = new List<FileInfo>();
            string[] extensions = filters.Split(';');
            foreach (string filter in extensions) fileList.AddRange(di.GetFiles(filter));
            FileInfo[] files = fileList.ToArray();

            Bitmap bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, Color.Azure);
            imageList48.Images.Add("default", bmp);
            imageList64.Images.Add("default", bmp);
            imageList96.Images.Add("default", bmp);
            imageList128.Images.Add("default", bmp);
            imageList256.Images.Add("default", bmp);
            foreach (FileInfo file in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Name = file.FullName;
                lvi.Text = file.Name;
                lvi.ImageKey = "default";
                lvi.Tag = "file";
                uiTexturesListView.Items.Add(lvi);
            }
            
            _stopwatch.Start();
            backgroundWorker1.RunWorkerAsync(files);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo[] files = (FileInfo[])e.Argument;
            BackgroundWorker worker = (BackgroundWorker)sender;
            int filesprogressed = 0;
            foreach (FileInfo file in files)
            {
                try
                {
                    PassedObject po = new PassedObject();
                    po.bmp = new Bitmap(file.FullName);
                    po.fileinfo = file;
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    filesprogressed++;
                    worker.ReportProgress(filesprogressed, po);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PassedObject po = (PassedObject)e.UserState;
            
            imageList48.Images.Add(po.fileinfo.FullName, getThumbnail(po.bmp, 48, 48));
            imageList64.Images.Add(po.fileinfo.FullName, getThumbnail(po.bmp, 64, 64));
            imageList96.Images.Add(po.fileinfo.FullName, getThumbnail(po.bmp, 96, 96));
            imageList128.Images.Add(po.fileinfo.FullName, getThumbnail(po.bmp, 128, 128));
            imageList256.Images.Add(po.fileinfo.FullName, getThumbnail(po.bmp, 256, 256));

            uiTexturesListView.Items[po.fileinfo.FullName].ImageKey = po.fileinfo.FullName;
            uiTexturesListView.Items[po.fileinfo.FullName].ToolTipText = po.fileinfo.Name + " (" + po.bmp.Width.ToString() + " x " + po.bmp.Height.ToString() + ")";

            /*ListViewItem lvi = new ListViewItem();
            lvi.Name = po.fileinfo.FullName;
            lvi.Text = po.fileinfo.Name;
            lvi.ImageKey = po.fileinfo.FullName;
            lvi.Tag = "file";
            lvi.ToolTipText = po.fileinfo.Name + " (" + po.bmp.Width + " x " + po.bmp.Height + ")";
            listView1.Items.Add(lvi);
             * */
            
            toolStripStatusLabel1.Text = e.ProgressPercentage.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _stopwatch.Stop();
            toolStripStatusLabel1.Text = "Time: " + _stopwatch.Elapsed.TotalSeconds.ToString();
            _stopwatch.Reset();
        }
    }
}
