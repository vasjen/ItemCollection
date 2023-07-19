using Common.Core.Entities;

namespace  Common.Models
{
    public class Collection : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public Theme Theme {get;set;}
        public List<Item> Items { get; } = new();
        public DateTimeOffset CreatedTime { get; init; }
        public string Description { get; set; } = string.Empty;
        public Guid ApplicationUserId {get;set;}
        public ApplicationUser User {get;set;}
        public Fields? Fields {get;set;}
    }

    public enum Theme 
    {Books,Coins, Stamps, Postcards, Comics, Figures,Trading, Cards, Vinyl, Records, Movie, 
    Memorabilia, Sports, Autographs, Art, Prints, Antique, Furniture, Vintage, Clothing, Toys, Cars, Dolls, Trains, 
    Instruments, Wristwatches, Jewelry, Bottles, Sneakers, Figurines, Vinyls, Hats, Lighters, Other}
}