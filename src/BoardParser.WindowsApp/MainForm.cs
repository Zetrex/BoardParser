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

        public MainForm()
        {
            InitializeComponent();

            _siteParserService = (ISiteParserService)Program.ServiceProvider.GetService(typeof(ISiteParserService));
            _settings = new ParsingSettings();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pageTextBox.Text = "https://www.rupostings.com/show?id=157534";     // siteRadioButton1.Text;
            filePathTextBox.Text = System.Reflection.Assembly.GetExecutingAssembly().Location;

            InitSettings();
        }

        private void InitSettings()
        {
            if (siteRadioButton1.Checked)
                _settings.SiteName = siteRadioButton1.Text;
            else if (siteRadioButton2.Checked)
                _settings.SiteName = siteRadioButton2.Text;

            _settings.Page = pageTextBox.Text;
            _settings.ExportFilePath = pageTextBox.Text;

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

            if (settings.Page == _siteParserService.GetSiteName())
                list = _siteParserService.ParseMainPageAsync().Result;
            else
                list.Add(_siteParserService.ParsePageAsync(settings.Page).Result);

            MessageBox.Show("Parsing was finished", "Status");
            Invoke(new Action(() => startButton.Enabled = true));
        }


    }
}
