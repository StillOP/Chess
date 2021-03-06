﻿using System;
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
            m_tour = 1;

            SetWhiteChessMan();
            SetBlackChessMan();
        }
        void AddChessMan<T>(ref Texture l_texture, Vector2f l_position, Color l_color) where T: ChessMan, new()
        {

        }

        private void SetWhiteChessMan()
        {
            foreach (Case kz in m_board.m_cases)
            {
                if (kz.m_center.Y <= 660) { continue; }
                if (kz.m_center.Y == 900) { break; }
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

            Tour tour1 = new Tour();
            tour1.SetWindow(ref m_window);
            tour1.SetTexture(ref m_whiteChess);
            tour1.GetSprite.Position = new Vector2f(60, 900);
            tour1.Position = new Vector2f(60, 900);
            tour1.Color = Color.White;
            tour1.Boundaries = new FloatRect(tour1.Position.X - (m_board.m_cases[0].m_boundaries.Width / 2.0f), tour1.Position.Y - (m_board.m_cases[0].m_boundaries.Height / 2.0f), m_board.m_cases[0].m_boundaries.Width, m_board.m_cases[0].m_boundaries.Height);
            tour1.SetBoard(ref m_board);
            m_joueur1.Add(tour1);

            Tour tour2 = new Tour();
            tour2.SetWindow(ref m_window);
            tour2.SetTexture(ref m_whiteChess);
            tour2.GetSprite.Position = new Vector2f(900, 900);
            tour2.Position = new Vector2f(900, 900);
            tour2.Color = Color.White;
            tour2.Boundaries = new FloatRect(tour2.Position.X - (m_board.m_cases[0].m_boundaries.Width / 2.0f), tour2.Position.Y - (m_board.m_cases[0].m_boundaries.Height / 2.0f), m_board.m_cases[0].m_boundaries.Width, m_board.m_cases[0].m_boundaries.Height);
            tour2.SetBoard(ref m_board);
            m_joueur1.Add(tour2);
        }

        private void SetBlackChessMan()
        {
            foreach (Case kz in m_board.m_cases)
            {
                if (kz.m_center.Y == 60) { continue; }
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
                        if (m_board.m_cases[i].m_chessMan != null)
                        {
                            m_joueur2.Remove(m_board.m_cases[i].m_chessMan);
                        }
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
                        if (m_board.m_cases[i].m_chessMan != null)
                        {
                            m_joueur1.Remove(m_board.m_cases[i].m_chessMan);
                        }
                        if (m_board.m_cases[i].m_chessMan != m_joueur2[j])
                        {
                            Case kz = m_board.m_cases[i];
                            kz.m_chessMan = m_joueur2[j];
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
                if (m_tour % 2 != 0)
                {
                    foreach (ChessMan chess in m_joueur1)
                    {
                        if (chess.Boundaries.Contains(mousepos.X, mousepos.Y))
                        {
                            possibleMovements = chess.PossibleMovement();
                            if (possibleMovements.Count == 0) { return; }
                            m_currentChessman = chess;
                            SetMovePrediction(ref possibleMovements, ref m_currentChessman);
                            m_mouseState = MouseState.Selection;
                        }
                    }
                }
                else
                {
                    foreach (ChessMan chess in m_joueur2)
                    {
                        if (chess.Boundaries.Contains(mousepos.X, mousepos.Y))
                        {
                            possibleMovements = chess.PossibleMovement();
                            if (possibleMovements.Count == 0) { return; }
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
                        int c = 0;
                        for(int i = 0; i < possibleMovements.Count; ++i)
                        {
                            if (position != possibleMovements[i]) { ++c; }
                            if (c == possibleMovements.Count)
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
                ++m_tour;
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
        private int m_tour;
    }
}



/* foreach(Case kz in m_board.m_cases)
                {
                    if (kz.m_boundaries.Contains(mousepos.X, mousepos.Y))
                    {
                        position = kz.m_center;
                        int c = 0;
                        for(int i = 0; i < possibleMovements.Count; ++i)
                        {
                            if (position != possibleMovements[i]) { ++c; }
                            if (c == possibleMovements.Count)
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
                ++m_tour;*/