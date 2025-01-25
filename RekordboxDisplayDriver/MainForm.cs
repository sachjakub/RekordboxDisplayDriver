using RekordboxDisplayDriver.Entities;
using System.Timers;
using System.Threading.Tasks;

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

        private List<string> _configs;
        private Transmiter _transmiterL;
        public Transmiter _transmiterR;
        public ConfigHandler _configHandler;

        private System.Timers.Timer _updateDecksTimer;

        public MainForm()
        {
            InitializeComponent();
            SetEntities();
        }

        private void SetEntities()
        {
            _configHandler = new ConfigHandler();
            _transmiterL = new Transmiter();
            _transmiterR = new Transmiter();

            _isLoadedConfig = false;
            SetBoundariesComboBox();
            SetPortsComboBoxes();

            _deck1Start = _configHandler.GetDeck1Start(_configs[0]);
            _deck1Height = _configHandler.GetDeck1Height(_configs[0]);
            _deck2Start = _configHandler.GetDeck2Start(_configs[0]);
            _deck2Height = _configHandler.GetDeck2Height(_configs[0]);

            _deck1capturer = new Capturer(_deck1Start, _deck1Height);
            _deck2capturer = new Capturer(_deck2Start, _deck2Height);

            _previewing = false;
            _status = string.Empty;
            _isReady = false;
            _transmitting = false;

            _fps = 0;
        }

        private void SetBoundariesComboBox()
        {
            try
            {
                _configs = _configHandler.LoadConfig();
                boundariesCombobox.DataSource = _configs;
                _isLoadedConfig = true;
            }
            catch (Exception)
            {
                _isLoadedConfig = false;
            }
        }

        private void SetPortsComboBoxes()
        {
            try
            {
                comboBox2.DataSource = _transmiterL.GetAvailablePorts();
                comboBox3.DataSource = _transmiterR.GetAvailablePorts();
            }
            catch (Exception)
            {
                MessageBox.Show("No available connections have been found. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
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
            }
            else if (comboBox2.SelectedItem == null /*|| comboBox3.SelectedItem == null*/)
            {
                _status = "Please select a ports";
                _isReady = false;
            }
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

        private async Task UpdateDecks()
        {
            try
            {
                await Task.Run(() => _transmiterL.SendBitmap(_deck1capturer.Capture()));
                //await Task.Run(() => _transmiterR.SendBitmap(_deck2capturer.Capture()));
            }
            catch (Exception ex)
            {
                transmit_Click(null, null);
                MessageBox.Show("Disconnected due to minimalizing the rekordbox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDecksPreview()
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

            GC.Collect();
        }

        private void SetTransmitButton()
        {
            button1.BackColor = _isReady ? Color.LightGreen : Color.LightYellow;

            if (_transmitting)
                button1.BackColor = Color.IndianRed;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            SetStatus();
        }

        private void preview_Tick(object sender, EventArgs e)
        {
            UpdateDecksPreview();
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            if (_fps == 0)
            {
                MessageBox.Show("Please select a framerate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
        private async void transmit_Click(object sender, EventArgs e)
        {
            if (_transmitting)
            {
                _transmitting = false;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                _updateDecksTimer?.Stop();
                _updateDecksTimer?.Dispose();

                progressBar1.Value = 0;
                button1.Text = "CONNECT";
            }
            else
            {
                if (_isReady)
                {
                    progressBar1.Value = 100;
                    await Task.Delay(2000);

                    if (_isReady)
                    {
                        comboBox2.Enabled = false;
                        comboBox3.Enabled = false;
                        button1.Text = "DISCONNECT";

                        try
                        {

                            _transmitting = true;

                            _transmiterL.SetTransmiter(comboBox2.SelectedItem!.ToString()!, 2000000);
                            _transmiterL.Open();

                            //_transmiterR.SetTransmiter(comboBox3.SelectedItem!.ToString()!, 2000000);
                            //_transmiterR.Open();

                            _updateDecksTimer = new System.Timers.Timer(1000.0 / _fps);
                            _updateDecksTimer.Elapsed += async (sender, args) => await UpdateDecks();
                            _updateDecksTimer.AutoReset = true;
                            _updateDecksTimer.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _transmiterL.Close();
                            _transmiterR.Close();
                            _transmitting = false;
                            comboBox2.Enabled = true;
                            comboBox3.Enabled = true;
                            _updateDecksTimer?.Stop();
                            _updateDecksTimer?.Dispose();

                            progressBar1.Value = 0;
                            button1.Text = "CONNECT";
                        }


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
            _configHandler.OpenConfig();
        }

        private void boundariesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_deck1capturer == null || _deck2capturer == null)
                return;

            string currentConfig = boundariesCombobox.SelectedValue.ToString();

            _deck1capturer.ChangeCaptureBoundaries(
                _configHandler.GetDeck1Start(currentConfig),
                _configHandler.GetDeck1Height(currentConfig));

            _deck2capturer.ChangeCaptureBoundaries(
                _configHandler.GetDeck2Start(currentConfig),
                _configHandler.GetDeck2Height(currentConfig));
        }

        private void boundariesCombobox_MouseClick(object sender, MouseEventArgs e)
        {
            SetBoundariesComboBox();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _transmiterL.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _transmiterR.Close();
        }
    }
}
