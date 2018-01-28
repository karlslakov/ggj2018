using UnityEngine;


public class CameraEffect : MonoBehaviour {

    private Material material;
    // Use this for initialization
    void Awake() {
        if (LocalState.PlayerType == PlayerType.Pessimist)
        {
            material = new Material(Shader.Find("Hidden/PessimistEffect"));
        }
        else
        {
            material = new Material(Shader.Find("Hidden/OptimistEffect"));
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
