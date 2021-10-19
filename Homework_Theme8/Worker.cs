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
    struct Worker
    {
        #region Конструкторы

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Age"></param>
        /// <param name="Salary"></param>
        /// <param name="Department"></param>
        /// <param name="ProjectsCount"></param>
        public Worker(uint ID, string FirstName, string LastName, byte Age, uint Salary, string Department,byte ProjectsCount)
        {
            this.id = ID;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.age = Age;
            this.department = Department;
            this.salary = Salary;
            this.projectsCount = ProjectsCount;
        }

        #endregion

        #region Методы
        /// <summary>
        /// печать в консоль
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{this.id,5} {this.firstName,10} {this.lastName,20} {this.age,5} {this.department,20}  {this.salary,10} {this.projectsCount,8}";
        }


        /// <summary>
        /// воркер в ХМЛ
        /// </summary>
        /// <returns></returns>
        public XElement SerializeWorkerToXML()
        {
            XElement xConcreteWorker = new XElement("ConcreteWorker");
            XAttribute xConcreteWokerId = new XAttribute("Id", this.id);
            XAttribute xConcreteWokerFirstName = new XAttribute("FirstName", this.firstName);
            XAttribute xConcreteWokerLastName = new XAttribute("LastName", this.lastName);
            XAttribute xConcreteWokerAge = new XAttribute("Age", this.age);
            XAttribute xConcreteWokerDepartment = new XAttribute("Department", this.department);
            XAttribute xConcreteWokerSalary = new XAttribute("Salary", this.salary);
            XAttribute xConcreteWokerProjectsCount = new XAttribute("ProjectsCount", this.projectsCount);

            xConcreteWorker.Add(xConcreteWokerId,
                                xConcreteWokerFirstName,
                                xConcreteWokerLastName,
                                xConcreteWokerAge,
                                xConcreteWokerDepartment,
                                xConcreteWokerSalary,
                                xConcreteWokerProjectsCount);

            return xConcreteWorker;
        }

        public JObject SerializeWorkerToJson()
        {
            JObject jWorker = new JObject
            {
                ["ID"] = this.Id,
                ["FirstName"] = this.FirstName,
                ["LastName"] = this.LastName,
                ["Age"] = this.Age,
                ["Salary"] = this.Salary,
                ["Department"] = this.Department,
                ["ProjectCount"] = this.ProjectsCount
            };
            return jWorker;
        }
           
        #endregion

        #region Свойства

        public uint Id { get { return this.id; } }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }

        /// <summary>
        /// Возраст
        /// </summary>
        public byte Age { get { return this.age; } set { this.age = value; } }

        /// <summary>
        /// Отдел
        /// </summary>
        public string Department { get { return this.department; } set { this.department = value; } }

        /// <summary>
        /// Оплата труда
        /// </summary>
        public uint Salary { get { return this.salary; } set { this.salary = value; } }

        /// <summary>
        /// Количество проектов
        /// </summary>
        public byte ProjectsCount { get { return this.projectsCount; } set { this.projectsCount = value; } }

        /// <summary>
        /// Почасовая оплата
        /// </summary>
        public double HourRate
        {
            get
            {
                byte workingDays = 25; // Рабочих дней в месяце
                byte workingHours = 8; // Рабочих часов в день
                return ((double)Salary) / workingDays / workingHours;
            }
        }

        #endregion

        #region Поля

        /// <summary>
        /// Уникальный номер
        /// </summary>
        private uint id;

        /// <summary>
        /// Поле "Имя"
        /// </summary>
        private string firstName;

        /// <summary>
        /// Поле "Фамилия"
        /// </summary>
        private string lastName;

        /// <summary>
        /// Возраст
        /// </summary>
        private byte age;

        /// <summary>
        /// Поле "Отдел"
        /// </summary>
        private string department;

        /// <summary>
        /// Поле "Оплата труда"
        /// </summary>
        private uint salary;

        /// <summary>
        /// Количество закрепленных проектов
        /// </summary>
        private byte projectsCount;

        #endregion
    }
}
