﻿using System.Linq;
using System.Threading.Tasks;
using Facebook.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Options;

namespace Facebook.Repository
{
    public interface IPostRepository : IDocumentDatabaseRepository<Post>
    {

    }
    public class PostRepository : DocumentDatabaseRepository<Post>, IPostRepository
    {
        private readonly ISocialInteractionsRepository _socialInteractionsDocumentDatabaseRepository;

        public PostRepository(IOptions<ApplicationSettings> applicationSettingsOptions, ISocialInteractionsRepository socialInteractionsDocumentDatabaseRepository) : base(applicationSettingsOptions)
        {
            _socialInteractionsDocumentDatabaseRepository = socialInteractionsDocumentDatabaseRepository;
        }

        public override async Task<Post> GetItemAsync(string id)
        {
            var post = await base.GetItemAsync(id);
            var socialInteractions = await
                _socialInteractionsDocumentDatabaseRepository.GetItemByPostId(post.Id);
            post.SocialInteractions = socialInteractions;
            return post;
        }

        public override async Task<Document> CreateItemAsync(Post newPost)
        {
            await _socialInteractionsDocumentDatabaseRepository.CreateItemAsync(
                new SocialInteractions
                {
                    PostId = newPost.Id,
                    TotalComments = 0,
                    TotalLikes = 0
                });

            // If this is from a child
            var socialInteractions =
            await _socialInteractionsDocumentDatabaseRepository.GetItemsWhereAsync(itemFromDb => itemFromDb.PostId == newPost.ParentId.GetValueOrDefault());

            var socialInteraction = socialInteractions.FirstOrDefault();
            //TODO: Just check if Post.SocialInteraction is null, then create it yah?
            if (socialInteraction != null)
            {
                socialInteraction.TotalComments++;
                await _socialInteractionsDocumentDatabaseRepository.UpdateItemAsync(socialInteraction.Id.ToString(),
                    socialInteraction);

            }

            return await base.CreateItemAsync(newPost);
        }

        protected override string CollectionId { get; } = nameof(Post);
    }
}