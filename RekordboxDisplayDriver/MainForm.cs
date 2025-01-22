using RekordboxDisplayDriver.Entities;
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
        public MainForm()
        {
            InitializeComponent();

            SetEntities();
        }

        private void SetEntities()
        {
            _deck1capturer = new Capturer(80, 70);
            _deck2capturer = new Capturer(151, 70);
            _previewing = false;
            _status = string.Empty;
            _fps = 30;
        }

        private void SetStatus()
        {
            _status = string.Empty;

            if (fpsCombobox.SelectedItem == null)
            {
                _status = "Please select a FPS";
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void fpsCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fps = int.Parse(fpsCombobox.SelectedItem!.ToString()!);
            preview.Interval = 1000 / _fps;
        }

        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        UpdatePicturebox();
        //        await Task.Delay(10);
        //    }
        //}
    }
}
