extends GPUParticles2D

func _ready():
	var max_lifetime: float = lifetime / speed_scale
	
	emitting = true
	
	for child in get_children():
		# Skip non particles nodes
		if not child is GPUParticles2D: continue
		
		# Tell the particles to emit
		child.emitting = true
		
		# Select the highest lifetime
		var curr_lifetime: float = child.lifetime / child.speed_scale
		if curr_lifetime > max_lifetime: max_lifetime = curr_lifetime

	# Start a timer to wait for the highest lifetime
	# Plus a small amount to account for randomness
	await get_tree().create_timer(max_lifetime + 1.0).timeout
	queue_free()
