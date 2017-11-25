using System.Net;
using System.IO;

namespace DIploma_repair.LogIn
{
    class InternetConection
    {
        public string GetInetStaus()
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry("dns.msftncsi.com");
                if (entry.AddressList.Length == 0)
                {
                    return "Без доступу до інтернету";
                }
                else
                {
                    if (!entry.AddressList[0].ToString().Equals("131.107.255.255"))
                    {

                        return "Обмежений доступ";
                    }
                }
            }
            catch
            {
                return "Без доступу до інтернету";
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.msftncsi.com/ncsi.txt");
            try
            {
                HttpWebResponse responce = (HttpWebResponse)request.GetResponse();

                if (responce.StatusCode != HttpStatusCode.OK)
                {
                    return "Обмежений доступ";
                }
                using (StreamReader sr = new StreamReader(responce.GetResponseStream()))
                {
                    if (sr.ReadToEnd().Equals("Microsoft NCSI"))
                    {
                        return "Доступ до інтернету";
                    }
                    else
                    {
                        return "Обмежений доступ";
                    }
                }
            }
            catch
            {
                return "Без доступу до інтернету";
            }
        }
    }
}
