using System;

namespace FunBooksAndVideos.Models
{
    public abstract class ProductType
    {
        public abstract int Id { get; }

        public abstract string Name { get; }
    }

    public class BookProductType : ProductType
    {
        public override int Id => 1;
        public override string Name => "Book";
    }

    public class VideoProductType : ProductType
    {
        public override int Id => 2;
        public override string Name => "Video";
    }

    public class MembershipProductType : ProductType
    {
        public override int Id => 3;
        public override string Name => "Membership";
    }
}