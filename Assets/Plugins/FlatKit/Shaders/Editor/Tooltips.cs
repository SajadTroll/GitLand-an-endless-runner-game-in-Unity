using System;
using System.Collections.Generic;

namespace FlatKit.StylizedSurface {
public static class Tooltips {
    public static Dictionary<String, String> map = new Dictionary<string, string> {
	{"Color", "Color"},
	{"Cel Shading Mode", "Lets you choose how the coloring/shading is applied. 'None' uses only flat Color parameter above, no primary cel is added; 'Single' adds a primary cel layer; 'Steps' works with a special texture for adding as many cels as there are in the texture; 'Curve' works with a special texture, but unlike 'Steps', it uses a smooth interpolated one"},
	{"Color Shaded", "Color of a cel layer. Usually it is the dark part of cel shaded objects"},
	{"Self Shading Size", "How big of a part of a mesh is covered with a cel layer"},
	{"Shadow Edge Size", "How smooth or sharp a cel layer is. Values to the left mean 'sharper', values to the right are 'smoother'"},
	{"Localized Shading", ""},
	{"Enable Extra Cel Layer", "If one cel is not enough, here's another one"},
	{"Enable Specular", "Enables specular. It is kind of a glare effect"},
	{"Specular Color", "Color of specular"},
	{"Specular Size", "How big the specular is"},
	{"Specular Edge Smoothness", "How smooth or sharp the specular is. Values to the left mean 'sharper', values to the right are 'smoother'"},
	{"Enable Rim", "Enables Rim. It can be used as a pseudo-outline layer or even as an additional color layer"},
	{"Rim Color", "Color of the rim"},
	{"Light Align", "Use it to move the rim part on the mesh"},
	{"Rim Size", "How big the rim part is"},
	{"Rim Edge Smoothness", "How smooth or sharp the rim part is. Values to the left mean 'sharper', values to the right are 'smoother'"},
	{"Enable Height Gradient", "Enables Height Gradient"},
	{"Gradient Color", "Sets the color of the gradient overlay part"},
        {"Center X", "World-space X coordinate of the middle gradient point"},
        {"Center Y", "World-space Y coordinate of the middle gradient point"},
        {"Size", "How stretched the gradient is"},
        {"Gradient Angle", "Rotates the gradient on the mesh"},
        {"Enable Vertex Colors", ""},
        {"Light Color Contribution", ""},
        {"Falloff size (point / spot)", ""},
        {"Enable", "Enables overriding of light direction. Use it when you want to re-align the shaded part for the current material only"},
        {"Pitch", "Moves the shaded part across world-space X coordinate"},
        {"Yaw", "Moves the shaded part across world-space Y coordinate"},
	{"Mode", "Use this menu to let the current material receive shadows. 'Multiply' parameter multiplies black shadow over existing colors of shading; 'Color' parameter applies freely colored shadow over existing colors of shading"},
	{"Power", "How opaque the received shadows are"},
	{"Sharpness", "How smooth or sharp the received shadows are. Values to the left mean 'sharper', values to the right are 'smoother'"},
	{"Blending Mode", "Select which blending mode to use for an albedo texture — 'Add' or 'Multiply'"},
	{"Texture Impact", "How opaque or transparent the texture is"},
	{"Surface Type", "'Opaque'; 'Transparent'"},
	{"Render Faces", "'Front'; 'Back'; 'Both'"},
	{"Alpha Clipping", ""},
	{"Enable GPU Instancing", ""},
    };
}
}