using UnityEngine;

/*
 * Used for a scene
 * Each frame consists of
 *    1 visual frame
 *    list of text
 *    1 speaker
 *    audio (voiceover) - add later
 */
public class SingleFrame : MonoBehaviour
{
    [SerializeField] private Sprite visual;
    [SerializeField] private string[] text;
    [SerializeField] private string speaker;

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
}
