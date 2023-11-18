extends Node2D

var cursorTest = preload("res://Piper/assets/cursorTest.png")
# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_custom_mouse_cursor(cursorTest,Input.CURSOR_ARROW, Vector2(12,12))
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func playLayerOne():
	$Layer1.volume_db = 0;
	$Layer2.volume_db = -80;
	pass
	
func playeLayerTwo():
	$Layer2.volume_db = 0;
	$Layer1.volume_db = -80;
	pass
