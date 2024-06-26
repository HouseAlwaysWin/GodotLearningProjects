extends Node2D

@onready var platform_parent = $PlatformParent
@onready var player = $Player

var camera_scene = preload("res://scenes/game_camera.tscn")
var platform_scene = preload("res://scenes/platform.tscn")
var camera = null
var start_platform_y 
var level_size = 10
var y_distance_between_platform = 100
var viewport_size
var generate_platform_count = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	camera = camera_scene.instantiate()
	camera.setup_camera($Player)
	add_child(camera)
	
	viewport_size = get_viewport_rect().size
	generate_platform_count = 0
	start_platform_y = viewport_size.y - (y_distance_between_platform * 2)
	generate_level(start_platform_y,true)
		
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Input.is_action_just_pressed("quit"):
		get_tree().quit()
	if Input.is_action_just_pressed("reset"):
		get_tree().reload_current_scene()
	
	if player:
		var py = player.global_position.y
		var end_of_level_pos = start_platform_y - (generate_platform_count * y_distance_between_platform)
		var thresold = end_of_level_pos + (y_distance_between_platform *6)
		if py <= thresold:
			generate_level(end_of_level_pos,false)

func create_platform(location:Vector2):
	var platform = platform_scene.instantiate()
	platform.global_position = location
	platform_parent.add_child(platform)
	return platform
	

func generate_level(start_y:float,generate_ground:bool):
	
	var platform_width = 134
	
	if generate_ground == true:

		var ground_layer_platform_count = (viewport_size.x/ platform_width)
		
		var ground_layer_y_offset = 62
		for i in range(ground_layer_platform_count):
			var ground_location = Vector2(i * platform_width,viewport_size.y - ground_layer_y_offset)
			create_platform(ground_location)
		

	for i in range(level_size):
		var max_x_position = viewport_size.x - platform_width
		var random_x = randf_range(0.0,max_x_position)
		
		var location:Vector2
		location.x = random_x
		location.y = start_y - (y_distance_between_platform * i)
		create_platform(location)
		generate_platform_count += 1
		print(generate_platform_count)
