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
        private bool _isLoadedConfig;

        private int _deck1Start;
        private int _deck1Height;
        private int _deck2Start;
        private int _deck2Height;

        private List<string> configs;
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

            _isLoadedConfig = false;
            SetBoundariesComboBox();

            _deck1Start = configHandler.GetDeck1Start(configs[0]);
            _deck1Height = configHandler.GetDeck1Height(configs[0]);
            _deck2Start = configHandler.GetDeck2Start(configs[0]);
            _deck2Height = configHandler.GetDeck2Height(configs[0]);

            _deck1capturer = new Capturer(_deck1Start, _deck1Height);
            _deck2capturer = new Capturer(_deck2Start, _deck2Height);

            _previewing = false;
            _status = string.Empty;
            _isReady = false;
            _transmitting = false;

            _fps = 30;
        }

        private void SetBoundariesComboBox()
        {
            try { 
                configs = configHandler.LoadConfig();
                boundariesCombobox.DataSource = configs;
                _isLoadedConfig = true;
            }
            catch (Exception){
                _isLoadedConfig = false;
            }
        }

        private void SetStatus()
        {
            _status = string.Empty;

            if (fpsCombobox.SelectedItem == null)
            {
                _status = "Please select a framerate";
                _isReady = false;
            }
            else if (boundariesCombobox.SelectedItem == null)
            {
                _status = "Please select a boundaries";
                _isReady = false;
            }
            else if (!_isLoadedConfig)
            {
                _status = "Loading config failed";
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
            SetTransmitButton();
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

            try
            {
                deck1picturebox.Image = _deck1capturer.Capture();
                deck2picturebox.Image = _deck2capturer.Capture();
            }
            catch (Exception ex)
            {
                previewButton_Click(null, null);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetTransmitButton()
        {
            button1.BackColor = _isReady ? Color.LightGreen : Color.LightYellow;
            
            if (_transmitting )
                button1.BackColor = Color.IndianRed;
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
                button1.Text = "CONNECT";
            }
            else
            {
                if (_isReady)
                {
                    progressBar1.Value = 100;
                    await Task.Delay(1100);
                    if (_isReady)
                    {
                        button1.Text = "DISCONNECT";
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

        private void boundariesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_deck1capturer == null || _deck2capturer == null)
            {
                // Handle the null case appropriately
                return;
            }

            string currentConfig = boundariesCombobox.SelectedValue.ToString();

            _deck1capturer.ChangeCaptureBoundaries(
                configHandler.GetDeck1Start(currentConfig),
                configHandler.GetDeck1Height(currentConfig));

            _deck2capturer.ChangeCaptureBoundaries(
                configHandler.GetDeck2Start(currentConfig),
                configHandler.GetDeck2Height(currentConfig));
        }

        private void boundariesCombobox_MouseClick(object sender, MouseEventArgs e)
        {
            SetBoundariesComboBox();
        }
    }
}
