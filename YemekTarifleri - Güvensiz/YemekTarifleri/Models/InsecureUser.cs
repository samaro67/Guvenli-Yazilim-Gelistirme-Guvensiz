namespace YemekTarifleri.Models
{
    public class InsecureUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        // ⚠️ Şifre düz metin tutulacak!
        public string Password { get; set; }
    }
}

