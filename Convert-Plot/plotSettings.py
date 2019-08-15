######################### AXES/TITLE SETTINGS #########################

X_LABEL_SIZE = 12
X_LABEL_FONT = "fantasy"# available - {'cursive', 'fantasy', 'monospace', 'sans', 'sans serif', 'sans-serif', 'serif'}.
X_LABEL_COLOR = 'black'

X_TICK_SIZE = 10
X_TICK_FONT = "fantasy"# available - {'cursive', 'fantasy', 'monospace', 'sans', 'sans serif', 'sans-serif', 'serif'}.
X_TICK_LINE_COLOR = 'black'
X_TICK_LABEL_COLOR = 'black'
X_TICK_DIRECTION = "in" # where tick is relative to axis {'in', 'out', 'inout'}

Y_LABEL_SIZE = 12
Y_LABEL_FONT = "fantasy"# available - {'cursive', 'fantasy', 'monospace', 'sans', 'sans serif', 'sans-serif', 'serif'}.
Y_LABEL_COLOR = 'black'

Y_TICK_SIZE = 10
Y_TICK_FONT = "fantasy"# available - {'cursive', 'fantasy', 'monospace', 'sans', 'sans serif', 'sans-serif', 'serif'}.
Y_TICK_LABEL_COLOR = 'black'
Y_TICK_LINE_COLOR = 'black'

Y_TICK_DIRECTION = "in" # where tick is relative to axis {'in', 'out', 'inout'}
TITLE_LABEL_SIZE = 14
TITLE_LABEL_FONT = "fantasy"# available - {'cursive', 'fantasy', 'monospace', 'sans', 'sans serif', 'sans-serif', 'serif'}.
TITLE_LABEL_COLOR = 'black'




######################## RESIZE AXES ####################################
X_CROP = False # These two must be true to resize
Y_CROP = False
X_MIN = 1
X_MAX = 4
Y_MIN = 0
Y_MAX = 0

######################### LINE/POINT SETTINGS #########################


"""
----- Line Style character descriptions -------
'-'       solid line style
'--'      dashed line style
'-.'      dash-dot line style
':'       dotted line style
'.'       point marker
','       pixel marker
'o'       circle marker
'v'       triangle_down marker
'^'       triangle_up marker
'<'       triangle_left marker
'>'       triangle_right marker
'1'       tri_down marker
'2'       tri_up marker
'3'       tri_left marker
'4'       tri_right marker
's'       square marker
'p'       pentagon marker
'*'       star marker
'h'       hexagon1 marker
'H'       hexagon2 marker
'+'       plus marker
'x'       x marker
'D'       diamond marker
'd'       thin_diamond marker
'|'       vline marker
'_'       hline marker


------ Color Descriptions -----------
‘b’	= blue
‘g’	= green
‘r’	= red
‘c’	= cyan
‘m’	= magenta
‘y’ = yellow
‘k’	= black
‘w’	= white
"""

LINE_WIDTH = 2
POINT_SIZE = 10
LINE_STYLE = '--'
POINT_STYLE = '.'




######################### LEGEND SETTINGS #########################

"""
------ Legend Location Descriptions -------
'best'	= 0
'upper right'	= 1
'upper left'	= 2
'lower left'	= 3
'lower right'	= 4
'right'	= 5
'center left'	= 6
'center right'	= 7
'lower center'	= 8
'upper center'	= 9
'center'	= 10
"""

LEGEND = True
LEGEND_LOCATION = 'upper right'
LEGEND_TEXT_SIZE = 16
LEGEND_TEXT_FONT = "cursive" # available - {'cursive', 'fantasy', 'monospace', 'sans', 'sans serif', 'sans-serif', 'serif'}.


######################### OTHER PLOT SETTINGS #########################

GRID = False

