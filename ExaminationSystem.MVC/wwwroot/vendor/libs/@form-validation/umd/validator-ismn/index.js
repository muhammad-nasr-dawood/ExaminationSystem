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

/***/ "./src/libs/@form-validation/umd/validator-ismn/index.js":
/*!***************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-ismn/index.js ***!
  \***************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function ismn() {\n    return {\n      /**\n       * Validate ISMN (International Standard Music Number)\n       * @see http://en.wikipedia.org/wiki/International_Standard_Music_Number\n       */\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            meta: null,\n            valid: true\n          };\n        }\n        // Groups are separated by a hyphen or a space\n        var tpe;\n        switch (true) {\n          case /^M\\d{9}$/.test(input.value):\n          case /^M-\\d{4}-\\d{4}-\\d{1}$/.test(input.value):\n          case /^M\\s\\d{4}\\s\\d{4}\\s\\d{1}$/.test(input.value):\n            tpe = 'ISMN10';\n            break;\n          case /^9790\\d{9}$/.test(input.value):\n          case /^979-0-\\d{4}-\\d{4}-\\d{1}$/.test(input.value):\n          case /^979\\s0\\s\\d{4}\\s\\d{4}\\s\\d{1}$/.test(input.value):\n            tpe = 'ISMN13';\n            break;\n          default:\n            return {\n              meta: null,\n              valid: false\n            };\n        }\n        var v = input.value;\n        if ('ISMN10' === tpe) {\n          v = \"9790\".concat(v.substr(1));\n        }\n        // Replace all special characters except digits\n        v = v.replace(/[^0-9]/gi, '');\n        var sum = 0;\n        var length = v.length;\n        var weight = [1, 3];\n        for (var i = 0; i < length - 1; i++) {\n          sum += parseInt(v.charAt(i), 10) * weight[i % 2];\n        }\n        sum = (10 - sum % 10) % 10;\n        return {\n          meta: {\n            type: tpe\n          },\n          valid: \"\".concat(sum) === v.charAt(length - 1)\n        };\n      }\n    };\n  }\n  return ismn;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-ismn/index.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-ismn/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});