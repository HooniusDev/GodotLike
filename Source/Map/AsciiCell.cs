using Godot;
using System;
namespace GodotLike.Map
{
    public class AsciiCell : IAsciiAppearance
    {
        public string Glyph{ get; private set;}
        public Color FgColor{ get; set; } = new Color(.2f,.2f,.2f);
        public Color BgColor{ get; set; } = new Color(.05f,.05f,.05f);

        public AsciiCell( char glyph ){
            Glyph = glyph.ToString();
        }

        public AsciiCell( char glyph, Color? fgColor = null, Color? bgColor = null ){
            Glyph = glyph.ToString();
            FgColor = fgColor ?? new Color(.2f,.2f,.2f);
            BgColor = bgColor ?? new Color(.05f,.05f,.05f);
        }
    }
}