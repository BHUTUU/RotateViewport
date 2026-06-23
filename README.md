# RotateViewport

Rotate the current viewport view by picking two points.

`RotateViewport` aligns the viewport view to a selected direction using AutoCAD's **DVIEW TWIST** command and automatically updates **SNAPANG** so the crosshair follows the new orientation.

## Commands

### ROTATEVIEWPORT

Rotates the active viewport view based on two picked points.

### RV

Shortcut alias for `ROTATEVIEWPORT`.

## Features

* Rotate viewport view using two points.
* Uses AutoCAD's native **DVIEW TWIST** for accurate results.
* Automatically sets **SNAPANG** to match the rotated view.
* Prevents execution in model space.
* Warns if the active viewport is locked.
* Ideal for road alignments, utility corridors, survey drawings, and long linear features.

## Usage

```text
Command: ROTATEVIEWPORT

Specify First Point:
Specify Second Point:
```

The viewport rotates so that the selected direction becomes aligned with the screen, and the cursor orientation is updated accordingly.

## Requirements

* AutoCAD 2025
* .NET 8
* x64 Platform

## Installation

1. Build the project in **Release | x64**.
2. Open AutoCAD.
3. Run:

```text
NETLOAD
```

4. Select the generated DLL.
5. Run `ROTATEVIEWPORT` or `RV`.

## Author

**Suman Kumar**

GitHub: https://github.com/BHUTUU
