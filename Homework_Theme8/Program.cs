using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


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
                Console.WriteLine("1 - Показать все записи \n2 - Экспорт записей \n3 - Импорт записей из XML\n" +
                                  "4 - Импорт записей из JSON\n5 - Редактирование Департаментов \n6 - Упорядочивание записей\n0 - ВЫХОД");

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

                    case "3":   //импорт из XML
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

                    case "4":   //импорт из Json
                        {
                            Console.Clear();
                            deps.Clear();
                            depsIndex = 0;

                            string json = System.IO.File.ReadAllText("OrganizationImport.json");

                            var departments = JObject.Parse(json)["Departments"].ToArray();

                            foreach (var item in departments)
                            {

                                deps.Add(new Department( Convert.ToInt32(item["ID"]),
                                                         item["depName"].ToString() ,
                                                         item["creationDate"].ToString() ,
                                                         Department.GetWorkersJSON(item.ToString()) ));
                            }


                            Console.WriteLine("Импорт записей завершен!");
                            Console.ReadKey();

                            break;
                        }

                    case "5":   //редактирование
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

                    case "6":   //упорядочивание
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
