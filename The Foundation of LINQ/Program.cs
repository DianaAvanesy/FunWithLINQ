using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

// Read the data
var fielContent = await File.ReadAllTextAsync("DATA.json"); // dont forget to change DATA.json -> copy always
var cars = JsonSerializer.Deserialize<Car[]>(fielContent);

var carsWithAtLeastFourDoors = cars.Where(car => car.Doors >= 4);
foreach (var car in carsWithAtLeastFourDoors) {
    //Console.WriteLine($"The car {car.Model} has {car.Doors} doors");
}

var carsMazdaWithAtLeastFourDoors = cars.Where(car => car.Doors > 0 && car.Make == "Mazda");
foreach (var car in carsMazdaWithAtLeastFourDoors)
{
    //Console.WriteLine($"The car {car.Make} has {car.Doors} doors");
}


//OR you can do ToList().ForEach()
cars.Where(car => car.Make.StartsWith("M"))
    .Select(car => $"{car.Make} + {car.Model}")
    .ToList()
    .ForEach(car => Console.WriteLine(car)); // For each doesnt need a var -> its an action 


// Display a list of 10 most powerful cars ( in terms of hp )
//select top(10) * from x  order by hp
//cars.Max(car => car.hp); // only gets max hp

Console.WriteLine("---------------------");
cars.OrderByDescending(car => car.hp)
    .Take(10)
    .ToList()
    .ForEach(car => Console.WriteLine(car.hp));



// Display the number of models per make that apeared after 1995
// select Make, numOfModels from .. 
// group by model
// where  year > 1995
Console.WriteLine("---------------------");
//check this:
cars.GroupBy(car => car.Model)
    .Select(car => car.Count())
    .ToList()
    .ForEach(car => Console.WriteLine(car));

Console.WriteLine("---------------------");

cars.Where(car => car.Year >= 2000)
    .GroupBy(car => car.Model)
    .Select(c => new { c.Key, num = c.Count()  })
    .ToList()
    .ForEach(car => Console.WriteLine(car));





// Makes should be displayed with a number of ziro if there are no models after 2008
Console.WriteLine("---------------------");
cars.GroupBy(car => car.Model)
    //                              c is a group
    .Select(c => new { c.Key, num = c.Where(car => car.Year >= 2000) })
    //                              group doesn't exist anymore
    .ToList()
    .ForEach(car => Console.WriteLine(car));

//OR use .Count

cars.GroupBy(car => car.Model)
    .Select(c => new { c.Key, num = c.Count(car => car.Year >= 2000) })
    .ToList()
    .ForEach(car => Console.WriteLine(car));

Console.WriteLine("---------------------");


//Display a list of makes that have at least 2 models with >= 400hp
cars.Where(car => car.hp >= 400)
    .GroupBy(car => car.Make)
    .Select(rs => new { Make = rs.Key, NumofCars = rs.Count()})
    .Where(car => car.NumofCars > 2)
    .ToList()
    .ForEach(car => Console.WriteLine(car));


//Display there average hp per make 
//group by makes
cars.GroupBy(car => car.Make)
    .Select(rs => new { make = rs.Key, average = rs.Average(car => car.hp) });


//How many makes build cars with hp between 0..100, 200..300, 400..500
cars.GroupBy(car => car.hp switch
{
    <= 100 => "0..100",
    <= 200 => "200..300",
    <= 400 => "400..500"
}).Select(car => new { HPCat = car.Key, NumbOfMAke = car.Select(c => c.Make).Distinct().Count() });

class Car 
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("car_make")]
    public string Make { get; set; }

    [JsonPropertyName("car_model")]
    public string Model { get; set; }

    [JsonPropertyName("car_year")]
    public int Year { get; set; }

    [JsonPropertyName("doors")]
    public int Doors { get; set; }

    [JsonPropertyName("hp")]
    public int hp { get; set; }

}
























////only even numbers
//var result = GenerateNumbers(10)
//    .Where(n =>  n % 2 == 0)
//    .Select( n => n * 3);

//// 
////foreach (var item in result) { 
////    Console.WriteLine(item);    
////}
//result = result.OrderByDescending(n => n);
//result = result.Select(n => n * 4);

//Console.WriteLine(result.Count());


//IEnumerable<int> GenerateNumbers(int maxNumber)
//{
//    for (var i = 0; i <= maxNumber; i++)
//    {
//        // will return without array of int
//         yield return i;
//    }
    
//}


