using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Decanat.DecanatForms
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        

        private void buttonAuth_Click(object sender, EventArgs e)
        {
            // https://learn.microsoft.com/ru-ru/dotnet/api/system.security.cryptography.hashalgorithm.computehash?view=net-7.0#system-security-cryptography-hashalgorithm-computehash(system-byte())
            string source = textBoxPassword.Text.Trim();
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    string hash = GetHash(sha256Hash, source);

                    using (SqlConnection connection = new SqlConnection(Connection.connection))
                    {
                        connection.Open();

                        string query = $"SELECT [id], [login], [password], [status], [fullname] FROM [Decanat].[dbo].[Users]" +
                          $"WHERE [login] = @login AND [password] = @password";

                        SqlCommand sqlCommand = new SqlCommand(query, connection);
                        sqlCommand.Parameters.AddWithValue("@login", textBoxLogin.Text);
                        sqlCommand.Parameters.AddWithValue("@password", hash);
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        if (reader.HasRows == false)
                        {
                            MessageBox.Show("Account not found", "Auth", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (reader.Read())
                            {
                                Personal.UserFullname = reader[4].ToString();
                                Personal.UserStatus = reader[3].ToString();
                                Personal.UserIsAuth = true;
                                MessageBox.Show($"Hi, {Personal.UserFullname}! You log in as {Personal.UserStatus}.", "Auth", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        reader.Close();
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}");
            }
            if (Personal.UserIsAuth == true)
                this.Close();
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            var hashOfInput = GetHash(hashAlgorithm, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

    }
}
