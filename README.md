# Radar Chart Unity Component

## Overview

This Unity component generates a radar chart background image that represents a given set of statistics. The background image can be used in various projects to visualize data at the end of a game or for other purposes. Additionally, there is a separate component included that overlays data on top of this background image by creating a mesh from an array of data values.

## Features

- **Adjustable Radar Chart Background**:
    - Adjust the radius of the image
    - Set the number of rings
    - Modify the thickness of the lines
    - Apply an angle offset
    - Toggle radial lines on or off

- **Data Visualization**:
    - Overlay data on top of the radar chart background
    - Creates a mesh to represent the data values visually

## How to Use

1. **Setting Up the Radar Chart Background**:
    - Add the `DiagramCreator` prefab to your scene.
    - Adjust the settings (radius, number of rings, line thickness, angle offset, radial lines) using the Inspector.
    - Once the settings are adjusted, click the **Save** button to generate and store the radar chart image.
    - Delete `DiagramCreator`.

   ![DiagramCreator](https://github.com/user-attachments/assets/66dd3036-23d4-4dda-a594-60a73bccf0ee)

2. **Displaying Data**:
    - Add the `Diagram` prefab to your scene.
    - The generated image will be placed in the `Image` component of the `Diagram`.
    - From another component (which you will need to create), provide the array of data values to the `DiagramStatsPlotter` component.
    - The `DiagramStatsPlotter` component will use this data to generate a mesh and overlay it on top of the radar chart background.
    - The `ScriptableObject` containing the radar chart configuration should be assigned to the `Diagram Configuration So` field of the `DiagramStatsPlotter` component.

   ![Diagram](https://github.com/user-attachments/assets/b45c47f8-56da-4994-aeb3-5776a9107d09)

   **Note**: The component that provides data to `DiagramStatsPlotter` is not included in this project. This is because the source and calculation of the data can vary widely depending on the use case. For example, the data might come from in-game calculations, a database, or other sources outside of the scope of this project. You will need to create a custom component that fits your specific needs for providing the data.

   **Important**: The maximum value of the data should not exceed the radius of the radar chart. The `DiagramStatsPlotter` component includes a safeguard to ensure that data values are limited to the radius to prevent any visual distortions.

## Data Visualization Details

- An additional image will be provided to clearly indicate the starting point.
- Data values are plotted starting from a specific point on the polygon and moving in a **clockwise** direction.
- Ensure your data array is ordered according to this direction to match the vertices of the polygon.


   


