using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CentralDeErros.ServicesTests
{
    public class FakeContext : IDisposable
    {
        public DbContextOptions<CentralDeErrosDbContext> FakeOptions { get; }

        private Dictionary<Type, string> DataFileNames { get; } =
            new Dictionary<Type, string>();
        private string FileName<T>() { return DataFileNames[typeof(T)]; }

        public CentralDeErrosDbContext context;
        public FakeContext(string testName)
        {
            FakeOptions = new DbContextOptionsBuilder<CentralDeErrosDbContext>()
                .UseInMemoryDatabase(databaseName: $"CentralDeErrors_{testName}")
                .Options;
            
            context = new CentralDeErrosDbContext(FakeOptions);

            DataFileNames.Add(typeof(Model.Models.Environment), $"FakeData{Path.DirectorySeparatorChar}environment.json");
            DataFileNames.Add(typeof(Error), $"FakeData{Path.DirectorySeparatorChar}error.json");
            DataFileNames.Add(typeof(Level), $"FakeData{Path.DirectorySeparatorChar}level.json");
            DataFileNames.Add(typeof(Microsservice), $"FakeData{Path.DirectorySeparatorChar}microsservice.json");

        }
        

        public void FillWithAll()
        {
            FillWith<Model.Models.Environment>();
            FillWith<Error>();
            FillWith<Level>();
            FillWith<Microsservice>();
        }

        public void FillWith<T>() where T : class
        {
            using (var context = new CentralDeErrosDbContext(FakeOptions))
            {
                if (context.Set<T>().Count() == 0)
                {
                    foreach (T item in GetFakeData<T>())
                        context.Set<T>().Add(item);
                    context.SaveChanges();
                }
            }
        }

        public List<T> GetFakeData<T>()
        {
            string content = File.ReadAllText(FileName<T>());
            return JsonConvert.DeserializeObject<List<T>>(content);
        }


        public CentralDeErrosDbContext GenerateContext()
        {
            FillWithAll();
            context = new CentralDeErrosDbContext(FakeOptions);
            return context;
        }
        public CentralDeErrosDbContext GenerateEmptyContext()
        {
            context = new CentralDeErrosDbContext(FakeOptions);
            return context;
        }

        public void Dispose()
        {
            context?.Database.EnsureDeleted();
            context?.Dispose();
        }
    }
}
