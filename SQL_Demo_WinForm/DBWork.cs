using System;
using System.Collections.Generic;
using System.Data.SQLite; // Добавляем библиотеку

namespace SQL_Demo_WinForm
{
    internal class DBWork
    {
        static public string MakeDB(string _dbname = "test02")
        {
            string result = "Ошибка чтения данных ...";
            string path = $"Data Source={_dbname};"; // Путь для подключения к БД
            // Переменная для sql-запросов (таблица запросов)
            string init_db = "CREATE TABLE IF NOT EXISTS " + // Создаём таблицу, если она не существует
                "Category " +
                "(id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Name VARCHAR);";
            string init_data = "INSERT INTO " + // Заполняем таблицу данными
                "Category" +
                "(Name)" +
                "VALUES" +
                "('SportwatchWatch');";
            string showAllData = "SELECT * FROM Category;"; // Переменная для просмотра данных
            SQLiteConnection conn = new SQLiteConnection(path);
            // Создание команд, которые мы будем последовательно выполнять
            SQLiteCommand cmd01 = conn.CreateCommand();
            SQLiteCommand cmd02 = conn.CreateCommand();
            SQLiteCommand cmd03 = conn.CreateCommand();
            cmd01.CommandText = init_db; // Инициализируем базу данных
            cmd02.CommandText = init_data; // Заполнение данных
            cmd03.CommandText = showAllData; // Вернёт что-то или ничего
            conn.Open(); // Открытие соединения с БД
            cmd01.ExecuteNonQuery(); // Наш запрос выполнится, но его результат успех или неуспех
            cmd02.ExecuteNonQuery(); // Возвращет кол-во изменённых строк            
            var reader = cmd03.ExecuteReader(); // Будет возвращать System.object, который можно изменять
            if (reader.HasRows) // Если объект имеет поля
            {
                result = " "; // Обнуляем результат
                // reader.FieldCount // Если в while делать цикл for
                while (reader.Read()) // Пока идёт чтение таблицы
                {
                    result += reader.GetValue(0).ToString();
                    result += " | "; // Разделитель
                    result += reader.GetValue(1).ToString();
                    result += "\n"; // Когда прочитал всю строку
                }
            }
            conn.Close(); // Закрытие соединения с БД
            return result;
        }
    }
}
