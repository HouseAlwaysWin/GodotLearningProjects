extends Camera2D

var player: Player = null
var viewport_size 

# Called when the node enters the scene tree for the first time.
func _ready():
	viewport_size = get_viewport_rect().size
	global_position.x = viewport_size.x / 2
	
	limit_bottom = viewport_size.y
	limit_left = 0
	limit_right = viewport_size.x

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if player:
		var limit_distance = 420
		if limit_bottom > player.global_position.y + limit_distance:
			limit_bottom = player.global_position.y + limit_distance

func setup_camera(_player:Player):
	if _player != null:
		player = _player

func _physics_process(delta):
	if player:
		global_position.y = player.global_position.y
