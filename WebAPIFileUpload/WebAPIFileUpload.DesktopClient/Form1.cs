using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebAPIFileUpload.Common.Infrastructure;
using WebAPIFileUpload.Common.Utilities;

namespace WebAPIFileUpload.DesktopClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (textBox1.Visible)
            {
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(sInfo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    //SimpleProgressHandler handler = new SimpleProgressHandler();
                    //handler.HttpSendProgress += (object owner, System.Net.Http.Handlers.HttpProgressEventArgs evt) =>
                    //{
                    //    Debug.WriteLine("BytesTransferred =[{0}]", evt.BytesTransferred);

                    //    //handler.BytesTransferred = evt.BytesTransferred;
                    //    //Debug.WriteLine("ProgressPercentage {0} %", handler.ProgressPercentage);
                    //};

                    HttpClient httpClient = new HttpClient();
                    
                    // Read the files 
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        var fileStream = File.Open(file, FileMode.Open);
                        var fileInfo = new FileInfo(file);
                        FileUploadResult uploadResult = null;
                        bool _fileUploaded = false;

                        //handler.BytesTransferred = 0;
                        //handler.FileSize = fileInfo.Length;

                        var content = new MultipartFormDataContent();
                        content.Add(new StreamContent(fileStream), "\"file\"", string.Format("\"{0}\"", fileInfo.Name));

                        Task taskUpload = httpClient.PostAsync(uploadServiceBaseAddress, content).ContinueWith(task =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion)
                            {
                                var response = task.Result;

                                if (response.IsSuccessStatusCode)
                                {
                                    uploadResult = response.Content.ReadAsAsync<FileUploadResult>().Result;
                                    if (uploadResult != null)
                                        _fileUploaded = true;

                                    // Read other header values if you want..
                                    foreach (var header in response.Content.Headers)
                                    {
                                        Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                                    }
                                }
                                else
                                {
                                    Debug.WriteLine("Status Code: {0} - {1}", response.StatusCode, response.ReasonPhrase);
                                    Debug.WriteLine("Response Body: {0}", response.Content.ReadAsStringAsync().Result);
                                }
                            }

                            fileStream.Dispose();
                        });

                        taskUpload.Wait();
                        if (_fileUploaded)
                            AddMessage(uploadResult.FileName + " with length " + uploadResult.FileLength + " has been uploaded at " + uploadResult.LocalFilePath);
                    }

                    httpClient.Dispose();
                }
                catch (Exception ex)
                {
                    AddMessage(ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.linkLabel1.Links.Remove(linkLabel1.Links[0]);
            this.linkLabel1.Links.Add(0, linkLabel1.Text.Length, "http://chsakell.com/");

            // Set the file dialog to filter for graphics files. 
            this.openFileDialog1.Filter =
                "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                "All files (*.*)|*.*";

            // Allow the user to select multiple images. 
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Browse files to upload.";
        }

        private void AddMessage(string message)
        {
            try
            {
                textBox1.AppendText(message);
                textBox1.AppendText(Environment.NewLine);
                textBox1.AppendText(Environment.NewLine);
            }
            catch
            {
            }
        }

        private string uploadServiceBaseAddress
        {
            get
            {
                var serviceAddress = Configurations.GetServiceAddress();
                // var serviceAddress = Configurations.GetServiceAddress("SelfhostBaseServiceAddress");

                AddMessage(string.Format("Connected to Service Address = [{0}]", serviceAddress));

                return serviceAddress;
            }
        }
    }
}
