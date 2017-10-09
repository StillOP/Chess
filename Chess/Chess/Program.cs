using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Chess
{
    class Program
    {
       static void Main(string[] args)
        {
            Game game = new Game();

            while (game.GetWindow().GetRenderWindow.IsOpen)
            {
                game.Update();
                game.Render();
            }
        }
    }
}


/*static void OnClosed(object sender, EventArgs e)
       {
           RenderWindow window = (RenderWindow)sender;
           window.Close();
       }

       static void Text(object sender, TextEventArgs e)
       {
           RenderWindow window = (RenderWindow)sender;
           Console.WriteLine(e.Unicode);
       }*/

/*RenderWindow window = new RenderWindow(new VideoMode(800, 400), "Chess", Styles.Default);
        window.Closed += new EventHandler(OnClosed);
        window.TextEntered += new EventHandler<TextEventArgs>(Text);

        while(window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear();
            window.Display();
        }*/

