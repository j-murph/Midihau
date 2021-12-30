using System;
using System.Threading;

namespace Midihau
{
    class MidiMachine : IDisposable
    {
        private Thread thread;
        private AutoResetEvent startEvent;
        private AutoResetEvent stopEvent;

        private object actionLock = new object();
        public bool IsPlaying { get; private set; }

        private volatile SimpleMidiTrack currentTrack;

        private INotePlayer notePlayer;

        public MidiMachine(INotePlayer notePlayer)
        {
            this.notePlayer = notePlayer ?? throw new ArgumentNullException(nameof(notePlayer));
            CreateThread();
        }

        private void CreateThread()
        {
            try
            {
                startEvent = new AutoResetEvent(false);
                stopEvent = new AutoResetEvent(false);

                thread = new Thread(ThreadProc);
                thread.Start();
            }
            catch (Exception ex)
            {
                if (startEvent != null)
                {
                    startEvent.Dispose();
                    startEvent = null;
                }

                if (stopEvent != null)
                {
                    stopEvent.Dispose();
                    stopEvent = null;
                }

                if (thread != null)
                {
                    if (thread.IsAlive)
                    {
                        thread.Abort();
                    }

                    thread = null;
                }

                throw ex;
            }
        }

        public void PlayTrack(SimpleMidiTrack track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));

            lock (actionLock)
            {
                if (IsPlaying)
                {
                    stopEvent.Set();
                }

                // Wait for thread to release currentTrack
                while (currentTrack != null) ;

                if (track.Notes.Count == 0) return;

                Interlocked.Exchange(ref currentTrack, track);

                startEvent.Set();
                IsPlaying = true;
            }
        }

        public void StopTrack()
        {
            lock (actionLock)
            {
                if (IsPlaying)
                {
                    stopEvent.Set();

                    // Wait for thread to release currentTrack
                    while (currentTrack != null) ;

                    IsPlaying = false;
                }
            }
        }

        private void ThreadProc(object param)
        {
            try
            {
                while (1 == 1)
                {
                    startEvent.WaitOne();

                    var currentNote = 0;
                    var currentTime = 0.0f;

                    while (Wait(0))
                    {
                        if (currentTrack.Notes.Count > 0)
                        {
                            var notes = currentTrack.Notes;
                            var note = notes[currentNote];

                            var sleepTime = (int)((note.StartTime - currentTime) * 1000);

                            if (!Wait(sleepTime)) break;

                            notePlayer.Play(note);

                            currentTime = note.StartTime;
                            if (++currentNote == notes.Count)
                            {
                                currentNote = 0;
                                currentTime = 0;

                                if (!Wait(250)) break;
                            }
                        }
                    }

                    Interlocked.Exchange(ref currentTrack, null);
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private bool Wait(int milliseconds)
        {
            // TODO: Spinlock for more accurate timings?
            return !stopEvent.WaitOne(milliseconds);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (thread != null)
                    {
                        thread.Abort();
                        thread = null;
                    }

                    startEvent?.Dispose();
                    stopEvent?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
