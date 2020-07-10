using System.Collections.Generic;

namespace ShopApp.DataAccess.Concrete.Memory
{
    public static class FakeDatabase
    {
        public static List<City> Cities = new List<City>()
        {
            new City{ Id = 34, Name ="İstanbul"},
            new City{ Id = 35, Name ="İzmir"},
            new City{ Id = 06, Name ="Ankara"},
            new City{ Id = 24, Name ="Erzincan"},
            new City{ Id = 25, Name ="Erzurum"},
            new City{ Id = 07, Name ="Antalya"},
            new City{ Id = 41, Name ="Kocaeli"}
        };

        public static List<Month> Months = new List<Month>()
        {
            new Month{ Id = 1, Name ="Ocak"},
            new Month{ Id = 2, Name ="Şubat"},
            new Month{ Id = 3, Name ="Mart"},
            new Month{ Id = 4, Name ="Nisan"},
            new Month{ Id = 5, Name ="Mayıs"},
            new Month{ Id = 6, Name ="Haziran"},
            new Month{ Id = 7, Name ="Temmuz"},
            new Month{ Id = 8, Name ="Ağustos"},
            new Month{ Id = 9, Name ="Eylül"},
            new Month{ Id = 10, Name ="Ekim"},
            new Month{ Id = 11, Name ="Kasım"},
            new Month{ Id = 12, Name ="Aralık"},
        };

        public static int[] Years = { 2030, 2029, 2028, 2027, 2026, 2025, 2024, 2023, 2022, 2021, 2020, 2019 };
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
