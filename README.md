# Dat2dxf Converter

A lightweight, standalone Windows utility designed for aerodynamicists and RC modelers. This tool converts airfoil coordinate files (**Top-down .dat**) into CAD-ready **.dxf** files.

## Features
* **Batch Conversion**: Convert an entire directory of `.dat` files to `"spline".dxf` in one click.
* **Airfoil Flipping**: Mirror your profiles horizontally ($1 - x$) to reverse leading/trailing edge positions while maintaining point order continuity.
* **Standalone**: No installation required. Runs as a portable executable.
* **Modern UI**: Clean interface with integrated log view and custom airfoil iconography.

## How to use
1. **Place your .dat files** in the application folder or select your working directory.
2. **Click "Flip .dat"** if you need to mirror your profiles.
3. **Click "Convert"** to generate the `.dxf` files.
4. **Import** the resulting files directly into your favorite CAD software (AutoCAD, Rhino, SolidWorks, etc.).

## Requirements
* Windows 10/11
* The `netDxf.dll` must be present in the same folder as the executable.

## Credits
Built with .NET and the **netDxf** library.
