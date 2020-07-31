using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Commons.Music.Midi;
using Midihau.Controls;

namespace Midihau.Forms
{
    partial class MainForm : Form
    {
        private IEnumerable<string> midiFiles;
        private MidiMachine midiMachine;

        private MidiMusic midiMusic;
        private SimpleMidiTrack currentTrack;

        public MainForm(MidiMachine midiMachine)
        {
            this.midiMachine = midiMachine ?? throw new ArgumentNullException(nameof(midiMachine));

            InitializeComponent();

            txtFolderLocation.Leave += TxtFolderLocation_Leave;
            txtFolderLocation.KeyDown += TxtFolderLocation_KeyDown;
            txtFilter.TextChanged += TxtFilter_TextChanged;

            ksPlay.SelectedKeyCode = Keys.F1;
            ksStop.SelectedKeyCode = Keys.F2;

            ksPlay.OnKeySelected += OnKeySelected;
            ksStop.OnKeySelected += OnKeySelected;

            GlobalKeyListener.OnKeyPressed += GlobalKeyListener_OnKeyPressed;

            txtFolderLocation.Text = @"C:\Users\PCUser\Desktop\Outputs";
            LoadMidiDirectory(@"C:\Users\PCUser\Desktop\Outputs");

            SetReady(false);
            SetPlaying(false);
        }

        private void GlobalKeyListener_OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key == ksPlay.SelectedKeyCode)
            {
                if (currentTrack != null)
                {
                    midiMachine.PlayTrack(currentTrack);
                    SetPlaying(true);
                }
            }
            else if (e.Key == ksStop.SelectedKeyCode)
            {
                midiMachine.StopTrack();
                SetPlaying(false);
            }
        }

        private void OnKeySelected(object sender, KeySelectedEvent e)
        {
            if (sender == ksPlay)
                ;
            else if (sender == ksStop)
                ;
        }

        private void LoadMidiDirectory(string path)
        {
            try
            {
                midiFiles = Directory.EnumerateFiles(path, "*.mid");
                ApplyFilter(txtFilter.Text);
            }
            catch(Exception e)
            {
                ShowError(e.Message);
            }
        }

        private void ApplyFilter(string filter)
        {
            lvMidiFiles.Clear();

            filter = filter.ToLowerInvariant();
            var emptyFilter = string.IsNullOrEmpty(filter);

            if (midiFiles != null)
            {
                foreach (var filePath in midiFiles.OrderBy(f => f))
                {
                    var fileName = Path.GetFileName(filePath);
                    var testString = fileName.ToLowerInvariant();
                    if (emptyFilter || testString.Contains(filter))
                    {
                        var item = new ListViewItem() { Text = fileName };
                        lvMidiFiles.Items.Add(item);
                    }
                }
            }
        }

        private void LoadMidiFile(string filePath)
        {
            midiMusic = MidiMusic.Read(File.OpenRead(filePath));
            if (midiMusic.Tracks.Any())
            {
                var track = midiMusic.Tracks[0];
                currentTrack = MidiHelper.ToSimpleMidiTrack(track, midiMusic.DeltaTimeSpec);

                cbTrack.Items.Clear();
                for (int x = 1; x <= midiMusic.Tracks.Count; x++)
                {
                    cbTrack.Items.Add(x);
                }
                cbTrack.SelectedIndex = 0;
            }
        }

        private void SetReady(bool ready)
        {
            lblStatus.Visible = ready;
        }

        private void SetPlaying(bool playing)
        {
            if (InvokeRequired)
                Invoke(new Action(() => { lblPlaying.Visible = playing; }));
            else
                lblPlaying.Visible = playing;
        }

        private void ShowError(string error, string caption = "Error")
        {
            MessageBox.Show($"Error loading directory: ${error}", caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TxtFolderLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBrowse.Focus();
            }
        }

        private void TxtFolderLocation_Leave(object sender, EventArgs e)
        {
            LoadMidiDirectory(txtFolderLocation.Text);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtFolderLocation.Text = dlg.SelectedPath;
                    LoadMidiDirectory(dlg.SelectedPath);
                }
            }
        }

        private void TxtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter(txtFilter.Text);
        }

        private void lvMidiFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMidiFiles.SelectedItems.Count != 0)
            {
                var filePath = Path.Combine(txtFolderLocation.Text, lvMidiFiles.SelectedItems[0].Text);
                try
                {
                    LoadMidiFile(filePath);
                    SetReady(true);
                }
                catch (Exception ex)
                {
                    ShowError($"Error loading ${filePath}: ${ex.Message}");
                    SetReady(true);
                }

                midiMachine.StopTrack();
            }
        }

        private void cbTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            var track = midiMusic.Tracks[cbTrack.SelectedIndex];
            currentTrack = MidiHelper.ToSimpleMidiTrack(track, midiMusic.DeltaTimeSpec);
            if (midiMachine.IsPlaying)
            {
                midiMachine.StopTrack();
            }
        }
    }
}
