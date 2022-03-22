/*

constellation.js - similar effect to to https://vincentgarreau.com/particles.js/

Give the header the id "backSplash" and start the script in your Body:
<body class="bg-light" onload="startAnimation('backSplash', '#002440', 10);">

*/
class PointField {

	constructor(div, backgroundColor) {
		this.div = div;
		this.backgroundColor = backgroundColor;
		this.canvas = document.createElement('canvas');
		this.div.appendChild(this.canvas);
		this.resize();
		this.randomize();
	}

	randomize() {
		let starCount = this.width * this.height / 5e3;
		this.stars = GetRandomStars(this, starCount);
		this.renderCount = 0;
	}

	resize() {
		this.width = this.div.clientWidth;
		this.height = this.div.clientHeight;
		if (this.canvas != null) {
			this.canvas.width = this.div.clientWidth;
			this.canvas.height = this.div.clientWidth;
		}
		this.randomize();
	}

	stepForward(dt = .005) {
		for (var i = 0; i < this.stars.length; i++) {
			let star = this.stars[i];
			star.x += dt * star.xVel;
			star.y += dt * star.yVel;
			if (star.x > this.width || star.x < 0)
				star.xVel *= -1;
			if (star.y > this.height || star.y < 0)
				star.yVel *= -1;
		}

		this.render();
	}

	render() {

		let ctx = this.canvas.getContext("2d");

		ctx.fillStyle = this.backgroundColor;
		ctx.fillRect(0, 0, this.width, this.height);

		ctx.lineWidth = 1;
		for (let indexA = 0; indexA < this.stars.length; indexA++) {
			let starA = this.stars[indexA];
			let brightestLine = 0;
			for (let indexB = 0; indexB < this.stars.length; indexB++) {
				if (indexA == indexB)
					continue;
				let starB = this.stars[indexB];
				let distance = this.distance(starA, starB);
				if (distance > 100)
					continue;

				let alpha = (100 - distance) / 100.0 * .2;

				// start with total transparency and fade-in opacity
				alpha = Math.min(alpha, this.renderCount / 100) * .7;
				brightestLine = Math.max(alpha, brightestLine);

				ctx.strokeStyle = `rgba(255,255,255,${.3 * alpha})`;
				ctx.beginPath();
				ctx.moveTo(starA.x, starA.y);
				ctx.lineTo(starB.x, starB.y);
				ctx.stroke();
			}

			ctx.fillStyle = `rgba(255,255,255,${brightestLine})`;
			ctx.beginPath();
			ctx.arc(starA.x, starA.y, 2, 0, Math.PI * 2);
			ctx.fill();
		}
		this.renderCount += 1;
	}

	distance(starA, starB) {
		let dX = Math.abs(starB.x - starA.x);
		let dY = Math.abs(starB.y - starA.y);
		return Math.sqrt(dX * dX + dY * dY);
	}
}

function Star(x, y, xVel, yVel) {
	this.x = x;
	this.y = y;
	this.xVel = xVel;
	this.yVel = yVel;
}

function GetRandomStar(field, minVel = 15, maxVel = 30) {
	let x = Math.random() * field.width;
	let y = Math.random() * field.height;
	let yVel = (Math.random() * (maxVel - minVel)) + minVel
	let xVel = (Math.random() * (maxVel - minVel)) + minVel
	if (Math.random() < .5) yVel *= -1;
	if (Math.random() < .5) xVel *= -1;
	let star = new Star(x, y, xVel, yVel)
	return star;
}

function GetRandomStars(field, starCount = 100) {
	let stars = []
	for (let i = 0; i < starCount; i++)
		stars[i] = GetRandomStar(field);
	return stars;
}

function startAnimation(divID, backgroundColor = '#000000', fps = 10) {

	var myField = new PointField(document.getElementById(divID), backgroundColor);

	myField.stepForward();

	window.addEventListener('resize', function resize(event) {
		myField.resize();
		myField.stepForward();
	});

	setInterval(function () { myField.stepForward(); }, 1000 / fps);
}