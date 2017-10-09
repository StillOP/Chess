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
    class Config_State : State
    {
        public Config_State()
            : base(StateType.Config)
        {
        }

        public override void Initialise()
        {
            base.Initialise();
        }
        public override void Destroy() { }

        public override void Update() { }
        public override void Render() { }
    }
}
