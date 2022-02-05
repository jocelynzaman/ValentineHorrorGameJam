using UnityEngine;

/*
 * Used for a scene
 * Each frame consists of
 *    1 visual frame
 *    list of text
 *    1 speaker
 *    audio (voiceover)
 *    list of animation assets
 */
public class SingleFrame : MonoBehaviour
{
    [SerializeField] private Sprite visual;
    [SerializeField] private string speaker;
    [SerializeField] private string audioName;
    [SerializeField] private string[] text;
    [SerializeField] private bool isAutoProgressFrame;

    //animation
    [SerializeField] private GameObject[] animations;

    //getters
    public Sprite GetVisual()
    {
        return visual;
    }

    public string[] GetText()
    {
        return text;
    }

    public string GetSpeaker()
    {
        return speaker;
    }

    public string GetAudio()
    {
        return audioName;
    }

    public GameObject[] GetAnimations()
    {
        return animations;
    }

    public bool IsAutoProgressFrame()
    {
        return isAutoProgressFrame;
    }
}
