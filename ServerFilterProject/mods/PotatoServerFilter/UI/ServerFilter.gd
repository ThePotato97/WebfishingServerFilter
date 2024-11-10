extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

func is_dedicated_enabled():
	return $Panel2/filter2/HBoxContainer/dedicatedmatch.pressed
	
func is_lure_enabled():
	return $Panel2/filter2/HBoxContainer/lurematch.pressed
