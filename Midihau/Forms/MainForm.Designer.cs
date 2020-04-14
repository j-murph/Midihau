namespace Midihau.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolderLocation = new System.Windows.Forms.TextBox();
            this.lblMidiFolder = new System.Windows.Forms.Label();
            this.lvMidiFiles = new System.Windows.Forms.ListView();
            this.lblMidiFiles = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblPlayKey = new System.Windows.Forms.Label();
            this.lblStopKey = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTrack = new System.Windows.Forms.Label();
            this.ksStop = new Midihau.Controls.KeySelector();
            this.ksPlay = new Midihau.Controls.KeySelector();
            this.cbTrack = new System.Windows.Forms.ComboBox();
            this.lblPlaying = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(565, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(98, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse Folder...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFolderLocation
            // 
            this.txtFolderLocation.Location = new System.Drawing.Point(12, 25);
            this.txtFolderLocation.Name = "txtFolderLocation";
            this.txtFolderLocation.Size = new System.Drawing.Size(547, 20);
            this.txtFolderLocation.TabIndex = 1;
            // 
            // lblMidiFolder
            // 
            this.lblMidiFolder.AutoSize = true;
            this.lblMidiFolder.Location = new System.Drawing.Point(12, 9);
            this.lblMidiFolder.Name = "lblMidiFolder";
            this.lblMidiFolder.Size = new System.Drawing.Size(61, 13);
            this.lblMidiFolder.TabIndex = 2;
            this.lblMidiFolder.Text = "Midi Folder:";
            // 
            // lvMidiFiles
            // 
            this.lvMidiFiles.FullRowSelect = true;
            this.lvMidiFiles.Location = new System.Drawing.Point(415, 68);
            this.lvMidiFiles.MultiSelect = false;
            this.lvMidiFiles.Name = "lvMidiFiles";
            this.lvMidiFiles.Size = new System.Drawing.Size(248, 282);
            this.lvMidiFiles.TabIndex = 3;
            this.lvMidiFiles.UseCompatibleStateImageBehavior = false;
            this.lvMidiFiles.View = System.Windows.Forms.View.List;
            this.lvMidiFiles.SelectedIndexChanged += new System.EventHandler(this.lvMidiFiles_SelectedIndexChanged);
            // 
            // lblMidiFiles
            // 
            this.lblMidiFiles.AutoSize = true;
            this.lblMidiFiles.Location = new System.Drawing.Point(412, 52);
            this.lblMidiFiles.Name = "lblMidiFiles";
            this.lblMidiFiles.Size = new System.Drawing.Size(53, 13);
            this.lblMidiFiles.TabIndex = 4;
            this.lblMidiFiles.Text = "Midi Files:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(453, 383);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(210, 20);
            this.txtFilter.TabIndex = 5;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(415, 387);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 6;
            this.lblFilter.Text = "Filter:";
            // 
            // lblPlayKey
            // 
            this.lblPlayKey.AutoSize = true;
            this.lblPlayKey.Location = new System.Drawing.Point(12, 52);
            this.lblPlayKey.Name = "lblPlayKey";
            this.lblPlayKey.Size = new System.Drawing.Size(51, 13);
            this.lblPlayKey.TabIndex = 8;
            this.lblPlayKey.Text = "Play Key:";
            // 
            // lblStopKey
            // 
            this.lblStopKey.AutoSize = true;
            this.lblStopKey.Location = new System.Drawing.Point(12, 91);
            this.lblStopKey.Name = "lblStopKey";
            this.lblStopKey.Size = new System.Drawing.Size(53, 13);
            this.lblStopKey.TabIndex = 9;
            this.lblStopKey.Text = "Stop Key:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(97, 302);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(218, 73);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Ready";
            // 
            // lblTrack
            // 
            this.lblTrack.AutoSize = true;
            this.lblTrack.Location = new System.Drawing.Point(415, 360);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(38, 13);
            this.lblTrack.TabIndex = 13;
            this.lblTrack.Text = "Track:";
            // 
            // ksStop
            // 
            this.ksStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ksStop.Location = new System.Drawing.Point(12, 107);
            this.ksStop.Name = "ksStop";
            this.ksStop.SelectingText = "Press key...";
            this.ksStop.Size = new System.Drawing.Size(145, 20);
            this.ksStop.TabIndex = 10;
            this.ksStop.Text = "Press key...";
            // 
            // ksPlay
            // 
            this.ksPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ksPlay.Location = new System.Drawing.Point(12, 68);
            this.ksPlay.Name = "ksPlay";
            this.ksPlay.SelectingText = "Press key...";
            this.ksPlay.Size = new System.Drawing.Size(145, 20);
            this.ksPlay.TabIndex = 7;
            this.ksPlay.Text = "Press key...";
            // 
            // cbTrack
            // 
            this.cbTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrack.FormattingEnabled = true;
            this.cbTrack.Location = new System.Drawing.Point(453, 357);
            this.cbTrack.Name = "cbTrack";
            this.cbTrack.Size = new System.Drawing.Size(210, 21);
            this.cbTrack.TabIndex = 14;
            this.cbTrack.SelectedIndexChanged += new System.EventHandler(this.cbTrack_SelectedIndexChanged);
            // 
            // lblPlaying
            // 
            this.lblPlaying.AutoSize = true;
            this.lblPlaying.Location = new System.Drawing.Point(175, 379);
            this.lblPlaying.Name = "lblPlaying";
            this.lblPlaying.Size = new System.Drawing.Size(63, 13);
            this.lblPlaying.TabIndex = 15;
            this.lblPlaying.Text = "** Playing **";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 415);
            this.Controls.Add(this.lblPlaying);
            this.Controls.Add(this.cbTrack);
            this.Controls.Add(this.lblTrack);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.ksStop);
            this.Controls.Add(this.lblStopKey);
            this.Controls.Add(this.lblPlayKey);
            this.Controls.Add(this.ksPlay);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.lblMidiFiles);
            this.Controls.Add(this.lvMidiFiles);
            this.Controls.Add(this.lblMidiFolder);
            this.Controls.Add(this.txtFolderLocation);
            this.Controls.Add(this.btnBrowse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Midihau";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFolderLocation;
        private System.Windows.Forms.Label lblMidiFolder;
        private System.Windows.Forms.ListView lvMidiFiles;
        private System.Windows.Forms.Label lblMidiFiles;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter;
        private Controls.KeySelector ksPlay;
        private System.Windows.Forms.Label lblPlayKey;
        private System.Windows.Forms.Label lblStopKey;
        private Controls.KeySelector ksStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTrack;
        private System.Windows.Forms.ComboBox cbTrack;
        private System.Windows.Forms.Label lblPlaying;
    }
}

