using System.Text.Json.Serialization;
using MediTrack.Services;

namespace MediTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.UseInlineDefinitionsForEnums();
            });
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<UserService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseMiddleware<AuthMiddleware>();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
