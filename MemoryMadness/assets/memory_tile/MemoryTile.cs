using Godot;
using Godot.Collections;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Net.Sockets;

public partial class MemoryTile : TextureButton
{

    [OnReady("FrameImage")]
    public TextureRect FrameImage;

    [OnReady("ItemImage")]
    public TextureRect ItemImage;
    [OnReady("/root/SignalManager")]
    public SignalManager SGManager;
    string ItemName;
    bool CanSelectMe = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        SGManager.Connect(SignalManager.SignalName.OnSelectionEnabled, Callable.From(OnSelectionEnabled));
        SGManager.Connect(SignalManager.SignalName.OnSelectionDisabled, Callable.From(OnSelectionDisabled));
        Pressed += OnPress;
    }

    private void OnPress()
    {
        if (CanSelectMe)
        {
            // Reveal(true);
            this.SGManager.EmitSignal(SignalManager.SignalName.OnTileSelected, this);
        }
    }

    public void Reveal(bool reveal)
    {
        FrameImage.Visible = reveal;
        ItemImage.Visible = reveal;
    }

    private void OnSelectionEnabled()
    {
        CanSelectMe = true;
    }

    private void OnSelectionDisabled()
    {
        CanSelectMe = false;
    }

    public string GetItemName()
    {
        return ItemName;
    }

    public void Setup(Dictionary iiDict, CompressedTexture2D fi)
    {
        FrameImage.Texture = fi;
        ItemImage.Texture = (Texture2D)iiDict["item_texture"];
        ItemName = (string)iiDict["item_name"];
        Reveal(false);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


}
