using TN.Prototype.CleanArchitecture.Domain.Entities;
using TN.Prototype.CleanArchitecture.Infrastructure.Persistence.Constants;
using FluentMigrator;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace TN.Prototype.CleanArchitecture.Infrastructure.Persistence.Migrations
{
    [Migration(20210715153700)]
    public class CreateDb : Migration
    {
        public override void Up()
        {
            Create.Table(nameof(Employee))
                  .WithColumn(nameof(Employee.Id)).AsInt32().PrimaryKey().Identity()
                  .WithColumn(nameof(Employee.FirstName)).AsString(50).NotNullable()
                  .WithColumn(nameof(Employee.LastName)).AsString(50).NotNullable()
                  .WithColumn(nameof(Employee.JobTitle)).AsString(100).NotNullable()
                  .WithColumn(nameof(Employee.HourlyRate)).AsDecimal().NotNullable()
                  .WithColumn(nameof(Employee.Creator)).AsString(50).Nullable()
                  .WithColumn(nameof(Employee.CreateDate)).AsDateTime2().Nullable()
                  .WithColumn(nameof(Employee.Modifier)).AsString(50).Nullable()
                  .WithColumn(nameof(Employee.ModifyDate)).AsDateTime2().Nullable();

            var sampleData = new List<Tuple<string, string, string, decimal>>()
            {
                    new Tuple<string, string, string, decimal>("John", "Smith", "Software Developer", 45.00m),
                    new Tuple<string, string, string, decimal>("Emily", "Johnson", "Project Manager", 55.00m),
                    new Tuple<string, string, string, decimal>("Michael", "Brown", "UI/UX Designer", 50.00m),
                    new Tuple<string, string, string, decimal>("Sarah", "Davis", "Data Analyst", 40.00m),
                    new Tuple<string, string, string, decimal>("David", "Wilson", "Systems Analyst", 48.00m),
                    new Tuple<string, string, string, decimal>("Jessica", "Moore", "QA Engineer", 42.00m),
                    new Tuple<string, string, string, decimal>("Daniel", "Taylor", "Frontend Developer", 47.00m),
                    new Tuple<string, string, string, decimal>("Laura", "Anderson", "Backend Developer", 52.00m),
                    new Tuple<string, string, string, decimal>("James", "Thomas", "DevOps Engineer", 60.00m),
                    new Tuple<string, string, string, decimal>("Olivia", "Martinez", "Business Analyst", 46.00m)
            };

            foreach (var data in sampleData)
            {
                Insert.IntoTable(nameof(Employee))
                      .Row(
                            new
                            {
                                FirstName = data.Item1,
                                LastName = data.Item2,
                                JobTitle = data.Item3,
                                HourlyRate = data.Item4,
                                Creator = "Migration",
                                CreateDate = DateTime.Now
                            }
                      );
            }
        }

        public override void Down()
        {
            Delete.Table(nameof(Employee));
        }
    }
}
