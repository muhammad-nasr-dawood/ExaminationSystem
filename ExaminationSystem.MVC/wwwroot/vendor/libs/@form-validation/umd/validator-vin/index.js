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

/***/ "./src/libs/@form-validation/umd/validator-vin/index.js":
/*!**************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-vin/index.js ***!
  \**************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function vin() {\n    return {\n      /**\n       * Validate an US VIN (Vehicle Identification Number)\n       */\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            valid: true\n          };\n        }\n        // Don't accept I, O, Q characters\n        if (!/^[a-hj-npr-z0-9]{8}[0-9xX][a-hj-npr-z0-9]{8}$/i.test(input.value)) {\n          return {\n            valid: false\n          };\n        }\n        var v = input.value.toUpperCase();\n        var chars = {\n          A: 1,\n          B: 2,\n          C: 3,\n          D: 4,\n          E: 5,\n          F: 6,\n          G: 7,\n          H: 8,\n          J: 1,\n          K: 2,\n          L: 3,\n          M: 4,\n          N: 5,\n          P: 7,\n          R: 9,\n          S: 2,\n          T: 3,\n          U: 4,\n          V: 5,\n          W: 6,\n          X: 7,\n          Y: 8,\n          Z: 9,\n          0: 0,\n          1: 1,\n          2: 2,\n          3: 3,\n          4: 4,\n          5: 5,\n          6: 6,\n          7: 7,\n          8: 8,\n          9: 9\n        };\n        var weights = [8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2];\n        var length = v.length;\n        var sum = 0;\n        for (var i = 0; i < length; i++) {\n          sum += chars[\"\".concat(v.charAt(i))] * weights[i];\n        }\n        var reminder = \"\".concat(sum % 11);\n        if (reminder === '10') {\n          reminder = 'X';\n        }\n        return {\n          valid: reminder === v.charAt(8)\n        };\n      }\n    };\n  }\n  return vin;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-vin/index.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-vin/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});