// See https://aka.ms/new-console-template for more information

using AlgorithmsSeminar4;

var hashmap = new HashMap<string, string>(6);
hashmap.Put("a", "1");
hashmap.Put("b", "2");
hashmap.Put("c", "3");
hashmap.Put("d", "4");
hashmap.Put("e", "5");
hashmap.Put("f", "6");
hashmap.Put("g", "7");
hashmap.Put("h", "8");
hashmap.Put("i", "9");
hashmap.Put("j", "10");

Console.WriteLine("Содержимое hashTable");
foreach (var val in hashmap)
{
    Console.WriteLine($"index: {val.Item1}, key: {val.Item2}, value: {val.Item3}");
}


// var test = hashmap.Get("b");
// Console.WriteLine(test);

// test = hashmap.Remove("b");
// Console.WriteLine(test);
// test = hashmap.Remove("b");
// Console.WriteLine(test);


// test = hashmap.Get("b");
// Console.WriteLine(test);
