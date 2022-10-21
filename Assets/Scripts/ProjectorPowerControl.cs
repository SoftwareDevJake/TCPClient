//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ProjectorPowerControl : MonoBehaviour
//{
//    //Network_Client_Hardware NCH;
//    public static ProjectorPowerControl instance;

//    public int ProjectorSwitchIndex;

//    public Button totalPower;

//    public Image[] thisImg;

//    public Sprite normalImg;
//    public Sprite changeImg;

//    public static int[] index = new int[8];

//    private string ipNameInPPC;
//    public GameObject[] power;

//    private string powerOnOff;
//    private void Awake()
//    {
//        if(instance == null)
//        {
//            instance = this;
//        }
//    }
//    void Start()
//    {
//        //NCH = this.gameObject.AddComponent<Network_Client_Hardware>();
//        Network_Client_Hardware.instance.ConnectToTcpServer();
//        for (int i = 0; i < index.Length; i++)
//        {
//            index[i] = 0;
//        }
//    }

//    /// <summary>
//    /// ��ü �Ǵ� �� ��ư���� ON, OFF ����
//    /// </summary>
//    public void OnButtonClicked()
//    {
//        // PC ��ü�� ������ ���� �� ��
//        if (ProjectorSwitchIndex == Strings.PROJECTOR_TOTAL)
//        {
//            ChangeTotalSwitch(); // ��ü ��ư
//        }
//        else
//        {
//            changeEachSwitch(); // ������ ��ư��
//        }
//    }

//    /// <summary>
//    /// ��ü ��ư�� ���� �� (PROJECTORTOTAL = 0)
//    /// </summary>
//    private void ChangeTotalSwitch()
//    {
//        // Ŭ���Ͽ� ������ �� ��
//        if (index[Strings.PROJECTOR_TOTAL] == Strings.OFF)
//        {
//            for (int i = 0; i < thisImg.Length; i++)
//            {
//                thisImg[i].sprite = changeImg;
//                index[i] = Strings.ON;
//                // ��ü ��ư�� ������ ������ ��ư���� �������͵��� ��
//                if(i > Strings.PROJECTOR_TOTAL)
//                {
//                    ipNameInPPC = power[i].name.Substring(18, 2);
//                    Network_Client_Hardware.instance.m_sIP_1 = "192.168.0." + ipNameInPPC;
//                    Debug.Log("NCH.m_sIP_1 : " + Network_Client_Hardware.instance.m_sIP_1);
//                    Network_Client_Hardware.instance.BeamCommand("power = 1 \r");
//                }
//            }
//        }
//        // Ŭ���Ͽ� ������ �� ��
//        else if (index[Strings.PROJECTOR_TOTAL] == Strings.ON)
//        {
//            for (int i = 0; i < thisImg.Length; i++)
//            {
//                thisImg[i].sprite = normalImg;
//                index[i] = Strings.OFF;
//                // ��ü ��ư�� ������ ������ ��ư���� �������͵��� ��
//                if (i > Strings.PROJECTOR_TOTAL)
//                {
//                    ipNameInPPC = power[i].name.Substring(18, 2);
//                    Network_Client_Hardware.instance.m_sIP_1 = "192.168.0." + ipNameInPPC;
//                    Debug.Log("NCH.m_sIP_1 : " + Network_Client_Hardware.instance.m_sIP_1);
//                    Network_Client_Hardware.instance.BeamCommand("power = 0 \r");
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// ��ü ��ư�� ������ ������ ��ư���� ������ ��
//    /// </summary>
//    private void changeEachSwitch()
//    {
//        for(int i = 1; i <= thisImg.Length; i++)
//        {
            
//            if (ProjectorSwitchIndex == i)
//            {
//                OnOrOff();
//            }
//        }
//    }
//    /// <summary>
//    /// �������� ���� ON or OFF
//    /// </summary>
//    public void OnOrOff()
//    {
//        if (index[ProjectorSwitchIndex] == Strings.OFF)
//        {
//            thisImg[ProjectorSwitchIndex].sprite = changeImg;
//            powerOnOff = "power = 1 \r";
//            index[ProjectorSwitchIndex] = Strings.ON;
//            ipSelection(ProjectorSwitchIndex, powerOnOff);
            
//        }
//        else if (index[ProjectorSwitchIndex] == Strings.ON)
//        {
//            thisImg[ProjectorSwitchIndex].sprite = normalImg;
//            index[ProjectorSwitchIndex] = Strings.OFF;
//            powerOnOff = "power = 0 \r";
//            ipSelection(ProjectorSwitchIndex, powerOnOff);
//        }
//    }
//    /// <summary>
//    /// �� �������Ϳ� �˸´� mac address ����
//    /// </summary>
//    /// <param name="ProjectorSwitchIndex"></param>
//    /// <param name="powerOnOff"></param>
//    public void ipSelection(int ProjectorSwitchIndex, string powerOnOff)
//    {
//        // ��ü ��ư�� �� ��� ip�� ���ؼ� ��� �������� ON
//        if (ProjectorSwitchIndex == Strings.PROJECTOR_TOTAL)
//        {
//            for(int i = 1; i <= power.Length-1; i++)
//            {
//                ipNameInPPC = power[i].name.Substring(18, 2);
//                Network_Client_Hardware.instance.m_sIP_1 = "192.168.0." + ipNameInPPC;
//                Debug.Log("NCH.m_sIP_1 : " + Network_Client_Hardware.instance.m_sIP_1);
//                Network_Client_Hardware.instance.BeamCommand("power = 1 \r"); ;
//            }
//        }
//        else if(ProjectorSwitchIndex == Strings.PROJECTOR_1)
//        {
//            ipSetting(ProjectorSwitchIndex, powerOnOff);
//        }
//        else if(ProjectorSwitchIndex == Strings.PROJECTOR_2)
//        {
//            ipSetting(ProjectorSwitchIndex, powerOnOff);
//        }
//        else if (ProjectorSwitchIndex == Strings.PROJECTOR_3)
//        {
//            ipSetting(ProjectorSwitchIndex, powerOnOff);
//        }
//        else if (ProjectorSwitchIndex == Strings.PROJECTOR_4)
//        {
//            ipSetting(ProjectorSwitchIndex, powerOnOff);
//        }
//        else if (ProjectorSwitchIndex == Strings.PROJECTOR_5)
//        {
//            ipSetting(ProjectorSwitchIndex, powerOnOff);
//        }
//        else if (ProjectorSwitchIndex == Strings.PROJECTOR_6)
//        {
//            ipSetting(ProjectorSwitchIndex, powerOnOff);
//        }
//        else if (ProjectorSwitchIndex == Strings.PROJECTOR_7)
//        {
//            ipSetting(ProjectorSwitchIndex, powerOnOff);
//        }

//    }
//    /// <summary>
//    /// ������ �ε����� ���� ���θ� ���� �޾� �����Ű�� �Լ�
//    /// </summary>
//    /// <param name="i"></param>
//    /// <param name="powerOnOff"></param>
//    private void ipSetting(int i, string powerOnOff)
//    {
//        ipNameInPPC = power[i].name.Substring(18, 2);
//        Network_Client_Hardware.instance.m_sIP_1 = "192.168.0." + ipNameInPPC;
//        Debug.Log("NCH.m_sIP_1 in ip settings: " + Network_Client_Hardware.instance.m_sIP_1);
//        Network_Client_Hardware.instance.BeamCommand(powerOnOff);
//    }
//}
