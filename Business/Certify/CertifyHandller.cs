﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.Certify
{
    public class CertifyHandller : ICertifyHandller
    {
        private readonly MyDB_Context _myDbContext;

        public CertifyHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateCertify(CertifyModel model)
        {
            var data = AutoMapperUtils.AutoMap<CertifyModel, Data.DataModel.Certify>(model);
            _myDbContext.Certify.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<CertifyModel>(model, "Thêm mới thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Thêm mới thất bại");
            }
        }

        public async Task<Response> DeleteCertify(Guid certifyId)
        {
            var data = await _myDbContext.Certify.FirstOrDefaultAsync(x => x.IdCertify == certifyId);
            _myDbContext.Certify.Remove(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<Guid>(certifyId, "Xóa dữ liệu thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Xóa dữ liệu thất bại");
            }
        }

        public async Task<Response> GetAllCertify(CertifyPageModel model)
        {
            var data = await _myDbContext.Certify.ToListAsync();

            if (model.PageSize.HasValue && model.PageNumber.HasValue)
            {
                if (model.PageSize <= 0)
                {
                    model.PageSize = 20;
                }

                int excludeRows = (model.PageNumber.Value - 1) * (model.PageSize.Value);
                if (excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                //query 
                data = data.Skip(excludeRows).Take(model.PageSize.Value).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Certify, CertifyModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<CertifyModel>>(entity, "Lấy dữ liệu thành công ", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }

        }

        public async Task<Response> GetCertifyById(Guid certifyId)
        {
            var data = await _myDbContext.Certify.FirstOrDefaultAsync(x => x.IdCertify.Equals(certifyId));
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Certify, CertifyModel>(data);
            if (entity != null)
            {
                return new ResponseObject<CertifyModel>(entity, "Lấy dữ liệu thành công ", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }
        }

        public async Task<Response> GetCertifyByName(string certifyName)
        {
            var data = await _myDbContext.Certify.ToListAsync();
            if (!String.IsNullOrEmpty(certifyName))
            {
                data = data.Where(x => x.CertifyType.Contains(certifyName)).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Certify, CertifyModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<CertifyModel>>(entity, "Lấy dữ liệu thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lâys dữ liệu thất bại");
            }
        }

        public async Task<Response> UpdateCertify(CertifyModel model)
        {
            var data = AutoMapperUtils.AutoMap<CertifyModel, Data.DataModel.Certify>(model);
            _myDbContext.Certify.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<CertifyModel>(model, "Cập nhật thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Cập nhật thất bại");
            }
        }
    }
}