
class Star {
    constructor(x, y, xVel, yVel) {
        this.x = x;
        this.y = y;
        this.xVel = xVel;
        this.yVel = yVel;
    }
}

class PointField {

    constructor(div) {
        this.div = div;
        this.canvas = document.createElement('canvas');
        this.canvas.style.width = "100%";
        this.div.appendChild(this.canvas);
        this.resize();
        this.randomize();
    }

    resize() {
        this.width = this.div.clientWidth;
        this.height = this.div.clientHeight;
        if (this.canvas != null) {
            this.canvas.width = this.div.clientWidth;
            this.canvas.height = this.div.clientHeight;
        }
    }

    randomize() {
        const starCount = this.width * this.height / 5e3;
        const minVel = 15;
        const maxVel = 30;

        this.stars = []
        for (let i = 0; i < starCount; i++) {

            let x = Math.random() * this.width;
            let y = Math.random() * this.height;
            let yVel = (Math.random() * (maxVel - minVel)) + minVel
            let xVel = (Math.random() * (maxVel - minVel)) + minVel
            if (Math.random() < .5) yVel *= -1;
            if (Math.random() < .5) xVel *= -1;

            this.stars[i] = new Star(x, y, xVel, yVel)
        }

        this.renderCount = 0;
    }

    stepForward(dt = .01) {
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
        const ctx = this.canvas.getContext("2d");

        ctx.fillStyle = this.div.style.backgroundColor;
        ctx.fillRect(0, 0, this.width, this.height);

        ctx.lineWidth = 1;
        for (let indexA = 0; indexA < this.stars.length; indexA++) {
            const starA = this.stars[indexA];
            let brightestLine = 0;
            for (let indexB = 0; indexB < this.stars.length; indexB++) {
                if (indexA == indexB)
                    continue;
                const starB = this.stars[indexB];

                const dX = Math.abs(starB.x - starA.x);
                const dY = Math.abs(starB.y - starA.y);
                const distance = Math.sqrt(dX * dX + dY * dY);

                if (distance > 100)
                    continue;

                let alpha = (100 - distance) / 100.0;
                alpha = Math.min(alpha, this.renderCount / 50);
                brightestLine = Math.max(alpha, brightestLine);

                ctx.strokeStyle = `rgba(255,255,255,${.05 * alpha})`;
                ctx.beginPath();
                ctx.moveTo(starA.x, starA.y);
                ctx.lineTo(starB.x, starB.y);
                ctx.stroke();
            }
            ctx.fillStyle = `rgba(255,255,255,${.2 * brightestLine})`;
            ctx.beginPath();
            ctx.arc(starA.x, starA.y, 2, 0, Math.PI * 2);
            ctx.fill();
        }
        this.renderCount += 1;
    }
}

function startAnimation() {
    let fps = 10;
    let myField = new PointField(document.getElementById('canvas'));
    window.addEventListener('resize', function resize(event) { myField.resize(); myField.randomize(); myField.render(); });
    setInterval(function () { myField.stepForward(); }, 1000 / fps);
}

function drawOnce() {
    let myField = new PointField(document.getElementById('canvas'));
    myField.resize();
    myField.randomize();
    myField.renderCount = 999;
    myField.render();
}

/*
<body onload="drawOnce();">

    <header class="animated-header">
        <div id="canvas" style="background-color: #002440;"></div>
        <div class="py-5">
            <div class="container-fluid" style="max-width: 1000px;">
                <div class="display-4 fw-normal"><a href='/csdv'>C# Data Visualization</a></div>
                <div class="" style="opacity: .5;">Resources for visualizing data using C# and the .NET platform</div>
            </div>
        </div>
    </header>
*/

/*

.animated-header {
    display: grid;
    grid-template-columns: 1fr;
    color: white;
}

.animated-header div {
    grid-row-start: 1;
    grid-column-start: 1;
    display: flex;
    flex-direction: column;
}

.animated-header a {
    color: white;
}
*/