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
    struct Case
    {
        public FloatRect m_boundaries;
        public Vector2f m_center;
        public ChessMan m_chessMan;
    }

    class ChessBoard
    {
        public ChessBoard()
        {
            m_cases = new List<Case>();
            m_caseSize = 120;
            SetUpBoard();
            m_texture = new Texture(@"Resources\board.png");
            m_sprite = new Sprite(m_texture);
        }

        private void SetUpBoard()
        {
            int line = 0;
            int row = 0;
            for (int i = 0; i < 64; ++i)
            {
                if (i > 0) { ++row; }
                if ((i > 0) && (row % 8 == 0)) { row = 0; }
                if ((i > 0) && (i % 8 == 0)) { ++line; }

                Case kz = new Case();
                kz.m_boundaries = new FloatRect(row * m_caseSize, line * m_caseSize, 120, 120);
                kz.m_center = new Vector2f(kz.m_boundaries.Left + (m_caseSize / 2.0f), kz.m_boundaries.Top + (m_caseSize / 2.0f));
                kz.m_chessMan = null;
                m_cases.Add(kz);
            }
        }

        public Sprite GetSprite { get => m_sprite; }

        public ChessMan IsOccuped(Vector2f l_position)
        {
            foreach(Case kz in m_cases)
            {
                if (kz.m_center == l_position)
                {
                    if (kz.m_chessMan != null) { return kz.m_chessMan; }
                    return null;
                }
            }
            return null;
        }

        public Case GetCase(Vector2f l_position)
        {
            foreach (Case kz in m_cases)
            {
                if (kz.m_center == l_position)
                {
                    return kz;
                }
            }
            Case c = new Case();
            c.m_center = new Vector2f(0, 0);
            return c;
        }

        public List<Case> m_cases;
        private Texture m_texture;
        private Sprite m_sprite;
        private int m_caseSize;
    }
}
