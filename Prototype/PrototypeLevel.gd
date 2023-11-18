extends Node2D

var cursorTest = preload("res://Piper/assets/cursorTest.png")
# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_custom_mouse_cursor(cursorTest,Input.CURSOR_ARROW, Vector2(12,12))
	$Layer1.play()
	$Layer2.play()
	playLayerOne()
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func playLayerOne():
	$Layer1.volume_db = 0;
	$Layer2.volume_db = -80;
	pass
	
func playLayerTwo():
	$Layer2.volume_db = 0;
	$Layer1.volume_db = -80;
	pass


#func _on_area_2d_area_entered(area):
##	playLayerTwo()
#	pass


#func _on_area_2d_area_exited(area):
#	playLayerOne()
#	pass # Replace with function body.


func _on_area_2d_body_entered(body):
	if (body.is_in_group("Player")):
		playLayerTwo()
	pass # Replace with function body.


func _on_area_2d_body_exited(body):
	if (body.is_in_group("Player")):
		playLayerOne()
	pass # Replace with function body.
