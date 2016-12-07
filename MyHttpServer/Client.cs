using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace MyHttpServer
{
    class Client
    {
        private int count = 0;
        private MySqlConnectionController menController;
        private String pathToFolder = "C:/Users/Rostyslav/Desktop/MyHttpServer/MyHttpServer/MyHttpServer/bin/Debug/www/";
        private void SendError(TcpClient Client, int Code)
        {
            string CodeStr = Code.ToString() + " " + ((HttpStatusCode)Code).ToString();
            string Html = CodeStr;
            string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            Client.GetStream().Write(Buffer, 0, Buffer.Length);
            Client.Close();
        }


        public Client(TcpClient Client)
        {

            //try
            //{
            //    connection = new MySqlConnection(myConnectionString);
            //    connection.Open();
            //    command = connection.CreateCommand();
            //    command.CommandText = "Insert into man (name) values('hahaha')";
                
            //    //command.Prepare();
            //    command.ExecuteNonQuery();
            //    Console.WriteLine("connected");
            //}
            //catch (MySqlException e)
            //{
            //    Console.Write("Error:" + e.ToString());
            //}
           
            

            string Request = "";


            byte[] Buffer = new byte[1024];

            int Count;

            while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
            {

                Request += Encoding.ASCII.GetString(Buffer, 0, Count);

                if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                {
                    break;
                }
            }


            Match ReqMatch = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");


            if (ReqMatch == Match.Empty)
            {

                SendError(Client, 400);
                return;
            }


            string RequestUri = ReqMatch.Groups[1].Value;


            RequestUri = Uri.UnescapeDataString(RequestUri);


            if (RequestUri.IndexOf("..") >= 0)
            {
                SendError(Client, 400);
                return;
            }

            if (RequestUri.EndsWith("/"))
            {
                RequestUri += "index.html";
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                if (RequestUri.ToString().Split('/')[1] == "createElement")
                {
                    string name = Request.ToString();
                    int foundSymb = 10;
                    char splitchar = (char)foundSymb;
                    string[] findValue = name.Split(' ');
                    //Console.WriteLine(name);
                    //Console.WriteLine("-----------------------------------------");
                    //Console.WriteLine("-----------------------------------------");
                    //Console.WriteLine("-----------------------------------------");
                    //Console.WriteLine("---------------------------------My input");
                    for (int i = 0; i < findValue.Length; i++)
                    {
                        //можна скільки хочеш інпутів добавляти і нічого не міняти в коді, оскільки Response.ToString() в останніх рядках видасть
                        //всі імена наших інпутів = 'значення, що ми ввели в даному інпуті' без пробілів а через знак &, тому ми отримаємо їх в ряд
                        //але перед тим там буде ще якійсь значення в 1 рядку і потім порожній рядок і аж потім вже рядок з нашими всіма інпутами
                        //тому нам потрібно ще поділити стрічку символом з кодом 10 і взяти останній елемент
                        if (findValue[i].Contains("nameThis"))
                        {
                            
                            string[] values = findValue[i].Split(splitchar);
                            //взяли цей останній елемент і поділили на &, отримали пари з ключем - назва інпуту та значенням - тим, що ми ввели
                            string[] filterToGroup = values[values.Length - 1].Split('&');
                            string[] fields = new string[filterToGroup.Length];

                            for (int j = 0; j < filterToGroup.Length; j++)
                            {
                                fields[j] = filterToGroup[j].Split('=')[1];
                                //Console.WriteLine(filterToGroup[j]);
                            }
                            Man man = new Man(fields[0], fields[1], int.Parse(fields[2]));
                            //Console.WriteLine(man.Name + " " + man.Lastname + " " + man.Age);
                            menController = new MySqlConnectionController();
                            menController.add(man);
                                   
                            string head = "<!DOCTYPE HTML/>\r\n<html><head><title>My Table</title><link href = 'style1.css' type = 'text/css' rel = 'stylesheet'/> <link rel = 'stylesheet' href = 'style960.css'/></head>";
                            string body = "<body><p>Hellooooooo</p></div>";
                            List<Man> men = menController.findAll();

                            CubeGenerator cube = new CubeGenerator(3, men);
                            cube.generateFacet(0);
                            cube.generateFacet(1);
                            cube.generateFacet(2);
                            cube.generateFacet(3);

                            Console.Write("-------------------------------------------------------------\n");
                            Console.Write("-------------------------------------------------------------\n");
                            Console.Write("-------------------------------------------------------------\n");
                            
                            SectionFormer sectionFormer = new SectionFormer(cube);
                            int id = 56;
                            int index = menController.findIndexOf(56);
                            Console.WriteLine("Index: " + (index-1));
                            sectionFormer.generateSection(index-1);

                         

                            foreach (Man m in men)
                            {
                                body+= "<p>___________________________________________</p>"; 
                                body += "<p>Name: " + m.Name + "</p>";
                                body += "<p>Last name: " + m.Lastname + "</p>";
                                body += "<p>Age: " + m.Age + "</p>";
                                body += "<p>___________________________________________</p>";
                            }

                            string bodyFinish = "</body></html>";
                            menController.close();
                            string pathToFile = pathToFolder+"info.html";
                            File.WriteAllText(pathToFile, head+body+bodyFinish);     
                            RequestUri = "info.html";
                            break;
                        }
                    }
                    //Console.WriteLine("Create element");
                }

                
            }
            catch (Exception ex)
            {

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string FilePath = "www/" + RequestUri;


            if (!File.Exists(FilePath))
            {
                SendError(Client, 404);
                return;
            }


            string Extension = RequestUri.Substring(RequestUri.LastIndexOf('.'));


            string ContentType = "";

            switch (Extension)
            {
                case ".html":
                    ContentType = "text/html";
                    break;
                case ".css":
                    ContentType = "text/css";
                    break;
                case ".js":
                    ContentType = "text/javascript";
                    break;
                case ".jpg":
                    ContentType = "image/jpeg";
                    break;
                case ".jpeg":
                case ".png":
                case ".gif":
                    ContentType = "image/" + Extension.Substring(1);
                    break;
                default:
                    if (Extension.Length > 1)
                    {
                        ContentType = "application/" + Extension.Substring(1);
                    }
                    else
                    {
                        ContentType = "application/unknown";
                    }
                    break;
            }


            FileStream FS;
            try
            {
                FS = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {

                SendError(Client, 500);
                return;
            }


            string Headers = "HTTP/1.1 200 OK\nContent-Type: " + ContentType + "\nContent-Length: " + FS.Length + "\n\n";
            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
            Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);


            while (FS.Position < FS.Length)
            {

                Count = FS.Read(Buffer, 0, Buffer.Length);

                Client.GetStream().Write(Buffer, 0, Count);
            }
            //count++;
            //Console.WriteLine(count);
            FS.Close();
            Client.Close();
        }
    }
}
