'use strict';

angular.module('token-auth.version', [
    'token-auth.version.interpolate-filter',
    'token-auth.version.version-directive'
])

.value('version', '0.1');