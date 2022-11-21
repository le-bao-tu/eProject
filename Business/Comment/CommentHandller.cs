using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.Comment
{
    public class CommentHandller : ICommentHandller
    {
        private MyDB_Context _myDB_Context;

        public CommentHandller(MyDB_Context myDB_Context)
        {
            _myDB_Context = myDB_Context;
        }

        public async Task<Response> CreateComment(CommentModel model)
        {
            if(model.Account_Id == null)
            {
                return new ResponseError(Code.Forbidden, $"Không tìm thấy Id của người dùng ");
            }
            var itemProduct = await _myDB_Context.ItemProduct.FirstOrDefaultAsync(x => x.StyleCode == model.ProId);
            if(itemProduct != null)
            {
                model.StyleCodeItemProduct = itemProduct.StyleCode;
            }
            var stoneMst = await _myDB_Context.StoneMst.FirstOrDefaultAsync(x => x.StyleCode == model.ProId);
            if(stoneMst != null)
            {
                model.StyleCodeStoneMst = stoneMst.StyleCode;
            }
            var codeDimMst = await _myDB_Context.DimMst.FirstOrDefaultAsync(x => x.Style_Code == model.ProId);
            if (codeDimMst != null)
            {
                model.StyleCodeDimMst = codeDimMst.Style_Code; 
            }

            var data = AutoMapperUtils.AutoMap<CommentModel, Data.DataModel.Comment>(model);
            _myDB_Context.Comment.Add(data);
            int rs = await _myDB_Context.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<CommentModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }


        public async Task<Response> DeleteComment(Guid comId)
        {
            var data = await _myDB_Context.Comment.FirstOrDefaultAsync(x => x.Id == comId);

            if(data != null)
            {
                _myDB_Context.Comment.Remove(data);
                int rs = await _myDB_Context.SaveChangesAsync();
                if(rs > 0)
                {
                    return new ResponseObject<Guid>(comId, $"{Message.DeleteSuccess}", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
                }
            }
            else
            {
                return new ResponseError(Code.BadRequest, $"Không tìm thấy Id comment");
            }
        }

        public async Task<Response> GetAllComment(CommentPageModel model)
        {
            var data = await _myDB_Context.Comment.ToListAsync();

            if(model.PageSize.HasValue && model.PageNumber.HasValue)
            {
                if(model.PageSize <= 0)
                {
                    model.PageSize = 20;
                }

                int excludeRows = (model.PageSize.Value - 1) * (model.PageNumber.Value);
                if(excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                // Query 
                data = data.Skip(excludeRows).Take(model.PageSize.Value).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Comment, CommentModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<CommentModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetCommentById(Guid comId)
        {
            var data = await _myDB_Context.Comment.FirstOrDefaultAsync(x => x.Id == comId);
            if(data == null)
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Comment, CommentModel>(data);
            if(entity != null)
            {
                return new ResponseObject<CommentModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetCommentByName(string comName)
        {
            var data = await _myDB_Context.Comment.ToListAsync();

            if(!String.IsNullOrEmpty(comName))
            {
                List<CommentModel> dt;
                dt = await _myDB_Context.Comment.Where(x => x.ContentComment.Contains(comName)).Select(x => new CommentModel() {
                    Id = x.Id,
                    ContentComment = x.ContentComment,
                    Feedback = x.Feedback,
                    Account_Id = x.Account_Id,
                    StyleCodeItemProduct = x.StyleCodeItemProduct,
                    StyleCodeStoneMst = x.StyleCodeStoneMst,
                    StyleCodeDimMst = x.StyleCodeDimMst
                }).ToListAsync();


                var entity = AutoMapperUtils.AutoMap<Data.DataModel.Comment, CommentModel>(data);
                if(entity != null)
                {
                    return new ResponseObject<List<CommentModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
                }
            }
            else
            {
                return new ResponseError(Code.BadRequest, $"Dywx liệu trường không được để trống");
            }
        }

        public async Task<Response> UpdateComment(CommentModel model)
        {
            if (model.Account_Id == null)
            {
                return new ResponseError(Code.Forbidden, $"Không tìm thấy Id của người dùng ");
            }

            var itemProduct = await _myDB_Context.ItemProduct.FirstOrDefaultAsync(x => x.StyleCode == model.ProId);
            if (itemProduct != null)
            {
                model.StyleCodeItemProduct = itemProduct.StyleCode;
            }
            var stoneMst = await _myDB_Context.StoneMst.FirstOrDefaultAsync(x => x.StyleCode == model.ProId);
            if (stoneMst != null)
            {
                model.StyleCodeStoneMst = stoneMst.StyleCode;
            }
            var codeDimMst = await _myDB_Context.DimMst.FirstOrDefaultAsync(x => x.Style_Code == model.ProId);
            if (codeDimMst != null)
            {
                model.StyleCodeDimMst = codeDimMst.Style_Code;
            }

            var data = AutoMapperUtils.AutoMap<CommentModel, Data.DataModel.Comment>(model);
            _myDB_Context.Comment.Update(data);
            int rs = await _myDB_Context.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<CommentModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
