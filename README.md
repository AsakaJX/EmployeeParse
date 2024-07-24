# Requirements
> [!IMPORTANT]
> Install `Newtonsoft.Json` package: `dotnet add package Newtonsoft.Json --version 13.0.3`

# Usage
`-add FirstName:John LastName:Doe Salary:100,50` - Добавляет новую запись в файл с указанными данными.

`-update Id:123 FirstName:James` - Обновляет для указанного Id определенное поле.

`-delete Id:123` - Удаляет запись по Id.

`-get Id:123` - Получает все поля по Id.

`-getall` - Получает все записи и их поля.

> [!IMPORTANT]
> Также в консоль будут выводиться логи о каждом действии.
