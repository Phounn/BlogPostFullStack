using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Forms;
using Share.Models;

namespace APIGateway
{
    public interface IRepository
    {
        Task<List<BlogPost>> GetAll();
        Task<BlogPost> GetById(string id);
        Task<BlogPost> Create(BlogPost blog);
        Task Update(BlogPost blog, string id);
        Task Delete(string id);
        Task<List<CategoriesOfBlog>> GetAllCate();
        Task<CategoriesOfBlog> CreateCate(CategoriesOfBlog cate);
        Task DeleteCate(string id);
    }
    public class Repository : IRepository
    {
        private readonly IBucketProvider bucketProvider;
        private readonly string BucketName = "BlogPost";
        private readonly string ScopeName = "MyScope";
        private readonly string CollectionName = "MyCollection";
        public Repository(IBucketProvider bucketProvider)
        {
            this.bucketProvider = bucketProvider;
        }
        public async Task<List<BlogPost>> GetAll()
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var Query = await scope.QueryAsync<BlogPost>("SELECT t.* FROM BlogPost.MyScope.MyCollection t");
            var data = await Query.ToListAsync();
            return data;

        }

        public async Task<BlogPost> GetById(string id)
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var collection = scope.Collection(CollectionName);
            var result = collection.GetAsync(id);
            var data = result.Result.ContentAs<BlogPost>();
            return data!;


        }
        public async Task<BlogPost> Create(BlogPost blog)
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var collection = scope.Collection(CollectionName);
            blog.BlogId = Guid.NewGuid().ToString();
            blog.CreatedDate = DateTime.Now;

            await collection.InsertAsync(blog.BlogId, blog);
            return blog;
        }
        public async Task Delete(string id)
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var collection = scope.Collection(CollectionName);
            await collection.RemoveAsync(id);
        }

        public async Task Update(BlogPost blog, string id)
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var collection = scope.Collection(CollectionName);
            blog.BlogId = id;
            await collection.UpsertAsync(id, blog);
        }
        public async Task<List<CategoriesOfBlog>> GetAllCate()
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var Query = await scope.QueryAsync<CategoriesOfBlog>("SELECT t.* FROM BlogPost.MyScope.Categories t");
            var data = await Query.ToListAsync();
            return data;

        }
        public async Task<CategoriesOfBlog> CreateCate(CategoriesOfBlog cate)
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var collection = scope.Collection("Categories");
            cate.Id = Guid.NewGuid().ToString();
            await collection.InsertAsync(cate.Id, cate);
            return cate;
        }
        public async Task DeleteCate(string id)
        {
            var bucket = await bucketProvider.GetBucketAsync(BucketName);
            var scope = bucket.Scope(ScopeName);
            var collection = scope.Collection("Categories");
            await collection.RemoveAsync($"{id}");

        }
    }
}
