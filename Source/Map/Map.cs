using Godot;
using System;
using System.Collections.Generic;
using RogueSharp;

namespace GodotLike.Map
{
    public class Map : ViewportContainer
    {
        
        private DynamicFont font;

        public int Width;
        public int Height;

        public DynamicFont Font
        {
            get => font;
            set
            {
                font = value;
                CellWidth = (int)(font.GetStringSize("W").x );
                CellHeight = ( int )font.GetHeight();
            }

        }

        public int CellWidth = 7;
        public int CellHeight = 12;

        private AsciiCell[,] cells;

        private List<Point> dirtyCells;

        public override void _Ready()
        {
             Font = (DynamicFont) ResourceLoader.Load("res://Fonts/ProggySquare.tres");
             Initialize( 50,40 );
        }
        
        public override void _Process(float delta)
        {
            // Draw Cells modified in SetCellProperties
            if ( dirtyCells.Count > 0 )
            {
                Update(); // Calls _Draw() to update the visuals
            }
        }

        public void Initialize( int width, int height )
        {
           
            Width = width;
            Height = height;

            dirtyCells = new List<Point>();

            cells = new AsciiCell[ Width,Height ];

            for ( int x = 0; x < Width; x++ )
            {
                for ( int y = 0; y < Height; y++)
                {
                        cells[x,y] = new AsciiCell( '.' );
                }
            }

            // just a test to see if dirtyCells work
            SetCellProperties( new Point(4,8), new AsciiCell( '@', new Color(1,0,0), new Color(.4f,.4f,.4f) ) );
        }

        public void SetCellProperties( Point point, AsciiCell cellAppearance, bool walkable = false, bool transparent = false, bool inFov = false, bool explored = false )
        {
            cells[point.X,point.Y] = cellAppearance;
            dirtyCells.Add(point);
        }

        /// <summary> Draws contents of AsciiCell[,] cells contents to child Viewport </summary> ///
        public override void _Draw()
        {

            foreach ( Point point in dirtyCells)
            {
                DrawRect( new Rect2( CellToRectPosition( point), new Point( CellWidth, CellHeight) ), cells[point.X,point.Y].BgColor );
                DrawChar( Font, CellToGlyphPosition( point ), cells[point.X,point.Y].Glyph, "W", cells[point.X,point.Y].FgColor);
            }
            dirtyCells.Clear();

            for ( int x = 0; x < Width; x++ )
            {
                for ( int y = 0; y < Height; y++ )
                {
                    DrawRect( new Rect2( CellToRectPosition( new Point(x,y)), new Point( CellWidth, CellHeight) ), cells[x,y].BgColor );
                    DrawChar( Font, CellToGlyphPosition( new Point(x,y) ), cells[x,y].Glyph, "W", cells[x,y].FgColor);
                }
            }
        }

        public Point CellToGlyphPosition( Point p )
        {
            return new Point( p.X * CellWidth, p.Y * CellHeight + CellHeight);
        }        
        public Point CellToRectPosition( Point p )
        {
            return new Point( p.X * CellWidth, p.Y * CellHeight);
        }


    }
}