using UnityEngine;
using UnityEditor;

public class GradientCreator : MonoBehaviour
{
    [MenuItem("Assets/Create/Gradient from Curve")]
    static void CreateGradientFromCurve()
    {
        // Define the gradient
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(Color.black, 1f),
            new GradientColorKey(Color.white, 0f),
        };
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1.0f, 0f),
            new GradientAlphaKey(1.0f, 1f)
        };

        gradient.colorKeys = colorKeys;
        gradient.alphaKeys = alphaKeys;

        // Define texture size
        int textureWidth = 256; // Width of the texture
        int textureHeight = 1; // Height of the texture (1 for a gradient strip)

        // Create the texture
        Texture2D texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

        // Sample the gradient
        for (int x = 0; x < textureWidth; x++)
        {
            float t = x / (float)(textureWidth - 1);
            Color color = gradient.Evaluate(t);
            texture.SetPixel(x, 0, color);
        }

        // Apply changes to the texture
        texture.Apply();

        // Save the texture to the Assets folder
        string path = "Assets/Shaders/GradientTexture.png";
        System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());

        // Refresh the AssetDatabase to recognize the new texture
        AssetDatabase.Refresh();

        Debug.Log("Gradient texture created at " + path);
    }
}