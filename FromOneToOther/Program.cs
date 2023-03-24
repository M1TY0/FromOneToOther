using MySql.Data.MySqlClient;
//string n = Console.ReadLine();
//Console.WriteLine(GetWordsFromServerDb(int.Parse(n)));
for (int i = 0; i < CompareNumberOfWrodsInBothDbs(); i++)
{
    if (CheckWordsFromServerDb(GetWordsFromLaptopDb(i)))
    { }
    else
    {

        InsertWordsFromServerDb(GetWordsFromLaptopDb(i), GetTranslationOfWordsFromLaptopDb(i), GetCurrentNumberOfWordsInServer() + 1);
    }
}

int CompareNumberOfWrodsInBothDbs()
{
    string connection = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\connectionStringServer.txt");
    MySqlConnection conn = new MySqlConnection(connection);
    conn.Open();
    string connection2 = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\LocalConnectionString.txt");
    MySqlConnection conn2 = new MySqlConnection(connection2);
    conn2.Open();
    string query2 = "Select Count(id) from word";
    string query1 = "Select Count(id) from word";
    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
    MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
    int wordNum1 = int.Parse(cmd1.ExecuteScalar().ToString());
    int wordNUm2 = int.Parse(cmd2.ExecuteScalar().ToString());
    if (wordNum1 <= wordNUm2)
    {
        return wordNUm2;
    }
    else
    {
        return wordNum1;
    }
    conn.Close();
    conn2.Close();
}
int GetCurrentNumberOfWordsInServer()
{
    string connection = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\connectionStringServer.txt");
    MySqlConnection conn = new MySqlConnection(connection);
    conn.Open();


    string query1 = "Select Count(id) from word";
    MySqlCommand cmd1 = new MySqlCommand(query1, conn);

    int wordNUm2 = int.Parse(cmd1.ExecuteScalar().ToString());
    return wordNUm2;
    conn.Close();
}
bool CheckWordsFromServerDb(string wordForChecking)
{
    string connection = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\connectionStringServer.txt");
    MySqlConnection conn = new MySqlConnection(connection);
    conn.Open();
    string query1 = $"Select word from word where word = '{wordForChecking}'";
    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
    try
    {
        string word = cmd1.ExecuteScalar().ToString();

    }
    catch (Exception)
    {
        return false;

    }
    return true;
    conn.Close();
}
void InsertWordsFromServerDb(string word, string prevod, int id)
{
    string connection = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\connectionStringServer.txt");
    MySqlConnection conn = new MySqlConnection(connection);
    conn.Open();
    string query1 = $"insert into word values('{word}','{prevod}',0,{id});";
    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
    cmd1.ExecuteScalar().ToString();
    conn.Close();

}
string GetTranslationOfWordsFromLaptopDb(int id)
{
    string connection = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\LocalConnectionString.txt");
    MySqlConnection conn = new MySqlConnection(connection);
    conn.Open();
    string query1 = $"Select prevod from word where id ={id}";
    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
    string word = cmd1.ExecuteScalar().ToString();
    return word;
    conn.Close();
}

string GetWordsFromLaptopDb(int id)
{
    string connection = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\LocalConnectionString.txt");
    MySqlConnection conn = new MySqlConnection(connection);
    conn.Open();
    string query1 = $"Select word from word where id ={id}";
    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
    string word = cmd1.ExecuteScalar().ToString();
    return word;
    conn.Close();
}
