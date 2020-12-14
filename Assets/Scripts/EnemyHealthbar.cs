using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealthbar : MonoBehaviour
{
    private const int time = 30;

    public int maxHp = 0, from = 0, to = 0;
    public bool start = false;
    private float fromPercent, toPercent, step;
    private int frame;
    public TextMeshPro textMeshPro;
    public GameObject attackObject;
    public GameObject bloodPrefab;

    public void Start()
    {
        Instantiate(bloodPrefab, new Vector3(0, 2.4f, -5f), Quaternion.identity); 
    }

    void FixedUpdate()
    {
        if (frame == time)
        {
            transform.localScale = new Vector3(fromPercent - (step * time), 1, 1);
            frame++; // Pour pas refaire l'action inutilement
            StartCoroutine("DestroySelf");
        }

        if (start && frame < time)
        {
            textMeshPro.text = (from - to).ToString();
            fromPercent = from / (float)(maxHp);
            toPercent = to / (float)(maxHp);
            step = Mathf.Abs(fromPercent - toPercent) / (float)(time);
            transform.localScale = new Vector3(fromPercent - (step * frame), 1, 1);
            frame++;
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject.Find("Pattern Manager").GetComponent<PatternManager>().StartPattern(Turn.GetTurn().GetPattern());
        Destroy(attackObject);
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(GameObject.FindWithTag("Attack Bar"));
        Destroy(gameObject);
    }
}
