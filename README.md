files-bulk-updater
==================

Small console app making udjustment for my AngularJS codebases. Automatic refactor to good AngularJS style guide at https://github.com/toddmotto/angularjs-styleguide


Using
==================

Set path to root directory of an application you want to update:

new Runner().Run(plugins, @"path/to/root/folder");

Plugins that can be used:

**JavaScriptIIFEPlugin** - https://github.com/toddmotto/angularjs-styleguide#modules and **IIFE**

**AngularNamedControllerPlugin** - https://github.com/toddmotto/angularjs-styleguide#modules: **Methods** Pass functions into module methods rather than assign as a callback
