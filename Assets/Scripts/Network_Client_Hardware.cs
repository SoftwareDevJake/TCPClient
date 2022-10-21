using System;
using System.Collections;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Network_Client_Hardware : MonoBehaviour
{
    #region private members _Contents   
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion
    
    //public static Network_Client_Hardware instance;

    public Button[] projectorPowerControl;
    private string ipName;

    //public Network_RecieveMgr m_sc_uiDisplay;
    public string m_sIP_1; // ip number of projector
    int m_iPort = 7000;

    string m_strInitCode = string.Empty;

    bool m_isConnected = false;
    [HideInInspector] public bool m_isThreadRunning = false;

    /// <summary>
    /// ProjectorPowerControl.cs
    /// </summary>
    /// public int ProjectorSwitchIndex;
    public int ProjectorSwitchIndex;

    public Image[] thisImg;

    public Sprite normalImg;
    public Sprite changeImg;

    public static int[] index = new int[8];

    private string ipNameInPPC;
    public GameObject[] power;

    private string powerOnOff;

    private bool AllProjectorsAreOn = false;
    public static bool Projector1 = false;
    public static bool Projector2 = false;
    public static bool Projector3 = false;
    public static bool Projector4 = false;
    public static bool Projector5 = false;
    public static bool Projector6 = false;
    public static bool Projector7 = false;
    
    // Use this for initialization    
    //void Awake()
    //{
    //    instance = this;
    //}

    public void Beam(bool _doOn)
    {
        StartCoroutine(co_beam2(_doOn));
    }
    
    IEnumerator co_beam2(bool _doOn)
    {
        ConnectToTcpServer();
        yield return new WaitForSeconds(1f);
        if (_doOn)
            BeamCommand("power=0");
        else
            BeamCommand("POF");
    }
    public void BeamCommand(string _strCommand)
    {
        //string header = "00";
        string command = "*" + _strCommand + "<CR>";
        //byte[] end = new byte[] { 0x0d };
        // string strEnd = System.Text.Encoding.ASCII.GetString(command);
        //string hash = "admin:admin:" + m_strInitCode.Substring(0, 8);
        //* Power = 0
        //ClientSendMessage(/*MD5Hash(hash) +*/ header + command + strEnd);
        ClientSendMessage(command);
        Debug.Log("command : " + command);
        //Debug.Log("m_sIP_1 in BeamCommand: " + m_sIP_1);
        
        
    }

    //string MD5Hash(string str)
    //{
    //    StringBuilder MD5Str = new StringBuilder();
    //    byte[] byteArr = Encoding.ASCII.GetBytes(str);
    //    byte[] resultArr = (new MD5CryptoServiceProvider()).ComputeHash(byteArr);

    //    for (int i = 0; i < resultArr.Length; i++)
    //    {
    //        MD5Str.Append(resultArr[i].ToString("X2"));
    //    }
    //    return MD5Str.ToString();
    //}
    private void Awake()
    {
        if (ProjectorSwitchIndex > 0)
        {
            ConnectToTcpServer();
        }
    }

    IEnumerator CheckPower()
    {
        if (ProjectorSwitchIndex > 0)
            BeamCommand("status ?"); // 전원이 켜져있는지 아닌지 확인
        yield return new WaitForSeconds(0.3f);

        AllProjectorsAreOn = Projector1 && Projector2 && Projector3 && Projector4 && Projector5 && Projector6 && Projector7; // 모든 프로젝터들이 켜져 있을 경우

        if (AllProjectorsAreOn)
        {
            for(int i = 0; i < thisImg.Length; i++)
            {
                thisImg[i].sprite = changeImg;
                index[i] = Strings.ON;
            }
        }
        else
        {
            if (Projector1)
            {
                thisImg[Strings.PROJECTOR_1].sprite = changeImg;
                index[Strings.PROJECTOR_1] = Strings.ON;
            }
            if (Projector2)
            {
                thisImg[Strings.PROJECTOR_2].sprite = changeImg;
                index[Strings.PROJECTOR_2] = Strings.ON;
            }
            if (Projector3)
            {
                thisImg[Strings.PROJECTOR_3].sprite = changeImg;
                index[Strings.PROJECTOR_3] = Strings.ON;
            }
            if (Projector4)
            {
                thisImg[Strings.PROJECTOR_4].sprite = changeImg;
                index[Strings.PROJECTOR_4] = Strings.ON;
            }
            if (Projector5)
            {
                thisImg[Strings.PROJECTOR_5].sprite = changeImg;
                index[Strings.PROJECTOR_5] = Strings.ON;
            }
            if (Projector6)
            {
                thisImg[Strings.PROJECTOR_6].sprite = changeImg;
                index[Strings.PROJECTOR_6] = Strings.ON;
            }
            if (Projector7)
            {
                thisImg[Strings.PROJECTOR_7].sprite = changeImg;
                index[Strings.PROJECTOR_7] = Strings.ON;
            }
        }
    }

    private void Start()
    {
        StartCoroutine(CheckPower());
    }


    private void OnApplicationQuit()
    {
        QuitThread();
    }

    void QuitThread()
    {
        if (clientReceiveThread != null)
            clientReceiveThread.Abort();
        m_isThreadRunning = false;

        if (socketConnection != null)
            socketConnection.Close();
        socketConnection = null;
    }

    /// <summary>    
    /// Setup socket connection.    
    /// </summary>    
    public void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary>    
    /// Runs in background clientReceiveThread; Listens for incomming data.    
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            //Debug.Log("m_sIP_1 in ListenForData: " + m_sIP_1);
            m_isThreadRunning = true;
            socketConnection = new TcpClient(m_sIP_1, m_iPort);
            Byte[] bytes = new Byte[1024];
            while (m_isThreadRunning)// Add your exit flag here
            {
                // Get a stream object for reading             
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    if (!m_isConnected)
                    {
                        m_isConnected = true;
                        //Network_RecieveMgr.instance.ConnectionState(CODE_CONNECTION.SUCCEED);
                    }
                    // Read incomming stream into byte arrary.                
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        //Debug.Log("$$$$$$$$$$$$$$$$$$$$$$$");
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message.                   
                        string serverMessage = Encoding.UTF8.GetString(incommingData);
                        Debug.Log("server message received as: " + serverMessage); // 서버로 부터의 응답 + 여기부분에서 프로젝터의 상태 확인 가능 ***

                        if (serverMessage.Contains("ack status = 2") && m_sIP_1 == "192.168.0.21")
                        {
                            Projector1 = true;
                        }
                        if (serverMessage.Contains("ack status = 2") && m_sIP_1 == "192.168.0.23")
                        {
                            Projector2 = true;
                        }
                        if (serverMessage.Contains("ack status = 2") && m_sIP_1 == "192.168.0.24")
                        {
                            Projector3 = true;
                        }
                        if (serverMessage.Contains("ack status = 2") && m_sIP_1 == "192.168.0.22")
                        {
                            Projector4 = true;
                        }
                        if (serverMessage.Contains("ack status = 2") && m_sIP_1 == "192.168.0.25")
                        {
                            Projector5 = true;
                        }
                        if (serverMessage.Contains("ack status = 2") && m_sIP_1 == "192.168.0.26")
                        {
                            Projector6 = true;
                        }
                        if (serverMessage.Contains("ack status = 2") && m_sIP_1 == "192.168.0.27")
                        {
                            Projector7 = true;
                        }


                        //m_sc_uiDisplay.m_sPath = serverMessage;
                        //m_sc_uiDisplay.m_bRecieve = true;
                        //Network_RecieveMgr.instance.RecieveData(serverMessage);
                        if (serverMessage.Contains("NTCONTROL "))
                        {
                            m_strInitCode = serverMessage.Split(' ')[2];
                        }
                    }
                }
            }
        }
        catch (Exception socketException)
        {
            Debug.Log("Socket exception: " + socketException);
            m_isConnected = false;
            m_isThreadRunning = false;
            //Network_RecieveMgr.instance.ConnectionState(CODE_CONNECTION.FAILED);
        }
    }
    /// <summary>    
    /// Send message to server using socket connection.    
    /// </summary>    
    public void ClientSendMessage(string _strMessage)
    {
        
        if (socketConnection == null)
        {
            Debug.Log("Socket exception: " + socketConnection);
            BeamCommand("status ?");
            return;
        }
        try
        {
            // Get a stream object for writing.          
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                //string clientMessage = "This is a message from one of your clients.";
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(_strMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
                Debug.Log("_strMessage : " + _strMessage);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
        catch (InvalidOperationException e)
        {
            Debug.Log("InvalidOperationException " + e);
            //ConnectToTcpServer();
            //AppMain.instance.m_isHardwareOn = false;
            //AppMain.instance.OnHardwareUI();
            //ClientSendMessage(_strMessage);
        }
    }

    public void OnButtonClicked()
    {
        changeEachSwitch(); // 각각의 버튼들
        int j = 0;
        for(int i = 1; i < thisImg.Length; i++)
        {

            if (index[i] == Strings.ON)
            {
                j++;
            }
        }
        if(j == 7)
        {
            thisImg[0].sprite = changeImg;
            index[0] = Strings.ON;
            j = 0;
        }
        else
        {
            thisImg[0].sprite = normalImg;
            index[0] = Strings. OFF;
        }
    }

    /// <summary>
    /// 전체 버튼이 눌릴 때 (PROJECTORTOTAL = 0)
    /// </summary>
    public void ChangeTotalSwitch()
    {
        // 클릭하여 전원을 켤 때
        if (index[Strings.PROJECTOR_TOTAL] == Strings.OFF)
        {
            for (int i = 0; i < thisImg.Length; i++)
            {
                if(i > 0 && index[i] != Strings.ON)
                {
                    thisImg[0].sprite = changeImg;
                    index[0] = Strings.ON;

                    projectorPowerControl[i].onClick.Invoke();
                }
            }
        }
        // 클릭하여 전원을 끌 때
        else if (index[Strings.PROJECTOR_TOTAL] == Strings.ON)
        {
            for (int i = 0; i < thisImg.Length; i++)
            {
                if (i > 0 && index[i] != Strings.OFF)
                {
                    thisImg[0].sprite = normalImg;
                    index[0] = Strings.OFF;

                    projectorPowerControl[i].onClick.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// 전체 버튼을 제외한 각각의 버튼들이 눌려질 때
    /// </summary>
    private void changeEachSwitch()
    {
        for (int i = 1; i < thisImg.Length; i++)
        {

            if (ProjectorSwitchIndex == i)
            {
                OnOrOff();
            }
        }
    }
    /// <summary>
    /// 프로젝터 전원 ON or OFF
    /// </summary>
    public void OnOrOff()
    {
        if (index[ProjectorSwitchIndex] == Strings.OFF)
        {
            thisImg[ProjectorSwitchIndex].sprite = changeImg;
            powerOnOff = Strings.PROJECTOR_POWER_ON;
            index[ProjectorSwitchIndex] = Strings.ON;
            BeamCommand(powerOnOff);
        }
        else if (index[ProjectorSwitchIndex] == Strings.ON)
        {
            thisImg[ProjectorSwitchIndex].sprite = normalImg;
            index[ProjectorSwitchIndex] = Strings.OFF;
            powerOnOff = Strings.PROJECTOR_POWER_OFF;
            BeamCommand(powerOnOff);
        }
    }
}