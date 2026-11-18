# Самостійна робота №10
**Тема:** Колекції в практиці ООП: інвертований індекс і вимірювання продуктивності
**Проєкт:** `InvertedIndexDemo`

Опис реалізації, таблиця вимірювань і висновки: [REPORT.md](REPORT.md).

## Структура

| Файл | Призначення |
|---|---|
| `Document.cs` | Клас `Document` (`Id`, `Title`, `Content`) |
| `TextProcessor.cs` | `Tokenize` (Regex `\w+`, нижній регістр) та `RemoveStopWords` |
| `InvertedIndex.cs` | Індекс: `SearchSingleWord`, `SearchMultipleWords` (AND, `IntersectWith`), `SearchAnyWord` (OR, `UnionWith`) |
| `TestData.cs` | П'ять тестових документів і генератор для 10/100/1000/10000 документів |
| `PerformanceTest.cs` | Порівняння лінійного пошуку та індексу |
| `Program.cs` | Інтерактивний пошук і запуск вимірювань |

## Запуск

```
dotnet run            # інтерактивний пошук
dotnet run -- bench   # вимірювання продуктивності (Markdown-таблиця)
```

У інтерактивному режимі також працюють команди: `any:слово1 слово2` (пошук OR) та `bench`.

## Приклад виводу програми

```
Loaded 5 documents
Index contains 37 unique words
Tips: 'any:word1 word2' - OR search, 'bench' - performance test

Enter search query (or 'exit'): programming language

--- Search Results (AND) ---
[1] Introduction to C#
  C# is a modern object-oriented programming language developed by Microsoft....
[2] Java Basics
  Java is a popular programming language known for its portability and performance....
[3] Python Overview
  Python is a high-level programming language with simple syntax....

Found 3 documents

Enter search query (or 'exit'): c# async

--- Search Results (AND) ---
[4] C# Advanced Features
  C# supports LINQ, async/await, and many modern programming paradigms....

Found 1 documents

Enter search query (or 'exit'): any:java python

--- Search Results (OR) ---
[2] Java Basics
  Java is a popular programming language known for its portability and performance....
[3] Python Overview
  Python is a high-level programming language with simple syntax....
[5] Comparing Languages
  C#, Java, and Python are among the most popular programming languages today....

Found 3 documents

Enter search query (or 'exit'): exit
```

