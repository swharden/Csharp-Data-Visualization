---
title: Boids in C#
description: A procedural animation demonstrating emergent flocking behavior implemented in C#
date: 2020-05-11
weight: 2
---

**This project implements the [Boids flocking algorithm](https://en.wikipedia.org/wiki/Boids) in C# to create an interesting procedural animation of bird-drones (boids) scurrying about the screen.** This simulation produces complex emergent flocking behavior from a system where individual boids follow a simple set of rules. Code shown on this page uses `System.Drawing` in a Windows Forms application, but source is available for an OpenGL-accelerated version using SkiaSharp.

<div class="text-center">
<img src="csharp-boids.gif">
</div>

### Primary Rules

The Boids algorithm was created by [Craig Reynolds](https://www.red3d.com/cwr/boids/) in 1986 and is a term used to describe "bird-oid objects". In this simulation complex emergent behavior comes from simple rules:

* **Rule 1:** boids steer toward the center of mass of nearby boids
* **Rule 2:** boids adjust direction to match nearby boids
* **Rule 3:** boids steer away from very close boids

### Optional Rules

You can get fancy and apply additional rules to create even more complex and interesting behavior. In my example program I added 3 additional rules:

* **Rule 4:** boids speed up or slow down to match a target speed
* **Rule 5:** boids are repelled by the edge of the box
* **Rule 6:** boids steer away from boids marked as predators

## Boids Model Code

### Strategy

The velocity of voids is controlled by two variables, `Xvel` and `Yvel`. It's worth noting that trig functions can be used to convert these values to heading (in degrees) and speed (pixels per iteration), but this is typically not required.

All rules "steer" boids (adjusting their heading and speed) by acting on their X and Y velocities. Rules never move boids. After the application of all the rules, the position of each boid (`X` and `Y`) is moved by its velocity (`Xvel` and `Yvel`).

Each rule is given a `distance` that describes how far away it can act. Avoidance only acts on close boids, while flocking distances are much greater. Similarly, each rule is given a weight (termed `power` in this code) that describes how much it influences the final velocity. Typically rules with a larger distance have a smaller weight. Flocking weight is less than predator avoidance weight.

#### The `Boid` Class

Some helper functions have been omitted, but this is the gist of the `Boid` class. This class only stores position and velocity of one boid, and any information about the outside world must be passed-in.

```cs
public class Boid
{
    public double X;
    public double Y;
    public double Xvel;
    public double Yvel;

    public Boid(double x, double y, double xVel, double yVel)
    {
        (X, Y, Xvel, Yvel) = (x, y, xVel, yVel);
    }
}
```

#### The `Field` Class

The `Field` class contains a `List` of `Boid` objects and is responsible for applying the rules to each `Boid`. It is instantiated with a set of dimensions and a number of initial boids, and random boids (with random positions and velocities) are placed upon instantiation.

```cs
public readonly double Width;
public readonly double Height;
public readonly List<Boid> Boids = new List<Boid>();
private readonly Random Rand = new Random();

public Field(double width, double height, int boidCount = 100)
{
    (Width, Height) = (width, height);
    for (int i = 0; i < boidCount; i++)
        Boids.Add(new Boid(Rand, width, height));
}
```

#### Model Advancement

This method applies all the rules and advances the boids model in time. Distances and weights for each rule are defined in arguments.

```cs
public void Advance(bool bounceOffWalls = true, bool wrapAroundEdges = false)
{
    // update void speed and direction (velocity) based on rules
    foreach (var boid in Boids)
    {
        (double flockXvel, double flockYvel) = Flock(boid, 50, .0003);
        (double alignXvel, double alignYvel) = Align(boid, 50, .01);
        (double avoidXvel, double avoidYvel) = Avoid(boid, 20, .001);
        (double predXvel, double predYval) = Predator(boid, 150, .00005);
        boid.Xvel += flockXvel + avoidXvel + alignXvel + predXvel;
        boid.Yvel += flockYvel + avoidYvel + alignYvel + predYval;
    }

    // move all boids forward in time
    foreach (var boid in Boids)
    {
        boid.MoveForward();
        if (bounceOffWalls)
            BounceOffWalls(boid);
        if (wrapAroundEdges)
            WrapAround(boid);
    }
}
```

#### Rule 1: Steer Toward Center of Mass of Nearby Boids
Return the velocity adjustment needed to point toward the center of the flock (mean flock boid position). Notice that we define a flock (and neighbors) as boids within the given distance.

```cs
private (double xVel, double yVel) Flock(Boid boid, double distance, double power)
{
    var neighbors = Boids.Where(x => x.GetDistance(boid) < distance);
    double meanX = neighbors.Sum(x => x.X) / neighbors.Count();
    double meanY = neighbors.Sum(x => x.Y) / neighbors.Count();
    double deltaCenterX = meanX - boid.X;
    double deltaCenterY = meanY - boid.Y;
    return (deltaCenterX * power, deltaCenterY * power);
}
```

#### Rule 2: Mimic Direction and Speed of Nearby Boids

Return the velocity adjustment needed to approach the mean speed and direction of nearby boids.

```cs
private (double xVel, double yVel) Align(Boid boid, double distance, double power)
{
    var neighbors = Boids.Where(x => x.GetDistance(boid) < distance);
    double meanXvel = neighbors.Sum(x => x.Xvel) / neighbors.Count();
    double meanYvel = neighbors.Sum(x => x.Yvel) / neighbors.Count();
    double dXvel = meanXvel - boid.Xvel;
    double dYvel = meanYvel - boid.Yvel;
    return (dXvel * power, dYvel * power);
}
```

#### Rule 3: Steer Away from Extremely Close Boids

Return the velocity adjustment needed to avoid very close boids. This method doesn't use the center of the close flock, but instead summates the "closeness" of all close birds to generate the velocities.

```cs
private (double xVel, double yVel) Avoid(Boid boid, double distance, double power)
{
    var neighbors = Boids.Where(x => x.GetDistance(boid) < distance);
    (double sumClosenessX, double sumClosenessY) = (0, 0);
    foreach (var neighbor in neighbors)
    {
        double closeness = distance - boid.GetDistance(neighbor);
        sumClosenessX += (boid.X - neighbor.X) * closeness;
        sumClosenessY += (boid.Y - neighbor.Y) * closeness;
    }
    return (sumClosenessX * power, sumClosenessY * power);
}
```

#### Rule 4: Speed Limit

After the first three rules are applied, the new velocity is calculated for each boid. An operation can then be performed to scale these velocities (keeping their ratio the same) to adjust speed. I accomplish this inside the advancement method in the `Boid` class. 

Notice the `IsNan` method has to be used to accommodate cases where speed is zero so as not to break the trig functions which calculate heading later.

```cs
public void MoveForward(double minSpeed = 1, double maxSpeed = 5)
{
    X += Xvel;
    Y += Yvel;

    var speed = GetSpeed();
    if (speed > maxSpeed)
    {
        Xvel = (Xvel / speed) * maxSpeed;
        Yvel = (Yvel / speed) * maxSpeed;
    }
    else if (speed < minSpeed)
    {
        Xvel = (Xvel / speed) * minSpeed;
        Yvel = (Yvel / speed) * minSpeed;
    }

    if (double.IsNaN(Xvel))
        Xvel = 0;
    if (double.IsNaN(Yvel))
        Yvel = 0;
}
```

#### Rule 5: Avoid Edges

This code accelerates boids away from walls with each iteration. Originally it just slows them down as they approach, but with more time they reverse course and travel away from the edge. This method is safe to use with fast boids that may travel off the screen for a brief period of time.

```cs
private void BounceOffWalls(Boid boid)
{
    double pad = 50;
    double turn = .5;
    if (boid.X < pad)
        boid.Xvel += turn;
    if (boid.X > Width - pad)
        boid.Xvel -= turn;
    if (boid.Y < pad)
        boid.Yvel += turn;
    if (boid.Y > Height - pad)
        boid.Yvel -= turn;
}
```

#### Alternate Rule 5: Wrap the Universe

This code isn't used in my example, but it could be used instead of the "avoid edges" method above. In this method boids that fall off the screen on one edge reappear on the opposite edge.

```cs
private void WrapAround(Boid boid)
{
    if (boid.X < 0)
        boid.X += Width;
    if (boid.X > Width)
        boid.X -= Width;
    if (boid.Y < 0)
        boid.Y += Height;
    if (boid.Y > Height)
        boid.Y -= Height;
}
```

#### Rule 6: Avoid Predators

Return the velocity adjustment needed to steer away from predators. In this example predators are simply defined as the first N boids using a class-level variable. Similar to the earlier boid avoidance method, this one summates avoidances based on each predator's position instead of responding to the mean position of all predators.

```cs
public int PredatorCount = 3;
private (double xVel, double yVel) Predator(Boid boid, double distance, double power)
{
    (double sumClosenessX, double sumClosenessY) = (0, 0);
    for (int i = 0; i < PredatorCount; i++)
    {
        Boid predator = Boids[i];
        double distanceAway = boid.GetDistance(predator);
        if (distanceAway < distance)
        {
            double closeness = distance - distanceAway;
            sumClosenessX += (boid.X - predator.X) * closeness;
            sumClosenessY += (boid.Y - predator.Y) * closeness;
        }
    }
    return (sumClosenessX * power, sumClosenessY * power);
}
```


## Rendering the Boids Model

### Graphics Transformation and Rotation

The outline of a boid is defined as a `Point[]` array. Instead of rotating the points to match the direction of each boid, the entire _canvas_ is rotated around the boid, then the boid is drawn right-side-up. This method greatly simplifies the act of drawing rotated shapes with System.Drawing.

```cs
private void RenderBoid(Graphics gfx, Boid boid)
{
    // drawing of a boid centered at (0, 0)
    var boidOutline = new Point[]
    {
        new Point(0, 0),
        new Point(-5, -1),
        new Point(0, 10),
        new Point(5, -1),
        new Point(0, 0),
    };

    using (var brush = new SolidBrush(Color.LightGreen))
    {
        // translate and rotate the canvas around the boid
        gfx.TranslateTransform((float)boid.X, (float)boid.Y);
        gfx.RotateTransform((float)boid.GetAngle());

        // draw the boid at (0, 0)
        gfx.FillClosedCurve(brush, boidOutline);

        // reset before drawing the next object
        gfx.ResetTransform();
    }
}
```

Rendering is triggered using a timer set to 1 ms. The first 3 boids are predators so they are colored differently.

```cs
Field field = new Field(pictureBox1.Width, pictureBox1.Height, 100);
private void timer1_Tick(object sender, EventArgs e)
{
    field.Advance();
    pictureBox1.Image?.Dispose();
    pictureBox1.Image = RenderField(field);
}

public static Bitmap RenderField(Field field)
{
    Bitmap bmp = new Bitmap((int)field.Width, (int)field.Height);
    using (Graphics gfx = Graphics.FromImage(bmp))
    {
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        gfx.Clear(ColorTranslator.FromHtml("#003366"));
        for (int i = 0; i < field.Boids.Count(); i++)
        {
            if (i < 3)
                RenderBoid(gfx, field.Boids[i], Color.White);
            else
                RenderBoid(gfx, field.Boids[i], Color.LightGreen);
        }
    }
    return bmp;
}
```


## Resources

#### Boids Simulators (JavaScript)
* [Boids algorithm demonstration](https://eater.net/boids) by Ben Eater (featured in SmarterEveryDay's [YouTube Video](https://youtu.be/4LWmRuB-uNU?t=187) about flocking birds)
* [Boids: Flocking made simple](http://www.harmendeweerd.nl/boids/) by Harmen de Weerd
* [Flocking](https://processing.org/examples/flocking.html) by Daniel Shiffman
* [Flocking Simulation](http://www.emergentmind.com/boids) by Matt Mazur
* [Simulate How Birds Flock in Processing](https://medium.com/swlh/boids-a-simple-way-to-simulate-how-birds-flock-in-processing-69057930c229) by Takuma Kakehi

#### Literature
* [Boids: Background and Update](https://www.red3d.com/cwr/boids/) by Craig Reynolds (the inventor of boids)
* [Boids on Wikipedia](https://en.wikipedia.org/wiki/Boids)
* [3 Simple Rules of Flocking Behaviors: Alignment, Cohesion, and Separation](https://gamedevelopment.tutsplus.com/tutorials/3-simple-rules-of-flocking-behaviors-alignment-cohesion-and-separation--gamedev-3444)
* [Boids Code Golf](https://codegolf.stackexchange.com/questions/154277/implement-the-boids-algorithm)

## Source Code

### System.Drawing Version
* GitHub: [source code](https://github.com/swharden/Csharp-Data-Visualization/tree/main/dev/old/drawing/boids)

### OpenGL Version
* Download: [boids.zip](boids.zip)
* GitHub: [source code](https://github.com/swharden/Csharp-Data-Visualization/tree/main/dev/old/drawing/boids2)

<img src="Boids-Csharp-OpenGL.png">