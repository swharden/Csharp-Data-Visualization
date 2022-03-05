:: create large PNGs with transparent backgrounds
convert -background transparent favicon.svg -resize 512x512 favicon-512.png
convert -background transparent favicon.svg -resize 256x256 favicon-256.png
convert -background transparent favicon.svg -resize 192x192 favicon-192.png
convert -background transparent favicon.svg -resize 180x180 favicon-180.png

:: resize the transparent PNG to individual sizes
convert favicon-256.png -resize 16x16 favicon-16.png
convert favicon-256.png -resize 32x32 favicon-32.png
convert favicon-256.png -resize 64x64 favicon-64.png
convert favicon-256.png -resize 128x128 favicon-128.png

:: create a small and simple 32px icon file
convert favicon-32.png -colors 256 favicon-32.ico

:: pack individual sizes into a single icon file
convert favicon-16.png favicon-32.png favicon-64.png favicon-128.png favicon-256.png -colors 256 favicon.ico

pause