using Godot;
using Godot.Collections;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class MemoryTile : TextureButton
{

    [OnReady("FrameImage")]
    public TextureRect FrameImage;

    [OnReady("ItemImage")]
    public TextureRect ItemImage;
    string ItemName;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
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
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


}
