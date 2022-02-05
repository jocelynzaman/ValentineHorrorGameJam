using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * Progresses through a list of SingleFrame objects and iterates through each SingleFrame components
 */
public class Scene : MonoBehaviour
{
    public GameObject[] frames;
    private int frameIndex;

    public Image visual;

    public TextMeshProUGUI scriptText;
    private int textIndex;

    public TextMeshProUGUI speaker;

    private AudioSource audioClip;

    private List<GameObject> animations;

    // Start is called before the first frame update
    void Start()
    {
        frameIndex = 0;
        animations = new List<GameObject>();

        SetSceneComponents();
        textIndex++;
    }

    public void StartScene()
    {
        frameIndex = 0;
        animations = new List<GameObject>();

        SetSceneComponents();
        textIndex++;
    }

    //Player clicks text box
    public void ProgressScene()
    {
        if (frameIndex < frames.Length)
        {
            //if text is left in the current frame, then change text to next script line
            if (textIndex < frames[frameIndex].GetComponent<SingleFrame>().GetText().Length)
            {
                scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
                textIndex++;
                //print(frames[frameIndex].GetComponent<SingleFrame>().GetText().Length);
            }
            //no more text in current frame
            else
            {
                //disable player input on scene progression so audio finishes playing
                scriptText.GetComponent<Button>().interactable = false;

                if (audioClip != null)
                {
                    //wait for audio to finish
                    StartCoroutine(WaitForAudio());
                }
                else
                {
                    scriptText.GetComponent<Button>().interactable = true;

                    DeleteAllAnimations();
                    frameIndex++;
                    SetSceneComponents();
                }
            }
        }
        else
        {
            //disable player input on scene progression
            scriptText.GetComponent<Button>().interactable = false;

            //switch to gameplay screen
            //print("exit scene");
        }

        //wait for audio to finish playing before progressing to next frame
        IEnumerator WaitForAudio()
        {
            yield return new WaitWhile(() => audioClip.isPlaying);

            scriptText.GetComponent<Button>().interactable = true;

            DeleteAllAnimations();
            frameIndex++;
            SetSceneComponents();            
        }
    }

    //set up scene components of next frame
    private void SetSceneComponents()
    {
        textIndex = 0;

        //set visual, text, speaker, and audio of next frame
        visual.sprite = frames[frameIndex].GetComponent<SingleFrame>().GetVisual();
        scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
        speaker.text = frames[frameIndex].GetComponent<SingleFrame>().GetSpeaker();
        audioClip = FindObjectOfType<AudioManager>().Play(frames[frameIndex].GetComponent<SingleFrame>().GetAudio());

        audioClip = FindObjectOfType<AudioManager>().Play(frames[frameIndex].GetComponent<SingleFrame>().GetAudio());

        foreach (GameObject anim in frames[frameIndex].GetComponent<SingleFrame>().GetAnimations())
        {
            animations.Add(Instantiate(anim, anim.transform.position, Quaternion.identity));
        }

        textIndex++;
    }

    //delete all animations from current frame
    private void DeleteAllAnimations()
    {
        foreach (GameObject anim in animations)
        {
            Destroy(anim);
        }

        animations.Clear();
    }
}
