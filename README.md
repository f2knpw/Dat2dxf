# Dat2dxf Converter

A lightweight, standalone Windows utility designed for aerodynamicists and RC modelers. This tool converts airfoil coordinate files (**Top-down .dat**) into CAD-ready **.dxf** files.

## Features
* **Batch Conversion**: Convert an entire directory of `.dat` files to `"spline".dxf` in one click.
* **Airfoil Flipping**: Mirror your profiles horizontally ($1 - x$) to reverse leading/trailing edge positions while maintaining point order continuity.
* **Standalone**: No installation required. Runs as a portable executable.

## How to use
1. **Place your .dat files** in the application folder and select this working directory.
2. **Click "Convert"** to generate the `.dxf` files.
3. **Click "Flip .dat"** if you need to mirror your profiles.
4. **Select** any resulting file to see the profile (blue is .dat, red is .dxf).
5. **Import** the resulting files directly into your favorite CAD software (AutoCAD, Rhino, SolidWorks, etc.).

 <img width="1117" height="482" alt="image" src="https://github.com/user-attachments/assets/57d2b426-e160-4775-a146-e27ed4c47bf8" />


## Requirements
* Windows 10/11
* The `netDxf.dll` must be present in the same folder as the executable.

## Credits
Built with .NET and the **netDxf** library.
Gemini for most of the code !
