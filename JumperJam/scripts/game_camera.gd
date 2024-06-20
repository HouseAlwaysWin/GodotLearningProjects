extends Camera2D

var player: Player = null
# Called when the node enters the scene tree for the first time.
func _ready():
	global_position.x = get_viewport_rect().size.x / 2

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func setup_camera(_player:Player):
	if _player != null:
		player = _player

func _physics_process(delta):
	if player:
		global_position.y = player.global_position.y
