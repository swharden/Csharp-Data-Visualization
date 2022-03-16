---
title: Plot Data with Infragistics Chart
description: todo
date: 3333-01-01
---

Infragistics has [charting tools for a variety of platforms](https://www.infragistics.com/products/ultimate) including Windows Forms and WPF. 

<div align="center">

![](graphics/infragistics-line-graph.png)

</div>

However as a company they seem to actively develop a wide variety of controls for many frameworks (including web platforms), and that diffusion of effort leaves them with a charting library that is interesting and looks nice, but isn't really the type of thing I'd want to embed into a scientific desktop application.

### Price

According to their [pricing page](https://www.infragistics.com/how-to-buy/product-pricing) they have two plans, but it's oddly devoid of information about what distinguishes them. The only thing I can gather is, the ultimate version comes with "unified platform for prototyping, usability testing, and code gen"... what is that?

* Professional: $1,500 / year
* Ultimate: $2,000 / year

I found prices for just platform controls:

* Windows Forms: $900 / year
* WPF: $900 / year

## Demo

Infragistics has a [Windows Forms Reference Application](https://www.infragistics.com/products/ultimate#reference-apps) you can use to check out their controls. It's huge (351 MB) and contains a ZIP of a MSI you must install before you can use. When it launches it tries to get you to review a modern-UI sample application, and you have to drill down into their legacy demo to access details about their charting control.

![](graphics/infragistics-ultraWinChart.png)

I didn't find any of these charts to be particularly performant or interactive, so after a quick flip through these examples I was happy to put this project down and move on. I'm sure it's great for data grids and UI-focused application development, but as a scientist trying to graph data that's just not what I'm looking for.

## Conclusions

The charting demos work, but they're not particularly interactive, and clearly not the primary focus of this development team. This seems to be a miscellaneous control product, not really one that focuses specifically on charting. 