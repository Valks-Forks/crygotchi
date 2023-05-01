using System;

using Godot;

public partial class StoragePopup : Node
{
    [Export] public ItemList List;

    private ChestDecorationInstance _storage;
    private CursorState _cursorState;

    private ItemEntry[] _items;

    public override void _Ready()
    {
        base._Ready();
        this._cursorState = this.GetNode<CursorState>("/root/CursorState");
        this._cursorState.SetBusy(true);
    }

    public void Setup(ChestDecorationInstance storage)
    {
        GD.Print("Configuring storage popup");
        this._storage = storage;
        this._items = this._storage.GetItems();

        //* Ensure list is empty
        this.List.Clear();

        //* Create a entry for each item
        foreach (var entry in this._items)
        {
            var item = entry.item;

            GD.Print($"Adding item [ \"{item.GetId()}\" ] => \"{item.Name} (x{entry.amount})\"");
            this.List.AddItem($"{item.Name} (x{entry.amount})", item.Icon);
        }

        //* Add event hooks
        this.List.ItemActivated += OnActivated;
    }

    private void OnActivated(long index)
    {
        var selected = this._items[index];
        GD.Print($"Activated item {selected.item.Name}({selected.id})");

        var item = this._storage.TakeItem(selected.id);
        this._cursorState.HoldItem(item);
        this.Close();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (Input.IsActionJustPressed("ui_cancel")) this.Close();
    }

    private void Close()
    {
        this._cursorState.SetBusy(false);
        this.QueueFree();
    }

    public override void _Process(double delta)
    {
        this.List.GrabFocus();
        this.List.GrabClickFocus();
    }
}
