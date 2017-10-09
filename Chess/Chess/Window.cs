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
    class Window
    {
        public Window(uint l_width, uint l_height, string l_name)
        {
            m_size = new Vector2f(l_width, l_height);
            m_name = l_name;
            m_window = new RenderWindow(new VideoMode(l_width, l_height, 32), l_name);
            m_eventManager = new EventManager(ref m_window);
        }
        
        public void Update()
        {
            m_window.DispatchEvents();
        }
        public void Clear() { m_window.Clear(); }
        public void Display() { m_window.Display(); }

        private RenderWindow m_window;
        private EventManager m_eventManager;
        private Vector2f m_size;
        private string m_name;

        public Vector2f Size { get => m_size; set => m_size = value; }
        public ref RenderWindow GetRenderWindow { get => ref m_window; }
    }
}
