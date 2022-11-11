using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business;
using Business.Account;
using Business.Brand;
using Business.Category;
using Business.Certify;
using Business.Goldk;
using Business.SubCategory;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace eProjectAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // cấu hình Authentication Token theo của trường 
            // +> lấy key trong tệp cấu hình appseting 
            var key = Configuration["Jwt:key"];
            // mã hoá cái key đấy 
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //Add Authentication Bearer (thêm mới xác thực  Bearer )
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // kiểm tra cái Issuer ( default true , Issuer tổ chức phát hành )
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    // kiểm tra cái Audience ( default true )
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    // cấu hỉnh thời gian hết hạn cho thằng Token 
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    // chỉ ra cái key sử dụng trong Token khi nó được sinh ra 
                    IssuerSigningKey = signingKey,
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.Zero
                };
            });



            // mở cổng cho bên frontend dùng
            services.AddCors();
            services.AddRouting();
            services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // cấu hình kết nối đến cơ sở dữ liệu 
            services.AddDbContext<MyDB_Context>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });
            // cấu hỉnh swagger 
            services.AddSwaggerGen();

            //đăng kí sử dụng AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            // chỗ cấu hình handeler (Dependency Injection)
            //account
            services.AddScoped<IAccountHandler, AccountHandler>();
            // category 
            services.AddScoped<ICategoryHandler, CategoryHandler>();
            //brand 
            services.AddScoped<IBrandHandller, BrandHandller>();
            //Goldk
            services.AddScoped<IGoldkHandller, GoldkHandller>();
            // Certify
            services.AddScoped<ICertifyHandller, CertifyHandller>();
            // SubCategory 
            services.AddScoped<ISubCategoryHandller, SubCategoryHandller>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // mở khoá http
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts();
            }

            // UseAuthentication phải đặt trước UseAuthorization
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
