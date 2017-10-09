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

            foreach (Vector2f pos in m_possibleMove)
            {
                ChessMan chessman = m_board.IsOccuped(pos);
                if (chessman != null)
                {
                    if (chessman.Color == Color)
                    {
                        m_possibleMove.Remove(pos);
                    }
                    if (chessman.Color != Color)
                    {
                        if (pos.X == m_position.X)
                        {
                            ChessMan chessman1 = m_board.IsOccuped(new Vector2f(pos.X, pos.Y + m_move));
                            ChessMan chessman2 = m_board.IsOccuped(new Vector2f(pos.X, pos.Y - m_move));
                            if (chessman1 != null) { m_possibleMove.Remove(new Vector2f(pos.X, pos.Y + m_move)); }
                            if (chessman2 != null) { m_possibleMove.Remove(new Vector2f(pos.X, pos.Y - m_move)); }
                        }
                        if (pos.Y == m_position.Y)
                        {
                            ChessMan chessman1 = m_board.IsOccuped(new Vector2f(pos.X + m_move, pos.Y));
                            ChessMan chessman2 = m_board.IsOccuped(new Vector2f(pos.X - m_move, pos.Y));
                            if (chessman1 != null) { m_possibleMove.Remove(new Vector2f(pos.X + m_move, pos.Y)); }
                            if (chessman2 != null) { m_possibleMove.Remove(new Vector2f(pos.X - m_move, pos.Y)); }
                        }
                    }
                }
            }

            return m_possibleMove;
        }
    }
}
