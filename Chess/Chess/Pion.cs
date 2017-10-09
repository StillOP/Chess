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
    class Pion : ChessMan
    {
        public Pion()
            : base(new IntRect(0, 0, 120, 120))
        {}

        public override List<Vector2f> PossibleMovement()
        {
            List<Vector2f> m_possibleMove = new List<Vector2f>();
            if (Color == Color.Black)
            {
                ChessMan chessman1 = m_board.IsOccuped(new Vector2f(m_position.X, m_position.Y + m_move));
                ChessMan chessman2 = m_board.IsOccuped(new Vector2f(m_position.X + m_move, m_position.Y + m_move));
                ChessMan chessman3 = m_board.IsOccuped(new Vector2f(m_position.X - m_move, m_position.Y + m_move));

                if (chessman1 == null) { m_possibleMove.Add(new Vector2f(m_position.X, m_position.Y + m_move)); }

                if (chessman2 != null)
                {
                    if (chessman2.Color != Color)
                    {
                        m_possibleMove.Add(new Vector2f(m_position.X + m_move, m_position.Y + m_move));
                    }
                }
                if (chessman3 != null)
                {
                    if (chessman3.Color != Color)
                    {
                        m_possibleMove.Add(new Vector2f(m_position.X - m_move, m_position.Y + m_move));
                    }
                }

                return m_possibleMove;
            }

            if (Color == Color.White)
            {
                ChessMan chessman1 = m_board.IsOccuped(new Vector2f(m_position.X, m_position.Y - m_move));
                ChessMan chessman2 = m_board.IsOccuped(new Vector2f(m_position.X + m_move, m_position.Y - m_move));
                ChessMan chessman3 = m_board.IsOccuped(new Vector2f(m_position.X - m_move, m_position.Y - m_move));

                if (chessman1 == null) { m_possibleMove.Add(new Vector2f(m_position.X, m_position.Y - m_move)); }

               if (chessman2 != null)
                {
                    if (chessman2.Color != Color)
                    {
                        m_possibleMove.Add(new Vector2f(m_position.X + m_move, m_position.Y - m_move));
                    }
                }
               if (chessman3 != null)
                {
                    if (chessman3.Color != Color)
                    {
                        m_possibleMove.Add(new Vector2f(m_position.X - m_move, m_position.Y - m_move));
                    }
                }
               // Console.WriteLine(m_possibleMove.Count);
                return m_possibleMove;
            }

            m_possibleMove = null;
            return m_possibleMove;

        }
    }
}
