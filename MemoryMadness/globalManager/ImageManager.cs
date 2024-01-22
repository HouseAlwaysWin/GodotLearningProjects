using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class ImageManager : Node
{

    public List<Dictionary<string, object>> ItemImages = new List<Dictionary<string, object>>();

    public List<Resource> FRAME_IMAGES = new List<Resource>
    {
        GD.Load("res://assets/frames/blue_frame.png"),
        GD.Load("res://assets/frames/red_frame.png"),
        GD.Load("res://assets/frames/yellow_frame.png"),
        GD.Load("res://assets/frames/green_frame.png"),
    };

    public override void _Ready()
    {
        LoadItemImages();
    }

    public void AddFileToList(string filename, string path)
    {
        var fullPath = $"{path}/{filename}";
        var resource = GD.Load(fullPath);

        Dictionary<string, object> iiDict = new()
        {
            {"item_name", filename.Replace(".png","")},
            {"item_texture", resource},
        };

        ItemImages.Add(iiDict);
    }

    public void LoadItemImages()
    {
        var path = "res://assets/glitch";
        DirAccess dir = DirAccess.Open(path);

        if (dir is null)
        {
            GD.Print("Error:", path);
            return;
        }

        var fileNames = dir.GetFiles();

        foreach (var fn in fileNames)
        {
            if (!fn.Contains(".import"))
            {
                AddFileToList(fn, path);
            }
        }

        GD.Print($"load:{ItemImages.Count}");
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
