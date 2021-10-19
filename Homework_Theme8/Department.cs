using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Threading;

namespace Homework_Theme8
{


    struct Department
    {
        #region Поля

        /// <summary>
        /// номер Департамента
        /// </summary>
        int depId;

        /// <summary>
        /// Название
        /// </summary>
        private string depName;

        /// <summary>
        /// Дата создания
        /// </summary>
        private DateTime depCreationDate;

        ///// <summary>
        ///// Количество сотрудников в департаменте
        ///// </summary>
        //private uint workersCount;

        /// <summary>
        /// Массив с работниками
        /// </summary>
        public List<Worker> workers;

        /// <summary>
        /// Заголовки
        /// </summary>
        string[] titles;

        #endregion

        #region Свойства

        ///// <summary>
        ///// Индекс
        ///// </summary>
        //public int Index { get { return this.index; } set { this.index = value; } }

        /// <summary>
        /// Название
        /// </summary>
        public string DepName { get { return this.depName; } set { this.depName = value; } }

        /// <summary>
        /// id
        /// </summary>
        public int DepId { get { return this.depId; } set { this.depId = value; } }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get { return this.depCreationDate; } set { this.depCreationDate = value; } }

        ///// <summary>
        ///// Количество работников
        ///// </summary>
        //public int WorkersCount { get { return this.index; } }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Автоматической генерации
        /// </summary>
        /// <param name="depNumber">номер департамента</param>
        /// <param name="empCount">количество работников</param>
        public Department(int depNumber, int empCount)
        {
            Thread.Sleep(1);   //для разных значений генератора
            Random r = new Random(DateTime.Now.Millisecond);
            this.depId = depNumber;
            this.depName = $"Department № {depNumber}";
            this.depCreationDate = new DateTime(2020, 03, depNumber);
            this.titles = new string[7] { "id", "Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.workers = new List<Worker>();

            for (int i = 1; i <= empCount; i++)
            {
                workers.Add(
                    new Worker(
                        (uint)(depNumber * 1000 + i),
                        $"Имя_{i}",
                        $"Фамилия_{i}",
                        (byte)r.Next(20, 100),
                        (uint)r.Next(10, 20) * 1000,
                        this.depName,
                        (byte)r.Next(1, 5)));
            }

        }

        /// <summary>
        /// Конструктор, собирающий департамент, используется для импорта из XML
        /// </summary>
        /// <param name="depNumber">ID департамента</param>
        /// <param name="depName">Имя департамента</param>
        /// <param name="depDate">Дата создания</param>
        /// <param name="works">Массив воркеров</param>
        public Department(int depNumber, string depName, string depDate, List<Worker> works)
        {
            this.depId = depNumber;
            this.depName = depName;
            this.depCreationDate = DateTime.Parse(depDate);
            this.titles = new string[7] { "id", "Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.workers = works;

        }

        #endregion

        #region Методы

        /// <summary>
        /// Печать в консоль
        /// </summary>
        public void PrintDepToConsole()
        {
            Console.WriteLine($"\nДепартамент № {depId}, Дата создания: {depCreationDate.ToShortDateString()}");
            Console.WriteLine($"{titles[0],3} {titles[1],10} {titles[2],20} {titles[3],10} {titles[4],15}  {titles[5],15} {titles[6],10}");
            foreach (var item in workers)
            {
                Console.WriteLine(item.Print());
            }
        }


        /// <summary>
        /// Распарсивает XML строку в массив воркеров
        /// </summary>
        /// <param name="s">строка</param>
        /// <returns>массив воркеров</returns>
        public static List<Worker> GetWorkersXML(string s)
        {
            var colWorks = XDocument.Parse(s)
                                    .Descendants("Workers")
                                    .Descendants("ConcreteWorker")
                                    .ToList();

            List<Worker> workers = new List<Worker>();
            foreach (var item in colWorks)
            {
                workers.Add(new Worker(Convert.ToUInt32(item.Attribute("Id").Value),
                                       item.Attribute("FirstName").Value,
                                       item.Attribute("LastName").Value,
                                       Convert.ToByte(item.Attribute("Age").Value),
                                       Convert.ToUInt32(item.Attribute("Salary").Value),
                                       item.Attribute("Department").Value,
                                       Convert.ToByte(item.Attribute("ProjectsCount").Value)));
            }
            return workers;
        }

        /// <summary>
        /// Сортировка по возрасту
        /// </summary>
        public void OrderDepartmentByAge()
        {
            var sortedTmp = from r in this.workers
                            orderby r.Age
                            select r;

            this.workers = sortedTmp.ToList(); ;
        }

        /// <summary>
        /// сортировка по зарплате
        /// </summary>
        public void OrderDepartmentBySalary()
        {
            var sortedTmp = from r in this.workers
                            orderby r.Salary
                            select r;

            this.workers = sortedTmp.ToList(); ;
        }

        /// <summary>
        /// Департмент в ХМЛ
        /// </summary>
        /// <returns></returns>
        public XElement SerializeDepartmentToXML()
        {
            XElement xConcreteDepartment = new XElement("ConcreteDepartment");
            XAttribute xConcreteDepartmentId = new XAttribute("Id", this.depId);
            XAttribute xConcreteDepartmentName = new XAttribute("FirstName", this.depName);
            XAttribute xConcreteDepartmentCreationdate = new XAttribute("CreationDate", this.depCreationDate);
            XElement xConcreteDepartmentWorkers = new XElement("Workers");

            foreach (var w in workers)
            {
                xConcreteDepartmentWorkers.Add(w.SerializeWorkerToXML());
            }

            xConcreteDepartment.Add(xConcreteDepartmentId,
                                xConcreteDepartmentName,
                                xConcreteDepartmentCreationdate,
                                xConcreteDepartmentWorkers);

            return xConcreteDepartment;
        }

        public JObject SerializeDepartmentToJson()
        {
            
            JArray jArray = new JArray();
            foreach (var w in this.workers)
            {
                JObject obj = w.SerializeWorkerToJson();

                jArray.Add(obj);
            }

            JObject jDep = new JObject
            {
                ["ID"] = this.DepId,
                ["depName"] = this.DepName,
                ["creationDate"] = this.CreationDate
            };
            jDep["workers"] = jArray;

            
            return jDep;
        }
    }





        #endregion
    
}
