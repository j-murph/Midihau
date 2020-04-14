using System.Collections.Generic;
using System.Linq;

namespace Midihau
{
    public class SimpleMidiTrack
    {
        public IList<SimpleMidiNote> Notes { get; } = new List<SimpleMidiNote>();

        public SimpleMidiTrack(IList<SimpleMidiNote> notes)
        {
            Notes = notes.OrderBy(n => n.StartTime).ToList();
        }
    }
}
