namespace EmployeeParse.Services;
class Logger {
    public enum Severity {
        Info, Warning, Critical
    };
    public void NewLog(Severity severity, string msg) {
        Console.WriteLine($"[ {severity} ] -> {msg}");
    }
}