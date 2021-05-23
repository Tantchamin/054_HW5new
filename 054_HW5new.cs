using System;

namespace newHW5
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputImage = Console.ReadLine();
            string convolution = Console.ReadLine();
            string testOutput = Console.ReadLine();
            double[,] image = ReadImageDataFromFile(inputImage);
            double[,] arrayConvolution = ReadImageDataFromFile(convolution);

            // Show image
            /*for (int row = 0; row < image.GetLength(0); row++)
            {
                for (int column = 0; column < image.GetLength(1); column++)
                {
                    Console.Write(image[row, column] + " ");
                }
                Console.WriteLine();
            }*/

            // Show arrayConvoluution
            /*for (int row = 0; row < arrayConvolution.GetLength(0); row++)
            {
                for (int column = 0; column < arrayConvolution.GetLength(1); column++)
                {
                    Console.Write(arrayConvolution[row, column] + " ");
                }
                Console.WriteLine();
            }*/

            // Make newImage
            double[,] newImage = new double[image.GetLength(0) + 2, image.GetLength(1) + 2];
            for (int row = 0; row < image.GetLength(0); row++) // newImage center
            {
                for (int column = 0; column < image.GetLength(1); column++)
                {
                    newImage[row + 1, column + 1] = image[row, column];
                }
            }
            for (int i = 0; i < image.GetLength(0); i++) // newImage row
            {
                newImage[0, i + 1] = image[image.GetLength(0) - 1, i];
                newImage[newImage.GetLength(0) - 1, i + 1] = image[0, i];
            }
            for (int j = 0; j < image.GetLength(1); j++) // newImage column
            {
                newImage[j + 1, 0] = image[j, image.GetLength(1) - 1];
                newImage[j + 1, newImage.GetLength(1) - 1] = image[j, 0];
            }
            // newImage corner
            newImage[0, 0] = image[image.GetLength(0) - 1, image.GetLength(1) - 1];
            newImage[0, newImage.GetLength(1) - 1] = image[image.GetLength(0) - 1, 0];
            newImage[newImage.GetLength(0) - 1, 0] = image[0, image.GetLength(1) - 1];
            newImage[newImage.GetLength(0) - 1, newImage.GetLength(1) - 1] = image[0, 0];

            // Show newImage
            /*for (int row2 = 0; row2 < newImage.GetLength(0); row2++)
            {
                for (int column2 = 0; column2 < newImage.GetLength(1); column2++)
                {
                    Console.Write(newImage[row2, column2] + " ");
                }
                Console.WriteLine();
            }*/

            // Make outputImage
            double[,] outputImage = new double[newImage.GetLength(0) - 2, newImage.GetLength(1) - 2];

            for (int a = 0; a < outputImage.GetLength(0); a++)
            {
                for (int b = 0; b < outputImage.GetLength(1); b++)
                {
                    outputImage[a, b] =
                    (arrayConvolution[0, 0] * newImage[a, b]) + (arrayConvolution[0, 1] * newImage[a, b + 1]) + (arrayConvolution[0, 2] * newImage[a, b + 2]) +
                    (arrayConvolution[1, 0] * newImage[a + 1, b]) + (arrayConvolution[1, 1] * newImage[a + 1, b + 1]) + (arrayConvolution[1, 2] * newImage[a + 1, b + 2]) +
                    (arrayConvolution[2, 0] * newImage[a + 2, b]) + (arrayConvolution[2, 1] * newImage[a + 2, b + 1]) + (arrayConvolution[2, 2] * newImage[a + 2, b + 2]);
                }
            }

            // Show outputImage         
            /*for (int row = 0; row < outputImage.GetLength(0); row++)
            {
                for (int column = 0; column < outputImage.GetLength(1); column++)
                {
                    Console.Write(outputImage[row, column] + " ");
                }
                Console.WriteLine();
            }*/

            WriteImageDataToFile(testOutput, outputImage);

        }
        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }
        static void WriteImageDataToFile(string imageDataFilePath, double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,
                                                imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }

            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }
    }
}
