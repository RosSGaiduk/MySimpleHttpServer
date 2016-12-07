using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer
{
    public class Man
    {
            private int id;
            private string name;
            private string lastname;
            private int age;

            public int Age
            {
                get
                {
                    return age;
                }

                set
                {
                    age = value;
                }
            }

            public string Lastname
            {
                get
                {
                    return lastname;
                }

                set
                {
                    lastname = value;
                }
            }

            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Man(int id,string name, string lname, int a) { this.Id = id; this.Name = name; Lastname = lname; Age = a; }

        public Man(string name, string lname, int a) { this.Name = name; Lastname = lname; Age = a; }


        public override string ToString()
        {
            return name + " " + lastname + " " + age+"|";
        }   

    }
 }

