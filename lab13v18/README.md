# Лабораторна робота №13 — Generic-методи, Func/Action/Predicate (Варіант 18)

## Про проєкт

Клас `FunctionalUtils` з generic-методами для роботи зі списками та делегатами.

## Реалізовані методи (варіант 18)

- `Map<T, TResult>` — перетворення елементів через `Func<T, TResult>`.
- `Filter<T>` — відбір елементів через `Func<T, bool>`.
- `Sum<T, TNum>` — сума значень, обмеження `where TNum : INumber<TNum>`.
- `Count<T>` — кількість елементів за умовою `Predicate<T>`.
- `FirstOrDefault<T>` — перший елемент за умовою або `default`.

## Демонстрація в Main

Кожен метод викликано для `int`, `string` і власного класу `Product`, умови передано лямбда-виразами.

## Приклад результату

![alt text]({CBE6CD2E-B77C-443D-A0C8-F4FA8169A397}.png)