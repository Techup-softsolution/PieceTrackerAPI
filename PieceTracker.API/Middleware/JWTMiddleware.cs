using Microsoft.IdentityModel.Tokens;
using PieceTracker.Model;
using PieceTracker.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PieceTracker.API
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration )
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
               await attachAccountToContext(context, token);

            await _next(context);
        }
        private  async Task attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = _configuration["Jwt:secret"];
                var key = Encoding.UTF8.GetBytes(secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidIssuer = _configuration["Jwt:Issuer"]
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)

            }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // attach account to context on successful jwt validation
                var db = context.RequestServices.GetService<AuthenticationMasterService>();
                context.Items["User"] = await db.GetLoggedInUserDetail(Convert.ToInt32(accountId));                
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }

    }
    
}
