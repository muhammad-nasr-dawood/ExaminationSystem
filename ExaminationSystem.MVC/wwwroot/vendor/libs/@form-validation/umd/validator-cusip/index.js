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

/***/ "./src/libs/@form-validation/umd/validator-cusip/index.js":
/*!****************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-cusip/index.js ***!
  \****************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function cusip() {\n    return {\n      /**\n       * Validate a CUSIP number\n       * @see http://en.wikipedia.org/wiki/CUSIP\n       */\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            valid: true\n          };\n        }\n        var value = input.value.toUpperCase();\n        // O, I aren't allowed\n        if (!/^[0123456789ABCDEFGHJKLMNPQRSTUVWXYZ*@#]{9}$/.test(value)) {\n          return {\n            valid: false\n          };\n        }\n        // Get the last char\n        var chars = value.split('');\n        var lastChar = chars.pop();\n        var converted = chars.map(function (c) {\n          var code = c.charCodeAt(0);\n          switch (true) {\n            case c === '*':\n              return 36;\n            case c === '@':\n              return 37;\n            case c === '#':\n              return 38;\n            // Replace A, B, C, ..., Z with 10, 11, ..., 35\n            case code >= 'A'.charCodeAt(0) && code <= 'Z'.charCodeAt(0):\n              return code - 'A'.charCodeAt(0) + 10;\n            default:\n              return parseInt(c, 10);\n          }\n        });\n        var sum = converted.map(function (v, i) {\n          var double = i % 2 === 0 ? v : 2 * v;\n          return Math.floor(double / 10) + double % 10;\n        }).reduce(function (a, b) {\n          return a + b;\n        }, 0);\n        var checkDigit = (10 - sum % 10) % 10;\n        return {\n          valid: lastChar === \"\".concat(checkDigit)\n        };\n      }\n    };\n  }\n  return cusip;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-cusip/index.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-cusip/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});