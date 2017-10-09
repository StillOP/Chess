using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Game
    {
        public Game()
        {
            m_window = new Window(1280, 960, "Chess");
            m_stateManger = new StateManager(ref m_window);
        }

        ~Game() {}

       public  void Update()
        {
            m_window.Update();
            m_stateManger.Update();
        }

        public void Render()
        {
            m_window.Clear();

            m_stateManger.Render();
   
            m_window.Display();
        }
        public Window GetWindow() { return m_window; }

        private Window m_window;
        private StateManager m_stateManger;
    }
}
