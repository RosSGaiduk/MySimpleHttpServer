using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer
{
    class SectionFormer
    {
        private CubeGenerator cube;


        public SectionFormer(CubeGenerator cube) { this.cube = cube; }


        public int[] findPositions(int index)
        {
            int[] pos = new int[3]; //facet,row,column
            int columns = cube.CountOfElementsInTheOneFacet;
            int rows = cube.Rows;
            

            int i = 0;
            int elementsInFacet = rows * columns;
            while (i <= 3)
            {
                if (index >= i * elementsInFacet && index < (i + 1) * elementsInFacet)
                {
                    pos[0] = i;
                    break;
                }
                i++;
            }

            int firstIndexInFoundFacet = pos[0]*elementsInFacet;
            int dif = index - firstIndexInFoundFacet;

            int rowOfElement = dif / columns;
            int colOfElement = dif % columns;

            

            pos[1] = rowOfElement;
            pos[2] = colOfElement;

            Console.WriteLine("Facet of element: " + pos[0]); //0
            Console.WriteLine("Row of element:" + pos[1]); //2
            Console.WriteLine("Column of element: " + pos[2]); //1

            return pos;
            
        }
        public void generateSection(int index)
        {
            Man[][][] cubeValues = cube.getCube();
            Console.WriteLine("***********************************************************");
            Console.WriteLine(cubeValues.Length + " " + cubeValues[0].Length + " " + cubeValues[0][0].Length);
            int[] positions = findPositions(index);
            int row = positions[1];

            int count = 0;
            int beginJ = positions[2];
            

            for (int k = positions[0]; k < 4; k++)
            {
                for (int j = beginJ; j < cubeValues[k][row].Length; j++)           
                    Console.Write(cubeValues[k][row][j]); 
                beginJ = 0; count++;
            }

            for (int j = 0; j < positions[2]; j++)
                Console.WriteLine(cubeValues[positions[0]][row][j]);
            Console.WriteLine("***********************************************************");
        }

    }
}
