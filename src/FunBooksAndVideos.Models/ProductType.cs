using System;

// applied on ORM level, db should store just the product type id
namespace FunBooksAndVideos.Models
{
    // public abstract class ProductTypeBase
    // {
    //     public abstract int Id { get; }

    //     public abstract string Name { get; }
    // }
    public interface IProductType
    {
        int Id { get; }

        string Name { get; }
    }

    public interface IMembershipProductType : IProductType
    {
        CustomerMembershipType MembershipType { get; }
    }

    public interface IPhysicalProductType : IProductType
    {
    }

    public class BookProductType : IPhysicalProductType
    {
        public int Id => 1;
        public string Name => "Book";
    }

    public class VideoProductType : IPhysicalProductType
    {
        public int Id => 2;
        public string Name => "Video";
    }

    public class BookMembershipProductType : IMembershipProductType
    {
        public int Id => 3;
        public string Name => "Book Membership";

        public CustomerMembershipType MembershipType => CustomerMembershipType.BookClub;
    }

    public class VideoMembershipProductType : IMembershipProductType
    {
        public int Id => 4;
        public string Name => "Video Membership";

        public CustomerMembershipType MembershipType => CustomerMembershipType.VideoClub;
    }
}