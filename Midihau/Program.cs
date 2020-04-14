using System;
using System.Windows.Forms;
using Midihau.Forms;

namespace Midihau
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                GlobalKeyListener.Start();
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error creating keyboard hook: {e.Message}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var midiMachine = new MidiMachine(new MordhauNotePlayer()))
                {
                    Application.Run(new MainForm(midiMachine));
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                GlobalKeyListener.CleanUp();
            }
        }
    }
}
