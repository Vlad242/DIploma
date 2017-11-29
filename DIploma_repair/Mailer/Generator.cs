using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DIploma_repair.Mailer
{
    class Generator
    {
        public MySqlConnection conn;

        private void SetConnection()
        {
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.getConnectInfo());
            conn.Open();
        }

        private void CloseCOnnection()
        {
            conn.Close();
        }

        public string GenerateBody(string login, int order_id, string ComandName)
        {
            string result = "";
            MailerConfig mailer = new MailerConfig();
            string mail = mailer.userName;
            List<string> detail = new List<string>();
            string userName = "";
            string status_name = "";
            string service_name = "";
            string Item_name = "";
            string M_name = "";
            string model_name = "";
            string serial_name = "";
            string complete_set = "";
            string Worker_name = "";
            string order_price = "";
            string order_date = "";
            string worker_phone = "";
            string office_name = "";
            string address = "";
            try
            {
                SetConnection();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT User_surname, User_name, User_fname FROM Users WHERE Login='" + login +"';")
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userName = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2);
                }
                reader.Close();
                ////////////////////
                cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Status.Status_name, Service.Service_name, Item.Item_name, Manufacturer.M_name, Model.Model_name, Serial.Serial_number, Orders.Complete_set, Worker.Worker_surname,Worker.Worker_name,Worker.Worker_fname, Orders.Order_price, Orders.Order_Date, Worker.Phone, Office.Address, Office.Office_name FROM Orders INNER JOIN Service on(Service.Service_id=Orders.Service_id) INNER JOIN Model on(Model.Model_id=Orders.Model_id) INNER JOIN Item on(Model.Item_id=Item.Item_id) INNER JOIN Manufacturer on(Item.M_id=Manufacturer.M_id) INNER JOIN Serial on(Model.Model_id=Serial.Model_id) INNER JOIN Status on(Status.Status_id=Orders.Status_id) INNER JOIN Worker on(Orders.Worker_id=Worker.Worker_id) INNER JOIN Office on(Office.Office_id=Worker.Office_id) WHERE Orders.Order_id=" + order_id + ";")
                };
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    status_name = reader.GetString(0);
                     service_name = reader.GetString(1);
                    Item_name = reader.GetString(2);
                    M_name = reader.GetString(3);
                    model_name = reader.GetString(4);
                    serial_name = reader.GetString(5);
                    complete_set = reader.GetString(6);
                    Worker_name = reader.GetString(7) + " " + reader.GetString(8) + " " + reader.GetString(9);
                    order_price = reader.GetString(10);
                    order_date = reader.GetString(11);
                    worker_phone = reader.GetString(12);
                    address = reader.GetString(13);
                    office_name = reader.GetString(14);
                }
                reader.Close();

                result = "Шановний користувачу " + userName + "." + "<br>" +
                                "Ваше замовлення #'" + order_id + "' зареєстровано в базі системи, статус замовлення " + status_name + "." + "<br>" +
                                "Деталі замовлення:" + "<br>" +
                                "Сервіс: " + service_name + ";" + "<br>" +
                                "Пристрій: " + Item_name + " " + M_name + " " + model_name + " serial:" + serial_name + ";" + "<br>" +
                                "Комплектація: " + complete_set + ";" + "<br>" + "<br>" +
                                "Заявку прийняв майстер: " + Worker_name + ";" + "<br>" +
                                "Адреса: " + address + " офіс " + office_name + ";" + "<br>" + "<br>";
                cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT  Detail.Detail_name, Detail.Prod_country, Detail.Price FROM Detail INNER JOIN is_for on(Detail.Detail_id=is_for.Detail_id) WHERE is_for.Order_id=" + order_id + ";")
                };
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    detail.Add(reader.GetString(0) + " -> " + reader.GetString(1) + " - " + reader.GetString(2));
                }

                if (detail.Count > 0)
                {
                    result += "Прайс лист:" + "<br>";
                    foreach (var item in detail)
                    {
                        result += item + ";" + "<br>";
                    }
                }
                result += "Загальна ціна замовлення: " + order_price + ";" + "<br>" +
                    "Дата замовлення: " + order_date.Remove(10) + ";" + "<br>" + "<br>" +
                    "Дякуємо, що скористалися послугами нашої компанії!" + "<br>" +
                    "Додаткова інформація доступна в додатку." + "<br>" +
                    "Телефон майстра: " + worker_phone + ";" + "<br>" +
                    "Електронна пошта: " + mail + ";" + "<br>" +
                    "Телефон для довідки: 937-99-92" + "<br>" + "<br>" +
                    "З повагою команда " + ComandName + " ! :)";

            }
            catch (Exception ex)
            {

            }
            CloseCOnnection();
            return result;
        }


        public string GenerateSubject(string CompanyName, int orderId)
        {
            return CompanyName + " замовлення № " + orderId;
        }
    }
}
