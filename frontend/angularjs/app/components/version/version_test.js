'use strict';

describe('token-auth.version module', function() {
    beforeEach(module('token-auth.version'));

    describe('version service', function() {
        it('should return current version', inject(function(version) {
            expect(version).toEqual('0.1');
        }));
    });
});