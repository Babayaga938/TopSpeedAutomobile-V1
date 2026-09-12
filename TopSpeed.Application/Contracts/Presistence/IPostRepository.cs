using System;
using System.Collections.Generic;
using System.Text;
using TopSpeed.Domain.Models;

namespace TopSpeed.Application.Contracts.Presistence
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task Update(Post post);

        Task<Post> GetPostById (Guid id);

        Task<List<Post>> GetAllPost();

    }
}
