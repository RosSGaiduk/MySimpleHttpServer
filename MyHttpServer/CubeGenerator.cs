using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer
{
    class CubeGenerator
    {
        private int countOfElementsInTheOneFacet; //count of columns
        private int generalCountOfRecordsInDb;
        private Man[][][] facets;
        private List<Man> men = new List<Man>();
        private int rows;


      

        public int Rows
        {
            get
            {
                return rows;
            }

            set
            {
                rows = value;
            }
        }

        public int GeneralCountOfRecordsInDb
        {
            get
            {
                return generalCountOfRecordsInDb;
            }

            set
            {
                generalCountOfRecordsInDb = value;
            }
        }

        public int CountOfElementsInTheOneFacet
        {
            get
            {
                return countOfElementsInTheOneFacet;
            }

            set
            {
                countOfElementsInTheOneFacet = value;
            }
        }

        public CubeGenerator(int countOfElements,List<Man>men)
        {
            this.men = men;
            this.CountOfElementsInTheOneFacet = countOfElements; GeneralCountOfRecordsInDb = men.Count;
            //стовпців = countOfElementsInTheOneFacet;
            Rows = GeneralCountOfRecordsInDb / (4*CountOfElementsInTheOneFacet);
            facets = new Man[4][][];

            for (int i = 0; i < facets.Length; i++)
            {
                facets[i] = new Man[Rows][];
            }

            for (int i = 0; i < facets.Length; i++)
            {
                for (int j = 0; j < facets[i].Length; j++)
                {
                    facets[i][j] = new Man[CountOfElementsInTheOneFacet];
                }
            }
        }
        

        public Man[][] generateFacet(int facetNumber)
        {
         Console.WriteLine("Грань " + facetNumber + "\n");
            Console.WriteLine("-------------------------------------");
           int startIndex = facetNumber * (Rows * CountOfElementsInTheOneFacet);
            Man[][] menFacet = new Man[Rows][];
            for (int i = 0; i < rows; i++)
                menFacet[i] = new Man[CountOfElementsInTheOneFacet];
           for (int i = 0; i < facets[facetNumber].Length; i++)
            {
                for (int j = 0; j < CountOfElementsInTheOneFacet; j++)
                {
                    facets[facetNumber][i][j] = men[startIndex];
                    menFacet[i][j] = men[startIndex];
                    startIndex++;
                    Console.Write(facets[facetNumber][i][j].Name + " "+facets[facetNumber][i][j].Lastname+" "+ facets[facetNumber][i][j].Age+" | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------");

            return menFacet;
        }

        public Man[][][] getCube() { return facets; }



   }
}
