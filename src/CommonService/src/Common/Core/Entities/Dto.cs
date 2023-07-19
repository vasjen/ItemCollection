using System.ComponentModel.DataAnnotations;

namespace Common.Core.Entities
{
    public record UserDto(string Id, string UserName);

    public record ItemDto(Guid Id, string NameItem, DateTimeOffset AddedDate);
    public record CreateItemDto([Required] string NameItem, List<string> Tags);
    public record UpdateItemDto([Required] string NameItem, List<string> Tags);

    public record CollectionDto(Guid Id, string NameCollection, string Description, IEnumerable<ItemDto> Items,string Theme, DateTimeOffset CreatedTimea);
    public record CreateCollectionDto([Required]string NameCollection,[Required] string Description, Theme Theme, Guid ApplicationUserId, Dictionary<int,string> fields);
    public record UpdateCollectionDto([Required]string NameCollection,[Required] string Description, Theme Theme, Guid ApplicationUserId);

    public record CommentDto(string CommentText, DateTimeOffset AddedDate, string Author);
    public record CreateCommentDto(string CommentText);

    public enum Theme 
    {Books,Coins, Stamps, Postcards, Comics, Figures,Trading, Cards, Vinyl, Records, Movie, 
    Memorabilia, Sports, Autographs, Art, Prints, Antique, Furniture, Vintage, Clothing, Toys, Cars, Dolls, Trains, 
    Instruments, Wristwatches, Jewelry, Bottles, Sneakers, Figurines, Vinyls, Hats, Lighters, Other}

}