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

/***/ "./src/libs/@form-validation/umd/locales/ar_MA.js":
/*!********************************************************!*\
  !*** ./src/libs/@form-validation/umd/locales/ar_MA.js ***!
  \********************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  /**\n   * Arabic language package\n   * Translated by @Arkni\n   */\n  var ar_MA = {\n    base64: {\n      default: 'الرجاء إدخال قيمة مشفرة طبقا للقاعدة 64.'\n    },\n    between: {\n      default: 'الرجاء إدخال قيمة بين %s و %s .',\n      notInclusive: 'الرجاء إدخال قيمة بين %s و %s بدقة.'\n    },\n    bic: {\n      default: 'الرجاء إدخال رقم BIC صالح.'\n    },\n    callback: {\n      default: 'الرجاء إدخال قيمة صالحة.'\n    },\n    choice: {\n      between: 'الرجاء إختيار %s-%s خيارات.',\n      default: 'الرجاء إدخال قيمة صالحة.',\n      less: 'الرجاء اختيار %s خيارات كحد أدنى.',\n      more: 'الرجاء اختيار %s خيارات كحد أقصى.'\n    },\n    color: {\n      default: 'الرجاء إدخال رمز لون صالح.'\n    },\n    creditCard: {\n      default: 'الرجاء إدخال رقم بطاقة إئتمان صحيح.'\n    },\n    cusip: {\n      default: 'الرجاء إدخال رقم CUSIP صالح.'\n    },\n    date: {\n      default: 'الرجاء إدخال تاريخ صالح.',\n      max: 'الرجاء إدخال تاريخ قبل %s.',\n      min: 'الرجاء إدخال تاريخ بعد %s.',\n      range: 'الرجاء إدخال تاريخ في المجال %s - %s.'\n    },\n    different: {\n      default: 'الرجاء إدخال قيمة مختلفة.'\n    },\n    digits: {\n      default: 'الرجاء إدخال الأرقام فقط.'\n    },\n    ean: {\n      default: 'الرجاء إدخال رقم EAN صالح.'\n    },\n    ein: {\n      default: 'الرجاء إدخال رقم EIN صالح.'\n    },\n    emailAddress: {\n      default: 'الرجاء إدخال بريد إلكتروني صحيح.'\n    },\n    file: {\n      default: 'الرجاء إختيار ملف صالح.'\n    },\n    greaterThan: {\n      default: 'الرجاء إدخال قيمة أكبر من أو تساوي %s.',\n      notInclusive: 'الرجاء إدخال قيمة أكبر من %s.'\n    },\n    grid: {\n      default: 'الرجاء إدخال رقم GRid صالح.'\n    },\n    hex: {\n      default: 'الرجاء إدخال رقم ست عشري صالح.'\n    },\n    iban: {\n      countries: {\n        AD: 'أندورا',\n        AE: 'الإمارات العربية المتحدة',\n        AL: 'ألبانيا',\n        AO: 'أنغولا',\n        AT: 'النمسا',\n        AZ: 'أذربيجان',\n        BA: 'البوسنة والهرسك',\n        BE: 'بلجيكا',\n        BF: 'بوركينا فاسو',\n        BG: 'بلغاريا',\n        BH: 'البحرين',\n        BI: 'بوروندي',\n        BJ: 'بنين',\n        BR: 'البرازيل',\n        CH: 'سويسرا',\n        CI: 'ساحل العاج',\n        CM: 'الكاميرون',\n        CR: 'كوستاريكا',\n        CV: 'الرأس الأخضر',\n        CY: 'قبرص',\n        CZ: 'التشيك',\n        DE: 'ألمانيا',\n        DK: 'الدنمارك',\n        DO: 'جمهورية الدومينيكان',\n        DZ: 'الجزائر',\n        EE: 'إستونيا',\n        ES: 'إسبانيا',\n        FI: 'فنلندا',\n        FO: 'جزر فارو',\n        FR: 'فرنسا',\n        GB: 'المملكة المتحدة',\n        GE: 'جورجيا',\n        GI: 'جبل طارق',\n        GL: 'جرينلاند',\n        GR: 'اليونان',\n        GT: 'غواتيمالا',\n        HR: 'كرواتيا',\n        HU: 'المجر',\n        IE: 'أيرلندا',\n        IL: 'إسرائيل',\n        IR: 'إيران',\n        IS: 'آيسلندا',\n        IT: 'إيطاليا',\n        JO: 'الأردن',\n        KW: 'الكويت',\n        KZ: 'كازاخستان',\n        LB: 'لبنان',\n        LI: 'ليختنشتاين',\n        LT: 'ليتوانيا',\n        LU: 'لوكسمبورغ',\n        LV: 'لاتفيا',\n        MC: 'موناكو',\n        MD: 'مولدوفا',\n        ME: 'الجبل الأسود',\n        MG: 'مدغشقر',\n        MK: 'جمهورية مقدونيا',\n        ML: 'مالي',\n        MR: 'موريتانيا',\n        MT: 'مالطا',\n        MU: 'موريشيوس',\n        MZ: 'موزمبيق',\n        NL: 'هولندا',\n        NO: 'النرويج',\n        PK: 'باكستان',\n        PL: 'بولندا',\n        PS: 'فلسطين',\n        PT: 'البرتغال',\n        QA: 'قطر',\n        RO: 'رومانيا',\n        RS: 'صربيا',\n        SA: 'المملكة العربية السعودية',\n        SE: 'السويد',\n        SI: 'سلوفينيا',\n        SK: 'سلوفاكيا',\n        SM: 'سان مارينو',\n        SN: 'السنغال',\n        TL: 'تيمور الشرقية',\n        TN: 'تونس',\n        TR: 'تركيا',\n        VG: 'جزر العذراء البريطانية',\n        XK: 'جمهورية كوسوفو'\n      },\n      country: 'الرجاء إدخال رقم IBAN صالح في %s.',\n      default: 'الرجاء إدخال رقم IBAN صالح.'\n    },\n    id: {\n      countries: {\n        BA: 'البوسنة والهرسك',\n        BG: 'بلغاريا',\n        BR: 'البرازيل',\n        CH: 'سويسرا',\n        CL: 'تشيلي',\n        CN: 'الصين',\n        CZ: 'التشيك',\n        DK: 'الدنمارك',\n        EE: 'إستونيا',\n        ES: 'إسبانيا',\n        FI: 'فنلندا',\n        HR: 'كرواتيا',\n        IE: 'أيرلندا',\n        IS: 'آيسلندا',\n        LT: 'ليتوانيا',\n        LV: 'لاتفيا',\n        ME: 'الجبل الأسود',\n        MK: 'جمهورية مقدونيا',\n        NL: 'هولندا',\n        PL: 'بولندا',\n        RO: 'رومانيا',\n        RS: 'صربيا',\n        SE: 'السويد',\n        SI: 'سلوفينيا',\n        SK: 'سلوفاكيا',\n        SM: 'سان مارينو',\n        TH: 'تايلاند',\n        TR: 'تركيا',\n        ZA: 'جنوب أفريقيا'\n      },\n      country: 'الرجاء إدخال رقم تعريف صالح في %s.',\n      default: 'الرجاء إدخال رقم هوية صالحة.'\n    },\n    identical: {\n      default: 'الرجاء إدخال نفس القيمة.'\n    },\n    imei: {\n      default: 'الرجاء إدخال رقم IMEI صالح.'\n    },\n    imo: {\n      default: 'الرجاء إدخال رقم IMO صالح.'\n    },\n    integer: {\n      default: 'الرجاء إدخال رقم صحيح.'\n    },\n    ip: {\n      default: 'الرجاء إدخال عنوان IP صالح.',\n      ipv4: 'الرجاء إدخال عنوان IPv4 صالح.',\n      ipv6: 'الرجاء إدخال عنوان IPv6 صالح.'\n    },\n    isbn: {\n      default: 'الرجاء إدخال رقم ISBN صالح.'\n    },\n    isin: {\n      default: 'الرجاء إدخال رقم ISIN صالح.'\n    },\n    ismn: {\n      default: 'الرجاء إدخال رقم ISMN صالح.'\n    },\n    issn: {\n      default: 'الرجاء إدخال رقم ISSN صالح.'\n    },\n    lessThan: {\n      default: 'الرجاء إدخال قيمة أصغر من أو تساوي %s.',\n      notInclusive: 'الرجاء إدخال قيمة أصغر من %s.'\n    },\n    mac: {\n      default: 'يرجى إدخال عنوان MAC صالح.'\n    },\n    meid: {\n      default: 'الرجاء إدخال رقم MEID صالح.'\n    },\n    notEmpty: {\n      default: 'الرجاء إدخال قيمة.'\n    },\n    numeric: {\n      default: 'الرجاء إدخال عدد عشري صالح.'\n    },\n    phone: {\n      countries: {\n        AE: 'الإمارات العربية المتحدة',\n        BG: 'بلغاريا',\n        BR: 'البرازيل',\n        CN: 'الصين',\n        CZ: 'التشيك',\n        DE: 'ألمانيا',\n        DK: 'الدنمارك',\n        ES: 'إسبانيا',\n        FR: 'فرنسا',\n        GB: 'المملكة المتحدة',\n        IN: 'الهند',\n        MA: 'المغرب',\n        NL: 'هولندا',\n        PK: 'باكستان',\n        RO: 'رومانيا',\n        RU: 'روسيا',\n        SK: 'سلوفاكيا',\n        TH: 'تايلاند',\n        US: 'الولايات المتحدة',\n        VE: 'فنزويلا'\n      },\n      country: 'الرجاء إدخال رقم هاتف صالح في %s.',\n      default: 'الرجاء إدخال رقم هاتف صحيح.'\n    },\n    promise: {\n      default: 'الرجاء إدخال قيمة صالحة.'\n    },\n    regexp: {\n      default: 'الرجاء إدخال قيمة مطابقة للنمط.'\n    },\n    remote: {\n      default: 'الرجاء إدخال قيمة صالحة.'\n    },\n    rtn: {\n      default: 'الرجاء إدخال رقم RTN صالح.'\n    },\n    sedol: {\n      default: 'الرجاء إدخال رقم SEDOL صالح.'\n    },\n    siren: {\n      default: 'الرجاء إدخال رقم SIREN صالح.'\n    },\n    siret: {\n      default: 'الرجاء إدخال رقم SIRET صالح.'\n    },\n    step: {\n      default: 'الرجاء إدخال قيمة من مضاعفات %s .'\n    },\n    stringCase: {\n      default: 'الرجاء إدخال أحرف صغيرة فقط.',\n      upper: 'الرجاء إدخال أحرف كبيرة فقط.'\n    },\n    stringLength: {\n      between: 'الرجاء إدخال قيمة ذات عدد حروف بين %s و %s حرفا.',\n      default: 'الرجاء إدخال قيمة ذات طول صحيح.',\n      less: 'الرجاء إدخال أقل من %s حرفا.',\n      more: 'الرجاء إدخال أكتر من %s حرفا.'\n    },\n    uri: {\n      default: 'الرجاء إدخال URI صالح.'\n    },\n    uuid: {\n      default: 'الرجاء إدخال رقم UUID صالح.',\n      version: 'الرجاء إدخال رقم UUID صالح إصدار %s.'\n    },\n    vat: {\n      countries: {\n        AT: 'النمسا',\n        BE: 'بلجيكا',\n        BG: 'بلغاريا',\n        BR: 'البرازيل',\n        CH: 'سويسرا',\n        CY: 'قبرص',\n        CZ: 'التشيك',\n        DE: 'جورجيا',\n        DK: 'الدنمارك',\n        EE: 'إستونيا',\n        EL: 'اليونان',\n        ES: 'إسبانيا',\n        FI: 'فنلندا',\n        FR: 'فرنسا',\n        GB: 'المملكة المتحدة',\n        GR: 'اليونان',\n        HR: 'كرواتيا',\n        HU: 'المجر',\n        IE: 'أيرلندا',\n        IS: 'آيسلندا',\n        IT: 'إيطاليا',\n        LT: 'ليتوانيا',\n        LU: 'لوكسمبورغ',\n        LV: 'لاتفيا',\n        MT: 'مالطا',\n        NL: 'هولندا',\n        NO: 'النرويج',\n        PL: 'بولندا',\n        PT: 'البرتغال',\n        RO: 'رومانيا',\n        RS: 'صربيا',\n        RU: 'روسيا',\n        SE: 'السويد',\n        SI: 'سلوفينيا',\n        SK: 'سلوفاكيا',\n        VE: 'فنزويلا',\n        ZA: 'جنوب أفريقيا'\n      },\n      country: 'الرجاء إدخال رقم VAT صالح في %s.',\n      default: 'الرجاء إدخال رقم VAT صالح.'\n    },\n    vin: {\n      default: 'الرجاء إدخال رقم VIN صالح.'\n    },\n    zipCode: {\n      countries: {\n        AT: 'النمسا',\n        BG: 'بلغاريا',\n        BR: 'البرازيل',\n        CA: 'كندا',\n        CH: 'سويسرا',\n        CZ: 'التشيك',\n        DE: 'ألمانيا',\n        DK: 'الدنمارك',\n        ES: 'إسبانيا',\n        FR: 'فرنسا',\n        GB: 'المملكة المتحدة',\n        IE: 'أيرلندا',\n        IN: 'الهند',\n        IT: 'إيطاليا',\n        MA: 'المغرب',\n        NL: 'هولندا',\n        PL: 'بولندا',\n        PT: 'البرتغال',\n        RO: 'رومانيا',\n        RU: 'روسيا',\n        SE: 'السويد',\n        SG: 'سنغافورة',\n        SK: 'سلوفاكيا',\n        US: 'الولايات المتحدة'\n      },\n      country: 'الرجاء إدخال رمز بريدي صالح في %s.',\n      default: 'الرجاء إدخال رمز بريدي صالح.'\n    }\n  };\n  return ar_MA;\n});\n\n//# sourceURL=webpack://Vuexy/./src/libs/@form-validation/umd/locales/ar_MA.js?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/libs/@form-validation/umd/locales/ar_MA.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});