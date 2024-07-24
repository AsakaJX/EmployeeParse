using System.Text;
using EmployeeParse.Services;
namespace EmployeeParse;
class Program {
    static readonly Logger Log = new();
    static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;

        if (args.Length == 0) {
            Log.NewLog(Logger.Severity.Critical, "Не было предоставлено аргументов.");
            Console.ReadLine();
            return;
        }

        string filePath = "employees.json";
        if (!File.Exists(filePath)) {
            File.Create(filePath);
        }

        var manager = new Services.EmployeeManager(filePath);
        ParseArguments(args, manager);

        Console.ReadLine();
    }
    static void ParseArguments(string[] args, Services.EmployeeManager manager) {
        try {
            switch (args[0]) {
                case "-add":
                    var firstName = args.FirstOrDefault(a => a.StartsWith("FirstName:"))?.Split(':')[1] ?? "null";
                    var lastName = args.FirstOrDefault(a => a.StartsWith("LastName:"))?.Split(':')[1] ?? "null";
                    var salaryString = args.FirstOrDefault(a => a.StartsWith("Salary:"))?.Split(':')[1];
                    if (decimal.TryParse(salaryString, out decimal salary)) {
                        manager.AddEmployee(firstName, lastName, salary);
                    } else {
                        Log.NewLog(Logger.Severity.Warning, "Неправильное значение зарплаты.");
                    }
                    break;

                case "-update":
                    var idString = args.FirstOrDefault(a => a.StartsWith("Id:"))?.Split(':')[1];
                    if (int.TryParse(idString, out int id)) {
                        firstName = args.FirstOrDefault(a => a.StartsWith("FirstName:"))?.Split(':')[1];
                        lastName = args.FirstOrDefault(a => a.StartsWith("LastName:"))?.Split(':')[1];
                        salaryString = args.FirstOrDefault(a => a.StartsWith("Salary:"))?.Split(':')[1];
                        decimal? newSalary = null;
                        if (salaryString != null && decimal.TryParse(salaryString, out decimal parsedSalary)) {
                            newSalary = parsedSalary;
                        }
                        manager.UpdateEmployee(id, firstName, lastName, newSalary);
                    } else {
                        Log.NewLog(Logger.Severity.Warning, "Неправильное Id.");
                    }
                    break;

                case "-get":
                    idString = args.FirstOrDefault(a => a.StartsWith("Id:"))?.Split(':')[1];
                    if (int.TryParse(idString, out id)) {
                        manager.GetEmployee(id);
                    } else {
                        Log.NewLog(Logger.Severity.Warning, "Неправильное Id.");
                    }
                    break;

                case "-delete":
                    idString = args.FirstOrDefault(a => a.StartsWith("Id:"))?.Split(':')[1];
                    if (int.TryParse(idString, out id)) {
                        manager.DeleteEmployee(id);
                    } else {
                        Log.NewLog(Logger.Severity.Warning, "Неправильное Id.");
                    }
                    break;

                case "-getall":
                    manager.GetAllEmployeesPrint();
                    break;

                default:
                    Log.NewLog(Logger.Severity.Warning, "Неизвестная команда!");
                    break;
            }
        } catch (Exception ex) {
            Log.NewLog(Logger.Severity.Critical, $"Возникла неизвестная ошибка: \n{ex.Message}");
        }
    }
}