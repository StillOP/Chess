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
    enum Color { White = 1, Black };

    abstract class ChessMan
    {
        public ChessMan(IntRect l_rect)
        {
            m_textureRect = l_rect;
            m_move = 120;
            m_shape = new List<RectangleShape>();
        }

        public void SetTexture(ref Texture l_texture)
        {
            m_texture = l_texture;
            m_sprite = new Sprite(m_texture, m_textureRect);
            m_sprite.Origin = new Vector2f(60, 60);
        }

        public abstract List<Vector2f> PossibleMovement();

        public Color Color { get => m_color; set => m_color = value; }
        public Vector2f Position { get => m_position; set => m_position = value; }
        public FloatRect Boundaries { get => m_boundaries; set => m_boundaries = value; }
        public Sprite GetSprite { get => m_sprite; }

        public void SetBoard(ref ChessBoard l_board) { m_board = l_board; }
        public void SetWindow(ref Window l_window) { m_window = l_window; }

        protected Vector2f m_position;
        protected FloatRect m_boundaries;
        protected Color m_color;

        protected Texture m_texture;
        protected Sprite m_sprite;
        protected IntRect m_textureRect;
        public int m_move;

        protected ChessBoard m_board;
        protected Window m_window;
        public List<RectangleShape> m_shape;
    }
}
