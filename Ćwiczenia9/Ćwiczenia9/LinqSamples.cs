using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Unicode;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Przykład 1, Query syntax");
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };
            foreach(var anonymous in res)
            {
                Console.WriteLine(anonymous.Nazwisko + ", " + anonymous.Zawod);
            }
            Console.WriteLine();
            Console.WriteLine("Przykład nr 1, Lambda");
            var res1 = Emps
                .Where(job => job.Job == "Backend programmer")
                .Select(emp => new
                {
                    Nazwisko = emp.Ename,
                    Zawod = emp.Job
                });
            foreach(var anonymous in res1)
            {
                Console.WriteLine(anonymous.Nazwisko + ", " + anonymous.Zawod);
            }
            Console.WriteLine("=============================");
            //2. Lambda and Extension methods
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            IEnumerable<Emp> query =
                from objects in Emps
                where objects.Job == "Frontend programmer"
                && objects.Salary > 1000
                orderby objects.Ename descending
                select objects;
            Console.WriteLine("Przykład nr 2, Query Syntax");

            foreach(Emp emp in query)
            {
                Console.WriteLine(emp.Ename + ", " + emp.Job + ", " + emp.Salary);
            }
            Console.WriteLine();
            var query1 = Emps
                .Where(obj => obj.Job == "Frontend programmer" && obj.Salary > 1000)
                .OrderByDescending(obj => obj.Ename);
            Console.WriteLine("Przykład nr 2, Lambda");
            foreach(Emp emp in query1)
            {
                Console.WriteLine(emp.Ename + ", " + emp.Job + ", " + emp.Salary);
            }
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            var emp =
                (from objects in Emps
                 orderby objects.Salary descending
                 select objects).First();
            Console.WriteLine("Przykład nr 3, Query Syntax");
            Console.WriteLine(emp.Salary);
            Console.WriteLine();
            var emp1 = Emps
                .OrderByDescending(obj => obj.Salary)
                .Select(obj => obj)
                .First();
            Console.WriteLine("Przykład nr 3, Lambda");
            Console.WriteLine(emp1.Salary);
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {
            var emp =
                (from objects in Emps
                 where objects == (from tmps in Emps
                                   orderby tmps.Salary descending
                                   select tmps).First()
                 select objects);
            Console.WriteLine("Przyklad 4, Query syntax");
            foreach(Emp tmp in emp)
            {
                Console.WriteLine(tmp.Ename + ", " + tmp.Salary);
            }
            Console.WriteLine();
            var emp1 = Emps
                .Where(emp => emp == (Emps.OrderByDescending(emp => emp.Salary)
                .First()))
                .Select(emp => emp);
            Console.WriteLine("Przykład nr 4, Lambda");
            foreach(Emp tmp in emp)
            {
                Console.WriteLine(tmp.Ename + ", " + tmp.Salary);
            }
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            var emps = from objects in Emps
                       select new
                       {
                           Nazwisko = objects.Ename,
                           Praca = objects.Job
                       };
            Console.WriteLine("Przykład nr 5, Query syntax");
            foreach(var obj in emps.ToList())
            {
                Console.WriteLine(obj.Nazwisko + ", " + obj.Praca);
            }
            Console.WriteLine();
            var emps1 = Emps.Select(obj => new
            {
                Nazwisko = obj.Ename,
                Praca = obj.Job
            });
            Console.WriteLine("Przykład nr 5, Lambda");
            foreach(var obj in emps1.ToList())
            {
                Console.WriteLine(obj.Nazwisko + ", " + obj.Praca);
            }
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            var objects = (from emp in Emps
                          join dept in Depts
                          on emp.Deptno equals dept.Deptno
                          select emp.Ename + ", " + dept.Deptno).ToList();
            Console.WriteLine("Przykład 6, Query syntax");
            foreach(var obj in objects)
            {
                Console.WriteLine(obj);
            }

            objects = Emps
                .Join(Depts,
                dept => dept.Deptno,
                emp => emp.Deptno,
                (emp, dept) => new
                {
                    emp,
                    dept
                })
                .Select(obj => obj.emp.Ename + ", " + obj.dept.Deptno)
                .ToList();
            Console.WriteLine();
            Console.WriteLine("Przykład 6, Lamda");
            foreach(var obj in objects)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            var objects = (from emps in Emps
                           group emps by emps.Job
                          into newEmps
                           select new
                           {
                               Praca = newEmps.Key,
                               EmpCount = newEmps.Count()
                           });
            Console.WriteLine("Przykład 7, Query syntax");
            foreach(var som in objects)
            {
                Console.WriteLine(som.Praca + ", " + som.EmpCount);
            }
            Console.WriteLine();
            Console.WriteLine("Przykład 7, Lambda");
            objects = Emps.
                GroupBy(emp => emp.Job)
                .Select(emp => new
                {
                    Praca = emp.Key,
                    EmpCount = emp.Count()
                });
            foreach(var som in objects)
            {
                Console.WriteLine(som.Praca + ", " + som.EmpCount);
            }
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            var @object = ((from emps in Emps
                            where emps.Job == "Backend programmer"
                            select emps).Count() != 0);
            Console.WriteLine("Przykład 8, Query syntax");
            Console.WriteLine(@object);
            Console.WriteLine();
            @object = (Emps
                .Where(emp => emp.Job == "Backend programmer")
                .Select(emp => emp)
                .Count() != 0);
            Console.WriteLine("Przykład 8, Lambda");
            Console.WriteLine(@object);
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {
            var @object = (from emps in Emps
                           where emps.Job == "Frontend programmer"
                           orderby emps.HireDate descending
                           select emps).First();
            Console.WriteLine("Przykład 9, Query syntax");
            Console.WriteLine(@object.Ename + " " + @object.Job);
            @object = Emps
                .Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .Select(emp => emp)
                .First();
            Console.WriteLine();
            Console.WriteLine("Przykład 9, Lambda");
            Console.WriteLine(@object.Ename + " " + @object.Job);
            Console.WriteLine("=============================");
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10Button_Click()
        {
            var obj = new
            {
                Ename = "Brak wartości",
                Job = (string)null,
                Hiredate = (string)null
            };
            var list = new List<dynamic>();
            list.Add(obj);
            var union = (from emp in Emps
                        select new
                        {
                            emp.Ename,
                            emp.Job,
                            emp.HireDate
                        }).Union(list);
            Console.WriteLine("Przykład 10, Query syntax");
            foreach(var something in union)
            {
                Console.WriteLine(something);
            }
            Console.WriteLine();
            Console.WriteLine("Przykład 10, Lambda");
            union = Emps
                .Select(e => new
                {
                    e.Ename,
                    e.Job,
                    e.HireDate
                })
                .Union(list);
            foreach(var something in union)
            {
                Console.WriteLine(something);
            }
            Console.WriteLine("=============================");
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
            
            Console.WriteLine("Przykład 11, Query syntax");
            var query = (from emp in Emps
                         where emp == (from tmp in Emps
                                              orderby tmp.Salary descending
                                              select tmp)
                                              .First()
                         select emp).First();
            Console.WriteLine(query.Ename + ", " + query.Salary);
            Console.WriteLine();
            Console.WriteLine("Przykład 11, Lambda");
            query = Emps.Aggregate((prev, next) => prev.Salary > next.Salary ? prev : next);
            Console.WriteLine(query.Ename + ", " + query.Salary);
            Console.WriteLine("=============================");
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {
            Console.WriteLine("Przykład 12, Query syntax");
            var result = (from emp in Emps
                          from dept in Depts
                          select new
                          {
                              dept.Dname,
                              emp.Ename,
                              emp.Salary
                          }).ToList();
            foreach(var obj in result)
            {
                Console.WriteLine("{ " + obj.Dname + ", " + obj.Ename + ", " + obj.Salary + " }");
            }
            Console.WriteLine();
            Console.WriteLine("Przykład 12, Lambda");
            result = Emps
                .SelectMany(emp => Depts,
                (e, d) => new
                {
                    d.Dname,
                    e.Ename,
                    e.Salary
                }).ToList();
            foreach(var obj in result)
            {
                Console.WriteLine("{ " + obj.Dname + ", " + obj.Ename + ", " + obj.Salary + " }");
            }
        }
    }
}
