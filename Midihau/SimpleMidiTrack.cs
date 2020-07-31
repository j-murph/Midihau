using System.Collections.Generic;
using System.Linq;

namespace Midihau
{
    class SimpleMidiTrack
    {
        public IList<SimpleMidiNote> Notes { get; } = new List<SimpleMidiNote>();

        public SimpleMidiTrack(IList<SimpleMidiNote> notes)
        {
            Notes = notes.OrderBy(n => n.StartTime).ToList();
        }
    }
}
