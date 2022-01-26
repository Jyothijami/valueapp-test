/**
 * 	
 * REPLACING ALL TEXTAREA'S WITH THE CLASS 'yui_editor' BY A YUI RICH EDITOR 
 * 
 * Important info:
 * - The width and height of the rich editor will be the same as the textarea's you replace
 * - Only the text areas with the 'yui_editor' class will be replaced
 * -  
 * 
 * FOR MORE INFORMATION SEE
 * http://developer.yahoo.com/yui/examples/editor/
 * 
 */

var yuiEditor = new function() {
	
	_self = this;
	
	_self.params = {
		SELF_FILE_NAME : 'yui-insert-editor.js'
	};

	// Helper function for adding css and js nodes to the document
	var insertHeaderItem = function(type, name) {

		// Assuming that the YUI library is relative to the yui-insert-editor.js - 
		// so we just get the yui-insert-editor.js's location, and add the url to all the other scripts as well
		var script_url;

		var script_tags = document.getElementsByTagName('script');
		var i = 0;
		while (!script_url && i<script_tags.length) {
			var tmp_url = script_tags[i].getAttribute('src');
			if (tmp_url && tmp_url.match( _self.params.SELF_FILE_NAME )) {
				var tmp = tmp_url.split('/js/' + _self.params.SELF_FILE_NAME);
				if (tmp.length > 1) {
					script_url = tmp[0];
				}
			}
			i++;
		}
		// ^^^
		
		if (!script_url) { script_url = '.'; }
		
		if (type == 'css') {
			document.writeln('<link rel="stylesheet" type="text/css" href="' + script_url + '/' + name + '">');
		}
		else if (type == 'js') {
			document.writeln('<script src="' + script_url + '/' + name + '"></script>');
		}	
	}

	///////////////////////////////////////////////////////////////////////////////////////////////
	// PUBLIC FUNCTIONS  //////////////////////////////////////////////////////////////////////////
	var publicfunc = {
		
		/// REPLACE A TEXTAREA OBJECT WITH THE RICH EDITOR  /////////////////////////////////////// 
		insertEditor : function(object) {

			if ( !$(object).attr("id") ) {
				if ( $(object).attr("name") ) {
					$(object).attr("id", $(object).attr("name") );
				} else {
					$(object).attr("id", "_EDITOR_" + Math.ceil(Math.random() * 10000) );
				}
			}
			
			var state = 'off';

			var myEditor = new YAHOO.widget.Editor($(object).attr('id'), {
	    		height: $(object).height() + 'px',
	    		width: $(object).width() + 'px',
			    dompath: true,
	    		animate: true,
				handleSubmit: true,
				filterWord: true,
				focusAtStart: false,
	    		toolbar: {
	        		titlebar: $(object).attr('title') ? $(object).attr('title') : "",
	        		buttons: [
			            { group: 'textstyle', label: '',
			                buttons: [
			                    { type: 'push', label: 'Bold', value: 'bold' },
			                    { type: 'push', label: 'Italic', value: 'italic' },
			                    { type: 'push', label: 'Underline', value: 'underline' },
			                    { type: 'separator' },
								{ type: 'select', label: 'Normal', value: 'heading', disabled: true,
									menu: [
										{ text: 'Normal', value: 'none', checked: true },
										{ text: 'Header 1', value: 'h1' },
										{ text: 'Header 2', value: 'h2' },
										{ text: 'Header 3', value: 'h3' },
										{ text: 'Header 4', value: 'h4' },
									]
								},
								{ type: 'separator' },
			                    { type: 'push', label: 'Create an Unordered List', value: 'insertunorderedlist' },
			                    { type: 'push', label: 'Create an Ordered List', value: 'insertorderedlist' },
			                    { type: 'separator' },
					            { type: 'push', label: 'Undo', value: 'undo' },
					            { type: 'push', label: 'Redo', value: 'redo'},
			                    { type: 'separator' },
					            { type: 'push', label: 'Edit HTML Code', value: 'editcode' }
					        ]
			            }
			        ]
		    	}
			});

			myEditor.on('toolbarLoaded', function() {
				var Dom = YAHOO.util.Dom;
				this.toolbar.on('editcodeClick', function() {

				var ta = this.get('element'),
				iframe = this.get('iframe').get('element');

					if (state == 'on') {
						state = 'off';
						this.toolbar.set('disabled', false);
						YAHOO.log('Show the Editor', 'info', 'example');
						YAHOO.log('Inject the HTML from the textarea into the editor', 'info', 'example');
						this.setEditorHTML(ta.value);
						if (!this.browser.ie) {
							this._setDesignMode('on');
						}
						Dom.removeClass(iframe, 'editor-hidden');
						Dom.addClass(ta, 'editor-hidden');
						this.show();
						this._focusWindow();
					} else {
						state = 'on';
						YAHOO.log('Show the Code Editor', 'info', 'example');
						this.cleanHTML();
						YAHOO.log('Save the Editors HTML', 'info', 'example');
						Dom.addClass(iframe, 'editor-hidden');
						Dom.removeClass(ta, 'editor-hidden');
						this.toolbar.set('disabled', true);
						this.toolbar.getButtonByValue('editcode').set('disabled', false);
						this.toolbar.selectButton('editcode');
						this.dompath.innerHTML = 'Editing HTML Code';
						this.hide();
					}
					return false;
				}, this, true);

				this.on('cleanHTML', function(ev) {
					YAHOO.log('cleanHTML callback fired..', 'info', 'example');
					this.get('element').value = ev.html;
				}, this, true);

				this.on('afterRender', function() {
					var wrapper = this.get('editor_wrapper');
					wrapper.appendChild(this.get('element'));
					this.setStyle('width', '100%');
					this.setStyle('height', '100%');
					this.setStyle('visibility', '');
					this.setStyle('top', '');
					this.setStyle('left', '');
					this.setStyle('position', '');
					this.addClass('editor-hidden');
				}, this, true);
		
			}, myEditor, true);

			myEditor.render();
		},
		
		// ADD CSS AND JS DEPENDENCIES TO THE DOCUMENT ////////////////////////////////////////////
		init : function() {
			insertHeaderItem('css', 'js/yui/build/assets/skins/sam/skin.css');
			insertHeaderItem('css', 'css/yui-insert-editor.css');
			
			insertHeaderItem('js', 'js/yui/build/yahoo-dom-event/yahoo-dom-event.js');
			insertHeaderItem('js', 'js/yui/build/element/element-min.js');
			insertHeaderItem('js', 'js/yui/build/container/container_core-min.js');
			insertHeaderItem('js', 'js/yui/build/menu/menu-min.js');
			insertHeaderItem('js', 'js/yui/build/button/button-min.js');
			insertHeaderItem('js', 'js/yui/build/editor/editor-min.js');
			insertHeaderItem('js', 'js/yui/build/editor/simpleeditor-min.js');
		}

	}
	
	return publicfunc;
}

// Init editor during page load
yuiEditor.init();

// After page loaded, replace textarea's with the classname 'yui_editor' with the rich editor >>>
$(document).ready(function() {
	$("BODY").addClass('yui-skin-sam');
	$('.yui_editor').each( function() {
		yuiEditor.insertEditor( $(this) );
	});
});
// ^^^
