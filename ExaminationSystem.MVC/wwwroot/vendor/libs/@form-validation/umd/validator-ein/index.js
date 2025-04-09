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

/***/ "./src/libs/@form-validation/umd/validator-ein/index.js":
/*!**************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-ein/index.js ***!
  \**************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function ein() {\n    // The first two digits are called campus\n    // See http://en.wikipedia.org/wiki/Employer_Identification_Number\n    // http://www.irs.gov/Businesses/Small-Businesses-&-Self-Employed/How-EINs-are-Assigned-and-Valid-EIN-Prefixes\n    var CAMPUS = {\n      ANDOVER: ['10', '12'],\n      ATLANTA: ['60', '67'],\n      AUSTIN: ['50', '53'],\n      BROOKHAVEN: ['01', '02', '03', '04', '05', '06', '11', '13', '14', '16', '21', '22', '23', '25', '34', '51', '52', '54', '55', '56', '57', '58', '59', '65'],\n      CINCINNATI: ['30', '32', '35', '36', '37', '38', '61'],\n      FRESNO: ['15', '24'],\n      INTERNET: ['20', '26', '27', '45', '46', '47'],\n      KANSAS_CITY: ['40', '44'],\n      MEMPHIS: ['94', '95'],\n      OGDEN: ['80', '90'],\n      PHILADELPHIA: ['33', '39', '41', '42', '43', '48', '62', '63', '64', '66', '68', '71', '72', '73', '74', '75', '76', '77', '81', '82', '83', '84', '85', '86', '87', '88', '91', '92', '93', '98', '99'],\n      SMALL_BUSINESS_ADMINISTRATION: ['31']\n    };\n    return {\n      /**\n       * Validate EIN (Employer Identification Number) which is also known as\n       * Federal Employer Identification Number (FEIN) or Federal Tax Identification Number\n       */\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            meta: null,\n            valid: true\n          };\n        }\n        if (!/^[0-9]{2}-?[0-9]{7}$/.test(input.value)) {\n          return {\n            meta: null,\n            valid: false\n          };\n        }\n        // Check the first two digits\n        var campus = \"\".concat(input.value.substr(0, 2));\n        for (var key in CAMPUS) {\n          if (CAMPUS[key].indexOf(campus) !== -1) {\n            return {\n              meta: {\n                campus: key\n              },\n              valid: true\n            };\n          }\n        }\n        return {\n          meta: null,\n          valid: false\n        };\n      }\n    };\n  }\n  return ein;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-ein/index.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-ein/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});