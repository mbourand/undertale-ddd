using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeFightCinematic : MonoBehaviour
{
    int currentDialog = 11;
    public DialogManager dialogManager;
    public GameObject dustParticles;
    public AudioSource rumble;
    public EyeAnimation eye;
    public CameraShake cameraShake;
    public CircleTransition circleTransition;

    string[] dialogs =
    {
        "before_fight_1",
        "before_fight_2",
        "before_fight_3",
        "before_fight_4",
        "before_fight_5",
        "before_fight_6",
        "before_fight_7",
        "before_fight_8",
        "before_fight_9",
        "before_fight_10",
        "before_fight_11",
        "before_fight_12",
        "before_fight_13",
        "before_fight_14"
    };

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        dialogManager.RunDialog(dialogs[currentDialog]);
    }

    void Start()
    {
        StartCoroutine("LateStart");
    }

    void Update()
    {
        if (Input.GetButtonDown("Confirm") && dialogManager.GetDialog(dialogs[currentDialog]).started && dialogManager.GetDialog(dialogs[currentDialog]).obj.GetComponent<Dialog>().finish && currentDialog != dialogs.Length - 1)
        {
            dialogManager.DeleteDialog(dialogs[currentDialog]);
            currentDialog++;
            if (currentDialog == dialogs.Length - 1)
                Destroy(GameObject.Find("Music Player"));
            dialogManager.RunDialog(dialogs[currentDialog]);
        }
        if (dialogManager.GetDialog(dialogs[currentDialog]).started && dialogManager.GetDialog(dialogs[currentDialog]).obj.GetComponent<Dialog>().finish && currentDialog == dialogs.Length - 1)
        {
            dialogManager.DeleteDialog(dialogs[currentDialog]);
            Destroy(GameObject.Find("BubbleDialog"));
            StartCoroutine("StartEye");
            Instantiate(dustParticles);
            rumble.Play();
            StartCoroutine(cameraShake.Shake(13f, .05f));
            GameObject.Find("Fight Music Player").GetComponent<AudioSource>().Play();
            StartCoroutine("LoadFightScene");
            StartCoroutine("StartTransition");
        }
    }

    IEnumerator LoadFightScene()
    {
        yield return new WaitForSeconds(12.9f);
        SceneManager.LoadScene("Fight Scene");
    }

    IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(9.3f);
        circleTransition.start = true;
    }

    IEnumerator StartEye()
    {
        yield return new WaitForSeconds(5f);
        eye.start = true;
    }
}
