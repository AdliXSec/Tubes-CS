using MySql.Data.MySqlClient;

string connectionString = "Server=localhost;Database=anonchat;User Id=root;Password=;";
int menu = 0;

MySqlConnection connection = new MySqlConnection(connectionString);

try
{
    connection.Open();
    // Console.WriteLine("Koneksi berhasil!");
    while (true)
    {
        Menu(menu, connection);
        Console.Write("\n\nLanjutkan? (y/n) > ");
        String? lanjut = Console.ReadLine();
        if (lanjut == "n" || lanjut == "N")
        {
            break;
        }
    }
    connection.Close();
}
catch (Exception ex)
{
    Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
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
        TampilkanPesan(connection, "SELECT * FROM message");
    }
    else if (menu == 2)
    {
        Console.Write("Untuk Siapa Pesan Ini > ");
        string? to_msg = Console.ReadLine();
        Console.Write("Masukkan Pesan > ");
        string? msg = Console.ReadLine();
        TambahkanPesan(connection, $"INSERT INTO message (to_msg, msg, date_msg) VALUES ('{to_msg}', '{msg}', NOW());");
    }
    else if (menu == 3)
    {
        Console.Write("Masukkan IDUnik > ");
        string? id_msg = Console.ReadLine();
        HapusPesan(connection, $"DELETE FROM message WHERE id_msg = {id_msg};");
    }
    else if (menu == 4)
    {
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
        Console.WriteLine("Terima kasih telah menggunakan program ini!");
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Pilihan tidak valid!");
    }
}