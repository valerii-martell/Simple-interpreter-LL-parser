using System;
using System.Collections.Generic;
using System.Linq;

namespace TableProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(new TableRow(new Key("One", 1), new Value(1.1f)),
                                    new TableRow(new Key("ЮЖТTwo", 2), new Value(1.2f)),
                                    new TableRow(new Key("южtThree", 3), new Value(1.3f)),
                                    new TableRow(new Key("Four", 4), new Value(1.4f)),
                                    new TableRow(new Key("Fourth", 4), new Value(1.5f)),
                                    new TableRow(new Key("Five", 5), new Value(1.5f)));

            Console.WriteLine("Inputed table");
            Console.WriteLine(table);
            Console.WriteLine();

            table.Insert(new TableRow(new Key("OneKey", 1), new Value(2.1f)));
            table.Insert(new TableRow(new Key("Six", 6), new Value(2.2f)));
            table.Insert(new TableRow(new Key("Six", 205), new Value(2.3f)));
            table.Insert(new TableRow(new Key("sixtySix", 66), new Value(2.4f)));
            table.Insert(new TableRow(new Key("tSixtyone", 61), new Value(2.5f)));
            table.Delete(new Key("Five", 5));
            table.Update(new Key("One", 1), new Value(2.7f));

            Console.WriteLine("Changed table");
            Console.WriteLine(table);
            Console.WriteLine();

            Console.WriteLine("---------Direct address select---------");
            byte directAddress = 2;
            Console.WriteLine("Selected by direct address " + directAddress + " : " + table.SelectByDirectAddress(directAddress));
            directAddress = 4;
            Console.WriteLine("Selected by direct address " + directAddress + " : " + table.SelectByDirectAddress(directAddress));
            Console.WriteLine();

            byte directKey = 4;
            Console.WriteLine("---------Direct key search: " + directKey + "---------");
            Console.WriteLine(table.SelectByDirectKey(directKey));
            Console.WriteLine(table.SelectByDirectKey(directKey));
            Console.WriteLine(table.SelectByDirectKey(directKey));
            Console.WriteLine(table.SelectByDirectKey(directKey));
            Console.WriteLine();

            Key keyForBinarySearch = new Key("One", 1);
            Console.WriteLine("--------Binary search. Key for search: " + keyForBinarySearch + "--------");
            Console.WriteLine(table.SelectByBinarySearch(keyForBinarySearch));
            Console.WriteLine(table.SelectByBinarySearch(keyForBinarySearch));
            Console.WriteLine(table.SelectByBinarySearch(keyForBinarySearch));
            Console.WriteLine(table.SelectByBinarySearch(keyForBinarySearch));
            Console.WriteLine();

            Key keyForLinearSearch = new Key("Six", 6);
            Console.WriteLine("--------Linear search. Key for search: " + keyForLinearSearch + "--------");
            Console.WriteLine(table.SelectByLinearSearch(keyForLinearSearch));
            Console.WriteLine(table.SelectByLinearSearch(keyForLinearSearch));
            Console.WriteLine(table.SelectByLinearSearch(keyForLinearSearch));
            Console.WriteLine(table.SelectByLinearSearch(keyForLinearSearch));
            Console.WriteLine(table.SelectByLinearSearch(keyForLinearSearch));
            Console.WriteLine(table.SelectByLinearSearch(keyForLinearSearch));
            Console.WriteLine();

            Key keyForSimilarSearch = new Key("Forty", 12);
            Console.WriteLine("--------------Task 2. Similar search. Key for search: " + keyForSimilarSearch + "--------------");
            Console.WriteLine(table.SelectBySimilarSearch(keyForSimilarSearch));

            //Delay
            Console.ReadKey();
        }
    }
}