using System;

namespace FunBooksAndVideos.Models
{
    public class ModelBase
    {
        public Guid Id { get; private set; }

        public ModelBase() => this.Id = Guid.NewGuid();
    }
}