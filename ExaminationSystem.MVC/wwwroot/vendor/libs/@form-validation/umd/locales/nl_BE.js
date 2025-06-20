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

/***/ "./src/libs/@form-validation/umd/locales/nl_BE.js":
/*!********************************************************!*\
  !*** ./src/libs/@form-validation/umd/locales/nl_BE.js ***!
  \********************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * Belgium (Dutch) language package\n   * Translated by @dokterpasta. Improved by @jdt\n   */\n  var nl_BE = {\n    base64: {\n      default: 'Geef een geldige base 64 geëncodeerde tekst in'\n    },\n    between: {\n      default: 'Geef een waarde in van %s tot en met %s',\n      notInclusive: 'Geef een waarde in van %s tot %s'\n    },\n    bic: {\n      default: 'Geef een geldig BIC-nummer in'\n    },\n    callback: {\n      default: 'Geef een geldige waarde in'\n    },\n    choice: {\n      between: 'Kies tussen de %s en %s opties',\n      default: 'Geef een geldige waarde in',\n      less: 'Kies minimaal %s opties',\n      more: 'Kies maximaal %s opties'\n    },\n    color: {\n      default: 'Geef een geldige kleurcode in'\n    },\n    creditCard: {\n      default: 'Geef een geldig kredietkaartnummer in'\n    },\n    cusip: {\n      default: 'Geef een geldig CUSIP-nummer in'\n    },\n    date: {\n      default: 'Geef een geldige datum in',\n      max: 'Geef een datum in die voor %s ligt',\n      min: 'Geef een datum in die na %s ligt',\n      range: 'Geef een datum in die tussen %s en %s ligt'\n    },\n    different: {\n      default: 'Geef een andere waarde in'\n    },\n    digits: {\n      default: 'Geef alleen cijfers in'\n    },\n    ean: {\n      default: 'Geef een geldig EAN-nummer in'\n    },\n    ein: {\n      default: 'Geef een geldig EIN-nummer in'\n    },\n    emailAddress: {\n      default: 'Geef een geldig emailadres op'\n    },\n    file: {\n      default: 'Kies een geldig bestand'\n    },\n    greaterThan: {\n      default: 'Geef een waarde in die gelijk is aan of groter is dan %s',\n      notInclusive: 'Geef een waarde in die groter is dan %s'\n    },\n    grid: {\n      default: 'Geef een geldig GRID-nummer in'\n    },\n    hex: {\n      default: 'Geef een geldig hexadecimaal nummer in'\n    },\n    iban: {\n      countries: {\n        AD: 'Andorra',\n        AE: 'Verenigde Arabische Emiraten',\n        AL: 'Albania',\n        AO: 'Angola',\n        AT: 'Oostenrijk',\n        AZ: 'Azerbeidzjan',\n        BA: 'Bosnië en Herzegovina',\n        BE: 'België',\n        BF: 'Burkina Faso',\n        BG: 'Bulgarije\"',\n        BH: 'Bahrein',\n        BI: 'Burundi',\n        BJ: 'Benin',\n        BR: 'Brazilië',\n        CH: 'Zwitserland',\n        CI: 'Ivoorkust',\n        CM: 'Kameroen',\n        CR: 'Costa Rica',\n        CV: 'Cape Verde',\n        CY: 'Cyprus',\n        CZ: 'Tsjechische',\n        DE: 'Duitsland',\n        DK: 'Denemarken',\n        DO: 'Dominicaanse Republiek',\n        DZ: 'Algerije',\n        EE: 'Estland',\n        ES: 'Spanje',\n        FI: 'Finland',\n        FO: 'Faeröer',\n        FR: 'Frankrijk',\n        GB: 'Verenigd Koninkrijk',\n        GE: 'Georgia',\n        GI: 'Gibraltar',\n        GL: 'Groenland',\n        GR: 'Griekenland',\n        GT: 'Guatemala',\n        HR: 'Kroatië',\n        HU: 'Hongarije',\n        IE: 'Ierland',\n        IL: 'Israël',\n        IR: 'Iran',\n        IS: 'IJsland',\n        IT: 'Italië',\n        JO: 'Jordan',\n        KW: 'Koeweit',\n        KZ: 'Kazachstan',\n        LB: 'Libanon',\n        LI: 'Liechtenstein',\n        LT: 'Litouwen',\n        LU: 'Luxemburg',\n        LV: 'Letland',\n        MC: 'Monaco',\n        MD: 'Moldavië',\n        ME: 'Montenegro',\n        MG: 'Madagascar',\n        MK: 'Macedonië',\n        ML: 'Mali',\n        MR: 'Mauretanië',\n        MT: 'Malta',\n        MU: 'Mauritius',\n        MZ: 'Mozambique',\n        NL: 'Nederland',\n        NO: 'Noorwegen',\n        PK: 'Pakistan',\n        PL: 'Polen',\n        PS: 'Palestijnse',\n        PT: 'Portugal',\n        QA: 'Qatar',\n        RO: 'Roemenië',\n        RS: 'Servië',\n        SA: 'Saudi-Arabië',\n        SE: 'Zweden',\n        SI: 'Slovenië',\n        SK: 'Slowakije',\n        SM: 'San Marino',\n        SN: 'Senegal',\n        TL: 'Oost-Timor',\n        TN: 'Tunesië',\n        TR: 'Turkije',\n        VG: 'Britse Maagdeneilanden',\n        XK: 'Republiek Kosovo'\n      },\n      country: 'Geef een geldig IBAN-nummer in uit %s',\n      default: 'Geef een geldig IBAN-nummer in'\n    },\n    id: {\n      countries: {\n        BA: 'Bosnië en Herzegovina',\n        BG: 'Bulgarije',\n        BR: 'Brazilië',\n        CH: 'Zwitserland',\n        CL: 'Chili',\n        CN: 'China',\n        CZ: 'Tsjechische',\n        DK: 'Denemarken',\n        EE: 'Estland',\n        ES: 'Spanje',\n        FI: 'Finland',\n        HR: 'Kroatië',\n        IE: 'Ierland',\n        IS: 'IJsland',\n        LT: 'Litouwen',\n        LV: 'Letland',\n        ME: 'Montenegro',\n        MK: 'Macedonië',\n        NL: 'Nederland',\n        PL: 'Polen',\n        RO: 'Roemenië',\n        RS: 'Servië',\n        SE: 'Zweden',\n        SI: 'Slovenië',\n        SK: 'Slowakije',\n        SM: 'San Marino',\n        TH: 'Thailand',\n        TR: 'Turkije',\n        ZA: 'Zuid-Afrika'\n      },\n      country: 'Geef een geldig identificatienummer in uit %s',\n      default: 'Geef een geldig identificatienummer in'\n    },\n    identical: {\n      default: 'Geef dezelfde waarde in'\n    },\n    imei: {\n      default: 'Geef een geldig IMEI-nummer in'\n    },\n    imo: {\n      default: 'Geef een geldig IMO-nummer in'\n    },\n    integer: {\n      default: 'Geef een geldig nummer in'\n    },\n    ip: {\n      default: 'Geef een geldig IP-adres in',\n      ipv4: 'Geef een geldig IPv4-adres in',\n      ipv6: 'Geef een geldig IPv6-adres in'\n    },\n    isbn: {\n      default: 'Geef een geldig ISBN-nummer in'\n    },\n    isin: {\n      default: 'Geef een geldig ISIN-nummer in'\n    },\n    ismn: {\n      default: 'Geef een geldig ISMN-nummer in'\n    },\n    issn: {\n      default: 'Geef een geldig ISSN-nummer in'\n    },\n    lessThan: {\n      default: 'Geef een waarde in die gelijk is aan of kleiner is dan %s',\n      notInclusive: 'Geef een waarde in die kleiner is dan %s'\n    },\n    mac: {\n      default: 'Geef een geldig MAC-adres in'\n    },\n    meid: {\n      default: 'Geef een geldig MEID-nummer in'\n    },\n    notEmpty: {\n      default: 'Geef een waarde in'\n    },\n    numeric: {\n      default: 'Geef een geldig kommagetal in'\n    },\n    phone: {\n      countries: {\n        AE: 'Verenigde Arabische Emiraten',\n        BG: 'Bulgarije',\n        BR: 'Brazilië',\n        CN: 'China',\n        CZ: 'Tsjechische',\n        DE: 'Duitsland',\n        DK: 'Denemarken',\n        ES: 'Spanje',\n        FR: 'Frankrijk',\n        GB: 'Verenigd Koninkrijk',\n        IN: 'Indië',\n        MA: 'Marokko',\n        NL: 'Nederland',\n        PK: 'Pakistan',\n        RO: 'Roemenië',\n        RU: 'Rusland',\n        SK: 'Slowakije',\n        TH: 'Thailand',\n        US: 'VS',\n        VE: 'Venezuela'\n      },\n      country: 'Geef een geldig telefoonnummer in uit %s',\n      default: 'Geef een geldig telefoonnummer in'\n    },\n    promise: {\n      default: 'Geef een geldige waarde in'\n    },\n    regexp: {\n      default: 'Geef een waarde in die overeenkomt met het patroon'\n    },\n    remote: {\n      default: 'Geef een geldige waarde in'\n    },\n    rtn: {\n      default: 'Geef een geldig RTN-nummer in'\n    },\n    sedol: {\n      default: 'Geef een geldig SEDOL-nummer in'\n    },\n    siren: {\n      default: 'Geef een geldig SIREN-nummer in'\n    },\n    siret: {\n      default: 'Geef een geldig SIRET-nummer in'\n    },\n    step: {\n      default: 'Geef een geldig meervoud in van %s'\n    },\n    stringCase: {\n      default: 'Geef enkel kleine letters in',\n      upper: 'Geef enkel hoofdletters in'\n    },\n    stringLength: {\n      between: 'Geef tussen %s en %s karakters in',\n      default: 'Geef een waarde in met de juiste lengte',\n      less: 'Geef minder dan %s karakters in',\n      more: 'Geef meer dan %s karakters in'\n    },\n    uri: {\n      default: 'Geef een geldige URI in'\n    },\n    uuid: {\n      default: 'Geef een geldig UUID-nummer in',\n      version: 'Geef een geldig UUID-nummer (versie %s) in'\n    },\n    vat: {\n      countries: {\n        AT: 'Oostenrijk',\n        BE: 'België',\n        BG: 'Bulgarije',\n        BR: 'Brazilië',\n        CH: 'Zwitserland',\n        CY: 'Cyprus',\n        CZ: 'Tsjechische',\n        DE: 'Duitsland',\n        DK: 'Denemarken',\n        EE: 'Estland',\n        EL: 'Griekenland',\n        ES: 'Spanje',\n        FI: 'Finland',\n        FR: 'Frankrijk',\n        GB: 'Verenigd Koninkrijk',\n        GR: 'Griekenland',\n        HR: 'Kroatië',\n        HU: 'Hongarije',\n        IE: 'Ierland',\n        IS: 'IJsland',\n        IT: 'Italië',\n        LT: 'Litouwen',\n        LU: 'Luxemburg',\n        LV: 'Letland',\n        MT: 'Malta',\n        NL: 'Nederland',\n        NO: 'Noorwegen',\n        PL: 'Polen',\n        PT: 'Portugal',\n        RO: 'Roemenië',\n        RS: 'Servië',\n        RU: 'Rusland',\n        SE: 'Zweden',\n        SI: 'Slovenië',\n        SK: 'Slowakije',\n        VE: 'Venezuela',\n        ZA: 'Zuid-Afrika'\n      },\n      country: 'Geef een geldig BTW-nummer in uit %s',\n      default: 'Geef een geldig BTW-nummer in'\n    },\n    vin: {\n      default: 'Geef een geldig VIN-nummer in'\n    },\n    zipCode: {\n      countries: {\n        AT: 'Oostenrijk',\n        BG: 'Bulgarije',\n        BR: 'Brazilië',\n        CA: 'Canada',\n        CH: 'Zwitserland',\n        CZ: 'Tsjechische',\n        DE: 'Duitsland',\n        DK: 'Denemarken',\n        ES: 'Spanje',\n        FR: 'Frankrijk',\n        GB: 'Verenigd Koninkrijk',\n        IE: 'Ierland',\n        IN: 'Indië',\n        IT: 'Italië',\n        MA: 'Marokko',\n        NL: 'Nederland',\n        PL: 'Polen',\n        PT: 'Portugal',\n        RO: 'Roemenië',\n        RU: 'Rusland',\n        SE: 'Zweden',\n        SG: 'Singapore',\n        SK: 'Slowakije',\n        US: 'VS'\n      },\n      country: 'Geef een geldige postcode in uit %s',\n      default: 'Geef een geldige postcode in'\n    }\n  };\n  return nl_BE;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/locales/nl_BE.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/locales/nl_BE.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});