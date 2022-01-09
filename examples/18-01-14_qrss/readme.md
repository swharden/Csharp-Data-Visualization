# QRSS Spectrograph in C#
**This project is a barebones high resolution spectrograph suitable for QRSS reception.** It is not optimized for speed, but it still runs very well. It is written with simplicity in mind, and is intended to be an educational tool. This project would be an excellent starting point for anybody interested in creating an actual QRSS viewer (adding features like scale bars, FTP upload, stitching, etc). The largest difference between this project and others available is the abity to _vertically scroll_ the spectrograph. This allows you to create output images thousands of pixels high (useful for experimental reception of bandwidth beyond a couple hundred Hz). [Compiled binaries](binaries.zip) have been made available for people interested in testing out the spectrograph.

## Screenshots
_The default spectrograph settings are configured for highspeed music visualization. Modify these values to reflect those used in the screenshots if you intend to visualize QRSS signals..._

Description | Screenshot | Output
---|---|---
**Music:** several songs in a row played one after the next|![](screenshot_music.png)|<img src="capture_music.jpg" height=400>
**QRSS:** several minutes of relatively poor quality radio reception at 10.140 MHz I recorded many years ago|![](screenshot_qrss.png)|<img src="capture_qrss.jpg" height=400>

## Useful Links
* [What is QRSS?](http://www.qsl.net/m0ayf/What-is-QRSS.html)
* [QRP Labs QRSS transmitter kit](https://www.qrp-labs.com/qrsskitmm.html)
* [Knights QRSS mailing list](https://groups.io/g/qrssknights) - a very active mailing list for worldwide QRSS experiments

## Similar Software
* QRSSVD ([website](https://www.swharden.com/wp/qrss-vd-the-free-open-source-cross-platform-qrss-spectrograph-by-aj4vd/),  [github](https://github.com/swharden/QRSS-VD)) - open-source cross-platform spectrograph (Python)
* Argo ([website](http://digilander.libero.it/i2phd/argo/)) - closed-source QRSS viewer for Windows
* SpectrumLab ([website](http://www.qsl.net/dl4yhf/spectra1.html)) - closed-source spectrum analyzer for Windows 
* QrssPIG ([github](https://github.com/MartinHerren/QrssPiG)) - open-source qrss viewer for Raspberry Pi (C++)
* Lopora ([website](http://www.qsl.net/pa2ohh/11lop.htm))- cross-platform open-source QRSS viewer (Python) 
