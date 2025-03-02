namespace Runtime.Models
{
    public class PlayerProfile
    {
        public ReactiveProperty<int> Money = new(100);
        public ReactiveProperty<string> Avatar = new("avatar_ninja");
        public string Name { get; set; } = "Big Punisher";
    }
}