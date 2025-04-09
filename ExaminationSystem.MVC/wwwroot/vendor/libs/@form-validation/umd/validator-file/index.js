/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
(function webpackUniversalModuleDefinition(root, factory) {
	if(typeof exports === 'object' && typeof module === 'object')
		module.exports = factory();
	else if(typeof define === 'function' && define.amd)
		define([], factory);
	else {
		var a = factory();
		for(var i in a) (typeof exports === 'object' ? exports : root)[i] = a[i];
	}
})(self, () => {
return /******/ (() => { // webpackBootstrap
/******/ 	var __webpack_modules__ = ({

/***/ "./src/libs/@form-validation/umd/validator-file/index.js":
/*!***************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-file/index.js ***!
  \***************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  // Get the file name without extension\n  var getFileName = function getFileName(fileName) {\n    return fileName.indexOf('.') === -1 ? fileName : fileName.split('.').slice(0, -1).join('.');\n  };\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function file() {\n    return {\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            valid: true\n          };\n        }\n        var extension;\n        var name;\n        var extensions = input.options.extension ? input.options.extension.toLowerCase().split(',').map(function (item) {\n          return item.trim();\n        }) : [];\n        var types = input.options.type ? input.options.type.toLowerCase().split(',').map(function (item) {\n          return item.trim();\n        }) : [];\n        var html5 = window['File'] && window['FileList'] && window['FileReader'];\n        if (html5) {\n          // Get FileList instance\n          var files = input.element.files;\n          var total = files.length;\n          var allSize = 0;\n          // Check the maxFiles\n          if (input.options.maxFiles && total > parseInt(\"\".concat(input.options.maxFiles), 10)) {\n            return {\n              meta: {\n                error: 'INVALID_MAX_FILES'\n              },\n              valid: false\n            };\n          }\n          // Check the minFiles\n          if (input.options.minFiles && total < parseInt(\"\".concat(input.options.minFiles), 10)) {\n            return {\n              meta: {\n                error: 'INVALID_MIN_FILES'\n              },\n              valid: false\n            };\n          }\n          var metaData = {};\n          for (var i = 0; i < total; i++) {\n            allSize += files[i].size;\n            extension = files[i].name.substr(files[i].name.lastIndexOf('.') + 1);\n            metaData = {\n              ext: extension,\n              file: files[i],\n              size: files[i].size,\n              type: files[i].type\n            };\n            // Check the minSize\n            if (input.options.minSize && files[i].size < parseInt(\"\".concat(input.options.minSize), 10)) {\n              return {\n                meta: Object.assign({}, {\n                  error: 'INVALID_MIN_SIZE'\n                }, metaData),\n                valid: false\n              };\n            }\n            // Check the maxSize\n            if (input.options.maxSize && files[i].size > parseInt(\"\".concat(input.options.maxSize), 10)) {\n              return {\n                meta: Object.assign({}, {\n                  error: 'INVALID_MAX_SIZE'\n                }, metaData),\n                valid: false\n              };\n            }\n            // Check file extension\n            if (extensions.length > 0 && extensions.indexOf(extension.toLowerCase()) === -1) {\n              return {\n                meta: Object.assign({}, {\n                  error: 'INVALID_EXTENSION'\n                }, metaData),\n                valid: false\n              };\n            }\n            // Check file type\n            if (types.length > 0 && files[i].type && types.indexOf(files[i].type.toLowerCase()) === -1) {\n              return {\n                meta: Object.assign({}, {\n                  error: 'INVALID_TYPE'\n                }, metaData),\n                valid: false\n              };\n            }\n            // Check file name\n            if (input.options.validateFileName && !input.options.validateFileName(getFileName(files[i].name))) {\n              return {\n                meta: Object.assign({}, {\n                  error: 'INVALID_NAME'\n                }, metaData),\n                valid: false\n              };\n            }\n          }\n          // Check the maxTotalSize\n          if (input.options.maxTotalSize && allSize > parseInt(\"\".concat(input.options.maxTotalSize), 10)) {\n            return {\n              meta: Object.assign({}, {\n                error: 'INVALID_MAX_TOTAL_SIZE',\n                totalSize: allSize\n              }, metaData),\n              valid: false\n            };\n          }\n          // Check the minTotalSize\n          if (input.options.minTotalSize && allSize < parseInt(\"\".concat(input.options.minTotalSize), 10)) {\n            return {\n              meta: Object.assign({}, {\n                error: 'INVALID_MIN_TOTAL_SIZE',\n                totalSize: allSize\n              }, metaData),\n              valid: false\n            };\n          }\n        } else {\n          // Check file extension\n          extension = input.value.substr(input.value.lastIndexOf('.') + 1);\n          if (extensions.length > 0 && extensions.indexOf(extension.toLowerCase()) === -1) {\n            return {\n              meta: {\n                error: 'INVALID_EXTENSION',\n                ext: extension\n              },\n              valid: false\n            };\n          }\n          // Check file name\n          name = getFileName(input.value);\n          if (input.options.validateFileName && !input.options.validateFileName(name)) {\n            return {\n              meta: {\n                error: 'INVALID_NAME',\n                name: name\n              },\n              valid: false\n            };\n          }\n        }\n        return {\n          valid: true\n        };\n      }\n    };\n  }\n  return file;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-file/index.js?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-file/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});