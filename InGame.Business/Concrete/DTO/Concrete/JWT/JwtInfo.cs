namespace InGame.Business.Concrete.DTO.Concrete.JWT
{
    public class JwtInfo
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public double Expires { get; set; }
    }
}
