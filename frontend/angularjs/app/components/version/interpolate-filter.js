'use strict';

angular.module('token-auth.version.interpolate-filter', [])

.filter('interpolate', ['version', function(version) {
    return function(text) {
        return String(text).replace(/\%VERSION\%/mg, version);
    };
}]);