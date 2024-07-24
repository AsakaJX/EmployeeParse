using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using EmployeeParse.Services;

namespace Tests;
[TestFixture]
public class EmployeeManagerTests {
    private string testFilePath = "test_employees.json";

    [SetUp]
    public void SetUp() {
        if (File.Exists(testFilePath))
            File.Delete(testFilePath);
    }

    [TearDown]
    public void TearDown() {
        if (File.Exists(testFilePath))
            File.Delete(testFilePath);
    }

    [Test]
    public void AddEmployee_ShouldAddEmployeeToFile() {
        var manager = new EmployeeManager(testFilePath);
        manager.AddEmployee("John", "Doe", 100.50m);

        var employees = manager.GetAllEmployees();
        Assert.AreEqual(1, employees.Count);
        Assert.AreEqual("John", employees[0].FirstName);
        Assert.AreEqual("Doe", employees[0].LastName);
        Assert.AreEqual(100.50m, employees[0].SalaryPerHour);
    }
}