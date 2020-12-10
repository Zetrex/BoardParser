using BoardParser.Common.Interfaces;
using BoardParser.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BoardParser.WindowsApp
{
    public partial class MainForm : Form
    {
        private ParsingSettings _settings;
        private readonly ISiteParserService _siteParserService;
        private readonly IFileService _fileService;

        public MainForm()
        {
            InitializeComponent();

            _siteParserService = (ISiteParserService)Program.ServiceProvider.GetService(typeof(ISiteParserService));
            _fileService = (IFileService)Program.ServiceProvider.GetService(typeof(IFileService));
            _settings = new ParsingSettings();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pageTextBox.Text = "https://www.rupostings.com/show?id=157534";     // siteRadioButton1.Text;
            filePathTextBox.Text = Environment.CurrentDirectory;

            InitSettings();
        }

        private void InitSettings()
        {
            if (siteRadioButton1.Checked)
                _settings.SiteName = siteRadioButton1.Text;
            else if (siteRadioButton2.Checked)
                _settings.SiteName = siteRadioButton2.Text;

            _settings.Page = pageTextBox.Text;
            _settings.ExportFilePath = filePathTextBox.Text;

        }

        private void siteRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            InitSettings();
        }

        private void siteRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            InitSettings();
        }

        private void pageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pageTextBox_Leave(object sender, EventArgs e)
        {
            InitSettings();
        }

        private void filePathTextBox_Leave(object sender, EventArgs e)
        {
            InitSettings();
        }

        private void customPageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pageTextBox.Enabled = customPageCheckBox.Checked;
        }
        private void filePathTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Thread x = new Thread(StartParsing);
            x.Start(_settings);
        }

        private void StartParsing(object input)
        {
            Invoke(new Action(() => startButton.Enabled = false));

            var settings = (ParsingSettings)input;

            List<BoardItem> list = new List<BoardItem>();

            try
            {
                if (settings.Page == _siteParserService.GetSiteName())
                {
                    var warning = MessageBox.Show($"This operation will take a long time. Do you want to continue?", "Warning", MessageBoxButtons.YesNo);
                    if (warning == DialogResult.Yes)
                        list = _siteParserService.ParseMainPageAsync().Result;
                    else
                    {
                        Invoke(new Action(() => startButton.Enabled = true));
                        return;
                    }
                }
                else
                    list.Add(_siteParserService.ParsePageAsync(settings.Page).Result);

                _fileService.WriteXml(settings.ExportFilePath, list);

                MessageBox.Show("Parsing was finished", "Status");
            }
            catch (Exception ex)
            {
                // TODO: add log
                MessageBox.Show($"Parsing Error: {ex.Message}, StackTrace: {ex.StackTrace}", "Error");
            }

            Invoke(new Action(() => startButton.Enabled = true));
        }


    }
}
