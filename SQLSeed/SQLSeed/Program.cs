using SQLSeed.Data;
using SQLSeed.Models;
using System.Text.Json;

//string counties = File.ReadAllText("Counties.json");
string walks = File.ReadAllText("Walks.json");
//string difficulties = File.ReadAllText("Difficulties.json");
//var listCounties = JsonSerializer.Deserialize<IEnumerable<County>>(counties);
var listWalks = JsonSerializer.Deserialize<IEnumerable<Walk>>(walks);
//var listDifficulties = JsonSerializer.Deserialize<IEnumerable<Difficulty>>(difficulties);



SeedDbContext db = new SeedDbContext();

//db.Counties.AddRange(listCounties);
//db.Difficulties.nge(listDifficulties);
db.Walks.AddRange(listWalks);
db.SaveChanges();





