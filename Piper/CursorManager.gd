extends CanvasLayer

var empty_cursor = preload("res://Piper/assets/cursorTest.png")
var new_cursor = preload("res://Piper/assets/1.png")
var cursorTwo = preload("res://Piper/assets/2.png")
var cursorThree = preload("res://Piper/assets/3.png")

func _ready():
	var n = 1
#	could set a variable per instance of character, then that would set a tempAsset as the asset we would want

	if n == 1:
		Input.set_custom_mouse_cursor(new_cursor,Input.CURSOR_ARROW, Vector2(12,12))
		Input.set_custom_mouse_cursor(cursorTwo,Input.CURSOR_IBEAM, Vector2(12,12))
		Input.set_custom_mouse_cursor(cursorThree,Input.CURSOR_DRAG, Vector2(12,12))
