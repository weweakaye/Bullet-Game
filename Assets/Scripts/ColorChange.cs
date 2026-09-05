using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public void ChangeColor(Color newColor)
    {
        // Change the color of this object
        Renderer myRenderer = GetComponent<Renderer>();

        if (myRenderer != null)
        {
            myRenderer.material.color = newColor;
        }

        // Change the color of all child objects
        Renderer[] childRenderers =
            GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childRenderers)
        {
            renderer.material.color = newColor;
        }
    }
}