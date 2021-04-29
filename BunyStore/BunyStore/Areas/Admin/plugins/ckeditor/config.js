/// <reference path="../ckfinder/config.js" />
/// <reference path="../ckfinder/ckfinder.js" />
/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	config.fullPage = true;
	CKEDITOR.config.entities = false;
	// Enable all default text formats:
	config.format_tags = 'p;h1;h2;h3;h4;h5;h6;pre;address;div';

	// Enable a limited set of text formats:
	config.format_tags = 'p;h1;h2;pre;div';
	CKEDITOR.config.autoParagraph = false;
	CKEDITOR.config.enterMode = CKEDITOR.ENTER_BR; 
	config.extraAllowedContent = 'span;ul;li;table;td;style;*[id];*(*);*{*}';
	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';
	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.language = 'vi';
	config.filebrowserBrowseUrl = '/Areas/Admin/plugins/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/Areas/Admin/plugins/ckfinder.html?Type=Images';
	config.filebrowserFlashBrowseUrl = '/Areas/Admin/plugins/ckfinder.html?Type=Flash';
	config.filebrowserUploadUrl = '/Areas/Admin/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
	config.filebrowserImageUploadUrl = '/Data';
	config.filebrowserFlashUploadUrl = '/Areas/Admin/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

	CKFinder.setupCKEditor(null, '/Areas/Admin/plugins/ckfinder/');

};
