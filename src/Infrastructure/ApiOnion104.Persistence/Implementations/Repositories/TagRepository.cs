using A.Application.Abstractions.Repositories;

using ApiOnion104.Domain.Entities;
using ApiOnion104.Persistence.Contexts;

using ApiOnion104.Persistence.Implementations.Repositories.Generic;


namespace ApiOnion104.Persistence.Implementations.Repositories
{
    internal class TagRepository:Repository<Tag>,ITagRepository
    {
        public TagRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
