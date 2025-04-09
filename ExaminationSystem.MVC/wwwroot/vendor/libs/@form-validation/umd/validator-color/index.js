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

/***/ "./src/libs/@form-validation/umd/validator-color/index.js":
/*!****************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-color/index.js ***!
  \****************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function color() {\n    var SUPPORTED_TYPES = ['hex', 'rgb', 'rgba', 'hsl', 'hsla', 'keyword'];\n    var KEYWORD_COLORS = [\n    // Colors start with A\n    'aliceblue', 'antiquewhite', 'aqua', 'aquamarine', 'azure',\n    // B\n    'beige', 'bisque', 'black', 'blanchedalmond', 'blue', 'blueviolet', 'brown', 'burlywood',\n    // C\n    'cadetblue', 'chartreuse', 'chocolate', 'coral', 'cornflowerblue', 'cornsilk', 'crimson', 'cyan',\n    // D\n    'darkblue', 'darkcyan', 'darkgoldenrod', 'darkgray', 'darkgreen', 'darkgrey', 'darkkhaki', 'darkmagenta', 'darkolivegreen', 'darkorange', 'darkorchid', 'darkred', 'darksalmon', 'darkseagreen', 'darkslateblue', 'darkslategray', 'darkslategrey', 'darkturquoise', 'darkviolet', 'deeppink', 'deepskyblue', 'dimgray', 'dimgrey', 'dodgerblue',\n    // F\n    'firebrick', 'floralwhite', 'forestgreen', 'fuchsia',\n    // G\n    'gainsboro', 'ghostwhite', 'gold', 'goldenrod', 'gray', 'green', 'greenyellow', 'grey',\n    // H\n    'honeydew', 'hotpink',\n    // I\n    'indianred', 'indigo', 'ivory',\n    // K\n    'khaki',\n    // L\n    'lavender', 'lavenderblush', 'lawngreen', 'lemonchiffon', 'lightblue', 'lightcoral', 'lightcyan', 'lightgoldenrodyellow', 'lightgray', 'lightgreen', 'lightgrey', 'lightpink', 'lightsalmon', 'lightseagreen', 'lightskyblue', 'lightslategray', 'lightslategrey', 'lightsteelblue', 'lightyellow', 'lime', 'limegreen', 'linen',\n    // M\n    'magenta', 'maroon', 'mediumaquamarine', 'mediumblue', 'mediumorchid', 'mediumpurple', 'mediumseagreen', 'mediumslateblue', 'mediumspringgreen', 'mediumturquoise', 'mediumvioletred', 'midnightblue', 'mintcream', 'mistyrose', 'moccasin',\n    // N\n    'navajowhite', 'navy',\n    // O\n    'oldlace', 'olive', 'olivedrab', 'orange', 'orangered', 'orchid',\n    // P\n    'palegoldenrod', 'palegreen', 'paleturquoise', 'palevioletred', 'papayawhip', 'peachpuff', 'peru', 'pink', 'plum', 'powderblue', 'purple',\n    // R\n    'red', 'rosybrown', 'royalblue',\n    // S\n    'saddlebrown', 'salmon', 'sandybrown', 'seagreen', 'seashell', 'sienna', 'silver', 'skyblue', 'slateblue', 'slategray', 'slategrey', 'snow', 'springgreen', 'steelblue',\n    // T\n    'tan', 'teal', 'thistle', 'tomato', 'transparent', 'turquoise',\n    // V\n    'violet',\n    // W\n    'wheat', 'white', 'whitesmoke',\n    // Y\n    'yellow', 'yellowgreen'];\n    var hex = function hex(value) {\n      return /(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test(value);\n    };\n    var hsl = function hsl(value) {\n      return /^hsl\\((\\s*(-?\\d+)\\s*,)(\\s*(\\b(0?\\d{1,2}|100)\\b%)\\s*,)(\\s*(\\b(0?\\d{1,2}|100)\\b%)\\s*)\\)$/.test(value);\n    };\n    var hsla = function hsla(value) {\n      return /^hsla\\((\\s*(-?\\d+)\\s*,)(\\s*(\\b(0?\\d{1,2}|100)\\b%)\\s*,){2}(\\s*(0?(\\.\\d+)?|1(\\.0+)?)\\s*)\\)$/.test(value);\n    };\n    var keyword = function keyword(value) {\n      return KEYWORD_COLORS.indexOf(value) >= 0;\n    };\n    var rgb = function rgb(value) {\n      return /^rgb\\((\\s*(\\b([01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\b)\\s*,){2}(\\s*(\\b([01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\b)\\s*)\\)$/.test(value) || /^rgb\\((\\s*(\\b(0?\\d{1,2}|100)\\b%)\\s*,){2}(\\s*(\\b(0?\\d{1,2}|100)\\b%)\\s*)\\)$/.test(value);\n    };\n    var rgba = function rgba(value) {\n      return /^rgba\\((\\s*(\\b([01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\b)\\s*,){3}(\\s*(0?(\\.\\d+)?|1(\\.0+)?)\\s*)\\)$/.test(value) || /^rgba\\((\\s*(\\b(0?\\d{1,2}|100)\\b%)\\s*,){3}(\\s*(0?(\\.\\d+)?|1(\\.0+)?)\\s*)\\)$/.test(value);\n    };\n    return {\n      /**\n       * Return true if the input value is a valid color\n       * @returns {boolean}\n       */\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            valid: true\n          };\n        }\n        var types = typeof input.options.type === 'string' ? input.options.type.toString().replace(/s/g, '').split(',') : input.options.type || SUPPORTED_TYPES;\n        for (var _i = 0, types_1 = types; _i < types_1.length; _i++) {\n          var type = types_1[_i];\n          var tpe = type.toLowerCase();\n          if (SUPPORTED_TYPES.indexOf(tpe) === -1) {\n            continue;\n          }\n          var result = true;\n          switch (tpe) {\n            case 'hex':\n              result = hex(input.value);\n              break;\n            case 'hsl':\n              result = hsl(input.value);\n              break;\n            case 'hsla':\n              result = hsla(input.value);\n              break;\n            case 'keyword':\n              result = keyword(input.value);\n              break;\n            case 'rgb':\n              result = rgb(input.value);\n              break;\n            case 'rgba':\n              result = rgba(input.value);\n              break;\n          }\n          if (result) {\n            return {\n              valid: true\n            };\n          }\n        }\n        return {\n          valid: false\n        };\n      }\n    };\n  }\n  return color;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-color/index.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-color/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});