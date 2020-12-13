using BoardParser.Common.Interfaces;
using BoardParser.Common.Models;
using BoardParser.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
            splitNumericUpDown.Value = 20;

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
            _settings.Split = splitCheckBox.Checked;
            _settings.AmountToSplit = Convert.ToInt32(splitNumericUpDown.Value);
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

        private void splitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InitSettings();
        }

        private void splitNumericUpDown_DragLeave(object sender, EventArgs e)
        {
            InitSettings();
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
                var pageType = _siteParserService.GetPageType(settings.Page);

                if (pageType == PageTypes.Unknown)
                {
                    MessageBox.Show($"Unknown page. Please enter url of another page.", "Error");
                    Invoke(new Action(() => startButton.Enabled = true));
                    return;
                }

                _siteParserService.ProcessEvent += DisplayProgress;
                list = _siteParserService.ParsePageAsync(settings.Page, pageType).Result;

                if (list == null || list.Count == 0)
                {
                    MessageBox.Show($"Empty parsing result", "Status");
                    Invoke(new Action(() => startButton.Enabled = true));
                    return;
                }
                else
                {
                    // check if item already exist
                    var ids = _fileService.GetIds().Result;
                    var filteredList = new List<BoardItem>();
                    foreach (var item in list)
                    {
                        if (!ids.Any(x => x == item.Id))
                            filteredList.Add(item);
                    }

                    // save new item ids 
                    ids.AddRange(filteredList.Select(x => x.Id));
                    _fileService.SaveIds(ids);

                    // TODO: refactor
                    if (filteredList == null || filteredList.Count == 0)
                    {
                        MessageBox.Show($"Empty parsing result", "Status");
                        Invoke(new Action(() => startButton.Enabled = true));
                        return;
                    }

                    if (settings.Split)
                    {
                        _fileService.WriteXmlSeparated(settings.ExportFilePath, filteredList, settings.AmountToSplit);
                        var open = MessageBox.Show("Parsing was finished.", "Status");
                    }
                    else
                    {
                        var path = _fileService.WriteXml(settings.ExportFilePath, filteredList).Result;

                        var open = MessageBox.Show("Parsing was finished. Open resuls file?", "Status", MessageBoxButtons.YesNo);
                        if (open == DialogResult.Yes)
                            Process.Start("notepad.exe", path);
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: add log
                MessageBox.Show($"Parsing Error: {ex.Message}, StackTrace: {ex.StackTrace}", "Error");
            }

            Invoke(new Action(() => startButton.Enabled = true));
            Invoke(new Action(() => progressBar.Value = 0));
        }

        private void DisplayProgress(int value, int maxValue)
        {
            Invoke(new Action(() => progressBar.Maximum = maxValue));
            Invoke(new Action(() => progressBar.Value = value));
        }

        private void AddLog(string line)
        {
            Invoke(new Action(() => logsTxtBox.AppendText(line + "\r\n")));
        }
    }
}
