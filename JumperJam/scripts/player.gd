extends CharacterBody2D

var speed = 300.0

var viewport_size
var gravity = 15.0
var max_fall_velocity = 1000
# Called when the node enters the scene tree for the first time.
func _ready():
	viewport_size = get_viewport_rect().size

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func _physics_process(delta):
	velocity.y += gravity
	if velocity.y > max_fall_velocity:
		velocity.y = max_fall_velocity
		
	var direction = Input.get_axis("move_left","move_right")
	if direction:
		velocity.x = direction * speed
	else:
		velocity.x = move_toward(velocity.x,0,speed)
	move_and_slide()
	var margin = 20
	if global_position.x > viewport_size.x + margin:
		global_position.x = -margin
	if global_position.x < -margin:
		global_position.x = viewport_size.x + margin
