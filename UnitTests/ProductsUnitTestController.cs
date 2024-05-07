using AutoMapper;
using CatalogAPI.Context;
using CatalogAPI.DTOs.Mappings;
using CatalogAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPIxUnitTests.UnitTests
{
    internal class ProductsUnitTestController
    {
        // Definição de variáveis
        private IUnitOfWork repository;
        private IMapper mapper;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        // Connection string
        public static string connectionString = 
            "Server=localhost;Database=catalogdb;Uid=root;Pwd=1234";

        // Construtores
        static ProductsUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        // Construtor padrão
        public ProductsUnitTestController()
        {
            // Mapper setup
            var config = new MapperConfiguration(config =>
            {
                config.AddProfile(new ProductDTOMappingProfile());
            });
            mapper = config.CreateMapper();

            // Context setup
            var context = new AppDbContext(dbContextOptions);

            // Repository setup
            repository = new UnitOfWork(context);
        }

    }
}
