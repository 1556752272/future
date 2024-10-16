using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // ���ڼ��������¼�

public class OptionSelector : MonoBehaviour
{
    public KeyCode selectKey = KeyCode.Return; // ȷ��ѡ��ļ��̰���
    public KeyCode leftKey = KeyCode.Q; // ����ѡ��ļ��̰���
    public KeyCode rightKey = KeyCode.E; // ����ѡ��ļ��̰���
    private bool firstTouch = true;
    public LevelUpSelectionButton[] options; // ����ѡ�����,�Ҿ�����һ�����ȽϺã�����˵�ı�һ�¹���

    private int selectedOptionIndex; // ��ǰѡ�е�ѡ������

    void Start()
    {
        // Ĭ��ѡ�е�һ��ѡ��
        //SelectOption(0);
    }

    void Update()
    {
        // ����������
        if ((Input.GetKeyDown(leftKey)|| Input.GetKeyDown(rightKey))&&firstTouch)
        {
            SelectOption(1);
            firstTouch = false;
            options[1].panel.SetActive(false);
            options[0].panel.SetActive(true);
            options[2].panel.SetActive(true);
        }
        else { 
        if (Input.GetKeyDown(leftKey))
        {
            SelectOption(selectedOptionIndex - 1);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            SelectOption(selectedOptionIndex + 1);
        }
        else if (Input.GetKeyDown(selectKey))
        {
            ConfirmSelection();
        }
        }
    }

    public void SelectOption(int index)
    {
        // ȷ��������ѡ�Χ��
        index = Mathf.Clamp(index, 0, options.Length - 1);

        // ȡ��֮ǰѡ�е�ѡ��
        if (options[selectedOptionIndex] != null)
        {
            options[selectedOptionIndex].panel.SetActive(true);
        }

        // ��ʾ��ѡ�е�ѡ��
        selectedOptionIndex = index;
        options[selectedOptionIndex].panel.SetActive(false);
    }

    public void ConfirmSelection()
    {
        // ����ȷ��ѡ��ѡ����߼�
        options[selectedOptionIndex].SelectUpgrade();
        firstTouch = true;//����������һ��
        options[1].panel.SetActive(false);
        options[0].panel.SetActive(false);
        options[2].panel.SetActive(false);
    }

    // ���������¼�
    public void OnPointerClick()
    {
        // ����Ƿ����˵�ǰѡ�е�ѡ��
        if (EventSystem.current.currentSelectedGameObject == options[selectedOptionIndex])
        {
            ConfirmSelection();
        }
    }
}