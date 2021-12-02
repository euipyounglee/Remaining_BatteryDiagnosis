#-*- coding:utf-8 -*-
from flask import Flask, render_template, request
import json
import os
import logging
import webbrowser
import psutil
from threading import Timer
import socket
import sys



# Gets or creates a logger
logger = logging.getLogger(__name__)  

# set log level
logger.setLevel(logging.WARNING)    
#fileMaxByte =1024 * 1024*100
#fileHandler = logging.handlers.RotatingFileHandler(filename='./logfile.log',maxBytes=fileMaxByte, backupCount=10)

#handler 생성
#streamHandler = logging.StreamHandler()
#fileHandler.setFormatter(formatter) 

#Exe 실행파일 만들때, 경로 찾기. 중요
if getattr(sys, 'frozen', False)  :
    program_directory = os.path.dirname(os.path.abspath(sys.executable))
    template_path = os.path.join(program_directory, 'templates')
    static_path = os.path.join(program_directory, 'static')
    app = Flask(__name__, template_folder=template_path,static_folder=static_path)
else:
    app = Flask(__name__)

 
@app.route('/')
def start_root():
   return render_template('index.html')
 
@app.route('/pbook',methods = ['POST', 'GET'])
def result():
   if request.method == 'POST':
      val = request.form #addrbook.html에서 name을 통해 submit한 값들을 val 객체로 전달
      return render_template("index_result.html",result = val) #name은 key, name에 저장된 값은 value

def processTimeBrowser(port):
    webprcess = configLoad("webbrowser")#webBrowseer 이름 체크한다.
    if "" == webprcess : webprcess = "BPDiagnosis.exe"

    for proc in psutil.process_iter():
      # 프로세스 이름을 ps_name에 할당
      ps_name = proc.name()
      try:
        if webprcess == ps_name :
            return
        else :
            # 실행 명령어와 인자(argument)를 리스트 형식으로 가져와 cmdline에 할당
            cmdline = proc.cmdline()
      except:
        print('except NAME:...')

      print('NAME:', ps_name, ' CMD:', cmdline)

    Timer(1,open_browser(port)).start()        

def myIp():
    ip= socket.gethostbyname(socket.gethostname()) 
    print(ip)
    return ip

def open_browser(port):
    ip = myIp()
    site='http://'+ip +":"
    localhost = site + str(port)+'/'
    print(localhost)
    webbrowser.open_new(localhost) 

def is_json_key_present(json, key):
    try:
        buf = json[key]
    except KeyError:
        return False

    return True
    
    
def configLoad(key):
    configFile = "config.json"
    value = ""
    if getattr(sys, 'frozen', False)  :
        program_directory = os.path.dirname(os.path.abspath(sys.executable)) #실행파일일 경우,> 경로 얻기 
        json_stringFile = program_directory +"\\" + configFile 
    else:
        json_stringFile = os.getcwd() +"\\" + configFile 
    
    logging.info("json_stringFile = {} ".format(json_stringFile))
    logger.info('An info message')

    if os.path.isfile(json_stringFile) : # 파일존재 확인
      with open(json_stringFile) as f: # 설정파일 읽어 들이기
        json_object = json.load(f)
        if is_json_key_present(json_object,key) == False : return value
        json_object[key] 
        value = json_object[key]
        
    return value
def logSetting():

    logging.basicConfig(filename='./logfile.log', level=logging.INFO)
    file_handler = logging.FileHandler('logfile.log')
    formatter  = logging.Formatter('%(asctime)s : %(levelname)s : %(name)s : %(message)s')
    file_handler.setFormatter(formatter)

def main():
    nPort = configLoad("port")
    if "" == nPort : nPort = 80
    if configLoad("preview") == True:
      processTimeBrowser(nPort)
    app.run(host='0.0.0.0', port = nPort, debug = True)

if __name__ == '__main__':
   logSetting()
   logging.info("__main__")
   main()
else:
   logSetting()
   logging.info("export __main__")
   main()

