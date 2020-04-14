using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using Commons.Music.Midi;

namespace Midihau
{
    class MidiHelper
    {
        public static float DefaultMidiTempo => 120.0f;

        public static SimpleMidiTrack ToSimpleMidiTrack(MidiTrack track, int ticksPerBeat)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));

            var currentTempo = DefaultMidiTempo;
            var currentTick = 0;
            var notes = new List<SimpleMidiNote>();

            var sb = new StringBuilder();

            foreach (var message in track.Messages)
            {
                //sb.AppendLine($"Message mess ${message.Event.StatusByte}");
                switch (message.Event.StatusByte)
                {
                    case MidiEvent.Meta:
                        if (message.Event.Msb == 0x51 && message.Event.ExtraDataLength == 3)
                        {
                            // Set tempo
                            var ed = message.Event.ExtraData;
                            currentTempo = 60000000.0f / (ed[2] | (ed[1] << 8) | (ed[0] << 16));
                        }
                        break;
                    case MidiEvent.NoteOn:
                        var note = ToSimpleMidiNote(message, currentTempo, ticksPerBeat, currentTick);
                        sb.AppendLine($"Note on @ {message.DeltaTime} @ {note.StartTime}");
                        if (note.StartTime < 0) throw new Exception();
                        notes.Add(note);
                        break;
                    case MidiEvent.NoteOff:
                        var offTime = TickToAbsTimeInSecs(currentTick, ticksPerBeat, currentTempo);
                        sb.AppendLine($" Note off @ {offTime}");
                        break;
                }

                currentTick += message.DeltaTime;
            }

            Trace.Write(sb);

            return new SimpleMidiTrack(notes); ;
        }

        public static SimpleMidiNote ToSimpleMidiNote(MidiMessage message, float tempo, int ticksPerBeat, int currentTick = 0)
        {
            if (message.Event.StatusByte != MidiEvent.NoteOn)
            {
                throw new Exception($"{nameof(message)} must be a NoteOn message.");
            }

            if (message.DeltaTime < 0)
            {
                throw new Exception("Sub-zero DeltaTime's are not supported.");
            }

            var time = TickToAbsTimeInSecs(currentTick + message.DeltaTime, ticksPerBeat, tempo);
            var pitchNum = message.Event.Msb & 0x7f;

            return new SimpleMidiNote()
            {
                StartTime = time,
                Pitch = (Pitch)(pitchNum % 12),
                Octave = (pitchNum / 12) - 2,
            };
        }

        public static float TickToAbsTimeInSecs(int tick, int ticksPerBeat, float tempo)
        {
            if (tick < 0.0f) throw new ArgumentException($"{nameof(tick)} must be at least 0.");
            if (ticksPerBeat <= 0.0f) throw new ArgumentException($"{nameof(ticksPerBeat)} must be greater than 0.");
            if (tempo <= 0.0f) throw new ArgumentException($"{nameof(tempo)} must be greater than 0.");

            float tickLengthInSecs = 60.0f / tempo / ticksPerBeat;

            return tickLengthInSecs * tick;
        }
    }
}
