Textarea to YUI Rich Edit
=========================

A jQuery script that replaces Textarea elements with a Simple Rich Editor.

Simple install: (1) add JS to header, then (2) add a css class.

To see a live demo, go to:
http://www.wimagguc.com/projects/textarea-to-yui-richedit/


INSTALL
=======

1. In your HTML import jQuery and yui-insert-editor.js in the header:
	<script src="js/jquery-1.7.2.min.js"></script>
	<script src="js/yui-insert-editor.js"></script>
	
2. Add the classname 'yui_editor' to all Textarea fields to turn them to Rich Editors


MORE INFO
=========

The default rich editor options are: bold, italic, underlined text; headline selector; ordered and unordered lists; undo and redo; code editing.

Simply modify the buttons list in the insertEditor() function in yui-insert-editor.js to add more.
Help available at: http://developer.yahoo.com/yui/examples/editor/

- The width and height of the editor will be the same as the Textarea's you replace.
- Only Textareas with 'yui_editor' class will be replaced.
- If you already have YUI loaded, you might want to modify the init() function in yui-insert-editor.js
- Only a subset of the YUI Library is included to maintain smaller package size

The JS also injects all the YUI dependencies and adds the classname 'yui-skin-sam' to the body tag required by YUI.


LICENSE
=======

Do with the code whatever you please. Show me if you did something cool.

This code uses the jQuery Javascript library and the YUI Library. To read more about these, go to:
http://jquery.com/
http://developer.yahoo.com/yui/


ABOUT
=====

Richard Dancsi
http://www.wimagguc.com/

@wimagguc on twitter: http://twitter.com/wimagguc/

linkedin: http://linkedin.com/in/richarddancsi
google+: https://plus.google.com/u/0/115939246085616544919
