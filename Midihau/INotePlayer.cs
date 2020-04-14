using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midihau
{
    public interface INotePlayer
    {
        void Play(SimpleMidiNote note);
    }
}
