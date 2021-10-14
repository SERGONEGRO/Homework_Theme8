using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private DateTime creationDate;

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
        /// Дата создания
        /// </summary>
        public DateTime CreaationDate { get { return this.creationDate; } set { this.creationDate = value; } }

        ///// <summary>
        ///// Количество работников
        ///// </summary>
        //public int WorkersCount { get { return this.index; } }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="depNumber">номер департамента</param>
        /// <param name="empCount">количество работников</param>
        public Department(int depNumber,int empCount)
        {
            Random r = new Random();
            
            this.depId = depNumber;        
            this.depName = $"Department № {depNumber}";
            this.creationDate = new DateTime(2020, 10, depNumber);
            this.titles = new string[7] {"id","Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.workers = new List<Worker>();

            for (int i = 1; i <= empCount; i++)
            {
                workers.Add(
                    new Worker(
                        (uint)(depNumber*1000+i),
                        $"Имя_{i}",
                        $"Фамилия_{i}",
                        (byte)r.Next(20,100),
                        (uint)r.Next(1000, 2000),
                        this.depName,
                        (byte)r.Next(1,5)));
            }

        }

        #endregion

        #region Методы

        /// <summary>
        /// Печать в консоль
        /// </summary>
        public void PrintDepToConsole()
        {
            Console.WriteLine($"\nДепартамент № {depId}");
            Console.WriteLine($"{titles[0],3} {titles[1],10} {titles[2],20} {titles[3],10} {titles[4],15}  {titles[5],15} {titles[6],10}" );
            foreach (var item in workers)
            {
                Console.WriteLine(item.Print());
            }
        }

        public void OrderDepartment()
        {
            var sortedTmp = from r in this.workers 
                            orderby r.Age
                            select r;

            this.workers = sortedTmp.ToList(); ;
        }

        #endregion
    }
}
