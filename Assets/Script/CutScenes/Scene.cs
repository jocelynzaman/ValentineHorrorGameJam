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

    [SerializeField] private GameObject textbox;
    private RectTransform textboxRect;
    private Image textboxImage;

    [SerializeField] private string music;
    private AudioSource musicSource;
    private AudioManager audioManager;
    private AudioSource audioClip;

    /*[SerializeField] private bool autoProgressScene;*/
    private List<GameObject> animations;

    private float speakerY;
    private float speakerX;
    private float scriptY;
    private float scriptX;

    private float textboxY;
    private float textboxWidth;

    // Start is called before the first frame update
    void Start()
    {
        frameIndex = 0;
        audioManager = FindObjectOfType<AudioManager>();
        musicSource = audioManager.Play(music);
        animations = new List<GameObject>();

        textboxRect = textbox.GetComponent<RectTransform>();
        textboxImage = textbox.GetComponent<Image>();
        textboxY = -335.0f;
        textboxWidth = 1820.0f;

        //old text positioning
        //speakerY = speaker.GetComponent<RectTransform>().anchoredPosition.y;
        //speakerX = speaker.GetComponent<RectTransform>().anchoredPosition.x;
        //scriptY = scriptText.GetComponent<RectTransform>().anchoredPosition.y;
        //scriptX = scriptText.GetComponent<RectTransform>().anchoredPosition.x;

        SetSceneComponents();
    }

    void Update()
    {
        //OLD TEXT POSITIONING
        ////move speaker and script text to the top of scene
        //if (frames[frameIndex].GetComponent<SingleFrame>().IsMoveTextToTop())
        //{
        //    speaker.GetComponent<RectTransform>().anchoredPosition = new Vector2(speakerX, 1030.0f);
        //    scriptText.GetComponent<RectTransform>().anchoredPosition = new Vector2(scriptX, 950.0f);
        //    scriptText.GetComponent<RectTransform>().sizeDelta = new Vector2(1776.0f, scriptText.GetComponent<RectTransform>().sizeDelta.y);
        //}
        //else
        //{
        //    speaker.GetComponent<RectTransform>().anchoredPosition = new Vector2(speakerX, 220.0f);
        //    scriptText.GetComponent<RectTransform>().anchoredPosition = new Vector2(scriptX, 120.0f);
        //    scriptText.GetComponent<RectTransform>().sizeDelta = new Vector2(1776.0f, scriptText.GetComponent<RectTransform>().sizeDelta.y);
        //}

        ////move speaker and script text to the right of scene
        //if (frames[frameIndex].GetComponent<SingleFrame>().IsMoveTextToRight())
        //{
        //    speaker.GetComponent<RectTransform>().anchoredPosition = new Vector2(1150.0f, speaker.GetComponent<RectTransform>().anchoredPosition.y);
        //    scriptText.GetComponent<RectTransform>().anchoredPosition = new Vector2(1370.0f, scriptText.GetComponent<RectTransform>().anchoredPosition.y);
        //    scriptText.GetComponent<RectTransform>().sizeDelta = new Vector2(1010.0f, scriptText.GetComponent<RectTransform>().sizeDelta.y);
        //}
        //else
        //{
        //    speaker.GetComponent<RectTransform>().anchoredPosition = new Vector2(330.0f, speaker.GetComponent<RectTransform>().anchoredPosition.y);
        //    scriptText.GetComponent<RectTransform>().anchoredPosition = new Vector2(930.0f, scriptText.GetComponent<RectTransform>().anchoredPosition.y);
        //    scriptText.GetComponent<RectTransform>().sizeDelta = new Vector2(1776.0f, scriptText.GetComponent<RectTransform>().sizeDelta.y);
        //}

        //move textbox to desired position
        //move textbox to the top of scene
        if (frameIndex < frames.Length)
        {
            if (frames[frameIndex].GetComponent<SingleFrame>().IsMoveTextToTop())
            {
                ResizeAndPositionTextbox(0.0f, 330.0f, textboxWidth);
            }
            //move textbox to the bottom of scene
            else
            {
                ResizeAndPositionTextbox(0.0f, -335.0f, textboxWidth);
            }

            //move textbox to the right of scene
            if (frames[frameIndex].GetComponent<SingleFrame>().IsMoveTextToRight())
            {
                ResizeAndPositionTextbox(406.0f, textboxRect.anchoredPosition.y, 1052.0f);
            }
            //move textbox to the center of scene
            else
            {
                ResizeAndPositionTextbox(0.0f, textboxRect.anchoredPosition.y, textboxWidth);
            }

            //auto progress scenes with no text
            if (frames[frameIndex].GetComponent<SingleFrame>().IsAutoProgressFrame())
            {
                scriptText.GetComponent<Button>().interactable = false;
                if (animations[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length <= animations[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
                {
                    frameIndex++;
                    DeleteAllAnimations();
                    ProgressScene();
                    //if (frameIndex < frames.Length)
                    //{
                    //    DeleteAllAnimations();
                    //    SetSceneComponents();
                    //scriptText.GetComponent<Button>().interactable = false;
                    //}
                }
            }
        }
        else
        {
            ProgressScene();
        }
    }


    private void ResizeAndPositionTextbox(float posX, float posY, float width)
    {
        textboxRect.anchoredPosition = new Vector2(posX, posY);
        textboxRect.sizeDelta = new Vector2(width, textboxRect.sizeDelta.y);
    }

    //remove? doesn't look it's used
    public void StartScene()
    {
        frameIndex = 0;
        animations = new List<GameObject>();

        SetSceneComponents();
    }

    //Player clicks text box
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

            //switch to mini game screen
            print("exit scene");
            GameManager.Instance.UpdateGameState(GameState.MiniGame);
            gameObject.SetActive(false);
        }
    }

    //wait for animation?

    //wait for audio to finish playing before progressing to next frame
    IEnumerator WaitForAudio()
    {
        yield return new WaitWhile(() => audioClip.isPlaying);

        DeleteAllAnimations();
        frameIndex++;

        speaker.text = "";
        scriptText.text = "";
        audioClip = null;

        ProgressScene();
    }

    //set up scene components of next frame
    private void SetSceneComponents()
    {
        textIndex = 0;

        //set visual, text, speaker, and audio of next frame
        visual.sprite = frames[frameIndex].GetComponent<SingleFrame>().GetVisual();

        //var textboxColor = textboxImage.color;

        if (frames[frameIndex].GetComponent<SingleFrame>().GetText().Length > 0)
        {
            scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
            //textboxColor.a = 1.0f;
            textboxImage.enabled = true;
            
        }
        else
        {
            //textboxColor.a = 0.0f;
            textboxImage.enabled = false;
        }
        //textboxImage.color = textboxColor;

        speaker.text = frames[frameIndex].GetComponent<SingleFrame>().GetSpeaker();

        if (frames[frameIndex].GetComponent<SingleFrame>().GetAudio().Equals(""))
        {
            audioClip = null;
            print("audioclip is null");
        }
        else
        {
            audioClip = audioManager.Play(frames[frameIndex].GetComponent<SingleFrame>().GetAudio());
        }
        

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
