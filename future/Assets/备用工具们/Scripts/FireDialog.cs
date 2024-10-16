using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FireDialog : MonoBehaviour
{
    public static FireDialog instance;
    
    /// <summary>
    /// �Ի��ı���csv
    /// </summary>
    public TextAsset dialogDataFile;

    /// <summary>
    /// ����ɫͼ��
    /// </summary>
    public Image spriteLeft;

    /// <summary>
    /// �Ҳ��ɫͼ��
    /// </summary>
    public SpriteRenderer spriteRight;

    /// <summary>
    /// ��ɫ�����ı�
    /// </summary>
    public TextMeshProUGUI nameText;

    /// <summary>
    /// �Ի������ı�
    /// </summary>
    public TextMeshProUGUI dialogText;

    /// <summary>
    /// ��ɫͼƬ�б�
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// ����ͼƬ�б�
    /// </summary>
    public List<Sprite> bgsprites = new List<Sprite>();

    /// <summary>
    /// ��ɫ���ֶ�ӦͼƬ���ֵ�
    /// </summary>
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    /// <summary>
    /// �������ֶ�ӦͼƬ���ֵ�
    /// </summary>
    Dictionary<string, Sprite> bgimageDic = new Dictionary<string, Sprite>();

    /// <summary>
    /// ���浱ǰ�Ի�����ֵ
    /// </summary>
    public int dialogIndex;

    /// <summary>
    /// �Ի��ı������зָ�
    /// </summary>
    public string[] dialogRows;
    private float Timerr = 2;
    public GameObject spritewhite;
    public int num = 0;
    private void Awake()
    {
        instance = this;
        imageDic["�����������"] = sprites[0];
        imageDic["������"] = sprites[1];
        imageDic["777"] = sprites[2];
        imageDic["������˹"] = sprites[3];
        imageDic["��ʯ�����"] = sprites[4];
        imageDic["�������"] = sprites[5];
        imageDic[""] = sprites[6];
        imageDic["����"] = sprites[7];
        imageDic["������"] = sprites[8];
        imageDic["��ũ"] = sprites[9];
        bgimageDic["����һ"] = bgsprites[0];
        bgimageDic["������"] = bgsprites[1];
        bgimageDic["������"] = bgsprites[2];
        bgimageDic["������"] = bgsprites[3];
        bgimageDic["������"] = bgsprites[4];
        bgimageDic["������"] = bgsprites[5];
        bgimageDic["������"] = bgsprites[6];
        bgimageDic["������"] = bgsprites[7];

    }



    // Start is called before the first frame update
    void Start()
    {

       

    }
    public void Fire()
    {
        Debug.Log("duqu");
        ReadText(dialogDataFile);
        ShowDialogRow();

        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Timerr >= 0)
        {
            Timerr -= Time.deltaTime;

        }
        if (Timerr <= 0 && Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("��ȡ�ɹ�");
            OnClickNext();
        }
    }

    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        StartCoroutine(TypeDialog(_text));

    }

    public void UpdateImage(string _name, string _position)
    {
        if (_position == "��")
        {
            spriteLeft.sprite = imageDic[_name];
        }
        else if (_position == "��")
        {
            spriteRight.sprite = imageDic[_name];
        }
    }

    public void bgUpdateImage(string _name, string _position)
    {
        if (_position == "��")
        {
            spriteRight.sprite = bgimageDic[_name];
        }
        else if (_position == "��")
        {
            spriteLeft.sprite = bgimageDic[_name];
        }
    }

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        //foreach (var row in rows)
        //{
        //    string[] cell = row.Split(',');

        //}
        Debug.Log("��ȡ�ɹ�");
    }

    public void ShowDialogRow()
    {
        foreach (var row in dialogRows)
        {
            if (num == 4) spritewhite.SetActive(true);
            string[] cells = row.Split(',');
            if (cells[0] == "a" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "b" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                //bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "%" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "!" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "@" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "*" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                Debug.Log("�������");
                SceneManager.LoadScene(1);
                break;
            }
            num++;
        }
    }

    public void OnClickNext()
    {
        Timerr = 0.1f;
        ShowDialogRow();
    }

    private IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";

        foreach (char letter in dialog)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / 30);
        }
    }
}
