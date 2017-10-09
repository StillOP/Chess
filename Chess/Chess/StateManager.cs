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
    class StateManager
    {
        public StateManager(ref Window l_window)
        {
            m_window = l_window;

            AddinFactory<Config_State>(StateType.Config);
            AddinFactory<Game_State>(StateType.Game);

            m_window.GetRenderWindow.KeyPressed += new EventHandler<KeyEventArgs>(SwitchToEvent);

            SwitchTo(StateType.Config);
        }

        public void Update()
        {
            if (m_states.Count == 0) { return; }
            for (int i = m_states.Count - 1; i > 0; --i)
            {
                m_states[i].Update();
                if ((i - 1) < 0) { return; }
                if (m_states[i].AllowUpdate)
                {
                    m_states[i -1].Update();
                }
                else { return; }
            }
        }

        public void Render()
        {
            if (m_states.Count == 0) { return; }
            int i;
            for (i = m_states.Count - 1; i <= 0; --i)
            {
               if (m_states[i].IsTransparent) { continue; }
               else { break; }
            }

            for (; i<= m_states.Count - 1; ++i)
            {
                m_states[i].Render();
            }
        }

        public void SwitchTo(StateType l_state)
        {
            if (m_currentState == l_state) { return; }
            m_currentState = l_state;
            State state = null;

            foreach (State s in m_states)
            {
                if(s.GetStateType == l_state) { state = s; }
            }

            if (state != null)
            {
                if(!m_states[m_states.Count - 1].IsLongerUsed)
                {
                    m_states[m_states.Count - 1].Destroy();
                    m_states.RemoveRange(m_states.Count - 1, 1);
                }

                int i = m_states.IndexOf(state);
                m_states.Remove(state);
                m_states.Add(state);
                return;
            }

            State newState = m_stateFactory[l_state]();
            newState.SetWindow(ref m_window);
            newState.Initialise();
            m_states.Add(newState);
        }

        private void AddState<T>(StateType l_stateType) where T: State, new()
        {
            foreach (State s in m_states)
            {
                if (s.GetStateType == l_stateType) { return; }
            }

            State state = new T();
            m_states.Add(state);
        }

        private void AddinFactory<T>(StateType l_state) where T: State, new()
        {
            m_stateFactory[l_state] = () => { return new T(); };
        }

        private void SwitchToEvent(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            if (e.Code == (Keyboard.Key)57)
            {
                if (m_currentState == StateType.Config) { SwitchTo(StateType.Game); }
            }
        }

        Window m_window;
        private List<State> m_states = new List<State>();
        private Dictionary<StateType, Func<State>> m_stateFactory = new Dictionary<StateType, Func<State>>();
        private StateType m_currentState;
    }
}
