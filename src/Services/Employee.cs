using Newtonsoft.Json;

namespace EmployeeParse.Services;
public class Employee {
    public int Id { get; set; }
    public string FirstName { get; set; } = "null";
    public string LastName { get; set; } = "null";
    public decimal SalaryPerHour { get; set; }
}

public class EmployeeManager(string filePath) {
    readonly Logger Log = new();
    private readonly string filePath = filePath;

    // Json manipulation methods
    public List<Employee> GetAllEmployees() {
        if (!File.Exists(filePath))
            return [];

        var jsonData = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Employee>>(jsonData) ?? [];
    }

    public void SaveAllEmployees(List<Employee> employees) {
        var jsonData = JsonConvert.SerializeObject(employees, Formatting.Indented);
        try {
            File.WriteAllText(filePath, jsonData);
            Log.NewLog(Logger.Severity.Info, $"Записано успешно!");
        } catch (Exception ex) {
            Log.NewLog(Logger.Severity.Critical, $"Ошибка при записи!\n{ex.Message}");
            return;
        }
    }

    public void AddEmployee(string firstName, string lastName, decimal salary) {
        var employees = GetAllEmployees();
        var newId = employees.Count != 0 ? employees.Max(e => e.Id) + 1 : 1;

        var newEmployee = new Employee {
            Id = newId,
            FirstName = firstName,
            LastName = lastName,
            SalaryPerHour = salary
        };

        employees.Add(newEmployee);
        SaveAllEmployees(employees);
        Log.NewLog(Logger.Severity.Info, $"Успешно добавлена новая запись!");
    }

    public void UpdateEmployee(int id, string? firstName = null, string? lastName = null, decimal? salary = null) {
        var employees = GetAllEmployees();
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null) {
            Log.NewLog(Logger.Severity.Critical, $"Работник с ID = {id} не найден.");
            return;
        }

        if (firstName != null) employee.FirstName = firstName;
        if (lastName != null) employee.LastName = lastName;
        if (salary.HasValue) employee.SalaryPerHour = salary.Value;

        SaveAllEmployees(employees);
        Log.NewLog(Logger.Severity.Info, $"Успешно обновлена запись!");
    }

    public void GetEmployee(int id) {
        var employees = GetAllEmployees();
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null) {
            Log.NewLog(Logger.Severity.Critical, $"Работник с ID = {id} не найден.");
            return;
        }

        Log.NewLog(Logger.Severity.Info, $"Id = {employee.Id}, FirstName = {employee.FirstName}, LastName = {employee.LastName}, SalaryPerHour = {employee.SalaryPerHour}");
    }

    public void DeleteEmployee(int id) {
        var employees = GetAllEmployees();
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null) {
            Log.NewLog(Logger.Severity.Info, $"Работник с ID = {id} не найден.");
            return;
        }

        employees.Remove(employee);
        SaveAllEmployees(employees);
        Log.NewLog(Logger.Severity.Info, $"Запись успешно удалена!");
    }

    public void GetAllEmployeesPrint() {
        var employees = GetAllEmployees();
        Log.NewLog(Logger.Severity.Info, "Список всех работников:");
        foreach (var employee in employees) {
            Console.WriteLine($"Id = {employee.Id}, FirstName = {employee.FirstName}, LastName = {employee.LastName}, SalaryPerHour = {employee.SalaryPerHour}");
        }
    }
}