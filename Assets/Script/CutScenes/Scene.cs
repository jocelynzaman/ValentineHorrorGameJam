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
    [SerializeField] private GameObject[] frames;
    private int frameIndex;

    [SerializeField] private Image visual;

    [SerializeField] private TextMeshProUGUI scriptText;
    private int textIndex;

    [SerializeField] private TextMeshProUGUI speaker;

    [SerializeField] private string music;
    private AudioSource musicSource;
    private AudioManager audioManager;
    private AudioSource audioClip;

    /*[SerializeField] private bool autoProgressScene;*/
    private List<GameObject> animations;

    // Start is called before the first frame update
    void Start()
    {
        frameIndex = 0;
        audioManager = FindObjectOfType<AudioManager>();
        musicSource = audioManager.Play(music);
        animations = new List<GameObject>();

        SetSceneComponents();
    }

    void Update()
    {
        //auto progress scenes with no text
        if ((frameIndex < frames.Length) && frames[frameIndex].GetComponent<SingleFrame>().IsAutoProgressFrame())
        {
            scriptText.GetComponent<Button>().interactable = false;
            if (animations[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length <= animations[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                frameIndex++;
                if (frameIndex < frames.Length)
                {
                    DeleteAllAnimations();
                    SetSceneComponents();
                    scriptText.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    public void StartScene()
    {
        frameIndex = 0;
        animations = new List<GameObject>();

        SetSceneComponents();
    }

    public void ProgressText()
    {
        //if text is left in the current frame, then change text to next script line
        if (textIndex < frames[frameIndex].GetComponent<SingleFrame>().GetText().Length)
        {
            scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
            textIndex++;
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
                DeleteAllAnimations();
                frameIndex++;

                ProgressScene();
            }
        }
    }

    //Player clicks text box
    public void ProgressScene()
    {
        if (frameIndex < frames.Length)
        {
            SetSceneComponents();
            scriptText.GetComponent<Button>().interactable = true;
        }
        else
        {
            //disable player input on scene progression
            scriptText.GetComponent<Button>().interactable = false;

            musicSource.Stop();

            //switch to gameplay screen
            //print("exit scene");
        }
    }

    //wait for animation?

    //wait for audio to finish playing before progressing to next frame
    IEnumerator WaitForAudio()
    {
        yield return new WaitWhile(() => audioClip.isPlaying);

        //scriptText.GetComponent<Button>().interactable = true;

        DeleteAllAnimations();
        frameIndex++;
        //SetSceneComponents();            
        ProgressScene();
    }

    //set up scene components of next frame
    private void SetSceneComponents()
    {
        textIndex = 0;

        //set visual, text, speaker, and audio of next frame
        visual.sprite = frames[frameIndex].GetComponent<SingleFrame>().GetVisual();
        if (frames[frameIndex].GetComponent<SingleFrame>().GetText().Length > 0)
        {
            scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
        }

        speaker.text = frames[frameIndex].GetComponent<SingleFrame>().GetSpeaker();
        audioClip = audioManager.Play(frames[frameIndex].GetComponent<SingleFrame>().GetAudio());

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
