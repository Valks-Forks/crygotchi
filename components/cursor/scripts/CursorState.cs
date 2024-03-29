namespace Crygotchi;

public partial class CursorState : Node
{
    private Vector2 Position = new(0, 0);
    private Item HeldItem = null;
    private bool _isBusy = false;

    public event EventHandler<CursorActionEventArgs> OnAction;
    public event EventHandler OnStateChange;
    public event EventHandler OnItemChange;

    public Vector2 GetPosition()
    {
        return Position;
    }

    public void SetPosition(Vector2 newPosition)
    {
        this.Position = newPosition;
        this.OnStateChange?.Invoke(this, null);
    }

    public void CursorActionPressed(ActionType type)
    {
        //* Should propagate that there was a action pressed
        this.OnAction?.Invoke(this, new CursorActionEventArgs() { Action = type });
    }

    public bool IsBusy()
    {
        return this._isBusy;
    }

    public void SetBusy(bool newBusy)
    {
        this._isBusy = newBusy;
        this.OnStateChange?.Invoke(this, null);
    }

    public bool IsHoldingItem()
    {
        return this.HeldItem != null;
    }

    public void HoldItem(Item heldItem)
    {
        this.HeldItem = heldItem;
        this.OnStateChange?.Invoke(this, null);
        this.OnItemChange?.Invoke(this, null);
    }

    public Item TakeItem()
    {
        var item = this.HeldItem;
        this.HeldItem = null;

        this.OnStateChange?.Invoke(this, null);
        this.OnItemChange?.Invoke(this, null);
        return item;
    }

    public Item PeekItem()
    {
        return this.HeldItem;
    }
}

public class CursorActionEventArgs : EventArgs
{
    public ActionType Action { get; set; }
}

public enum ActionType
{
    Primary = 0,
    Secondary = 1
}
