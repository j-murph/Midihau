using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Midihau
{
    class MordhauNotePlayer : INotePlayer
    {
        // EQUIPMENTCOMMAND
        private static DirectXKey[] equipKeys = new DirectXKey[]
        {
            DirectXKey.DIK_E,
            DirectXKey.DIK_Q,
            DirectXKey.DIK_U,
            DirectXKey.DIK_I,
            DirectXKey.DIK_P,
            DirectXKey.DIK_M,
            DirectXKey.DIK_E,
            DirectXKey.DIK_N,
            DirectXKey.DIK_T,
            DirectXKey.DIK_C,
            DirectXKey.DIK_O,
            DirectXKey.DIK_M,
            DirectXKey.DIK_M,
            DirectXKey.DIK_A,
            DirectXKey.DIK_N,
            DirectXKey.DIK_D,
            DirectXKey.DIK_SPACE,
        };

        private static Tuple<Pitch, int>[] noteTable = new Tuple<Pitch, int>[]
        {
            Tuple.Create(Pitch.A, 5),
            Tuple.Create(Pitch.ASharp, 6),
            Tuple.Create(Pitch.B, 7),
            Tuple.Create(Pitch.C, 8),
            Tuple.Create(Pitch.CSharp, 9),
            Tuple.Create(Pitch.D, 10),
            Tuple.Create(Pitch.DSharp, 11),
            Tuple.Create(Pitch.E, 0),
            Tuple.Create(Pitch.F, 1),
            Tuple.Create(Pitch.FSharp, 2),
            Tuple.Create(Pitch.G, 3),
            Tuple.Create(Pitch.GSharp, 4),
        };

        static MordhauNotePlayer()
        {
            noteTable = noteTable.OrderBy(t => t.Item1).ToArray();
        }

        public void Play(SimpleMidiNote note)
        {
            if (MordhauIsActive())
            {
                OpenConsole();
                SendPlayNoteCommand();
                SendNoteString(note);
                SubmitConsoleCommand();
            }
        }

        private bool MordhauIsActive()
        {
            var foregroundWindow = NativeMethods.GetForegroundWindow();
            if (foregroundWindow != IntPtr.Zero)
            {
                var sb = new StringBuilder(100, 100);
                NativeMethods.GetWindowText(foregroundWindow, sb, sb.Capacity);
                return sb.ToString().StartsWith("MORDHAU");
            }

            return false;
        }

        private void SubmitConsoleCommand()
        {
            SendInput.SendDirectXKey(DirectXKey.DIK_NUMPADENTER, true);
        }

        private void SendNoteString(SimpleMidiNote note)
        {
            var noteId = GetNoteId(note);
            foreach (var character in noteId.ToString())
            {
                var pitchKey = Enum.Parse(typeof(DirectXKey), $"DIK_NUMPAD{character}");
                SendInput.SendDirectXKey((DirectXKey)pitchKey, false);
            }
        }

        private int GetNoteId(SimpleMidiNote note)
        {
            Debug.Assert(Pitch.C == 0);

            var noteId = noteTable[(int)note.Pitch].Item2;
            if (note.Octave > 2)
                noteId += 12;

            return noteId;
        }

        private void SendPlayNoteCommand()
        {
            for (int i = 0; i < equipKeys.Length; i++)
            {
                SendInput.SendDirectXKey(equipKeys[i], false);
                SendInput.SendDirectXKey(equipKeys[i], true);
            }
        }

        private void OpenConsole()
        {
            SendInput.SendDirectXKey(DirectXKey.DIK_F5, false);
            SendInput.SendDirectXKey(DirectXKey.DIK_F5, true);
        }
    }
}
