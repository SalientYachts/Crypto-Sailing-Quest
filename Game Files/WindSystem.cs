using System.Collections;
using UnityEngine;

public class WindSystem : MonoBehaviour
{
    public float windAngle;
    public Vector3 windVector;
    public bool randomEvolution = true;
    public int timer = 15;
    public int amount = 15;
    public string evolution;
    [HideInInspector] public float start;
    [HideInInspector] public float end;
    static float t;

    public Material[] myMaterials;

    void Start()
    {
        windAngle = Random.Range(0f, 359f); // First wind direction, random
        if (randomEvolution) StartCoroutine(ChangeDirection());
        t = 0f;
    }

    public void Update()
    {
        this.transform.eulerAngles = new Vector3(0, windAngle, 0);
        windVector = new Vector3(Mathf.Sin((windAngle) * Mathf.PI / 180f), 0, Mathf.Cos((windAngle) * Mathf.PI / 180f));

        for (int i = 0; i < myMaterials.Length; i++)
        {
            myMaterials[i].SetFloat("WindAngle", windAngle);
        }

        if (randomEvolution)
        {
            if (evolution != "Constant")
            {
                t += Time.deltaTime / timer;
                windAngle = Mathf.Lerp(start, end, t);
            }

            if (t > 1f)
            {
                evolution = "Constant";
                t = 0f;
            }
        }
    }


    IEnumerator ChangeDirection()
    {
        int x = Random.Range(0, 3);
        switch (x)
        {
            case 0:
                evolution = "Constant";
                break;

            case 1:
                start = windAngle;
                end = windAngle + amount;
                
                // To avoid the 360-0 issue
                if (end > 359f)
                {
                    end = 359f;
                }
                if (start == 359f)
                {
                    windAngle = 0f;
                    start = 0f;
                    end = amount;
                }

                evolution = "Increase";
                break;

            case 2:
                start = windAngle;
                end = windAngle - amount;

                // To avoid the 360-0 issue
                if (end < 0f)
                {
                    end = 0f;
                }
                if (start == 0f)
                {
                    windAngle = 359f;
                    start = 359f;
                    end = 360 - amount;
                }

                evolution = "Decrease"; 
                break;
        }
        yield return new WaitForSeconds(timer);
        if (randomEvolution) StartCoroutine(ChangeDirection());
    }
}

