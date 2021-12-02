using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BatteryDashBoard
{
    class CMailMessage
    {
        public void mailSend()
        {
         //   MailMessage mail = new MailMessage();
            try
            {
                // 보내는 사람 메일, 이름, 인코딩(UTF-8)
#if false
                //mail.From = new MailAddress("nowonbun@gmail.com", "명월일지", System.Text.Encoding.UTF8);
#else
                string address= "lepdev@pmgrow.co.kr";// +
                string displayName = "명월일지";

                //mail.From = new MailAddress(address, displayName ,  System.Text.Encoding.UTF8);
#endif
                //// 받는 사람 메일
                //mail.To.Add("lepdev@pmgrow.co.kr");
                //// 참조 사람 메일
                //mail.CC.Add("storkegg@gmail.com");
                //// 비공개 참조 사람 메일
                //mail.Bcc.Add("lepdev@pmgrow.co.kr");
                //// 메일 제목
                //mail.Subject = "메일 제목";
                //// 본문 내용
                //mail.Body = "<html><body>hello wrold</body></html>";
                //// 본문 내용 포멧의 타입 (true의 경우 Html 포멧으로)
                //mail.IsBodyHtml = true;
                //// 메일 제목과 본문의 인코딩 타입(UTF-8)
                //mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                // 첨부 파일 (Stream과 파일 이름)
#if false

                mail.Attachments.Add(new Attachment(new FileStream(@"D:\test1.zip", FileMode.Open, FileAccess.Read), "test1.zip"));
                mail.Attachments.Add(new Attachment(new FileStream(@"D:\test2.zip", FileMode.Open, FileAccess.Read), "test2.zip"));
#else

                if (File.Exists(@"D:\test1.zip") == true)
                {

                 //   mail.Attachments.Add(new Attachment(new FileStream(@"D:\test1.zip", FileMode.Open, FileAccess.Read), "test1.zip"));
                  //  mail.Attachments.Add(new Attachment(new FileStream(@"D:\test2.zip", FileMode.Open, FileAccess.Read), "test2.zip"));
                }

#endif
                // smtp 서버 주소
             //   SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                // smtp 포트
          //      SmtpServer.Port = 587;
                // smtp 인증

                string userName = "lepdev@pmgrow.co.kr";
                string password ="210918!pm";
           //     SmtpServer.Credentials = new System.Net.NetworkCredential(userName, password);
                // SSL 사용 여부
                //if(25 != SmtpServer.Port)
                //    SmtpServer.EnableSsl = true;
                //// 발송
                //SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("메일발송실패");
            }
            finally
            {
                // 첨부 파일 Stream 닫기
                //foreach (var attach in mail.Attachments)
                //{
                //    attach.ContentStream.Close();
                //}
            }
        }


    }
}
