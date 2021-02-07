using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(new TableRow(new Key("One", 1), new Union("Value1", 1)),
                                    new TableRow(new Key("ЮЖТTwo", 2), new Union("Value2", 2)),
                                    new TableRow(new Key("южtThree", 3), new Union("Value3", 3)),
                                    new TableRow(new Key("Four", 4), new Union("Value4", 4)),
                                    new TableRow(new Key("Fourth", 4), new Union("ValueFourth", 44)),
                                    new TableRow(new Key("Five", 5), new Union("Value5", 5)));

            Console.WriteLine("Inputed table");
            Console.WriteLine(table);
            Console.WriteLine();

            table.Insert(new TableRow(new Key("OneKey", 1), new Union("Value1111", 11111)));
            table.Insert(new TableRow(new Key("Six", 6), new Union("InsertedValue6", 6)));
            table.Insert(new TableRow(new Key("Six", 600), new Union("InsertedValue600", 600)));
            table.Insert(new TableRow(new Key("sixtySix", 66), new Union("InsertedValue66", 66)));
            table.Insert(new TableRow(new Key("tSixtyone", 61), new Union("InsertedValue61", 61)));
            table.Delete(new Key("Five", 5));
            table.Update(new Key("One", 1), new Union("UpdatedValue1", 11));

            Console.WriteLine("Changed table");
            Console.WriteLine(table);
            Console.WriteLine();

            Console.WriteLine("---------Direct address select---------");
            int directAddress = 2;
            Console.WriteLine("Selected by direct address "+directAddress+" : "+table.SelectByDirectAddress(directAddress));
            directAddress = 4;
            Console.WriteLine("Selected by direct address " + directAddress + " : " + table.SelectByDirectAddress(directAddress));
            Console.WriteLine();

            ushort directKey = 4;
            Console.WriteLine("---------Direct key search: "+directKey+"---------");
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

            Key keyForSimilarSearch = new Key("юЖT", 12);
            Console.WriteLine("--------------Task 2. Similar search. Key for search: "+keyForSimilarSearch+"--------------");
            Console.WriteLine(table.SelectBySimilarSearch(keyForSimilarSearch));

            //Delay
            Console.ReadKey();
        }
    }
}