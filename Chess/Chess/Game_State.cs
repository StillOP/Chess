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
    class Game_State : State
    {
        public Game_State()
            : base (StateType.Game)
        {}

        public override void Initialise()
        {
            base.Initialise();
            m_manager = new Party_Manager(ref m_window);
        }
        public override void Destroy() { }

        public override void Update()
        {
            m_manager.Update();
        }

        public override void Render()
        {
            m_manager.Render();
        }

        private Party_Manager m_manager;
    }
}
