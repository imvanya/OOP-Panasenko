# Звіт з аналізу інкапсуляції в Open-Source проєкті

## 1. Обраний проєкт

- **Назва:** ASP.NET Core
- **Посилання на GitHub:** https://github.com/dotnet/aspnetcore
- **Класи, які я розглядав:**
  - `CookieBuilder` — https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http.Abstractions/src/CookieBuilder.cs
  - `PathString` — https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http.Abstractions/src/PathString.cs

Я обрав ASP.NET Core, бо це відомий фреймворк від Microsoft з відкритим кодом і зрозумілою структурою папок. Обидва класи лежать в одній збірці — `Http.Abstractions`. Код у фрагментах я трохи скоротив (прибрав коментарі), але суть не змінював.

## 2. Аналіз інкапсуляції

### Клас: `CookieBuilder`

- **Для чого потрібен:** зберігає налаштування cookie (назва, шлях, домен, `HttpOnly`, час життя тощо), а метод `Build()` створює з них готові параметри cookie.
- **Поля:** обидва приватні, публічних полів немає.

```csharp
private string? _name;
private List<string>? _extensions;
```

- **Властивості:**
  - `Name` — звичайна властивість з `get` і `set`, але в `set` є перевірка (про неї нижче).
  - `Path`, `Domain`, `HttpOnly` та інші — автоматичні властивості `{ get; set; }`, бо додаткова логіка їм не потрібна.
  - `Extensions` — тільки для читання (є `get`, немає `set`). Список створюється тільки тоді, коли до нього вперше звернулися.

```csharp
public virtual string? Path { get; set; }

public IList<string> Extensions
{
    get => _extensions ??= new List<string>();
}
```

  Тобто підмінити сам список на інший не можна, але додавати в нього елементи можна.

- **Індексатори та оператори:** немає.

### Клас: `PathString`

- **Для чого потрібен:** зберігає шлях з URL (наприклад, `/api/users`) і правильно екранує спеціальні символи.
- **Поля:** більшість приватні або `internal`. Єдине публічне — `Empty` (порожній шлях), але воно `readonly`, тому його не можна змінити. По суті це константа.

```csharp
private static readonly SearchValues<char> s_validPathChars = ...;
public static readonly PathString Empty = new(string.Empty);
```

- **Властивості:** `Value` — тільки для читання (`{ get; }`), значення записується один раз у конструкторі. `HasValue` рахується з `Value`, окремого поля для неї немає.

```csharp
public string? Value { get; }

public bool HasValue
{
    get { return !string.IsNullOrEmpty(Value); }
}
```

  Сам тип оголошено як `readonly struct`, тому після створення об'єкт змінити не можна взагалі.

- **Індексатори:** немає.
- **Оператори:** є. Вони роблять роботу зручнішою:

```csharp
public static bool operator ==(PathString left, PathString right) => left.Equals(right);
public static PathString operator +(PathString left, PathString right) => left.Add(right);
public static implicit operator PathString(string? s) => ConvertFromString(s);
public static implicit operator string(PathString path) => path.ToString();
```

  - `==` порівнює шляхи без урахування регістру;
  - `+` склеює два шляхи;
  - неявні перетворення дозволяють писати `PathString p = "/api";` і використовувати `PathString` там, де потрібен `string`.

## 3. Практики валідації у властивостях

**Приклад 1. Перевірка в `set` (`CookieBuilder.Name`).** Cookie без імені не має сенсу, тому порожнє значення або `null` одразу викликає помилку:

```csharp
set => _name = !string.IsNullOrEmpty(value)
    ? value
    : throw new ArgumentException(Resources.ArgumentCannotBeNullOrEmpty, nameof(value));
```

Тут використано `if`-подібну перевірку (тернарний оператор) і виняток `ArgumentException`.

**Приклад 2. Перевірка в конструкторі (`PathString`).** У `Value` немає `set`, тому перевіряти значення можна тільки в конструкторі. Непорожній шлях повинен починатися з `/` або `\`:

```csharp
public PathString(string? value)
{
    if (!string.IsNullOrEmpty(value) && value[0] is not '/' and not '\\')
    {
        throw new ArgumentException(
            Resources.FormatException_PathMustStartWithSlashOrBackslash(nameof(value)),
            nameof(value));
    }
    Value = value;
}
```

Після такої перевірки в об'єкті завжди коректне значення.

**Приклад 3. Перевірка аргументів методу (`CookieBuilder.Build`).**

```csharp
ArgumentNullException.ThrowIfNull(context);
```

Це перевірка на початку методу: якщо передали `null`, метод одразу зупиняється з помилкою.

**Атрибути `DataAnnotations`** у цих двох класах не використовуються. Вони більше підходять для моделей, які приходять із форм або JSON, а ці класи — низькорівневі.

**Мої висновки щодо валідації:**
- Спосіб «перевірити в `set` або конструкторі й викинути виняток» простий і зрозумілий, помилка з'являється там, де передали неправильні дані.
- Валідація в конструкторі незмінного типу (як у `PathString`) надійна, бо потім перевіряти нічого не треба.
- Недолік: винятки працюють повільніше, тому для даних від користувача іноді краще повертати результат перевірки, а не кидати помилку.
- Дивна деталь: `Name` має тип `string?` (тобто `null` можливий), але `set` забороняє присвоювати `null`. Тут `null` просто означає, що ім'я ще не задали.

## 4. Загальні висновки

1. В обох класах інкапсуляція зроблена правильно: поля приховані, доступ іде через властивості.
2. Вид властивості вибирають під задачу: автоматична для простих налаштувань, з логікою — де потрібна перевірка, тільки для читання — де значення не повинно змінюватися.
3. `readonly struct` з властивістю тільки для читання дає об'єкт, який неможливо зламати після створення.
4. Перевантажені оператори (`==`, `+`, перетворення) зручні для користувача і не порушують інкапсуляцію, бо вся логіка залишається всередині класу.
5. Для своїх проєктів я візьму звідси: перевіряти дані в `set` або конструкторі, не робити публічних змінних полів і використовувати властивості тільки для читання там, де дані не мають змінюватися.
