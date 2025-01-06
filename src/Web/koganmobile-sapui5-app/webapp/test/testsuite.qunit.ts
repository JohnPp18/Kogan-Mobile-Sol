export default {
	name: "QUnit test suite for the UI5 Application: kogan.mobile",
	defaults: {
		page: "ui5://test-resources/kogan/mobile/Test.qunit.html?testsuite={suite}&test={name}",
		qunit: {
			version: 2
		},
		sinon: {
			version: 1
		},
		ui5: {
			language: "EN",
			theme: "sap_horizon"
		},
		coverage: {
			only: "kogan/mobile/",
			never: "test-resources/kogan/mobile/"
		},
		loader: {
			paths: {
				"kogan/mobile": "../"
			}
		}
	},
	tests: {
		"unit/unitTests": {
			title: "Unit tests for kogan.mobile"
		},
		"integration/opaTests": {
			title: "Integration tests for kogan.mobile"
		}
	}
};
