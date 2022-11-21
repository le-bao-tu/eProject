using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.ImageProduct
{
    public class ImageProductHandller : IImageProductHandller
    {
        private MyDB_Context _myDB_Context;

        public ImageProductHandller(MyDB_Context myDB_Context)
        {
            _myDB_Context = myDB_Context;
        }

        public async Task<Response> InsertImage(ImageProductModel model)
        {
           if(model != null)
            {
                var itemProduct = await _myDB_Context.ItemProduct.FirstOrDefaultAsync(x => x.StyleCode == model.ProId);
                if(itemProduct != null)
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

                var data = AutoMapperUtils.AutoMap<ImageProductModel, Data.DataModel.ImageProduct>(model);
                _myDB_Context.ImageProduct.Add(data);
                int rs = await _myDB_Context.SaveChangesAsync();
                if(rs > 0)
                {
                    return new ResponseObject<ImageProductModel>(model, $"{Message.CreateSuccess}", Code.Success);
                }
            }
            return null;
        }

        public async Task<Response> UpdateImage(ImageProductModel model)
        {
            if (model != null)
            {
                var data = AutoMapperUtils.AutoMap<ImageProductModel, Data.DataModel.ImageProduct>(model);
                _myDB_Context.ImageProduct.Update(data);
                int rs = await _myDB_Context.SaveChangesAsync();
                if (rs > 0)
                {
                    return new ResponseObject<ImageProductModel>(model, $"{Message.CreateSuccess}", Code.Success);
                }
            }
            return null;
        }
    }
}
