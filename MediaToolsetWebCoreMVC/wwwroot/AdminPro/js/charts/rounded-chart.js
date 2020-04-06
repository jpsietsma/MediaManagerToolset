(function ($) {
 "use strict";
 
	/*  E: Storage Pie Chart */
	var ctx = document.getElementById("piechart-e");
	var piechart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: ["Free Space", "TV Shows"],
			datasets: [{
				label: 'pie Chart',
                backgroundColor: [
					'#03a9f4',
					'#303030'
				],
				data: [725, 1322]
            }]
		},
		options: {
			responsive: true
		}
	});


	/*  F: Storage Pie Chart */
	var ctx = document.getElementById("piechart-f");
	var piechart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: ["Free Space", "TV Shows"],
			datasets: [{
			label: 'Storage Drive F:',
				backgroundColor: [
					'#03a9f4',
					'#303030'
				],
				data: [725, 1322]
			}]
		},
		options: {
			responsive: true
		}
	});

	/*  G: Storage Pie Chart */
	var ctx = document.getElementById("piechart-g");
	var piechart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: ["Free Space", "TV Shows"],
			datasets: [{
				label: 'Storage Drive F:',
				backgroundColor: [
					'#03a9f4',
					'#303030'
				],
				data: [725, 1322]
			}]
		},
		options: {
			responsive: true
		}
	});

	/*  H: Storage Pie Chart */
	var ctx = document.getElementById("piechart-h");
	var piechart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: ["Free Space", "TV Shows"],
			datasets: [{
				label: 'Storage Drive F:',
				backgroundColor: [
					'#03a9f4',
					'#303030'
				],
				data: [725, 1322]
			}]
		},
		options: {
			responsive: true
		}
	});

	/*  I: Storage Pie Chart */
	var ctx = document.getElementById("piechart-i");
	var piechart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: ["Free Space", "TV Shows"],
			datasets: [{
				label: 'Storage Drive F:',
				backgroundColor: [
					'#03a9f4',
					'#303030'
				],
				data: [725, 1322]
			}]
		},
		options: {
			responsive: true
		}
	});

	/*  M: Storage Pie Chart */
	var ctx = document.getElementById("piechart-m");
	var piechart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: ["Free Space", "Movies"],
			datasets: [{
				label: 'Storage Drive F:',
				backgroundColor: [
					'#03a9f4',
					'#303030'
				],
				data: [725, 1322]
			}]
		},
		options: {
			responsive: true
		}
	});

	/*  S: Storage Pie Chart */
	var ctx = document.getElementById("piechart-s");
	var piechart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: ["<br>Free Space", "Sort Files"],
			datasets: [{
				label: 'Storage Drive F:',
				backgroundColor: [
					'#03a9f4',
					'#303030'
				],
				data: [725, 1322]
			}]
		},
		options: {
			responsive: true
		}
	});
	
})(jQuery); 