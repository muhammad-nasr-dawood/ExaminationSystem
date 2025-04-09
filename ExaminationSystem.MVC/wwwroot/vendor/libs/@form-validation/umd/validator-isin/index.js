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

/***/ "./src/libs/@form-validation/umd/validator-isin/index.js":
/*!***************************************************************!*\
  !*** ./src/libs/@form-validation/umd/validator-isin/index.js ***!
  \***************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * FormValidation (https://formvalidation.io)\n   * The best validation library for JavaScript\n   * (c) 2013 - 2023 Nguyen Huu Phuoc <me@phuoc.ng>\n   */\n  function isin() {\n    // Available country codes\n    // See http://isin.net/country-codes/\n    var COUNTRY_CODES = 'AF|AX|AL|DZ|AS|AD|AO|AI|AQ|AG|AR|AM|AW|AU|AT|AZ|BS|BH|BD|BB|BY|BE|BZ|BJ|BM|BT|BO|BQ|BA|BW|' + 'BV|BR|IO|BN|BG|BF|BI|KH|CM|CA|CV|KY|CF|TD|CL|CN|CX|CC|CO|KM|CG|CD|CK|CR|CI|HR|CU|CW|CY|CZ|DK|DJ|DM|DO|EC|EG|' + 'SV|GQ|ER|EE|ET|FK|FO|FJ|FI|FR|GF|PF|TF|GA|GM|GE|DE|GH|GI|GR|GL|GD|GP|GU|GT|GG|GN|GW|GY|HT|HM|VA|HN|HK|HU|IS|' + 'IN|ID|IR|IQ|IE|IM|IL|IT|JM|JP|JE|JO|KZ|KE|KI|KP|KR|KW|KG|LA|LV|LB|LS|LR|LY|LI|LT|LU|MO|MK|MG|MW|MY|MV|ML|MT|' + 'MH|MQ|MR|MU|YT|MX|FM|MD|MC|MN|ME|MS|MA|MZ|MM|NA|NR|NP|NL|NC|NZ|NI|NE|NG|NU|NF|MP|NO|OM|PK|PW|PS|PA|PG|PY|PE|' + 'PH|PN|PL|PT|PR|QA|RE|RO|RU|RW|BL|SH|KN|LC|MF|PM|VC|WS|SM|ST|SA|SN|RS|SC|SL|SG|SX|SK|SI|SB|SO|ZA|GS|SS|ES|LK|' + 'SD|SR|SJ|SZ|SE|CH|SY|TW|TJ|TZ|TH|TL|TG|TK|TO|TT|TN|TR|TM|TC|TV|UG|UA|AE|GB|US|UM|UY|UZ|VU|VE|VN|VG|VI|WF|EH|' + 'YE|ZM|ZW';\n    return {\n      /**\n       * Validate an ISIN (International Securities Identification Number)\n       * @see http://en.wikipedia.org/wiki/International_Securities_Identifying_Number\n       */\n      validate: function validate(input) {\n        if (input.value === '') {\n          return {\n            valid: true\n          };\n        }\n        var v = input.value.toUpperCase();\n        var regex = new RegExp('^(' + COUNTRY_CODES + ')[0-9A-Z]{10}$');\n        if (!regex.test(input.value)) {\n          return {\n            valid: false\n          };\n        }\n        var length = v.length;\n        var converted = '';\n        var i;\n        // Convert letters to number\n        for (i = 0; i < length - 1; i++) {\n          var c = v.charCodeAt(i);\n          converted += c > 57 ? (c - 55).toString() : v.charAt(i);\n        }\n        var digits = '';\n        var n = converted.length;\n        var group = n % 2 !== 0 ? 0 : 1;\n        for (i = 0; i < n; i++) {\n          digits += parseInt(converted[i], 10) * (i % 2 === group ? 2 : 1) + '';\n        }\n        var sum = 0;\n        for (i = 0; i < digits.length; i++) {\n          sum += parseInt(digits.charAt(i), 10);\n        }\n        sum = (10 - sum % 10) % 10;\n        return {\n          valid: \"\".concat(sum) === v.charAt(length - 1)\n        };\n      }\n    };\n  }\n  return isin;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/validator-isin/index.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/validator-isin/index.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});