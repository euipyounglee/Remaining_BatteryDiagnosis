#-*- coding:utf-8 -*-

import sys
import clr
#import threading
clr.AddReference('System')
clr.AddReference("System.Drawing")
clr.AddReference("System.Windows.Forms")
# clr.AddReference("System.Threading")
#clr.AddReference("System.Timers")
from System import *
from System.Drawing import Point,Size,Color
from System.Windows.Forms import DialogResult, GroupBox, FormBorderStyle,MessageBox
from System.Windows.Forms import Application, Form, Button, Label,TextBox,ComboBox, TabControl,TabPage,Panel,BorderStyle
from System.Diagnostics import Process
# from System.Threading import Threading
#from System.Timers import Timers


# import logger as log

arrayConnectState = ["연결","해제"]
arrayLabelNames = ["ACIR","PNE CTS", "Relay Controller", "BI Controller","Zebra Scanner","절연저항 시험기","Multimeter"]
_mainonFlg = 1
_comlist=["COM0","COM1"] 

def getPorNames():
    try:
        comlist = IO.Ports.SerialPort.GetPortNames()
    except :
        comlist=["COM00","COM01"] 
    return comlist

class MyForm(Form):
    def __init__(self,strTitle=""):
        self.ClientSize=Size(424, 300+110)

        # 창 크기 고정 
        self.FormBorderStyle = FormBorderStyle.FixedSingle
        self.MaximizeBox = False # MinimizeSize 

        if(""== strTitle ) :
            self.Text = "Menu: Setting"
        else:
            self.Text = strTitle #//"Menu: Setting"
        # self.Name = "Hello World"


        self.pn = Panel()
        self.Controls.Add(self.pn)

        self.pn.Name='pn'
        self.pn.Location=Point(16+2+2,68-15)
        self.pn.Size=Size(10, 10)
        self.pn.TabIndex=6
        if 1 == _mainonFlg :
            self.pn.BackColor=Color.FromArgb(255,0,0)


        self.tc = TabControl()
        self.Controls.Add(self.tc)
        self.tc.Name='tc'
        self.tc.TabIndex=4
        self.tc.Location=Point(5,5)
        self.tc.Size=Size(416, 304+50)
        

        self.LayoutTabControl01() 
        self.LayoutTabControl02()

        # //  적용
        self.btnTextApply = Button()
        self.btnTextApply.Text='적용'
        self.btnTextApply.Name='btnTextAdd'
        self.btnTextApply.Location=Point(168,16 + 350)
        self.btnTextApply.Size=Size(80, 32)
        self.btnTextApply.TabIndex=0
        self.btnTextApply.Click += self.onClick_buttonbtnApply
        self.Controls.Add(self.btnTextApply)
        
        # // 취소
        self.btnTextCancel = Button()
        self.btnTextCancel.Text='취소'
        self.btnTextCancel.Name='btnTextCancel'
        self.btnTextCancel.Location=Point(264,16 + 350)
        self.btnTextCancel.Size=Size(80, 32)
        self.btnTextCancel.TabIndex=2
        self.btnTextCancel.Click += self.onClick_buttonbtnCancel
        self.Controls.Add(self.btnTextCancel)

    def  ButtonDevmgmt(self):
        self.btnDevmgnt = Button()
        self.btnDevmgnt.Text='장치관리자...'
        self.btnDevmgnt.Name='btnTextCancel'
        self.btnDevmgnt.Location=Point(260+45,16 - 4)
        self.btnDevmgnt.Size=Size(80+16, 32)
        if  1 == _mainonFlg :
            self.btnDevmgnt.BackColor=Color.FromArgb(255,250,155)# Charp-Call :
        else:
            self.btnDevmgnt.BackColor=Color.FromArgb(102,250,155)# Main-Call : yellow

        self.btnDevmgnt.TabIndex=2
        self.btnDevmgnt.Click += self.onClick_buttonbtnDevmgnt
        self.Page1.Controls.Add(self.btnDevmgnt)

    def LayoutTabControl01(self):
        self.Page1 = TabPage()
        self.Page1.Text='장비연결'
        self.Page1.Size=Size(208, 78)
        self.Page1.Location=Point(4,22)
        self.txtACIRIPBox = TextBox()
        self.txtACIRPortBox = TextBox()
        self.LayoutTabControl01_ACIR(10,50,self.txtACIRIPBox,'192.168.0.100', self.txtACIRPortBox, 23)
        
        self.LayoutTabControl01_PNECTS(10,50+30)

        # pm-grow : Magnetic-Relay
        self.txtRelarCOMBox = ComboBox()#TextBox()
        self.BtnRelay = self.LayoutTabControl01_RelayCtl(10,50+60, self.txtRelarCOMBox,"COM7")
        self.BtnRelay.Click += self.onClick_buttonRelay

        self.txtBICOMBox =  ComboBox()#TextBox()TextBox()
        self.LayoutTabControl01_BICtl(10,50+90,self.txtBICOMBox,"COM4")

        self.txtZebraCOMBox = ComboBox()#T TextBox()
        self.btnZebraScan=self.LayoutTabControl01_ZebraCtl(10,140+30,  self.txtZebraCOMBox,"COM10")
        self.btnZebraScan.Click += self.onClick_buttonbtnZebraScan

        #HIOKI = ST5520
        self.resistanceMeterCOMBox = ComboBox()#TextBox()
        self.BtnResistanceMeter= self.LayoutTabControl01_resistanceMeter(10,170+30,  self.resistanceMeterCOMBox, "COM8")
        self.BtnResistanceMeter.Click += self.onClick_buttonResistanceMeter

        # KEYSIGHT-34461A _ Dight Multimeter
        self.txtMultimeterIPBox = TextBox()
        self.txtMultimeterPortBox = TextBox()
        self.BtnMultimeter = self.LayoutTabControl01_Multimeter(10,200+30, self.txtMultimeterIPBox,"169.254.4.61",self.txtMultimeterPortBox ,5025)
        self.BtnMultimeter.Click += self.onClick_buttonMultimeter

        #버튼 이벤트 선언
        # 장치 관리자 버튼
        self.ButtonDevmgmt()

        #추가 해준다.
        self.tc.Controls.Add(self.Page1)

    def LayoutTabControl01_ACIR(self,left,top,txt_Box1,vText,txt_Box2,nPort):
        self.label = Label()
        self.label.Text = arrayLabelNames[0]# "ACIR"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70
       

        self.textBox1 = txt_Box1 
        self.textBox1.Name='ACIR_IP_txt'
        self.textBox1.Text= vText #'192.168.0.100'
        self.textBox1.Location=Point(140,top)        
        self.textBox1.BackColor=Color.FromArgb(192,255,255)
        self.textBox1.TabIndex=1
        

        self.textBox2 = txt_Box2 
        self.textBox2.Name='ACIR_PORT_txt'
        self.textBox2.Text= str(nPort) #'8005'
        self.textBox2.Location=Point(250,top)
        self.textBox2.BackColor=Color.FromArgb(192,200,200)
        self.textBox2.TabIndex=2
        self.textBox2.Width = 50


        button1 = Button()
        button1.Text='연결'
        button1.Name='connect_ACIR_btn'
        button1.Location=Point(260+45,top-2)
        button1.Size=Size(96, 24)
        button1.TabIndex=3
        if 1 == _mainonFlg :
            button1.ForeColor = Color.FromArgb(255,0,0)

        self.Page1.Controls.Add(self.label)
        self.Page1.Controls.Add(self.textBox1)
        self.Page1.Controls.Add(self.textBox2)
        self.Page1.Controls.Add(button1)
        return  button1
       
    
    def LayoutTabControl01_PNECTS(self,left,top  ):
        self.label = Label()
        self.label.Text = arrayLabelNames[1]#"PNE CTS"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70

        button1 = Button()
        button1.Text='연결'
        button1.Name='connect_PNECTS_btn'
        button1.Location=Point(260+45,top-2)
        button1.Size=Size(96, 24)
        button1.TabIndex=3
        if 1 == _mainonFlg :
            button1.ForeColor = Color.FromArgb(255,0,0)

        self.Page1.Controls.Add(self.label)
        self.Page1.Controls.Add(self.textBox1)
        self.Page1.Controls.Add(self.textBox2)
        self.Page1.Controls.Add(button1)

        return button1
    
    def LayoutTabControl01_RelayCtl(self,left,top ,txt_ComBox1, vText):
        self.label = Label()
        self.label.Text = arrayLabelNames[2]#"Relary Controller"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70
        
        combobox = txt_ComBox1 #ComboBox()
        combobox.Location = Point(140,top)   
        combobox.Size=Size(96, 24)
        combobox.BackColor=Color.FromArgb(192,255,255)
        combobox.Items.AddRange(_comlist)
        self.comboSelect(combobox,vText)#"COM6"
        
        button1 = Button()
        button1.Text=arrayConnectState[0]+"-ok"#'연결'
        button1.Name='Relary_COM_btn'
        button1.Location=Point(260+45,top-2)
        button1.Size=Size(96, 24)
        
        if 1 == _mainonFlg :
            button1.ForeColor = Color.FromArgb(255,0,0)

        # self.btnpage.TabIndex=3

        self.Page1.Controls.Add(self.label)
        # self.Page1.Controls.Add(self.textBox)
        self.Page1.Controls.Add(combobox)
        self.Page1.Controls.Add(button1)

        return  button1

    def comboSelect(self, comboBox ,strPort ):
        index = 0
        if comboBox.Items.Count > 0 :
            index =comboBox.Items.IndexOf(strPort) #"COM8")

        if index > 0 :
            comboBox.SelectedIndex = index #선택시키기
        else:
            index = 0
            # comboBox.SelectedIndex = 0
        return index


    def LayoutTabControl01_BICtl(self,left,top ,txt_ComBox1,vText):
        self.label = Label()
        self.label.Text = arrayLabelNames[3]# "BI Controller"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70
        
        combobox = txt_ComBox1 #ComboBox()
        combobox.Location = Point(140,top)   
        combobox.Size = Size(96, 24)
        combobox.BackColor = Color.FromArgb(192,255,255)
        combobox.Items.AddRange(_comlist)
        self.comboSelect(combobox,vText)#"COM6")
        

        
        button1 = Button()
        button1.Text='연결'
        button1.Name='BI_COM_btn'
        button1.Location=Point(260+45,top-2)
        button1.Size=Size(96, 24)
        button1.TabIndex=3
        if 1 == _mainonFlg :
            button1.ForeColor = Color.FromArgb(255,0,0)

        self.Page1.Controls.Add(self.label)
        # self.Page1.Controls.Add(self.textBox)
        self.Page1.Controls.Add(txt_ComBox1)
        self.Page1.Controls.Add(button1)
        return button1

    def LayoutTabControl01_ZebraCtl(self,left, top , txt_ComBox1,vText):
        self.label = Label()
        self.label.Text = arrayLabelNames[4] #"Zebra Scanner"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70
        
        combobox = txt_ComBox1 #ComboBox()
        combobox.Location = Point(140,top)   
        combobox.Size = Size(96, 24)
        combobox.BackColor = Color.FromArgb(192,255,255)
        combobox.Items.AddRange(_comlist)
        self.comboSelect(combobox,vText)#"COM6")
        
        

        button1 = Button()
        button1.Text='연결'
        button1.Name='Zebra_COM_btn'
        button1.Location=Point(260+45,top-2)
        button1.Size=Size(96, 24)
        button1.TabIndex=3
        if 1 == _mainonFlg :
            button1.ForeColor = Color.FromArgb(255,0,0)

        self.Page1.Controls.Add(self.label)
        self.Page1.Controls.Add(txt_ComBox1)
        self.Page1.Controls.Add(button1)

        return button1

    # //절연저항계
    def LayoutTabControl01_resistanceMeter(self,left, top , txt_ComBox1,vText):
        self.label = Label()
        self.label.Text = arrayLabelNames[5]  #"절연저항 시험기"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70
        
        combobox = txt_ComBox1 #ComboBox()
        combobox.Location = Point(140,top)   
        combobox.Size = Size(96, 24)
        combobox.BackColor = Color.FromArgb(192,255,255)
        combobox.Items.AddRange(_comlist)
        self.comboSelect(combobox,vText)#"COM6")

        button1 = Button()
        button1.Text=arrayConnectState[0]+"-ok"#'연결'
        button1.Name='resistanceMeter_COM_btn'
        button1.Location=Point(260+45,top-2)
        button1.Size=Size(96, 24)
        button1.TabIndex=3
        if 1 == _mainonFlg :
            button1.ForeColor = Color.FromArgb(255,0,0)

        self.Page1.Controls.Add(self.label)
        # self.Page1.Controls.Add(self.textBox1)
        self.Page1.Controls.Add(combobox)
        self.Page1.Controls.Add(button1)
        return button1

    def LayoutTabControl01_Multimeter(self,left,top,txt_Box1,vText,txt_Box2,nPort):
        self.label = Label()
        self.label.Text = arrayLabelNames[6]  #"Multimeter"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70
        

        self.textBox1 = txt_Box1 #TextBox()
        self.textBox1.Name='ACIR_IP_txt'
        self.textBox1.Text= vText #'192.168.0.100'
        self.textBox1.Location=Point(140,top)        
        self.textBox1.BackColor=Color.FromArgb(192,255,255)
        self.textBox1.TabIndex=1
        

        self.textBox2 = txt_Box2 #TextBox()
        self.textBox2.Name='ACIR_PORT_txt'
        self.textBox2.Text= str(nPort) #'8005'
        self.textBox2.Location=Point(250,top)
        self.textBox2.BackColor=Color.FromArgb(192,200,200)
        self.textBox2.TabIndex=2
        self.textBox2.Width = 50

        button1 = Button()
        button1.Text=arrayConnectState[0]+"-ok"#'연결'
        button1.Name='connect_Multimeter_btn'
        button1.Location=Point(260+45,top-2)
        button1.Size=Size(96, 24)
        button1.TabIndex=3
        if 1 == _mainonFlg :
            button1.ForeColor = Color.FromArgb(255,0,0)

        self.Page1.Controls.Add(self.label)
        self.Page1.Controls.Add(self.textBox1)
        self.Page1.Controls.Add(self.textBox2)
        self.Page1.Controls.Add(button1)

        return  button1

    def onClick_buttonbtnZebraScan(self,sender, args):
        self.txtZebraBox.Text= "vText" #'192.168.0.100'

    def onClick_buttonbtnCancel(self, sender, args):
        print("Cancel")
        Application.Exit()

    def conigWrite(self):
        print("ConfigWrite...Apply") 

    def onClick_buttonbtnApply(self, sender, args):
        print("Apply")
        self.conigWrite()
        Application.Exit()

    def LayoutTabControl02(self):
        self.Page2 = TabPage()
        self.Page2.Text='화면설정'
        self.Page2.Location=Point(4,22)
        self.Page2.Size=Size(208, 78)
        
        self.txtDashBoardIPBox = TextBox()
        self.txtDashBoardPortBox = TextBox()
        self.BtnDashBoard = self.LayoutTabControl02_DashBoard(10,50, self.txtDashBoardIPBox,"192.168.30.1",self.txtDashBoardPortBox ,8005)
        self.BtnDashBoard.Click += self.onClick_buttonDashBoard

        # self.Page2.Controls.Add(self.btnPage2)
        self.tc.Controls.Add(self.Page2)

    def LayoutTabControl02_DashBoard(self,left,top,txt_Box1,vText,txt_Box2,nPort):
        self.label = Label()
        self.label.Text = "DashBoard IP"
        self.label.Location = Point(left, top+3)
        self.label.Height = 30
        self.label.Width = 40+70
        
        self.textBox1 = txt_Box1 #TextBox()
        self.textBox1.Name='DashBoard_IP_txt'
        self.textBox1.Text= vText #'192.168.0.100'
        self.textBox1.Location=Point(140,top)        
        self.textBox1.BackColor=Color.FromArgb(192,255,255)
        self.textBox1.TabIndex=1
        

        self.textBox2 = txt_Box2 #TextBox()
        self.textBox2.Name='BashBoard_PORT_txt'
        self.textBox2.Text= str(nPort) #'8005'
        self.textBox2.Location=Point(250,top)
        self.textBox2.BackColor=Color.FromArgb(192,200,200)
        self.textBox2.TabIndex=2
        self.textBox2.Width = 50
        btnpage = Button()
        btnpage.Text='연결체크'
        btnpage.Name='connect_DashBoard_btn'
        btnpage.Location=Point(260+45,top-2)
        btnpage.Size=Size(96, 24)
        btnpage.TabIndex=3

        self.Page2.Controls.Add(self.label)
        self.Page2.Controls.Add(self.textBox1)
        self.Page2.Controls.Add(self.textBox2)
        self.Page2.Controls.Add(btnpage)
        return  btnpage

    def CSharpConnectFuncCallTCP(self,vIpadress,vPort):
        result = ""
        try:
            ip_str = "{0}:{1}".format(vIpadress,vPort)
            # MessageBox.Show("call:" + ip_str)
            result = ConnectFuncCallTCP(ip_str)
        except :
            result="\nno call function error"
        return result

    def CSharpConnectFuncCallRS232C(self,vPort):
        result = ""
        try:
            comPort_str = "{0}".format(vPort)
            # MessageBox.Show("call:" + comPort_str)
            result = ConnectFuncCallRs232c(comPort_str)
        except :
            result="\nno call function error"
        return result

    def CSharpConnectFuncCallUSB2CAN(self,vPort):
        result = ""
        try:
            comPort_str = "{0}".format(vPort)
            # MessageBox.Show("call:" + comPort_str)
            result = ConnectFuncCallUSB2CAN(comPort_str)
        except :
            result="\nno call function error"
        return result

    def CSharpConnectFuncCallWebSocket(self,vIpadress,vPort):
        result = ""
        try:
            ip_str = "{0}:{1}".format(vIpadress,vPort)
            # MessageBox.Show("call:" + ip_str)
            result = ConnectFuncWebSocket(vIpadress,vPort)
        except :
            result="\nno call function error"
        return result


    def onClick_buttonDashBoard(self, sender, args):
        wsIP  = self.txtDashBoardIPBox.Text #"169.254.4.61"
        wsPort = self.txtDashBoardPortBox.Text # 5024 
        nPort=int(wsPort)
        self.CSharpConnectFuncCallWebSocket(wsIP,nPort)
        MessageBox.Show("연결체크:"+wsIP+ ":"+wsPort) 

    def my_func(self,ip):
        result = ""
        try:
            result = ConnectFuncCallTCP(ip)
            self.BtnMultimeter.Text = result #"해제"
            # self.timer.cancel()
            MessageBox.Show("200: Timer Start: OK" + result)
        except :
            result="\nno call function error"
        return result


    def onClick_buttonMultimeter(self, sender, args):
        # MessageBox.Show("Hello World")
        ip  = self.txtMultimeterIPBox.Text #"169.254.4.61"
        port = self.txtMultimeterPortBox.Text # 5024 
        result  = self.CSharpConnectFuncCallTCP(ip,port)
        MessageBox.Show("Hello World:" + result)
        # 값이 == 200 이면 타이머 동작하여 값을 갱신한다. self.BtnMultimeter.Text
        if "200" == result :
            MessageBox.Show("200: Timer Start:" + result) #시간 지연시킴 => 타이머 사용해야 할듯
            self.my_func("")
            # self.timer = threading.Timer(3,self.my_func("")) #.start()
            # self.timer.start()
        else:
            length = len(result)
            if 20 >= length  and length > 0 :
                self.BtnMultimeter.Text = result #"해제"
                # self.txtMultimeterIPBox.Enabled = False #아이피
                # self.txtMultimeterPortBox.Enabled = False #포트
            else :
                self.txtMultimeterIPBox.Enabled = True 
                self.txtMultimeterPortBox.Enabled =True
                self.BtnMultimeter.Text = arrayConnectState[0] #"연결" #"해제"
                MessageBox.Show(result,"Setting-TCP/IP Error:" + str(len(result)))

    def onClick_buttonResistanceMeter(self, sender, args):
        comPort  = self.resistanceMeterCOMBox.Text 
        result = self.CSharpConnectFuncCallRS232C(comPort)
        # MessageBox.Show("절연저항 측정값:"+ str(len(result)))
        length = len(result)
        if 13 >= length  and length > 0 :
            self.BtnResistanceMeter.Text = result #"해제"
        else:
            self.resistanceMeterCOMBox.Enabled = True 
            self.BtnResistanceMeter.Text = arrayConnectState[0] #"연결" #"해제"
            MessageBox.Show(result,"Setting-RS232c Error:" + str(len(result)))

    def onClick_buttonRelay(self, sender, args):
        comPort  = self.txtRelarCOMBox.Text 
        result = self.CSharpConnectFuncCallUSB2CAN(comPort)
        # MessageBox.Show("마그네틱 릴레이:"+ str(len(result)))
        length = len(result)
        if 13 >= length  and length > 0 :
            self.BtnRelary.Text = result #"해제"
            # self.txtRelarCOMBox.Enabled = False 
        else:
            self.txtRelarCOMBox.Enabled = True 
            self.BtnRelay.Text = arrayConnectState[0] #"연결" #"해제"
            MessageBox.Show(result,"Setting-USB2CAN Error:" + str(len(result)))

    def onClick_buttonbtnDevmgnt(self,sender,args):
        # MessageBox.Show("장치 관리자 ")
        Process.Start("devmgmt.msc")

    def run(self):
        Application.Run(self)

    def webServerConnect(var1 ,var2):
        print("log...")
def Apply_func(var2):
    return True

def GetCallTitle():
    _title = ""
    try:
        _title = GetTitleSetting()
    except :
        result="\nNo call function error"
        print(result)
    return _title


def main():
    myform = MyForm()
    Application.Run(myform) 
    
if __name__ == '__main__':
    _mainonFlg = 1
    _comlist= getPorNames()
    main()
else :
    _mainonFlg = 0
    title=GetCallTitle()
    _comlist= getPorNames()
    myform = MyForm(title)
    Application.Run(myform)
