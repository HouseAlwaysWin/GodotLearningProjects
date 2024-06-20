extends Node2D

var camera_scene = preload("res://scenes/game_camera.tscn")
var camera = null

# Called when the node enters the scene tree for the first time.
func _ready():
	camera = camera_scene.instantiate()
	camera.setup_camera($Player)
	add_child(camera) # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
