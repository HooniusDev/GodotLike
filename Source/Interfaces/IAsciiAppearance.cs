using Godot;
using System;

namespace GodotLike.Map
{
    public interface IAsciiAppearance
    {
        string Glyph{get;}
        Color FgColor{get;}
        Color BgColor{get;}

    }
}