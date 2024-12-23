﻿using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

string host, db, user, pass;

host = "localhost";
user = "root";
pass = "";
db = "tubescs";

string connectionString = $"Server={host};Database={db};User Id={user};Password={pass};";
int menu = 0;

MySqlConnection connection = new MySqlConnection(connectionString);

try
{
    connection.Open();
    // Console.WriteLine("Koneksi berhasil!");
    while (true)
    {
        ClearScreen();
        firstMenu(menu, connection);
        Console.Write("\n\nLanjutkan? (y/n) > ");
        String? lanjut = Console.ReadLine();
        if (lanjut == "n" || lanjut == "N")
        {
            Console.WriteLine("Terima kasih telah menggunakan program ini!");
            break;
        }
    }
    connection.Close();
}
catch (Exception ex)
{
    Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
}

static void ClearScreen()
{
    Console.Clear();
}

static string HashPassword(string password)
{
    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
}

static void TampilkanPesan(MySqlConnection connection, string query)
{
    string selectQuery = query;
    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
    using (MySqlDataReader reader = command.ExecuteReader())
    {
        int number = 1;
        Console.WriteLine("\nData dalam tabel message:");
        while (reader.Read())
        {
            Console.WriteLine($"\nPesan {number++}:");
            Console.WriteLine($"IDUnik: {reader["id_msg"]}\nTo: {reader["to_msg"]}\nMessage: {reader["msg"]}\nTime: {reader["date_msg"]}");
        }
    }
}

static void TambahkanPesan(MySqlConnection connection, string query)
{
    using (MySqlCommand command = new MySqlCommand(query, connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("\nPesan berhasil ditambahkan!");
    }
}

static void HapusPesan(MySqlConnection connection, string query)
{
    using (MySqlCommand command = new MySqlCommand(query, connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("\nPesan berhasil dihapus!");
    }
}

static void EditPesan(MySqlConnection connection, string query)
{
    using (MySqlCommand command = new MySqlCommand(query, connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("\nPesan berhasil diedit!");
    }
}


static void Menu(int menu, MySqlConnection connection)
{
    Console.WriteLine("\nMenu:");
    Console.Write("1. Tampilkan semua pesan\n2. Tambahkan pesan\n3. Hapus pesan\n4. Edit pesan\n5. Keluar\n\nMasukkan pilihan > ");
    menu = Convert.ToInt32(Console.ReadLine());
    if (menu == 1)
    {
        ClearScreen();
        TampilkanPesan(connection, "SELECT * FROM message");
    }
    else if (menu == 2)
    {
        ClearScreen();
        Console.Write("Untuk Siapa Pesan Ini > ");
        string? to_msg = Console.ReadLine();
        Console.Write("Masukkan Pesan > ");
        string? msg = Console.ReadLine();
        TambahkanPesan(connection, $"INSERT INTO message (to_msg, msg, date_msg) VALUES ('{to_msg}', '{msg}', NOW());");
    }
    else if (menu == 3)
    {
        ClearScreen();
        Console.Write("Masukkan IDUnik > ");
        string? id_msg = Console.ReadLine();
        HapusPesan(connection, $"DELETE FROM message WHERE id_msg = {id_msg};");
    }
    else if (menu == 4)
    {
        ClearScreen();
        Console.Write("Masukkan IDUnik > ");
        string? id_msg = Console.ReadLine();
        Console.Write("Untuk Siapa Pesan Ini > ");
        string? to_msg = Console.ReadLine();
        Console.Write("Masukkan Pesan > ");
        string? msg = Console.ReadLine();
        if (id_msg == null)
        {
            Console.WriteLine("IDUnik tidak boleh kosong!");
        }
        if (to_msg != "" && msg != "") { EditPesan(connection, $"UPDATE message SET to_msg = '{to_msg}', msg = '{msg}' WHERE id_msg = {id_msg};"); }
        if (msg != "") { EditPesan(connection, $"UPDATE message SET msg = '{msg}' WHERE id_msg = {id_msg};"); }
        if (to_msg != "") { EditPesan(connection, $"UPDATE message SET to_msg = '{to_msg}' WHERE id_msg = {id_msg};"); }
    }
    else if (menu == 5)
    {
        ClearScreen();
        Console.WriteLine("Terima kasih telah menggunakan program ini!");
        Environment.Exit(0);
    }
    else
    {
        ClearScreen();
        Console.WriteLine("Pilihan tidak valid!");
    }
}

static void Login(int menu, MySqlConnection connection)
{
    Console.Write("Masukkan email: ");
    String? username = Console.ReadLine();
    Console.Write("Masukkan Password: ");
    String? password = Console.ReadLine();


    string passwordhash = HashPassword(password ?? "");
    using (MySqlCommand command = new MySqlCommand($"SELECT * FROM user WHERE email_user = '{username}' AND password_user = '{passwordhash}'", connection))
    using (MySqlDataReader reader = command.ExecuteReader())

        if (reader.Read())
        {
        }
        else
        {
            Console.WriteLine("\nLogin Gagal!");
            return;
        }
    while (true)
    {
        ClearScreen();
        Menu(menu, connection);
        Console.Write("\n\nLanjutkan? (y/n) > ");
        String? lanjut = Console.ReadLine();
        if (lanjut == "n" || lanjut == "N")
        {
            break;
        }
    }

}

static void Register(MySqlConnection connection)
{
    Console.Write("Masukkan Username: ");
    String? username = Console.ReadLine();
    Console.Write("Masukkan Email: ");
    String? email = Console.ReadLine();
    Console.Write("Masukkan Password: ");
    String? password = Console.ReadLine();
    string passwordhash = HashPassword(password ?? "");
    using (MySqlCommand command = new MySqlCommand($"SELECT * FROM user WHERE email_user = '{email}'", connection))
    using (MySqlDataReader reader = command.ExecuteReader())

        if (!reader.Read())
        {
        }
        else
        {
            Console.WriteLine("\nEmail sudah terdaftar!");
            return;
        }
    addDataRegister(connection, $"INSERT INTO user (name_user, email_user, password_user) VALUES ('{username}', '{email}', '{passwordhash}');");


}

static void addDataRegister(MySqlConnection connection, string query)
{
    using (MySqlCommand command = new MySqlCommand(query, connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("\nData berhasil ditambahkan!");
    }
}

static void firstMenu(int menu, MySqlConnection connection)
{
    Console.WriteLine("\nMenu:");
    Console.Write("1. Login\n2. Register\n3. Keluar\n\nMasukkan pilihan > ");
    menu = Convert.ToInt32(Console.ReadLine());
    if (menu == 1)
    {
        ClearScreen();
        Login(menu, connection);
    }
    else if (menu == 2)
    {
        ClearScreen();
        Register(connection);
    }
    else if (menu == 3)
    {
        ClearScreen();
        Console.WriteLine("Terima kasih telah menggunakan program ini!");
        Environment.Exit(0);
    }
    else
    {
        ClearScreen();
        Console.WriteLine("Pilihan tidak valid!");
    }
}