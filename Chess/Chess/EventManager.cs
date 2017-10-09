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
    class EventManager
    {
       public EventManager(ref RenderWindow l_window)
        {
            m_window = l_window;
            SetEvent();
        }

        private void SetEvent()
        {
            m_window.Closed += new EventHandler(OnClosed);
        }

        private void OnClosed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        private RenderWindow m_window;
    }
}
