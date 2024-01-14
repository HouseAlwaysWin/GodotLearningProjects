extends TextureRect

const SCALE_SMALL: Vector2 = Vector2(0.1,0.1)
const SCALE_NORMAL:Vector2 = Vector2(1.0,1.0)
const SCALE_TIME:float = 0.5

# Called when the node enters the scene tree for the first time.
func _ready():
	run_me() # Replace with function body.


func run_me() -> void:
	var tween = get_tree().create_tween()
	tween.set_loops()
	tween.tween_property(self,"scale",SCALE_NORMAL,SCALE_TIME)
	tween.tween_property(self,"scale",SCALE_SMALL,SCALE_TIME)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
