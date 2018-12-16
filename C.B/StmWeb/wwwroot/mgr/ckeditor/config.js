/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
	// Define changes to default configuration here. For example:
	config.language = 'zh-CN';
	config.height = 450;
	config.width = 'auto';
	//  config.filebrowserUploadMethod = 'form';
	//  config.filebrowserUploadUrl = '/uploader/upload.php';
	//  config.filebrowserImageUploadUrl = '/uploader/upload.php?type=Images';
	//  config.filebrowserFlashUploadUrl = '/uploader/upload.php?type=Flash';

	config.filebrowserBrowseUrl = '../../Sys/File/Checker'; //'/browser/browse.php';
	config.filebrowserImageBrowseLinkUrl = '../../Sys/File/Checker?type=link';
	config.filebrowserImageBrowseUrl = '../../Sys/File/Checker?type=image';
	config.filebrowserFlashBrowseUrl = '../../Sys/File/Checker?type=flash';

	config.filebrowserWindowHeight = 580;
	// config.filebrowserWindowHeight = '50%';
	config.filebrowserWindowWidth = 750;
	// config.filebrowserWindowWidth = '50%';

	// config.uiColor = '#AADC6E';
};
