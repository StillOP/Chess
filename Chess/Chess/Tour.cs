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
    class Tour : ChessMan
    {
        public Tour()
            : base(new IntRect(120, 0, 120, 120))
        { }

        public override List<Vector2f> PossibleMovement()
        {
            List<Vector2f> m_possibleMove = new List<Vector2f>();

            foreach (Case kz in m_board.m_cases)
            {
                if (kz.m_center.Y == m_position.Y)
                {
                    m_possibleMove.Add(kz.m_center);
                }
                if (kz.m_center.X == m_position.X)
                {
                    m_possibleMove.Add(kz.m_center);
                }
            }

            List<Vector2f> m_toRemove = new List<Vector2f>();
            for (int i = 0; i < m_possibleMove.Count; ++i)
            {
                ChessMan chessman = m_board.IsOccuped(m_possibleMove[i]);
                if (chessman != null)
                {
                    if (chessman.Color == Color)
                    {
                        // m_possibleMove.Remove(m_possibleMove[i]);
                        m_toRemove.Add(m_possibleMove[i]);

                        if (m_possibleMove[i].Y == m_position.Y)
                        {
                            if (m_position.X > chessman.Position.X)
                            {
                                foreach(Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.Y == m_position.Y)
                                    {
                                        if (kz.m_center.X == chessman.Position.X) { break; }
                                        //m_possibleMove.Remove(kz.m_center);
                                        m_toRemove.Add(kz.m_center);
                                    }
                                }
                            }
                            else if(m_position.X < chessman.Position.X)
                            {
                                foreach (Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.Y == m_position.Y)
                                    {
                                        if (kz.m_center.X > chessman.Position.X)
                                        {
                                            //m_possibleMove.Remove(kz.m_center);
                                            m_toRemove.Add(kz.m_center);
                                        }
                                    }
                                }
                            }
                        }

                        if (m_possibleMove[i].X == m_position.X)
                        {
                            if (m_position.Y > chessman.Position.Y)
                            {
                                foreach (Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.X == m_position.X)
                                    {
                                        if (kz.m_center.Y == chessman.Position.Y) { break; }
                                        //m_possibleMove.Remove(kz.m_center);
                                        m_toRemove.Add(kz.m_center);
                                    }
                                }
                            }
                            else if (m_position.Y < chessman.Position.Y)
                            {
                                foreach (Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.X == m_position.X)
                                    {
                                        if (kz.m_center.Y > chessman.Position.Y)
                                        {
                                            //m_possibleMove.Remove(kz.m_center);
                                            m_toRemove.Add(kz.m_center);
                                        }
                                    }
                                }
                            }
                        }
                    }
                   if (chessman.Color != Color)
                    {
                        if (m_possibleMove[i].Y == m_position.Y)
                        {
                            if (m_position.X > chessman.Position.X)
                            {
                                foreach (Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.Y == m_position.Y)
                                    {
                                        if (kz.m_center.X == chessman.Position.X) { break; }
                                        //m_possibleMove.Remove(kz.m_center);
                                        m_toRemove.Add(kz.m_center);
                                    }
                                }
                            }
                            else if (m_position.X < chessman.Position.X)
                            {
                                foreach (Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.Y == m_position.Y)
                                    {
                                        if (kz.m_center.X > chessman.Position.X)
                                        {
                                            //m_possibleMove.Remove(kz.m_center);
                                            m_toRemove.Add(kz.m_center);
                                        }
                                    }
                                }
                            }
                        }

                        if (m_possibleMove[i].X == m_position.X)
                        {
                            if (m_position.Y > chessman.Position.Y)
                            {
                                foreach (Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.X == m_position.X)
                                    {
                                        if (kz.m_center.Y == chessman.Position.Y) { break; }
                                        //m_possibleMove.Remove(kz.m_center);
                                        m_toRemove.Add(kz.m_center);
                                    }
                                }
                            }
                            else if (m_position.Y < chessman.Position.Y)
                            {
                                foreach (Case kz in m_board.m_cases)
                                {
                                    if (kz.m_center.X == m_position.X)
                                    {
                                        if (kz.m_center.Y > chessman.Position.Y)
                                        {
                                            //m_possibleMove.Remove(kz.m_center);
                                            m_toRemove.Add(kz.m_center);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < m_toRemove.Count; ++i)
            {
                m_possibleMove.Remove(m_toRemove[i]);
            }
            return m_possibleMove;
        }
    }
}
