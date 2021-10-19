using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;



/// Создать прототип информационной системы, в которой есть возможност работать со структурой организации
/// В структуре присутствуют департаменты и сотрудники
/// Каждый департамент может содержать не более 1_000_000 сотрудников.
/// У каждого департамента есть поля: наименование, дата создания,
/// количество сотрудников числящихся в нём 
/// (можно добавить свои пожелания)
/// 
/// У каждого сотрудника есть поля: Фамилия, Имя, Возраст, департамент в котором он числится, 
/// уникальный номер, размер оплаты труда, количество закрепленным за ним.
///
/// В данной информаиционной системе должна быть возможность 
/// - импорта и экспорта всей информации в xml и json
/// Добавление, удаление, редактирование сотрудников и департаментов
/// 
/// * Реализовать возможность упорядочивания сотрудников в рамках одно департамента 
/// по нескольким полям, например возрасту и оплате труда
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
/// 
/// 
/// Упорядочивание по одному полю возраст
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
/// 
///
/// Упорядочивание по полям возраст и оплате труда
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
/// 
/// 
/// Упорядочивание по полям возраст и оплате труда в рамках одного департамента
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
/// 
namespace Homework_Theme8
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int depsIndex = 3;                      //количество департаментов
            
            List<Department> deps = new List<Department>();            //заполняем департаменты
            for (int i = 0; i < depsIndex; i++)       
            {
                deps.Add(new Department(i+1,rand.Next(5, 8)));    
            }
            

            
            string answer = "1";

            do
            {
                Console.Clear();
                Console.WriteLine($"\t\t***Всего департаментов : {deps.Count}***\n\n");
                Console.WriteLine("\t\t----ВЫБЕРИТЕ ПУНКТ МЕНЮ----\n");
                Console.WriteLine("1 - Показать все записи \n2 - Экспорт записей \n3 - Импорт записей \n" +
                                  "4 - Редактирование Департаментов \n5 - Упорядочивание записей\n0 - ВЫХОД");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":   //показ всех записей
                        {
                            Console.Clear();

                            for (int i = 0; i < deps.Count; i++)
                            {
                                deps[i].PrintDepToConsole();
                            }
                            Console.ReadKey();

                            break;
                        }
                        
                    case "2":   ///экспорт записей
                        {
                            Console.Clear();

                            ///В XML
                            XElement myOrganization = new XElement("Organization");
                            foreach (var d in deps)
                            {
                                myOrganization.Add(d.SerializeDepartmentToXML());
                            }
                            myOrganization.Save("OrganizationExport.xml");

                            //В JSON
                            //string json = JsonConvert.SerializeObject(deps);
                            //File.WriteAllText("Organization.json", json);

                            JObject mainTree = new JObject();
                            JArray jArray = new JArray();

                            mainTree["Organization"] = "My Organization";
                            mainTree["Departments"] = jArray;

                            foreach (var d in deps)
                            {
                                JObject obj = d.SerializeDepartmentToJson();
                                
                                jArray.Add(obj);
                            }

                            string json = mainTree.ToString();
                            File.WriteAllText("OrganizationExport.json", json);
                            Console.WriteLine("Экспорт записей завершен!");
                            Console.ReadKey();

                            break;
                        }

                    case "3":   //импорт
                        {
                            Console.Clear();
                            deps.Clear();
                            depsIndex = 0;

                            string xml = System.IO.File.ReadAllText("OrganizationImport.xml");

                            var col = XDocument.Parse(xml)
                               .Descendants("Organization")
                               .Descendants("ConcreteDepartment")
                               .ToList();

                            foreach (var item in col)
                            {

                                deps.Add(new Department(Convert.ToInt32(item.Attribute("Id").Value),
                                                        item.Attribute("FirstName").Value,
                                                        item.Attribute("CreationDate").Value,
                                                        Department.GetWorkersXML(item.ToString())));
                            }

                            Console.WriteLine("Импорт записей завершен!");
                            Console.ReadKey();

                            break;
                        }

                    case "4":   //редактирование
                        {
                            string answer4;
                            do
                            {

                                Console.Clear();

                                Console.WriteLine("\t\t----ВЫБЕРИТЕ ПУНКТ МЕНЮ----\n");
                                Console.WriteLine("1 - Добавить департамент \n2 - Редактировать департамент\n3 - Удалить департамент \n0 - ВЫХОД в предыдущее меню");

                                answer4 = Console.ReadLine();

                                switch (answer4)
                                {
                                    case "1":   //добавить департмент

                                        {
                                            Console.Clear();
                                            depsIndex++;
                                            deps.Add(new Department(depsIndex, rand.Next(5,8)));

                                            Console.WriteLine($"Департамент № {depsIndex} добавлен!"); ; ;
                                            Console.ReadKey();

                                            break;
                                        }
                                    case "2":   //Редактировать

                                        {
                                            Console.Clear();
                                            Console.WriteLine("Редактирование завершено!");
                                            Console.ReadKey();
                                            break;
                                        }

                                    case "3":   //Удалить

                                        {
                                            int depNumToDelete;
                                            Console.Clear();
                                            
                                            do
                                            {
                                                Console.WriteLine("Введите номер департамента:");
                                                depNumToDelete = EnterNumber();
                                            } while (depNumToDelete < 0 || depNumToDelete > deps.Count);

                                            deps.RemoveAt(depNumToDelete-1);

                                            Console.WriteLine("Департамент удален!");
                                            
                                            Console.ReadKey();
                                            break;
                                        }
                                }

                            } while (answer4 != "0");
                        }
                        break;


                    case "5":   //упорядочивание
                        {
                            string answer5;
                            do
                            {

                                Console.Clear();

                                Console.WriteLine("\t\t----ВЫБЕРИТЕ ПУНКТ МЕНЮ----\n");
                                Console.WriteLine("1 - Упорядочить по возрасту \n2 - Упорядочить по зарплате \n0 - ВЫХОД В предыдущее меню");

                                answer5 = Console.ReadLine();

                                switch (answer5)
                                {
                                    case "1":   //упорядочить по возрасту

                                        {
                                            Console.Clear();
                                            foreach(var dep in deps)
                                            {
                                                dep.OrderDepartmentByAge();
                                            }
                                            //for (int i = 0; i < deps.Count; i++)
                                            //{
                                            //    deps[i].OrderDepartmentByAge();
                                            //}
                                            Console.WriteLine("Упорядочивание по возрасту завершено!");
                                            Console.ReadKey();

                                            break;
                                        }
                                    case "2":   //упорядочить по зарплате

                                        {
                                            Console.Clear();
                                            foreach (var dep in deps)
                                            {
                                                dep.OrderDepartmentBySalary();
                                            }
                                            Console.WriteLine("Упорядочивание по зарплате завершено!");
                                            Console.ReadKey();
                                            break;
                                        }
                                }
                                
                            }while (answer5 != "0");
                        }
                        break;


                }
            }
            while (answer != "0");

            //Console.WriteLine(deps[1].workers[0].Age);
        }


        /// <summary>
        /// Ввод числа 
        /// </summary>
        /// <returns>число</returns>
        static public int EnterNumber()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Ошибка ввода! Введите целое число");
            }
            return number;
        }
    }
}
