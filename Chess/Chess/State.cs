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
    enum StateType { Config = 1, Game }

    abstract class State
    {
        public State(StateType l_type)
        {
            m_type = l_type;
            m_allowUpdate = false;
            m_isTransparent = false;
            m_longerUsed = false;
        }

        public virtual void Update() {}
        public virtual void Render() {}

        public virtual void Initialise() { Console.WriteLine("This is state " + m_type); }
        public virtual void Destroy() {}

        public bool AllowUpdate { get => m_allowUpdate; }
        public bool IsTransparent { get => m_isTransparent; }
        public bool IsLongerUsed { get => m_longerUsed; }

        public StateType GetStateType { get => m_type; }

        public void SetWindow(ref Window l_window) { m_window = l_window; }


        protected Window m_window;
        protected StateType m_type;
        protected bool m_allowUpdate;
        protected bool m_isTransparent;
        protected bool m_longerUsed;
    }
}
