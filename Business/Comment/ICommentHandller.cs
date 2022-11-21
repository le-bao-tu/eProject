using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Comment
{
    public interface ICommentHandller
    {
        Task<Response> GetAllComment(CommentPageModel model);

        Task<Response> CreateComment(CommentModel model);

        Task<Response> UpdateComment(CommentModel model);

        Task<Response> DeleteComment(Guid comId);

        Task<Response> GetCommentById(Guid comId);

        Task<Response> GetCommentByName(string comName);

    }
}
