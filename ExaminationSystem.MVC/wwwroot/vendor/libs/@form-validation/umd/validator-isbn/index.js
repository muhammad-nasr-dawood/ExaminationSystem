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

/***/ "./src/libs/@form-validation/umd/validator-isbn/index.js":
/*!***************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-isbn/index.js ***!
  \***************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function isbn() {\n    return {\n      /**\n       * Return true if the input value is a valid ISBN 10 or ISBN 13 number\n       * @see http://en.wikipedia.org/wiki/International_Standard_Book_Number\n       */\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            meta: {\n              type: null\n            },\n            valid: true\n          };\n        }\n        // http://en.wikipedia.org/wiki/International_Standard_Book_Number#Overview\n        // Groups are separated by a hyphen or a space\n        var tpe;\n        switch (true) {\n          case /^\\d{9}[\\dX]$/.test(input.value):\n          case input.value.length === 13 && /^(\\d+)-(\\d+)-(\\d+)-([\\dX])$/.test(input.value):\n          case input.value.length === 13 && /^(\\d+)\\s(\\d+)\\s(\\d+)\\s([\\dX])$/.test(input.value):\n            tpe = 'ISBN10';\n            break;\n          case /^(978|979)\\d{9}[\\dX]$/.test(input.value):\n          case input.value.length === 17 && /^(978|979)-(\\d+)-(\\d+)-(\\d+)-([\\dX])$/.test(input.value):\n          case input.value.length === 17 && /^(978|979)\\s(\\d+)\\s(\\d+)\\s(\\d+)\\s([\\dX])$/.test(input.value):\n            tpe = 'ISBN13';\n            break;\n          default:\n            return {\n              meta: {\n                type: null\n              },\n              valid: false\n            };\n        }\n        // Replace all special characters except digits and X\n        var chars = input.value.replace(/[^0-9X]/gi, '').split('');\n        var length = chars.length;\n        var sum = 0;\n        var i;\n        var checksum;\n        switch (tpe) {\n          case 'ISBN10':\n            sum = 0;\n            for (i = 0; i < length - 1; i++) {\n              sum += parseInt(chars[i], 10) * (10 - i);\n            }\n            checksum = 11 - sum % 11;\n            if (checksum === 11) {\n              checksum = 0;\n            } else if (checksum === 10) {\n              checksum = 'X';\n            }\n            return {\n              meta: {\n                type: tpe\n              },\n              valid: \"\".concat(checksum) === chars[length - 1]\n            };\n          case 'ISBN13':\n            sum = 0;\n            for (i = 0; i < length - 1; i++) {\n              sum += i % 2 === 0 ? parseInt(chars[i], 10) : parseInt(chars[i], 10) * 3;\n            }\n            checksum = 10 - sum % 10;\n            if (checksum === 10) {\n              checksum = '0';\n            }\n            return {\n              meta: {\n                type: tpe\n              },\n              valid: \"\".concat(checksum) === chars[length - 1]\n            };\n        }\n      }\n    };\n  }\n  return isbn;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-isbn/index.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-isbn/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});