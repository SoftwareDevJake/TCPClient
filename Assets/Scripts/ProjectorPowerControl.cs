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
//    /// 전체 또는 각 버튼들의 ON, OFF 조절
//    /// </summary>
//    public void OnButtonClicked()
//    {
//        // PC 전체의 전원을 조종 할 때
//        if (ProjectorSwitchIndex == Strings.PROJECTOR_TOTAL)
//        {
//            ChangeTotalSwitch(); // 전체 버튼
//        }
//        else
//        {
//            changeEachSwitch(); // 각각의 버튼들
//        }
//    }

//    /// <summary>
//    /// 전체 버튼이 눌릴 때 (PROJECTORTOTAL = 0)
//    /// </summary>
//    private void ChangeTotalSwitch()
//    {
//        // 클릭하여 전원을 켤 때
//        if (index[Strings.PROJECTOR_TOTAL] == Strings.OFF)
//        {
//            for (int i = 0; i < thisImg.Length; i++)
//            {
//                thisImg[i].sprite = changeImg;
//                index[i] = Strings.ON;
//                // 전체 버튼을 제외한 나머지 버튼들의 프로젝터들을 켬
//                if(i > Strings.PROJECTOR_TOTAL)
//                {
//                    ipNameInPPC = power[i].name.Substring(18, 2);
//                    Network_Client_Hardware.instance.m_sIP_1 = "192.168.0." + ipNameInPPC;
//                    Debug.Log("NCH.m_sIP_1 : " + Network_Client_Hardware.instance.m_sIP_1);
//                    Network_Client_Hardware.instance.BeamCommand("power = 1 \r");
//                }
//            }
//        }
//        // 클릭하여 전원을 끌 때
//        else if (index[Strings.PROJECTOR_TOTAL] == Strings.ON)
//        {
//            for (int i = 0; i < thisImg.Length; i++)
//            {
//                thisImg[i].sprite = normalImg;
//                index[i] = Strings.OFF;
//                // 전체 버튼을 제외한 나머지 버튼들의 프로젝터들을 끔
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
//    /// 전체 버튼을 제외한 각각의 버튼들이 눌려질 때
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
//    /// 프로젝터 전원 ON or OFF
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
//    /// 각 프로젝터에 알맞는 mac address 선택
//    /// </summary>
//    /// <param name="ProjectorSwitchIndex"></param>
//    /// <param name="powerOnOff"></param>
//    public void ipSelection(int ProjectorSwitchIndex, string powerOnOff)
//    {
//        // 전체 버튼일 때 모든 ip를 구해서 모든 프로젝터 ON
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
//    /// 각각의 인덱스와 전원 여부를 전달 받아 실행시키는 함수
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
