using Godot;

public partial class RoomTileInstance : Resource
{
    public string ID { get; set; }
    public Vector2 Position { get; set; }

#nullable enable
    public RoomTileDecorationInstance? Decoration { get; set; } = null;
#nullable disable
}
