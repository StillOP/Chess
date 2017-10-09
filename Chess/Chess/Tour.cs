using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Chess
{
    class Tour : ChessMan
    {
        public Tour()
            : base(new IntRect(120, 0, 120, 120))
        {}

}
