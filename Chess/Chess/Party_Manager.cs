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
    enum MouseState { Free = 1, Selection };

    class Party_Manager
    {
        public Party_Manager(ref Window l_window)
        {
            m_window = l_window;
            m_window.GetRenderWindow.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(HandleMove);

            m_whiteChess = new Texture(@"Resources\whiteChess.png");
            m_blackChess = new Texture(@"Resources\blackChess.png");
            m_board = new ChessBoard();
            m_joueur1 = new List<ChessMan>();
            m_joueur2 = new List<ChessMan>();
            m_prediction = new List<RectangleShape>();
            m_mouseState = MouseState.Free;
            m_currentChessman = null;

            SetWhiteChessMan();
            SetBlackChessMan();
        }

        private void SetWhiteChessMan()
        {
            foreach (Case kz in m_board.m_cases)
            {
                if (kz.m_center.Y <= 660) { continue; }
                Pion pion = new Pion();
                pion.SetWindow(ref m_window);
                pion.SetTexture(ref m_whiteChess);
                pion.GetSprite.Position = kz.m_center;
                pion.Position = kz.m_center;
                pion.Color = Color.White;
                pion.Boundaries = new FloatRect(pion.Position.X - (kz.m_boundaries.Width / 2.0f), pion.Position.Y - (kz.m_boundaries.Height / 2.0f), kz.m_boundaries.Width, kz.m_boundaries.Height);
                pion.SetBoard(ref m_board);
                m_joueur1.Add(pion);
            }
        }

        private void SetBlackChessMan()
        {
            foreach (Case kz in m_board.m_cases)
            {
                if (kz.m_center.Y > 180) { break; }
                Pion pion = new Pion();
                pion.SetWindow(ref m_window);
                pion.SetTexture(ref m_blackChess);
                pion.GetSprite.Position = kz.m_center;
                pion.Position = kz.m_center;
                pion.Color = Color.Black;
                pion.Boundaries = new FloatRect(pion.Position.X - (kz.m_boundaries.Width / 2.0f), pion.Position.Y - (kz.m_boundaries.Height / 2.0f), kz.m_boundaries.Width, kz.m_boundaries.Height);
                pion.SetBoard(ref m_board);
                m_joueur2.Add(pion);
            }
        }

        public void Update()
        {
            for (int i = 0; i < 64; ++i)
            {
                int count = 0;
                for (int j = 0; j < m_joueur1.Count; ++j)
                {
                    if(m_board.m_cases[i].m_center == m_joueur1[j].GetSprite.Position)
                    {
                        ++count;
                        if (m_board.m_cases[i].m_chessMan != m_joueur1[j])
                        {
                            Case kz = m_board.m_cases[i];
                            kz.m_chessMan = m_joueur1[j];
                            m_board.m_cases[i] = kz;
                        }
                    }
                }

                for (int j = 0; j < m_joueur2.Count; ++j)
                {
                    if (m_board.m_cases[i].m_center == m_joueur2[j].GetSprite.Position)
                    {
                        ++count;
                        if (m_board.m_cases[i].m_chessMan != m_joueur1[j])
                        {
                            Case kz = m_board.m_cases[i];
                            kz.m_chessMan = m_joueur1[j];
                            m_board.m_cases[i] = kz;
                        }
                    }
                }

                if (count == 0)
                {
                    Case kz = m_board.m_cases[i];
                    kz.m_chessMan = null;
                    m_board.m_cases[i] = kz;
                }
            }
        }

        public void Render()
        {
            m_window.GetRenderWindow.Draw(m_board.GetSprite);

            for(int i = 0; i < m_prediction.Count; ++i) { m_window.GetRenderWindow.Draw(m_prediction[i]); }

            foreach(ChessMan c in m_joueur1) { m_window.GetRenderWindow.Draw(c.GetSprite); }

            foreach (ChessMan c in m_joueur2) { m_window.GetRenderWindow.Draw(c.GetSprite); }
        }

        public void SetMovePrediction(ref List<Vector2f> l_list,ref  ChessMan l_chess)
        {
            m_prediction.Clear();
            for (int i = 0; i < l_list.Count; ++i)
            {
                RectangleShape r = new RectangleShape(new Vector2f(m_board.m_cases[0].m_boundaries.Width, m_board.m_cases[0].m_boundaries.Height));
                r.Origin = new Vector2f(r.Size.X / 2.0f, r.Size.Y / 2.0f);
                r.Position = l_list[i];
                r.OutlineThickness = 5.0f;
                r.OutlineColor = SFML.Graphics.Color.Red;
                r.FillColor = SFML.Graphics.Color.Transparent;
                m_prediction.Add(r);
            }
        }

        public void HandleMove(object sender, MouseButtonEventArgs e)
        {
            Vector2i mousepos = Mouse.GetPosition(m_window.GetRenderWindow);
            List<Vector2f> possibleMovements = new List<Vector2f>();
            if (m_mouseState == MouseState.Free)
            {
                bool get = false;
                foreach (ChessMan chess in m_joueur1)
                {
                    if (chess.Boundaries.Contains(mousepos.X, mousepos.Y))
                    {
                        possibleMovements = chess.PossibleMovement();
                        m_currentChessman = chess;
                        SetMovePrediction(ref possibleMovements, ref m_currentChessman);
                        get = true;
                        m_mouseState = MouseState.Selection;
                    }
                }
                if (!get)
                {
                    foreach (ChessMan chess in m_joueur2)
                    {
                        if (chess.Boundaries.Contains(mousepos.X, mousepos.Y))
                        {

                            possibleMovements = chess.PossibleMovement();
                            m_currentChessman = chess;
                            SetMovePrediction(ref possibleMovements, ref m_currentChessman);
                            m_mouseState = MouseState.Selection;
                        }
                    }
                }
            }
            else if (m_mouseState == MouseState.Selection)
            {
                Vector2f position = new Vector2f();
                possibleMovements = m_currentChessman.PossibleMovement();
               
                foreach(Case kz in m_board.m_cases)
                {
                    if (kz.m_boundaries.Contains(mousepos.X, mousepos.Y))
                    {
                        position = kz.m_center;
                        for(int i = 0; i < possibleMovements.Count; ++i)
                        {
                            if (position != possibleMovements[i] && i < possibleMovements.Count - 1) { continue; }
                            if (position != possibleMovements[i] && i == possibleMovements.Count -1)
                            {
                                m_mouseState = MouseState.Free;
                                m_currentChessman = null;
                                return;
                            }
                        }
                    }
                }
                m_currentChessman.Position = position;
                m_currentChessman.GetSprite.Position = position;
                m_currentChessman.Boundaries = new FloatRect(position.X - (m_currentChessman.Boundaries.Width / 2.0f), position.Y - (m_currentChessman.Boundaries.Height / 2.0f), m_currentChessman.Boundaries.Width, m_currentChessman.Boundaries.Height);
                m_mouseState = MouseState.Free;
            }
        }

        private ChessBoard m_board;
        private List<ChessMan> m_joueur1;
        private List<ChessMan> m_joueur2;

        private List<RectangleShape> m_prediction;

        private Texture m_whiteChess;
        private Texture m_blackChess;

        private Window m_window;
        private MouseState m_mouseState;
        private ChessMan m_currentChessman;
        //int s = 0;
    }
}











/* if (s == 0)
           {
               for(int h = 0; h < 64; ++h)
               {
                   if (m_board.m_cases[h].m_chessMan != null)
                   {
                       Console.WriteLine("La case " + m_board.m_cases[h].m_center.X + " " + m_board.m_cases[h].m_center.Y + " est occupé");
                   }
               }
           }
           ++s;*/
