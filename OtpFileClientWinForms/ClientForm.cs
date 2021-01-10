using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace OtpFileClientWinForms
{
    public partial class ClientForm : Form
    {
        private readonly OtpFileClientProxy _clientProxy;

        private OtpFileDownload _otpFileDownload;

        public ClientForm()
        {
            InitializeComponent();

            _clientProxy = new OtpFileClientProxy();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            UpdateDataSource();

            Initialize();
        }

        /// <summary>
        /// komponensek inicializálása
        /// </summary>
        private void Initialize()
        {
            docsDataGridView.Columns[0].HeaderText = "Fájl neve";
            docsDataGridView.Columns[0].ReadOnly = true;
            docsDataGridView.Columns[0].MinimumWidth = 150;
            docsDataGridView.Columns[0].Width = 500;

            var dataGridViewCellStyle1 = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                ForeColor = System.Drawing.Color.Navy,
                NullValue = null
            };

            var btn = new DataGridViewButtonColumn
            {
                HeaderText = "Fájl letöltése",
                ToolTipText = "File letöltése",
                Text = "Letöltés",
                Name = "btnFileDownload",
                UseColumnTextForButtonValue = true,
                DefaultCellStyle = dataGridViewCellStyle1,
                FlatStyle = FlatStyle.Popup,
                ReadOnly = true,
                Width = 120
            };

            docsDataGridView.Columns.Add(btn);

            var initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            saveOtpDownloadFileDialog.InitialDirectory = initialDirectory;

            openOtpUploadFileDialog.InitialDirectory = initialDirectory;
        }

        /// <summary>
        /// grid adatforrás beállítása
        /// </summary>
        private void UpdateDataSource()
        {
            var folderFiles = _clientProxy.GetFolderFiles();

            var fileList = folderFiles.Select(fileName => new OtpFile
            {
                Name = fileName
            }).ToList();

            var bindingList = new BindingList<OtpFile>(fileList);
            var source = new BindingSource(bindingList, null);

            docsDataGridView.DataSource = source;
        }

        /// <summary>
        /// fájl letöltése szerverről
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView dataGridView)
            {
                if (dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    var bindingSource = dataGridView.DataSource as BindingSource;

                    var bindingList = bindingSource.DataSource as BindingList<OtpFile>;

                    var otpFile = bindingList.ElementAtOrDefault(e.RowIndex);

                    _otpFileDownload = _clientProxy.DownloadFile(otpFile.Name);

                    saveOtpDownloadFileDialog.ShowDialog();
                }
            }
        }

        /// <summary>
        /// letöltött fájl mentése fájl rendszerbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveOtpDownloadFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (sender is SaveFileDialog saveFileDialog)
            {
                var byteContent = Convert.FromBase64String(_otpFileDownload.Content);

                File.WriteAllBytes(saveFileDialog.FileName, byteContent);
            }
        }

        /// <summary>
        /// fájl feltöltése szerverre fájlrendszerből
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOtpFileUpload_Click(object sender, EventArgs e)
        {
            openOtpUploadFileDialog.ShowDialog();
        }

        /// <summary>
        /// fájl feltöltés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openOtpUploadFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (sender is OpenFileDialog openFileDialog)
            {
                var byteContent = File.ReadAllBytes(openFileDialog.FileName);

                var content = Convert.ToBase64String(byteContent);

                var fileName = Path.GetFileName(openFileDialog.FileName);

                var mimeType = MimeMapping.GetMimeMapping(fileName);

                _clientProxy.UploadFile(fileName, new OtpFileUpload { Content = content, MimeType = mimeType });

                UpdateDataSource();
            }
        }

        /// <summary>
        /// segédosztály az adatkötéshez
        /// </summary>
        private class OtpFile
        {
            public string Name { get; set; }
        }
    }
}
