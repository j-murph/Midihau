namespace Midihau
{
    class SimpleMidiNote
    {
        public Pitch Pitch { get; set; }

        public int Octave { get; set; }

        /// <summary>
        /// Note start time, in seconds.
        /// </summary>
        public float StartTime { get; set; }
    }
}
