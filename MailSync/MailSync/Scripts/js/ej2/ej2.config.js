﻿System.config({
	baseURL: "/",
	transpiler: "typescript",
	typescriptOptions: {
		compilerOptions: {
			target: "umd",
			module: "commonjs",
			moduleResolution: "node",
			emitDecoratorMetadata: true,
			experimentalDecorators: true
		}
	},
	paths: {
        "syncfusion:": 'scripts/js/EJ2/',
	},
	map: {
		typescript: "https://unpkg.com/typescript@2.2.2/lib/typescript.js",
		"@syncfusion/ej2-base": "syncfusion:ej2-base.umd.min.js",
		"@syncfusion/ej2-data": "syncfusion:ej2-data.umd.min.js",
		"@syncfusion/ej2-buttons": "syncfusion:ej2-buttons.umd.min.js",
		"@syncfusion/ej2-popups": "syncfusion:ej2-popups.umd.min.js",
		"@syncfusion/ej2-grids": "syncfusion:ej2-grids.umd.min.js",
		"@syncfusion/ej2-navigations": "syncfusion:ej2-navigations.umd.min.js",
		"@syncfusion/ej2-calendars": "syncfusion:ej2-calendars.umd.min.js",
		"@syncfusion/ej2-inputs": "syncfusion:ej2-inputs.umd.min.js",
		"@syncfusion/ej2-lists": "syncfusion:ej2-lists.umd.min.js",
		"@syncfusion/ej2-pdf": "syncfusion:ej2-pdf.umd.min.js",
		"@syncfusion/ej2-dropdowns": "syncfusion:ej2-dropdowns.umd.min.js",
		"@syncfusion/ej2-excel-export": "syncfusion:ej2-excel-export.umd.min.js",
		"@syncfusion/ej2-pdf-export": "syncfusion:ej2-pdf-export.umd.min.js",
		"@syncfusion/ej2-compression": "syncfusion:ej2-compression.umd.min.js",
		"@syncfusion/ej2-file-utils": "syncfusion:ej2-file-utils.umd.min.js"
	}
});

System.import('scripts/js/ts/Release/Release.ts').catch(console.error.bind(console));