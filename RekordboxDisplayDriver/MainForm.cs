using RekordboxDisplayDriver.Entities;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RekordboxDisplayDriver
{
    public partial class MainForm : Form
    {
        private Capturer _deck1capturer;
        private Capturer _deck2capturer;
        private bool _previewing;
        private string _status;
        private int _fps;
        private bool _isReady;
        private bool _transmitting;
        public Transmiter transmiter { get; set; }
        public ConfigHandler configHandler { get; set; }

        public MainForm()
        {
            InitializeComponent();

            SetEntities();
        }

        private void SetEntities()
        {
            configHandler = new ConfigHandler();

            _deck1capturer = new Capturer(80, 70);
            _deck2capturer = new Capturer(151, 70);
            _previewing = false;
            _status = string.Empty;
            _isReady = false;
            _transmitting = false;
            _fps = 30;
            boundariesCombobox.DataSource = configHandler.LoadConfig();
        }

        private void SetStatus()
        {
            _status = string.Empty;

            if (fpsCombobox.SelectedItem == null)
            {
                _status = "Please select a FPS";
                _isReady = false;
            }
            else if (boundariesCombobox.SelectedItem == null)
            {
                _status = "Please select a boundaries";
                _isReady = false;
            }//elseif na displaye
            else
            {
                _status = "Ready";
                _isReady = true;
            }

            if (_transmitting)
            {
                _status = "Transmitting...";
            }

            label1.Text = "Status: " + _status;
        }

        private void UpdateDecks()
        {
            if (deck1picturebox.Image != null)
            {
                deck1picturebox.Image.Dispose();
                deck1picturebox.Image = null;
            }

            if (deck2picturebox.Image != null)
            {
                deck2picturebox.Image.Dispose();
                deck2picturebox.Image = null;
            }

            deck1picturebox.Image = _deck1capturer.Capture();
            deck2picturebox.Image = _deck2capturer.Capture();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            SetStatus();
        }

        private void preview_Tick(object sender, EventArgs e)
        {
            UpdateDecks();
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            if (_previewing)
            {
                previewButton.Text = "Start Preview";
                _previewing = false;
                preview.Enabled = false;
                deck1picturebox.Image = null;
                deck2picturebox.Image = null;
            }
            else
            {
                previewButton.Text = "Stop Preview";
                _previewing = true;
                preview.Enabled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void transmit_Click(object sender, EventArgs e)
        {
            if (_transmitting)
            {
                _transmitting = false;
                transmiter = null;
                progressBar1.Value = 0;
                button1.Text = "C O N N E C T  A N D  T R A N S M I T";
            }
            else
            {
                if (_isReady)
                {
                    progressBar1.Value = 100;
                    await Task.Delay(1100);
                    if (_isReady)
                    {
                        button1.Text = "D I S C O N N E C T";
                        transmiter = new Transmiter();
                        //start transmitting
                        _transmitting = true;
                    }
                }
                else
                {
                    progressBar1.Value = 0;
                }
            }
        }

        private void fpsCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fps = int.Parse(fpsCombobox.SelectedItem!.ToString()!);
            preview.Interval = 1000 / _fps;
        }

        private void openConfigButton_Click(object sender, EventArgs e)
        {
            configHandler.OpenConfig();
        }
    }
}
