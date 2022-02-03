using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * Progresses through a list of SingleFrame objects and iterates through each SingleFrame visual, text list, speaker, and audio
 */
public class Scene : MonoBehaviour
{
    public GameObject[] frames;
    private int frameIndex;

    public Image visual;

    public TextMeshProUGUI scriptText;
    private int textIndex;

    public TextMeshProUGUI speaker;

    // Start is called before the first frame update
    void Start()
    {
        frameIndex = 0;
        textIndex = 0;

        //set scene components: visual, text, speaker
        visual.sprite = frames[frameIndex].GetComponent<SingleFrame>().GetVisual();
        scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
        speaker.text = frames[frameIndex].GetComponent<SingleFrame>().GetSpeaker();

        textIndex++;
    }

    public void StartScene()
    {
        frameIndex = 0;
        textIndex = 0;

        //set scene components: visual, text, speaker
        visual.sprite = frames[frameIndex].GetComponent<SingleFrame>().GetVisual();
        scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
        speaker.text = frames[frameIndex].GetComponent<SingleFrame>().GetSpeaker();

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
                print(frames[frameIndex].GetComponent<SingleFrame>().GetText().Length);
            }
            //no more text in current frame
            else
            {
                //reset textIndex
                textIndex = 0;

                frameIndex++;

                //set visual, text, and speaker of next frame
                visual.sprite = frames[frameIndex].GetComponent<SingleFrame>().GetVisual();
                scriptText.text = frames[frameIndex].GetComponent<SingleFrame>().GetText()[textIndex];
                speaker.text = frames[frameIndex].GetComponent<SingleFrame>().GetSpeaker();

                textIndex++;
            }
        }
        else
        {
            //switch to gameplay screen
            print("exit scene");
        }
    }
}
