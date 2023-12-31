﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductAPI.DbContexts;

namespace ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220826213910_initial_local")]
    partial class initial_local
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "6.0.0-preview.3.21201.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductAPI.Models.ProcessProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Stock")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ProcessProducts");
                });

            modelBuilder.Entity("ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Stock")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Food",
                            Description = "Bread is a food consisting of flour or meal that is moistened, kneaded into dough, and often fermented using yeast, and it has been a major sustenance since prehistoric times.",
                            ImageUrl = "https://dojoblob.blob.core.windows.net/store/bread.jpg",
                            Name = "Bread",
                            Price = 1.0,
                            Stock = 10.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Food",
                            Description = "Butter is a dairy product made from the fat and protein components of churned cream. It is a semi-solid emulsion at room temperature, consisting of approximately 80% butterfat. It is used at room temperature as a spread, melted as a condiment, and used as a fat in baking, sauce-making, pan frying, and other cooking procedures.",
                            ImageUrl = "https://dojoblob.blob.core.windows.net/store/butter.jpg",
                            Name = "Butter",
                            Price = 13.99,
                            Stock = 10.0
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Food",
                            Description = "Flour is a powder made by grinding raw grains, roots, beans, nuts, or seeds",
                            ImageUrl = "https://dojoblob.blob.core.windows.net/store/flour.jpg",
                            Name = "Flour",
                            Price = 10.99,
                            Stock = 10.0
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Drink",
                            Description = "Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain flowering plants in the Coffea genus",
                            ImageUrl = "https://dojoblob.blob.core.windows.net/store/coffee.jpg",
                            Name = "Coffee",
                            Price = 15.0,
                            Stock = 10.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
