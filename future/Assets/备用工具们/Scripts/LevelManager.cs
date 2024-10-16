using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public void Awake()
    {
        instance =this;
    }

    private bool gameActive;
    [HideInInspector]public float timer=0f;

    private float waitToShowEndScreen = 1f;

    
    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if(gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);//����UIcontroller����ʱ��
        }
    }

    public void EndLevel()//����Ϸ��ɫ����ʱ����Э�̣�����Э��
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()//����ֹͣ��Ϸ����
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        //float minutes = Mathf.FloorToInt( timer / 60f);
        //float seconds = Mathf.FloorToInt( timer % 60f);

        //UIController.instance.endTimeText.text = minutes.ToString() + " mins" +seconds.ToString("00" + " secs");
        UIController.instance.levelEndScreen.SetActive(true);
    
    }
}
